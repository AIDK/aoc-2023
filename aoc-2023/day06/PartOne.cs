namespace aoc_2023.day06;

public static partial class PartOne
{
    public static void Process()
    {
        /*
            Time:      7  15   30
            Distance:  9  40  200
        */

        var lines = Extractor.Extract(@"day06/input.txt");
        var times = GetNumbers(lines[0]);
        var distances = GetNumbers(lines[1]);

        // we create a new array to store the number of ways to win for each time
        var output = new int[times.Length];

        for (int i = 0; i < times.Length; i++)
        {
            // we get the current time
            var time = times[i];

            // we create a new array to store the total milliseconds
            var total = Array.Empty<int>();

            // we start from 0 milliseconds and count up to the recorded time (times[i])
            for (int k = 0; k < time; k++)
            {
                /*
                NOTE
                    We calculate the number of milliseconds the button was pressed.
                    e.g total recorded time (time - current time (k)) * current time (k)

                    e.g (7 - 0) * 0 = 0
                    e.g (7 - 1) * 1 = 6
                    e.g (7 - 2) * 2 = 10
                 */

                if (((time - k) * k) > distances[i])
                {
                    // if the number of milliseconds is greater than the distance, we append it to the total array
                    total = total.Append(k).ToArray();
                }
            }

            // count total number of ways to win
            output[i] = total.Length;
        }

        Console.WriteLine(output.Aggregate((x, y) => x * y));
    }

    private static int[] GetNumbers(string line) =>
        line.Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1]
            .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
}
