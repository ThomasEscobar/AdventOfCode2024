using System.Diagnostics;

using AdventOfCode.ToolBox;

using Serilog.Core;

namespace AdventOfCode.Day5;

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
        logger.Information($"=== Day 5 ===");

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
        logger.Information("PART 1 - Finding correctly ordered updates, then doing the sum of middle page numbers");

        // Parse rules into dictionary
        var ruleDictionary = new Dictionary<int, List<int>>();
        foreach (var ruleLine in input.Where(x => x.Contains('|')))
        {
            var pageId = int.Parse(ruleLine.Split('|')[0]);
            var pageAfterId = int.Parse(ruleLine.Split('|')[1]);
            if (!ruleDictionary.ContainsKey(pageId))
            {
                ruleDictionary.Add(pageId, [pageAfterId]);
            }
            else
            {
                ruleDictionary.GetValueOrDefault(pageId)?.Add(pageAfterId);
            }
        }

        // Debug dictionary
        ruleDictionary.Select(kvp => $"{kvp.Key} - [{string.Join(',', kvp.Value)}]").ToList().ForEach(logger.Debug);

        // Process each update against the rules
        var sum = 0;
        foreach (var updateLine in input.Where(x => x.Contains(',')))
        {
            var updateNbList = updateLine.Split(',').Select(str => int.Parse(str)).ToList();
            if (UpdateIsOrdered(updateNbList, ruleDictionary))
            {
                logger.Debug($"Decided that list {string.Join(',', updateNbList)} is correctly ordered");
                sum += updateNbList[updateNbList.Count / 2];
            }
        }

        logger.Information($"The final sum is {sum}");
    }

    private bool UpdateIsOrdered(List<int> nbList, Dictionary<int, List<int>> ruleDictionary)
    {
        for (var i = 0; i < nbList.Count; i++)
        {
            // Ignore nbs that don't have rules assigned to them
            if (ruleDictionary.ContainsKey(nbList[i]))
            {
                // For the ones that do, check that no nb in the list breaks the rule, i.e. no nb are before when they should be after
                if (ruleDictionary.GetValueOrDefault(nbList[i]).Any(nbThatShouldBeAfter => nbList.GetRange(0, i + 1).Contains(nbThatShouldBeAfter))) return false;
            }
        }

        return true;
    }

    public void SolvePart2()
    {
        logger.Information("PART 2 - Finding non-ordered updates, updating them then doing the sum of middle page numbers");

        // Parse rules into dictionary
        var ruleDictionary = new Dictionary<int, List<int>>();
        foreach (var ruleLine in input.Where(x => x.Contains('|')))
        {
            var pageId = int.Parse(ruleLine.Split('|')[0]);
            var pageAfterId = int.Parse(ruleLine.Split('|')[1]);
            if (!ruleDictionary.ContainsKey(pageId))
            {
                ruleDictionary.Add(pageId, [pageAfterId]);
            }
            else
            {
                ruleDictionary.GetValueOrDefault(pageId)?.Add(pageAfterId);
            }
        }

        // Debug dictionary
        ruleDictionary.Select(kvp => $"{kvp.Key} - [{string.Join(',', kvp.Value)}]").ToList().ForEach(logger.Debug);

        // Process each update against the rules
        var sum = 0;
        foreach (var updateLine in input.Where(x => x.Contains(',')))
        {
            var updateNbList = updateLine.Split(',').Select(str => int.Parse(str)).ToList();
            if (!UpdateIsOrdered(updateNbList, ruleDictionary))
            {
                logger.Debug($"Decided that list {string.Join(',', updateNbList)} is not correctly ordered, re-ordering now...");
                updateNbList.Sort((a, b) => CompareNumbersWithRules(a, b, ruleDictionary));
                sum += updateNbList[updateNbList.Count / 2];
            }
        }

        logger.Information($"The final sum is {sum}");
    }

    private int CompareNumbersWithRules(int a, int b, Dictionary<int, List<int>> ruleDictionary)
    {
        logger.Debug($"Comparing {a} and {b}");
        if (ruleDictionary.ContainsKey(a) && ruleDictionary.GetValueOrDefault(a).Contains(b))
        {
            // Prefer first number
            logger.Debug($"We prefer {a} first, returning -1");
            return -1;
        }
        else if (ruleDictionary.ContainsKey(b) && ruleDictionary.GetValueOrDefault(b).Contains(a))
        {
            // Prefer second number
            logger.Debug($"We prefer {b} first, returning 1");
            return 1;
        }

        // Don't change anything if no rules apply to the numbers being compared
        return 0;
    }
}