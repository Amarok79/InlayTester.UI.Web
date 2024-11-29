// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using InlayTester.UI.Web.Validations;


namespace InlayTester.UI.Web.Users.Edit;


public sealed class UserViewModel
{
    private String mName = String.Empty;


    public Int32 MaxNameLength => User.MaxNameLength;

    public Int32 MaxPasswordLength => User.MaxPasswordLength;

    public Int32 MaxNotesLength => User.MaxNotesLength;


    [FieldRequired, UniqueNameRequired]
    public String Name
    {
        get => mName;
        set
        {
            mName        = value;
            IsNameUnique = null;
        }
    }

    public Boolean? IsNameUnique { get; set; }

    public Boolean IsFormValidation { get; set; }


    [FieldRequired, MatchingPasswordsRequired]
    public String Password1 { get; set; } = String.Empty;

    [FieldRequired, MatchingPasswordsRequired]
    public String Password2 { get; set; } = String.Empty;


    public Boolean IsOperator { get; set; }

    public Boolean IsSetter { get; set; }

    public Boolean IsAdministrator { get; set; }

    [SingleRoleRequired]
    public Int32 RolesCount => (IsOperator ? 1 : 0) + (IsSetter ? 1 : 0) + (IsAdministrator ? 1 : 0);


    public String Notes { get; set; } = String.Empty;


    public void SetDefaults()
    {
        Name            = String.Empty;
        Password1       = String.Empty;
        Password2       = String.Empty;
        IsOperator      = true;
        IsSetter        = false;
        IsAdministrator = false;
        Notes           = String.Empty;
    }

    public User ToModel(User? currentUser)
    {
        var roles = new HashSet<Role>();

        if (IsOperator)
        {
            roles.Add(new Role(Role.MachineOperatorId, String.Empty));
        }

        if (IsSetter)
        {
            roles.Add(new Role(Role.MachineSetterId, String.Empty));
        }

        if (IsAdministrator)
        {
            roles.Add(new Role(Role.AdministratorId, String.Empty));
        }

        var user = new User(Id.New(), Name) {
            Password   = Password1,
            Notes      = Notes,
            Roles      = roles,
            ModifiedOn = DateTime.Now,
            ModifiedBy = currentUser?.Name ?? String.Empty,
        };

        return user;
    }
}
