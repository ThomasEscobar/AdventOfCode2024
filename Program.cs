using AdventOfCode.Logging;

namespace AdventOfCode;

class AdventOfCode
{
    static void Main()
    {
        var logger = CustomLogging.Init("logfile.txt");
        logger.Information("Welcome to Thomas' Advent Of Code 2024 project !");

        // TODO: Figure out input parameter to select: day, example or not

        // Day 3
        new Day3.Solver("Day3/input.txt", logger).Solve();
    }
}
