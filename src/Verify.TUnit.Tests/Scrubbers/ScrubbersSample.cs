﻿#region ScrubbersSampleTUnit

public class ScrubbersSample
{
    [Test]
    public Task Lines()
    {
        var settings = new VerifySettings();
        settings.ScrubLinesWithReplace(
            replaceLine: _ =>
            {
                if (_.Contains("LineE"))
                {
                    return "NoMoreLineE";
                }

                return _;
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

    [Test]
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

    [Test]
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

    [Test]
    public Task EmptyLines() =>
        Verify("""

               LineA

               LineC

               """)
            .ScrubEmptyLines();
}

#endregion