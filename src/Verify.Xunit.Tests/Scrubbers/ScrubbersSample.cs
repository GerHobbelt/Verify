﻿public class ScrubbersSample
{
    [Fact]
    public Task Lines()
    {
        var settings = new VerifySettings();
        settings.ScrubLinesWithReplace(
            replaceLine: line =>
            {
                if (line.Contains("LineE"))
                {
                    return "NoMoreLineE";
                }

                return line;
            });
        settings.ScrubLines(removeLine: _ => _.Contains('J'));
        settings.ScrubLinesContaining("b", "D");
        settings.ScrubLinesContaining(StringComparison.Ordinal, "H");
        return Verify(
            settings: settings,
            target: """
                    LineA
                    LineB
                    LineC
                    LineD
                    LineE
                    LineH
                    LineI
                    LineJ
                    """);
    }

    [Fact]
    public Task EmptyLine()
    {
        var settings = new VerifySettings();
        settings.ScrubLinesWithReplace(
            replaceLine: _ => "");
        return Verify(
            settings: settings,
            target: "");
    }

    [Fact]
    public Task LinesFluent() =>
        Verify("""
               LineA
               LineB
               LineC
               LineD
               LineE
               LineH
               LineI
               LineJ
               """)
            .ScrubLinesWithReplace(
                replaceLine: _ =>
                {
                    if (_.Contains("LineE"))
                    {
                        return "NoMoreLineE";
                    }

                    return _;
                })
            .ScrubLines(removeLine: _ => _.Contains('J'))
            .ScrubLinesContaining("b", "D")
            .ScrubLinesContaining(StringComparison.Ordinal, "H");

    [Fact]
    public Task RemoveOrReplace() =>
        Verify("""
               LineA
               LineB
               LineC
               """)
            .ScrubLinesWithReplace(
                replaceLine: line =>
                {
                    if (line.Contains("LineB"))
                    {
                        return null;
                    }

                    return line.ToLower();
                });

    [Fact]
    public Task EmptyLines() =>
        Verify("""

               LineA

               LineC

               """)
            .ScrubEmptyLines();
}