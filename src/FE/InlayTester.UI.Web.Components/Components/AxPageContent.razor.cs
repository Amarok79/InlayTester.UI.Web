// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Components;


public partial class AxPageContent
{
    [Parameter]
    public RenderFragment? Header { get; set; }

    [Parameter]
    public RenderFragment? Content { get; set; }

    [Parameter]
    public RenderFragment? Footer { get; set; }
}
