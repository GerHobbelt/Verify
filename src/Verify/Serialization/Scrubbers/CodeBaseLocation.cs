﻿// ReSharper disable ConditionIsAlwaysTrueOrFalse

#if !NET6_0_OR_GREATER
static class CodeBaseLocation
{
    static CodeBaseLocation()
    {
        var assembly = typeof(CodeBaseLocation).Assembly;

        // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
        if (assembly.CodeBase is not null)
        {
            var uri = new UriBuilder(assembly.CodeBase);
            var path = Uri.UnescapeDataString(uri.Path);

            CurrentDirectory = Path.GetDirectoryName(path);
        }
    }

    public static string? CurrentDirectory;
}
#endif