namespace Cards;

internal class Program
{
    static void Main(string[] args)
    {
        string inputPath = "";  // To be edited
        string outputPath = ""; // To be edited

        if (inputPath == string.Empty || outputPath == string.Empty)
            throw new Exception("Please type in your input and output file paths before running this program.");

        using StreamReader sr = new(inputPath);
        using StreamWriter sw = new(outputPath);

        int problemCount = int.Parse(sr.ReadLine());
        int result; // Predefine the variable to not have to create it in every iteration of the cycle below

        // One iteration per problem
        for (int i = 0; i < problemCount; i++)
        {
            // Load and format data
            int[] data = Array.ConvertAll(sr.ReadLine().Split(' '), int.Parse);

            // Calculate result
            result = SolveProblem(data[0], data[1], data[2], data[3]);
            sw.Write(result);

            // Go to a new line in the output file unless this problem is the last one
            // (has to be done due to formatting conventions)
            if (i < problemCount - 1)
                sw.WriteLine();
        }
    }

    static int SolveProblem(int paperCount, int cutterDuration, int scissorDuration, int scissorBatchSize)
    {
        int totalCutTime = 0;

        // Main paper count is how many papers we can use to make batches of the scissors'
        // batch size, remaining paper count is paper count modulo scissor batch size (because it isn't
        // a full batch of papers anymore and we have to calculate its most efficient way of cutting
        // separately)
        int remainingPaperCount = paperCount % scissorBatchSize;
        int mainPaperCount = paperCount - remainingPaperCount;

        // Main cutter time is how long it takes to cut up all main papers using the cutter
        // Main scissors time is its equivalent for the scissors
        int mainCutterTime = mainPaperCount * cutterDuration;
        int mainScissorsTime = (mainPaperCount / scissorBatchSize) * scissorDuration;

        // Use the shorter cut time to cut the main papers
        totalCutTime += Math.Min(mainCutterTime, mainScissorsTime);

        // Calculate how long it takes to cut up the rest of the papers with the cutter
        // (the scissors always take their batch cut duration as the batch count is 1 - it's just a smaller batch)
        int remainingCutterTime = remainingPaperCount * cutterDuration;

        // Use the shorter cut time to cut the remaining papers
        totalCutTime += Math.Min(remainingCutterTime, scissorDuration);

        return totalCutTime;
    }
}
