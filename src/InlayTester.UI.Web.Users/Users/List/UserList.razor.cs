// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using System.Collections.ObjectModel;


namespace InlayTester.UI.Web.Users.List;


public partial class UserList
{
    [Inject]
    public required IUserManager UserManager { get; set; }

    [Inject]
    public required IUserSessionManager SessionManager { get; set; }


    private AxListSearchHeader? mSearchHeader;
    private String? mSearchText;
    private ObservableCollection<User>? mUsers;


    protected override void OnInitialized()
    {
        base.OnInitialized();

        Disposables.Add(SessionManager.CurrentChanged.Subscribe(x => StateHasChanged()));
    }

    protected override void OnInitializeHotKeys(HotKeysContext context)
    {
        base.OnInitializeHotKeys(context);

        context.Add(ModCode.Alt, Code.F, OnFocusSearch, exclude: Exclude.InputNonText | Exclude.TextArea);
        context.Add(ModCode.Alt, Code.T, OnCreate, exclude: Exclude.InputNonText | Exclude.TextArea);
    }

    protected override async Task OnLoading()
    {
        await base.OnLoading();

        await RunAsync(
            "USR100",
            async () => {
                var users = await UserManager.QueryUsersAsync();
                mUsers = users.OrderBy(x => x.Name).ToObservableCollection();
            },
            () => NavigationManager.GotoHome()
        );
    }

    private async Task OnFocusSearch()
    {
        if (mSearchHeader != null)
        {
            await mSearchHeader.FocusSearchAsync();
        }
    }

    private Boolean CanCreate()
    {
        if (!(SessionManager.Current?.IsAdministrator ?? false))
        {
            return false;
        }

        return true;
    }

    private void OnCreate()
    {
        if (!IsBusy && CanCreate())
        {
            NavigationManager.GotoUsers(Id.Empty());
        }
    }

    private Boolean CanEdit(User? user)
    {
        if (!(SessionManager.Current?.IsAdministrator ?? false))
        {
            return false;
        }

        return user?.CanEdit() ?? false;
    }

    private void OnEdit(User? user)
    {
        if (user != null)
        {
            NavigationManager.GotoUsers(user.Id);
        }
    }

    private Boolean CanDelete(User? user)
    {
        if (!(SessionManager.Current?.IsAdministrator ?? false))
        {
            return false;
        }

        return user?.CanDelete() ?? false;
    }

    private async Task OnDelete(User? user)
    {
        if (user != null)
        {
            var delete = await Shell.Dialogs.ShowDeleteConfirmationDialog(
                Loc["users.list.delete-message"],
                Loc["users.list.delete-title", user.Name]
            );

            if (delete)
            {
                if (await RunAsync("USR101", async () => await UserManager.DeleteAsync(user.Id)))
                {
                    mUsers?.Remove(user);
                }
            }
        }
    }

    private Boolean _FilterUsers(User item)
    {
        return item.Filter(mSearchText);
    }
}
