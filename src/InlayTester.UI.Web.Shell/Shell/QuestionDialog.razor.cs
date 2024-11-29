// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Shell;


public partial class QuestionDialog
{
    [Inject]
    public required IStringLocalizer Loc { get; set; }


    [CascadingParameter]
    public required IMudDialogInstance Dialog { get; set; }

    [Parameter]
    public required String Title { get; set; }

    [Parameter]
    public required String Message { get; set; }

    [Parameter]
    public required String? AcceptButton { get; set; }

    [Parameter]
    public required String? CancelButton { get; set; }


    private void OnAccept()
    {
        Dialog?.Close(DialogResult.Ok(true));
    }

    private void OnCancel()
    {
        Dialog?.Close(DialogResult.Ok(false));
    }
}
