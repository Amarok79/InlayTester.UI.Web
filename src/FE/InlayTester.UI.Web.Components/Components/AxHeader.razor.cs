// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Components;


public partial class AxHeader
{
    [Parameter]
    public String? Icon { get; set; }

    [Parameter]
    public String? Text { get; set; }

    [Parameter]
    public Color Color { get; set; } = Color.Inherit;

    [Parameter]
    public Boolean IsBusy { get; set; }
}
