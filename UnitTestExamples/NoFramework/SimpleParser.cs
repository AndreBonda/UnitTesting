namespace NoFramework;

class SimpleParser
{
    public int ParseAndSum(String numbers)
    {
        if (string.IsNullOrWhiteSpace(numbers))
            return 0;

        if (!numbers.Contains(','))
            return int.Parse(numbers);

        throw new InvalidOperationException("Not yet implemented");
    }
}

static class SimpleParserTest
{
    public static void TestReturnZeroWithEmptyString()
    {
        try
        {
            Console.WriteLine(@"***SimpleParserTest.TestReturnZeroWithEmptyString: running...");
            var sp = new SimpleParser();
            int result = sp.ParseAndSum(string.Empty);

            if (result != 0)
            {
                Console.WriteLine(@"***SimpleParserTest.TestReturnZeroWithEmptyString:
                ---
                ParseAndSum should have returned 0 on an empty string");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }
}