using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7_Tasks_Otus_Prof
{
    internal static class Calculate
    {
        private const int MAX_DEGREE_OF_PARALLELISM = 3;
        internal async static Task<IDictionary<string, int>> CountSpacesInFilesInFolderAsync(string folderPath)
        {
            var filesSpace = new Dictionary<string, int>();

            string[] files = Directory.GetFiles(folderPath);
            var tasks = new List<Task<KeyValuePair<string, int>>>();

            KeyValuePair<string, int>[] results = [];

            using (var semaphore = new SemaphoreSlim(MAX_DEGREE_OF_PARALLELISM))
            {
                foreach (var file in files)
                {
                    await semaphore.WaitAsync();
                    tasks.Add(Task.Run(() =>
                    {
                        try
                        {
                            return CountSpacesInFile(file);
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    }));
                }
                results = await Task.WhenAll(tasks);
            }

            foreach (var result in results)
            {
                filesSpace[result.Key] = result.Value;
            }

            return filesSpace;
        }

        private static KeyValuePair<string, int> CountSpacesInFile(string filePath)
        {
            Console.WriteLine($"Thread:{Thread.CurrentThread.ManagedThreadId}");

            int spacesCount = default;

            using (StreamReader reader = new(filePath))
                while (!reader.EndOfStream)
                {
                    string? line = reader.ReadLine();
                    spacesCount += line!.Count(char.IsWhiteSpace);
                }

            return new KeyValuePair<string, int>(Path.GetFileName(filePath), spacesCount);
        }
    }
}
