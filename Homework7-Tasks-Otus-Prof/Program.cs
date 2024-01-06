using Homework7_Tasks_Otus_Prof;
using Homework7_Tasks_Otus_Prof.Helpers;
using System.Diagnostics;

while (true)
{
    Stopwatch stopwatch = new();

    var folderPath = UserInteraction.GetFolderPathFromUser();

    stopwatch.Start();

    var filesSpace = await Calculate.CountSpacesInFilesInFolderAsync(folderPath);

    stopwatch.Stop();

    foreach (var file in filesSpace)
    {
        Console.WriteLine($"There are {file.Value} spaces in the file named {file.Key}");
    }
    Console.WriteLine($"Execution time: {stopwatch.Elapsed.TotalSeconds} seconds");
}
