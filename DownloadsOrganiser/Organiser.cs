using System;
using System.Data;
using System.IO;
using System.Text;
using System.Linq;

namespace DownloadsOrganiser
{
    class Organiser
    {
        static void Main(string[] args)
        {
            var dirPath = GetDirectoryPath();
            OrganiseFiles(dirPath);
            Console.WriteLine("\n\n Press any key to continue");
            Console.ReadKey();
        }

        public static string GetDirectoryPath()
        {
            // super simple command line interface
            Console.WriteLine("Please provide a path to the direcotry that you want to be organised:\n");
            var filePath = Console.ReadLine();
            while (filePath != null && filePath.Length == 0)
            {
                filePath = Console.ReadLine();
            }

            return filePath;
        }

        public static bool OrganiseFiles(string filePath)
        {
            if (!Directory.Exists(filePath))
            {
                Console.WriteLine("Could not find directory at the path specified.");
                return false;
            }
            DirectoryInfo dInfo = new DirectoryInfo(filePath);
            FileInfo[] files = dInfo.GetFiles();

            if (files.Length == 0)
            {
                Console.WriteLine("No files found in the specified directory");
                return false;
            }

            foreach (var file in files)
            {
                if (string.IsNullOrEmpty(file.Extension))
                {
                    continue;
                }
                var destinationPath = $"{filePath}\\{file.Extension.Remove(0, 1)}";
                Console.WriteLine($"{file.Name}");
                try
                {
                    if (!Directory.Exists(destinationPath))
                    {
                        try
                        {
                            dInfo.CreateSubdirectory(file.Extension.Remove(0, 1));
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            Console.WriteLine($"could not create subdirectory for file extention {file.Extension}");
                            continue;
                        }
                    }
                    file.MoveTo($"{destinationPath}\\{file.Name}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    Console.WriteLine($"could not move file {file.Name}");
                }
            }

            return true;
        }
    }
}
