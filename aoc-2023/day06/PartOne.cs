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

        /*
        NOTE
            - https://docs.microsoft.com/en-us/dotnet/api/system.linq.enumerable.zip?view=net-5.0

            We zip our times and distances together so we can iterate over them at the same time,
            we then iterate over the zipped list and calculate the number of ways to win.

            We then add the number of ways to win to our output array and repeat the process
            until we have iterated over all the values in our times array.

            We then multiply all the values in our output array to get our final result
         */
        times
            .Zip(distances) // zip times and distances together
            .ToList()
            .ForEach(x => // iterate over the zipped list
            {
                // we create a new array to store the total milliseconds
                var total = Array.Empty<int>();

                // we start from 0 milliseconds and count up to the recorded time (x.First)
                for (int i = 0; i < x.First; i++)
                {
                    /*
                    NOTE
                        Calculate the number of milliseconds the button was pressed.
                        e.g total recorded time ((x.First) - current time (i)) * current time (i)
                        e.g (7 - 0) * 0 = 0
                        e.g (7 - 1) * 1 = 6
                        e.g (7 - 2) * 2 = 10

                        If the the result is greater than the total distance recorded (x.Second)
                        then we add the current time (i) to our total array
                     */

                    // Console.WriteLine($"[Pressed: {i}, Traveled: {(x.First - i) * i}]");

                    var millisecondsHeld = (x.First - i) * i;
                    var distance = x.Second;

                    if (millisecondsHeld > distance)
                    {
                        total = total.Append(i).ToArray();
                    }
                }

                // count total number of ways to win
                output[Array.IndexOf(times, x.First)] = total.Length;
                // Console.WriteLine($"Total ways to win: {total.Length}");
            });

        Console.WriteLine(output.Aggregate((x, y) => x * y));
    }

    private static int[] GetNumbers(string line) =>
        line.Split(":", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)[1]
            .Split(" ", StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();
}
