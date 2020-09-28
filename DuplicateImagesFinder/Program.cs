using DuplicateImagesFinder.Services;
using System;

namespace DuplicateImagesFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            var duplicateImageFinder = new DuplicateImageFinder();

            if (args == null || args.Length == 0)
            {
                throw new ArgumentNullException($"You must specify the path for the directory where to run the application.");
            }

            var path = args[0];
            duplicateImageFinder.Process(path);

            Console.WriteLine("Press any key to stop...");
            Console.ReadKey();
        }
    }
}