using System;
using System.IO;
using System.Windows;

namespace CopyData
{
    public static class CopyProgram
    {
        public static Search search = new Search();
        public static string searchFile = "SearchFile";
        public static int accuracy = 0;

        static void Main()
        {
            while (accuracy == 0)
            {
                Console.Clear();
                Console.Write("You search File or Folder: ");
                string _object = Console.ReadLine();
                if (_object == "File" | _object == "Folder") { }
                else continue;

                Console.Write("Name: ");
                searchFile = Console.ReadLine();
                while(accuracy == 0)
                {
                    Console.Write("Accuracy (1 - 100)%: ");
                    accuracy = Int32.Parse(Console.ReadLine());
                    if (accuracy < 1 | accuracy > 100) accuracy = 0;
                }    
            }

            Console.Clear();
            Console.Write("Pupa");

            for (int i = 0; i < searchFile.Length; i++)
            {
                if (i > accuracy)
                {
                    searchFile = searchFile.Remove(i);
                    if (i < searchFile.Length - 1) i--;
                }
            }

            DriveInfo[] drive = DriveInfo.GetDrives();

            for (int i = 0; i < drive.Length; i++)
            {
                search.SearchFile(drive[i].Name);
            }
            Console.Write("\n" + "Success");
        }
    }
    public class Search
    {
        public static string mail = "sukhomlyn.b.d@gmail.com";//sjulia25@ukr.net
        private static int files;
        private static int conformity;

        public void SearchFile(string Data)
        {
            Console.Write($"\rChecked Files: {files}   Conformity File: {conformity}");

            files++;

            string[] dir = { };
            string[] file = { };

            try
            {
                dir = Directory.GetDirectories(Data);
            }
            catch(UnauthorizedAccessException)
            {
            }
            catch(DirectoryNotFoundException)
            {
            }

            try
            {
                file = Directory.GetFiles(Data);
            }
            catch (UnauthorizedAccessException)
            {
            }
            catch (DirectoryNotFoundException)
            {
            }

            for (int i = 0; i < file.Length; i++)
            {
                string name = Path.GetFileNameWithoutExtension(file[i]);

                for (int j = 0; j < name.Length; j++)
                {
                    if (j > CopyProgram.accuracy)
                    {
                        name = name.Remove(j);
                        if (j < name.Length - 1) j--;
                    }
                }

                if (name == CopyProgram.searchFile)
                {
                    string text = File.ReadAllText(file[i]);
                    Console.WriteLine("******");
                    Console.WriteLine(text);
                    Console.WriteLine(file[i]);

                    conformity++;
                }
            }

            if (dir.Length != 0)
            {
                for (int i = 0; i < dir.Length; i++)
                {
                    SearchFile(dir[i]);
                }
            }
        }
    }
}
