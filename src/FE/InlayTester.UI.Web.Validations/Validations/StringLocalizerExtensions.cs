// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using InlayTester.Domain;


namespace InlayTester.UI.Web.Validations;


public static class StringLocalizerExtensions
{
    public static String GetText(this IStringLocalizer localizer, Localizable localizable)
    {
        return localizable.Args.Length == 0
            ? localizer.GetString(localizable.ResourceKey).Value
            : localizer.GetString(localizable.ResourceKey, localizable.Args).Value;
    }
}
