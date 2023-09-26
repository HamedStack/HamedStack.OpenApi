// ReSharper disable IdentifierTypo
// ReSharper disable UnusedMember.Global

using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace HamedStack.OpenApi.Helpers;

internal static class Extensions
{
    internal static IEnumerable<T> Between<T>(
        this IEnumerable<T> source,
        Func<T, bool> firstCondition,
        Func<T, bool> secondCondition,
        bool includeBoundaryItems,
        out IList<T> excludingBetweenItems)
    {
        var foundFirst = false;
        var foundSecond = false;

        var betweenItems = new List<T>();
        excludingBetweenItems = new List<T>();

        foreach (var item in source)
        {
            if (foundSecond)
            {
                excludingBetweenItems.Add(item);
                continue;
            }

            if (foundFirst)
            {
                if (secondCondition(item))
                {
                    if (includeBoundaryItems)
                        betweenItems.Add(item);

                    foundSecond = true;
                    continue;
                }
                betweenItems.Add(item);
            }
            else if (firstCondition(item))
            {
                if (includeBoundaryItems)
                    betweenItems.Add(item);

                foundFirst = true;
            }
            else
            {
                excludingBetweenItems.Add(item);
            }
        }

        return betweenItems.AsEnumerable();
    }
    internal static string? GetDescription(this Enum @enum, bool returnEnumNameInsteadOfNull = false)
    {
        if (@enum == null) throw new ArgumentNullException(nameof(@enum));

        return
            @enum
                .GetType()
                .GetMember(@enum.ToString())
                .FirstOrDefault()
                ?.GetCustomAttribute<DescriptionAttribute>()
                ?.Description
            ?? (!returnEnumNameInsteadOfNull ? null : @enum.ToString());
    }

    internal static string RemoveMoreWhiteSpaces(this string text)
    {
        return Regex.Replace(text, @"\s+", " ");
    }

    internal static string ReplaceFirst(this string text, string search, string replace)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        var pos = text.IndexOf(search, StringComparison.Ordinal);
        if (pos < 0)
        {
            return text;
        }
        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }
}