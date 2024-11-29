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
                    FontFamily = [
                        "Noto Sans",
                        "Helvetica",
                        "Arial",
                        "sans-serif",
                    ],
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
            AppbarBackground = "#ffffff",
            DrawerBackground = "#ffffff",

            // foregrounds
            AppbarText    = "#5f6368",
            DrawerIcon    = "#5f6368",
            DrawerText    = "#5f6368",
            TextPrimary   = "#5f6368",
            ActionDefault = "#5f6368",

            // colors
            Primary   = "#006DAA",
            Secondary = "#FB8C00",
            Tertiary  = "#A0CED9",
            Info      = "#3299ff",
            Success   = "#0bba83",
            Warning   = "#D88C00",
            Error     = "#D83E3E",
            Dark      = "#27272f",

            // hover
            TableHover   = "#00000015",
            HoverOpacity = 0.20,
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
