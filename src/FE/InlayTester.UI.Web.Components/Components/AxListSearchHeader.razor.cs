// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.UI.Web.Components;


public partial class AxListSearchHeader
{
    private MudTextField<String>? mSearchField;


    [Parameter]
    public String? SearchText { get; set; }

    [Parameter]
    public String? SearchTooltip { get; set; }

    [Parameter]
    public String? SearchPlaceholder { get; set; }

    [Parameter]
    public EventCallback<String> SearchTextChanged { get; set; }


    [Parameter]
    public String? ButtonText { get; set; }

    [Parameter]
    public String? ButtonTooltip { get; set; }

    [Parameter]
    public EventCallback<MouseEventArgs> OnButtonClick { get; set; }


    [Parameter]
    public Boolean Disabled { get; set; }

    [Parameter]
    public Boolean ButtonDisabled { get; set; }


    public async Task FocusSearchAsync()
    {
        if (mSearchField != null && !Disabled)
        {
            await mSearchField.FocusAsync();
            await mSearchField.SelectAsync();
        }
    }

    private async Task OnSearchTextChanged(String text)
    {
        if (!Disabled)
        {
            SearchText = text;
            await SearchTextChanged.InvokeAsync(text);
        }
    }
}
