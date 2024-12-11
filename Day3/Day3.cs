using System.Diagnostics;
using System.Text.RegularExpressions;

using AdventOfCode.ToolBox;

using Serilog.Core;

namespace AdventOfCode.Day3;

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
        logger.Information($"=== Day 3 ===");

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
        logger.Information("PART 1 - Sum of all the multiplication instructions");

        var r = new Regex(@"mul\(([0-9]+),([0-9]+)\)");
        var sum = 0;

        foreach (var line in input)
        {
            var matches = r.Matches(line);
            foreach (Match match in matches)
            {
                sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
            }
        }

        logger.Information($"The final sum is {sum}");
    }


    public void SolvePart2()
    {
        logger.Information("PART 2 - Sum of all ENABLED multiplication instructions");

        var r = new Regex(@"mul\(([0-9]+),([0-9]+)\)|(don't)|(do)");
        var sum = 0;
        var enabled = true;

        foreach (var line in input)
        {
            var match = r.Match(line);
            while (match.Success)
            {
                // Groups are ordered, so 3 will always be "don't" for example
                // Found a "don't"
                if (match.Groups[3].Success)
                {
                    enabled = false;
                }
                // Found a "do"
                else if (match.Groups[4].Success)
                {
                    enabled = true;
                }
                // Found a mul statement
                else if (enabled)
                {
                    sum += int.Parse(match.Groups[1].Value) * int.Parse(match.Groups[2].Value);
                }

                match = match.NextMatch();
            }
        }

        logger.Information($"The final sum is {sum}");
    }
}