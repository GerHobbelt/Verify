﻿namespace VerifyTests;

public partial class SettingsTask
{
    /// <summary>
    /// Modify the resulting test content using custom code.
    /// </summary>
    [Pure]
    public SettingsTask AddScrubber(Action<StringBuilder> scrubber, ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.AddScrubber(scrubber, location);
        return this;
    }

    /// <summary>
    /// Modify the resulting test content using custom code.
    /// </summary>
    [Pure]
    public SettingsTask AddScrubber(string extension, Action<StringBuilder> scrubber, ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.AddScrubber(extension, scrubber, location);
        return this;
    }

    /// <summary>
    /// Replace inline <see cref="Guid" />s with a placeholder.
    /// </summary>
    [Pure]
    public SettingsTask ScrubInlineGuids(ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubInlineGuids(location);
        return this;
    }

    /// <summary>
    /// Replace inline <see cref="Guid" />s with a placeholder.
    /// </summary>
    [Pure]
    public SettingsTask ScrubInlineGuids(string extension, ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubInlineGuids(extension, location);
        return this;
    }

    /// <summary>
    /// Disables counting of dates.
    /// </summary>
    [Pure]
    public SettingsTask DisableDateCounting()
    {
        CurrentSettings.DisableDateCounting();
        return this;
    }

    /// <summary>
    /// Replace inline <see cref="DateTime" />s with a placeholder.
    /// </summary>
    [Pure]
    public SettingsTask ScrubInlineDateTimes(
        [StringSyntax(StringSyntaxAttribute.DateTimeFormat)] string format,
        Culture? culture = null,
        ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubInlineDateTimes(format, culture, location);
        return this;
    }

    /// <summary>
    /// Replace inline <see cref="DateTime" />s with a placeholder.
    /// </summary>
    [Pure]
    public SettingsTask ScrubInlineDateTimeOffsets(
        [StringSyntax(StringSyntaxAttribute.DateTimeFormat)] string format,
        Culture? culture = null,
        ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubInlineDateTimeOffsets(format, culture, location);
        return this;
    }

#if NET6_0_OR_GREATER

    /// <summary>
    /// Replace inline <see cref="Date" />s with a placeholder.
    /// </summary>
    [Pure]
    public SettingsTask ScrubInlineDates(
        [StringSyntax(StringSyntaxAttribute.DateOnlyFormat)] string format,
        Culture? culture = null,
        ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubInlineDates(format, culture, location);
        return this;
    }

#endif

    /// <summary>
    /// Remove the <see cref="Environment.MachineName" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubMachineName(ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubMachineName(location);
        return this;
    }

    /// <summary>
    /// Remove the <see cref="Environment.MachineName" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubMachineName(string extension, ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubMachineName(extension, location);
        return this;
    }

    /// <summary>
    /// Remove the <see cref="Environment.UserName" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubUserName(ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubUserName(location);
        return this;
    }

    /// <summary>
    /// Remove the <see cref="Environment.UserName" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubUserName(string extension,ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubUserName(extension, location);
        return this;
    }

    /// <summary>
    /// Remove any lines containing any of <paramref name="stringToMatch" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubLinesContaining(StringComparison comparison, params string[] stringToMatch)
    {
        CurrentSettings.ScrubLinesContaining(comparison, stringToMatch);
        return this;
    }

    /// <summary>
    /// Remove any lines containing any of <paramref name="stringToMatch" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubLinesContaining(string extension, StringComparison comparison, params string[] stringToMatch)
    {
        CurrentSettings.ScrubLinesContaining(extension, comparison, stringToMatch);
        return this;
    }

    /// <summary>
    /// Remove any lines containing any of <paramref name="stringToMatch" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubLinesContaining(StringComparison comparison, ScrubberLocation location = ScrubberLocation.First, params string[] stringToMatch)
    {
        CurrentSettings.ScrubLinesContaining(comparison, location, stringToMatch);
        return this;
    }

    /// <summary>
    /// Remove any lines containing any of <paramref name="stringToMatch" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubLinesContaining(string extension, StringComparison comparison, ScrubberLocation location = ScrubberLocation.First, params string[] stringToMatch)
    {
        CurrentSettings.ScrubLinesContaining(extension, comparison, location, stringToMatch);
        return this;
    }

    /// <summary>
    /// Remove any lines matching <paramref name="removeLine" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubLines(Func<string, bool> removeLine, ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubLines(removeLine, location);
        return this;
    }

    /// <summary>
    /// Remove any lines matching <paramref name="removeLine" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubLines(string extension, Func<string, bool> removeLine, ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubLines(extension, removeLine, location);
        return this;
    }

    /// <summary>
    /// Scrub lines with an optional replace.
    /// <paramref name="replaceLine" /> can return the input to ignore the line, or return a different string to replace it.
    /// </summary>
    [Pure]
    public SettingsTask ScrubLinesWithReplace(Func<string, string?> replaceLine, ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubLinesWithReplace(replaceLine, location);
        return this;
    }

    /// <summary>
    /// Scrub lines with an optional replace.
    /// <paramref name="replaceLine" /> can return the input to ignore the line, or return a different string to replace it.
    /// </summary>
    [Pure]
    public SettingsTask ScrubLinesWithReplace(string extension, Func<string, string?> replaceLine, ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubLinesWithReplace(extension, replaceLine, location);
        return this;
    }

    /// <summary>
    /// Remove any lines containing only whitespace from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubEmptyLines(ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubEmptyLines(location);
        return this;
    }

    /// <summary>
    /// Remove any lines containing only whitespace from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubEmptyLines(string extension, ScrubberLocation location = ScrubberLocation.First)
    {
        CurrentSettings.ScrubEmptyLines(extension, location);
        return this;
    }

    /// <summary>
    /// Remove any lines containing any of <paramref name="stringToMatch" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubLinesContaining(params string[] stringToMatch)
    {
        CurrentSettings.ScrubLinesContaining(stringToMatch);
        return this;
    }

    /// <summary>
    /// Remove any lines containing any of <paramref name="stringToMatch" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubLinesContaining(ScrubberLocation location = ScrubberLocation.First, params string[] stringToMatch)
    {
        CurrentSettings.ScrubLinesContaining(location, stringToMatch);
        return this;
    }

    /// <summary>
    /// Remove any lines containing any of <paramref name="stringToMatch" /> from the test results.
    /// </summary>
    [Pure]
    public SettingsTask ScrubLinesContaining(string extension, ScrubberLocation location = ScrubberLocation.First, params string[] stringToMatch)
    {
        CurrentSettings.ScrubLinesContaining(extension, location, stringToMatch);
        return this;
    }
}