// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Shell;


public partial class ShellLayout
{
    private Boolean mIsDrawerOpen;


    [Parameter]
    public required MudTheme Theme { get; set; }

    [Parameter]
    public Boolean IsDarkTheme { get; set; } = true;

    [Parameter]
    public RenderFragment? ChildContent { get; set; }

    [Parameter]
    public RenderFragment? NavMenuContent { get; set; }

    [Parameter]
    public RenderFragment? AppBarRightContent { get; set; }

    [Parameter]
    public String? Title { get; set; }


    private void OnToggleDrawer()
    {
        mIsDrawerOpen = !mIsDrawerOpen;
    }
}
