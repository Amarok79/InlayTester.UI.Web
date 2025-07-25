// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

namespace InlayTester.Domain;


/// <summary>
///     Represents nothing, void, empty, none.
/// </summary>
public readonly record struct None
{
    /// <summary>
    ///     The singleton instance.
    /// </summary>
    public static readonly None Instance = default;


    /// <inheritdoc/>
    public override String ToString()
    {
        return "<None>";
    }
}
