using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework7_Tasks_Otus_Prof.Helpers
{
    internal static class UserInteraction
    {
        internal static string GetFolderPathFromUser()
        {
            string? folderPath = default;

            while (string.IsNullOrEmpty(folderPath))
            {
                Console.WriteLine("Enter the path to the folder with files:");

                folderPath = Console.ReadLine();

                if (!Directory.Exists(folderPath))
                {
                    Console.WriteLine("Invalid folder path.");
                    folderPath = default;
                }
            }

            return folderPath;
        }
    }
}
