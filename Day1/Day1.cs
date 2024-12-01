using System.Diagnostics;
using Serilog.Core;
using AdventOfCode.ToolBox;

namespace AdventOfCode.Day1
{
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
            logger.Information($"=== Day 1 ===");

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
            logger.Information("PART 1 - Calculting sum of distances between sorted lists");

            // Split the 2 columns into separate lists
            List<string> leftList = [];
            List<string> rightList = [];
            foreach (var l in input)
            {
                var split = l.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                leftList.Add(split.FirstOrDefault());
                rightList.Add(split.LastOrDefault());
            }

            // Sort the lists
            leftList.Sort();
            rightList.Sort();

            // Calculate distance between matching numbers and add to total sum
            var sum = 0;
            for (var i = 0; i < leftList.Count; i++)
            {
                sum += Math.Abs(int.Parse(leftList[i]) - int.Parse(rightList[i]));
            }

            logger.Information($"The total sum of distances is: {sum}");
        }

        public void SolvePart2()
        {
            logger.Information("PART 2 - Calculating 'similarity score' between the two lists");

            // Split the 2 columns into separate lists
            List<string> leftList = [];
            List<string> rightList = [];
            foreach (var l in input)
            {
                var split = l.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                leftList.Add(split.FirstOrDefault());
                rightList.Add(split.LastOrDefault());
            }

            // Calculate and add up similarity score for each nb in left list
            var sum = 0;
            foreach (var str in leftList)
            {
                var x = int.Parse(str);
                sum += x * rightList.Where(y => x == int.Parse(y)).Count();
            }

            logger.Information($"The total similarity score is: {sum}");
        }
    }
}