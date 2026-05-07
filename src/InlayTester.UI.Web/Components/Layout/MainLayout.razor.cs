// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Components.Layout;


public partial class MainLayout
{
    private readonly MudTheme mTheme;
    private Boolean mIsDarkTheme = true;


    [Inject]
    public required IStringLocalizer Loc { get; set; }


    public MainLayout()
    {
        mTheme = _CreateTheme();
    }


    private void OnToggleTheme()
    {
        mIsDarkTheme = !mIsDarkTheme;
    }


    private MudTheme _CreateTheme()
    {
        MudGlobal.TooltipDefaults.Delay = TimeSpan.FromMilliseconds(250);

        return new MudTheme {
            Typography = new Typography {
                Default = {
                    FontFamily = [ "Noto Sans", "Helvetica", "Arial", "sans-serif" ],
                },
            },
            PaletteLight     = _CreateLightPalette(),
            PaletteDark      = _CreateDarkPalette(),
            LayoutProperties = new LayoutProperties(),
        };
    }

    private PaletteLight _CreateLightPalette()
    {
        return new PaletteLight {
            // backgrounds
            AppbarBackground = "#f5f5f5",
            DrawerBackground = "#fafafa",

            // foregrounds
            AppbarText    = "#424242",
            DrawerIcon    = "#616161",
            DrawerText    = "#424242",
            TextPrimary   = "#212121",
            ActionDefault = "#757575",

            // colors
            Primary   = "#1976d2",
            Secondary = "#dc004e",
            Tertiary  = "#9c27b0",
            Info      = "#0288d1",
            Success   = "#2e7d32",
            Warning   = "#ed6c02",
            Error     = "#d32f2f",
            Dark      = "#1a1a1a",

            // hover
            TableHover   = "#00000010",
            HoverOpacity = 0.15,
        };
    }

    private PaletteDark _CreateDarkPalette()
    {
        return new PaletteDark {
            // backgrounds
            Background       = "#1e1e1e",
            AppbarBackground = "#202020",
            DrawerBackground = "#202020",
            Surface          = "#242424",

            // foregrounds
            AppbarText    = "#bcbcbc",
            DrawerIcon    = "#bcbcbc",
            DrawerText    = "#bcbcbc",
            TextPrimary   = "#bcbcbc",
            ActionDefault = "#bcbcbc",

            // colors
            Primary   = "#006DAA",
            Secondary = "#FFB74D",
            Tertiary  = "#A0CED9",
            Info      = "#3299ff",
            Success   = "#0bba83",
            Warning   = "#D88C00",
            Error     = "#D83E3E",
            Dark      = "#27272f",

            // hover
            TableHover   = "#00000020",
            HoverOpacity = 0.20,
        };
    }
}
