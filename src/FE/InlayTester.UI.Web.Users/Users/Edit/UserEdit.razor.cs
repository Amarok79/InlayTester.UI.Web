// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Users.Edit;


public partial class UserEdit
{
    [Inject]
    public required IUserManager UserManager { get; set; }

    [Inject]
    public required IUserSessionManager SessionManager { get; set; }

    [Parameter]
    public required String Id { get; set; }


    private Boolean mIsNew;
    private Boolean mCanEditName;
    private Boolean mCanEditRoles;
    private readonly UserViewModel mViewModel;
    private readonly EditContext mEditContext;


    private String PageTitle => Loc[mIsNew ? "users.edit.page-header-new" : "users.edit.page-header-edit"];

    private String ButtonAccept
        => Loc[mIsNew ? "users.edit.accept-button-new" : "users.edit.accept-button-edit"];

    private String ButtonCancel
        => Loc[mIsNew ? "users.edit.cancel-button-new" : "users.edit.cancel-button-edit"];


    public UserEdit()
    {
        mViewModel   = new UserViewModel();
        mEditContext = new EditContext(mViewModel);

        mViewModel.SetDefaults();
    }


    protected override void OnInitialized()
    {
        base.OnInitialized();

        mIsNew = new Id(Id).IsEmpty;

        Disposables.Add(SessionManager.CurrentChanged.Subscribe(x => StateHasChanged()));
    }

    protected override async Task OnLoading()
    {
        await base.OnLoading();

        await RunAsync(
            "USR110",
            async () => {
                if (mIsNew)
                {
                    _InitForNewUser();
                }
                else
                {
                    await _InitForEditUser();
                }
            },
            () => NavigationManager.GotoUsers()
        );
    }

    private void _InitForNewUser()
    {
        mViewModel.SetDefaults();

        mIsNew = true;

        mCanEditName  = true;
        mCanEditRoles = true;
    }

    private async Task _InitForEditUser()
    {
        var user = await UserManager.GetUserAsync(Id);

        if (user != null)
        {
            mViewModel.Name            = user.Name;
            mViewModel.Password1       = user.Password;
            mViewModel.Password2       = user.Password;
            mViewModel.IsSetter        = user.IsMachineSetter;
            mViewModel.IsOperator      = user.IsMachineOperator;
            mViewModel.IsAdministrator = user.IsAdministrator;
            mViewModel.Notes           = user.Notes;

            mCanEditName  = false;
            mCanEditRoles = !user.IsSupervisor;
        }
        else
        {
            _InitForNewUser();
        }
    }


    public Task OnDiscard()
    {
        NavigationManager.GotoUsers();

        return Task.CompletedTask;
    }


    private Boolean CanSubmit()
    {
        if (!(SessionManager.Current?.IsAdministrator ?? false))
        {
            return false;
        }

        return true;
    }

    public async Task OnSubmit()
    {
        await RunAsync(
            "USR111",
            async () => {
                try
                {
                    mViewModel.IsFormValidation = true;

                    if (mIsNew)
                    {
                        await _CheckWhetherNameIsUnique();
                    }

                    if (mEditContext.Validate())
                    {
                        await _SaveUser();

                        NavigationManager.GotoUsers();
                    }
                }
                finally
                {
                    mViewModel.IsFormValidation = false;
                }
            }
        );
    }

    private async Task _SaveUser()
    {
        await RunAsync(
            "USR112",
            async () => {
                if (mIsNew)
                {
                    await _SaveNewUser();
                }
                else
                {
                    await _SaveExistingUser();
                }
            }
        );
    }

    private async Task _SaveNewUser()
    {
        var user = mViewModel.ToModel(SessionManager.Current);

        await UserManager.AddUserAsync(user);
    }

    private async Task _SaveExistingUser()
    {
        var user = mViewModel.ToModel(SessionManager.Current);

        var existing = await UserManager.GetUserAsync(Id);

        existing = existing! with {
            Password = user.Password,
            Roles = user.Roles,
            Notes = user.Notes,
            ModifiedBy = user.ModifiedBy,
            ModifiedOn = user.ModifiedOn,
        };

        await UserManager.UpdateUserAsync(existing);
    }


    private async Task _CheckWhetherNameIsUnique()
    {
        if (mViewModel.Name.IsNullOrWhitespace())
        {
            mViewModel.IsNameUnique = true;
        }
        else
        {
            var contains = await UserManager.ContainsUserNameAsync(mViewModel.Name);

            mViewModel.IsNameUnique = !contains;
        }
    }
}
