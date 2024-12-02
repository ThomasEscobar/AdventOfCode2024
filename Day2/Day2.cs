using System.Diagnostics;
using Serilog.Core;
using AdventOfCode.ToolBox;

namespace AdventOfCode.Day2;

public class Solver
{
    private readonly Logger logger;
    private List<string> input;
    public Solver(string inputFilePath, Logger logger)
    {
        this.logger = logger;
        try
        {
            this.input = ToolBoxClass.GetStringListFromInput(inputFilePath);
        }
        catch (Exception e)
        {
            logger.Error(e, $"There was an error reading the input file.{Environment.NewLine}");
            Environment.Exit(1);
        }
    }

    public void Solve()
    {
        logger.Information($"=== Day 2 ===");

        var sw = new Stopwatch();
        sw.Start();

        this.SolvePart1();
        logger.Information($"(Part 1 took {sw.ElapsedMilliseconds} ms)");

        sw.Restart();

        this.SolvePart2();
        logger.Information($"(Part 2 took {sw.ElapsedMilliseconds} ms)");

        logger.Information("=============");
    }

    public void SolvePart1()
    {
        logger.Information("PART 1 - Counting the number of safe reports");

        var safeCount = input.Count(line => IsReportSafe(line.Split(' ').Select(str => int.Parse(str)).ToList()));

        logger.Information($"There are {safeCount}/{input.Count} safe reports");
    }

    public void SolvePart2()
    {
        logger.Information("PART 2 - Counting the number of safe reports when using \"problem dampener\"");

        var kindaSafeCount = 0;
        foreach (var line in input)
        {
            var nbs = line.Split(' ').Select(str => int.Parse(str)).ToList();
            if (IsReportSafe(nbs)) kindaSafeCount++;
            else
            {
                List<List<int>> similarNbs = [];
                // Create a list of reports that are slightly smaller, then check if any of those is safe
                for (var i = 0; i < nbs.Count; i++)
                {
                    List<int> tmpNbs = [];
                    tmpNbs.AddRange(nbs);
                    tmpNbs.RemoveAt(i);
                    similarNbs.Add(tmpNbs);
                }
                if (similarNbs.Any(IsReportSafe)) kindaSafeCount++;
            }
        }

        logger.Information($"There are {kindaSafeCount}/{input.Count} safe reports when using the \"problem dampener\"");
    }

    private bool IsReportSafe(List<int> nbs)
    {
        // Calculate list of differences
        var differences = nbs.Select((nb, i) => i + 1 < nbs.Count ? nbs[i + 1] - nb : 0).ToList();
        // Remove last value which will always be 0 (no number to calculate next difference)
        differences.RemoveAt(differences.Count - 1);

        // Return false if difference is 0 or more than 3
        if (differences.Any(x => Math.Abs(x) > 3 || x == 0))
        {
            return false;
        }

        // Return false if not all differences are positive and they are not all negative (i.e. mix of both)
        if (!differences.All(x => x > 0) && !differences.All(x => x < 0))
        {
            return false;
        }

        // Else safe
        return true;
    }
}