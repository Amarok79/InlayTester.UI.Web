// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Components;


public partial class AxNavMenuItem
{
    [Parameter]
    public String? Icon { get; set; }

    [Parameter]
    public String? Text { get; set; }

    [Parameter]
    public Boolean Disabled { get; set; }

    [Parameter]
    public Boolean TooltipDisabled { get; set; }

    [Parameter]
    public String? Href { get; set; }

    [Parameter]
    public NavLinkMatch Match { get; set; } = NavLinkMatch.Prefix;
}
