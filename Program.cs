using AdventOfCode.Logging;

namespace AdventOfCode;

class AdventOfCode
{
    static void Main()
    {
        var logger = CustomLogging.Init("logfile.txt");
        logger.Information("Welcome to Thomas' Advent Of Code 2024 project !");

        // TODO: Figure out input parameter to select: day, example or not

        // Day 4
        new Day4.Solver("Day4/input.txt", logger).Solve();
    }
}
