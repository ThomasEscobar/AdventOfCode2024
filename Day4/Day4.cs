using System.Diagnostics;
using System.Text.RegularExpressions;

using AdventOfCode.ToolBox;

using Serilog.Core;

namespace AdventOfCode.Day4;

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
        logger.Information($"=== Day 4 ===");

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
        logger.Information("PART 1 - Looking for number of XMAS words !");

        var xmasCount = 0;

        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[0].Length; x++)
            {
                xmasCount += CountXmas(x, y);
            }
        }

        logger.Information($"Found a total of {xmasCount} XMAS");
    }

    public void SolvePart2()
    {
        logger.Information("PART 2 - Looking for number of X-MAS");

        var xMasCount = 0;

        for (var y = 0; y < input.Count; y++)
        {
            for (var x = 0; x < input[0].Length; x++)
            {
                if (CountXMas(x, y)) xMasCount++;
            }
        }

        logger.Information($"Found a total of {xMasCount} X-MAS");
    }

    private int CountXmas(int x, int y)
    {
        var count = 0;

        if (input[y][x] != 'X') return count;
        // Choose a direction to look in for the rest of the word
        for (var dy = -1; dy <= 1; dy++)
        {
            for (var dx = -1; dx <= 1; dx++)
            {
                try
                {
                    // Look for M at distance 1, A at distance 2, S at distance 3
                    if (input[y + dy][x + dx] != 'M') continue;
                    if (input[y + 2 * dy][x + 2 * dx] != 'A') continue;
                    if (input[y + 3 * dy][x + 3 * dx] != 'S') continue;
                    count++;
                }
                catch
                {
                    // Ignore out of bound exceptions
                }
            }
        }

        return count;
    }

    private bool CountXMas(int x, int y)
    {
        if (input[y][x] != 'A') return false;
        // Choose a direction to look in for the rest of the word
        for (var dy = -1; dy <= 1; dy++)
        {
            for (var dx = -1; dx <= 1; dx++)
            {
                // Skip horizontal or vertical direction, not X shaped
                if (dx == 0 || dy == 0) continue;
                try
                {
                    // Look for MAS in diagonal, i.e. M before and S after the A in any direction
                    if (input[y - dy][x - dx] != 'M') continue;
                    if (input[y + dy][x + dx] != 'S') continue;

                    // Look for diagonal MAS in the perpendicular direction, i.e. inverted X and Y
                    if (input[y - dy][x + dx] != 'M' && input[y + dy][x - dx] != 'M') continue;
                    if (input[y + dy][x - dx] != 'S' && input[y - dy][x + dx] != 'S') continue;

                    // If both found, true
                    return true;
                }
                catch
                {
                    // Ignore out of bound exceptions
                }
            }
        }

        return false;
    }
}