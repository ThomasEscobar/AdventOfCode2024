namespace AdventOfCode.ToolBox;

public static class ToolBoxClass
{
    public static List<int> GetIntListFromInput(string inputFilePath = null)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new Exception($"Error trying to open the input file: file doesn't exist. Path: {inputFilePath}");
        }

        var list = new List<int>();
        var inputLines = File.ReadAllLines(inputFilePath);

        foreach (var line in inputLines)
        {
            int i;
            if (Int32.TryParse(line, out i))
            {
                list.Add(i);
            }
            else
            {
                throw new Exception($"Error trying to parse a line from input file to int. Line content is: {line}");
            }
        }

        return list;
    }

    public static List<double> GetDoubleListFromInput(string inputFilePath = null)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new Exception($"Error trying to open the input file: file doesn't exist. Path: {inputFilePath}");
        }

        var list = new List<double>();
        var inputLines = File.ReadAllLines(inputFilePath);

        foreach (var line in inputLines)
        {
            double i;
            if (Double.TryParse(line, out i))
            {
                list.Add(i);
            }
            else
            {
                throw new Exception($"Error trying to parse a line from input file to double. Line content is: {line}");
            }
        }

        return list;
    }

    public static List<string> GetStringListFromInput(string inputFilePath = null)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new Exception($"Error trying to open the input file: file doesn't exist. Path: {inputFilePath}");
        }

        var list = new List<string>();
        list.AddRange(File.ReadAllLines(inputFilePath));
        return list;
    }
}