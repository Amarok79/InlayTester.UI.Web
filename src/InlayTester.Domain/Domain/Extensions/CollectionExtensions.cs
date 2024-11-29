// Copyright (c) 2025, Olaf Kober <olaf.kober@outlook.com>

using System.Collections.ObjectModel;


namespace InlayTester.Domain;


public static class CollectionExtensions
{
    public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> enumerable)
    {
        return new ObservableCollection<T>(enumerable);
    }
}
