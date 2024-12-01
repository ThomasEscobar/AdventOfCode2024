using AdventOfCode.Logging;

namespace AdventOfCode;

class AdventOfCode
{
    static void Main()
    {
        var logger = CustomLogging.Init("logfile.txt");
        logger.Information("Welcome to Thomas' Advent Of Code 2024 project !");

        // Day 1
        new Day1.Solver("Day1/input.txt", logger).Solve();
    }
}
