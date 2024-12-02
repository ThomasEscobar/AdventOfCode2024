using AdventOfCode.Logging;

namespace AdventOfCode;

class AdventOfCode
{
    static void Main()
    {
        var logger = CustomLogging.Init("logfile.txt");
        logger.Information("Welcome to Thomas' Advent Of Code 2024 project !");

        // TODO: Figure out input parameter to select: day, example or not

        // Day 2
        new Day2.Solver("Day2/input.txt", logger).Solve();
    }
}
