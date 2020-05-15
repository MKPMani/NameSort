using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Text;

namespace NameSort
{
    class Program
    {
        static void Main(string[] args)
        {
            NamingOrder();
            Console.ReadLine();
        }

        /// <summary>
        /// Sorting by Last name
        /// </summary>
        private static void NamingOrder()
        {
            try
            {
                //get name from file
                var file = ConfigurationManager.AppSettings["filepath"];

                //File validation
                if (File.Exists(file))
                {
                    //get all names from file
                    string[] names = File.ReadAllLines(file);

                    for (int iLoop = 0; iLoop <= names.Length - 1; iLoop++)
                    {
                        for (int jLoop = 0; jLoop < names.Length - 1; jLoop++)
                        {
                            string prevName = names[jLoop];
                            string nextName = names[jLoop + 1];

                            int prevIndex = prevName.Contains(" ") ? prevName.LastIndexOf(" ") : 0;
                            int nextIndex = nextName.Contains(" ") ? nextName.LastIndexOf(" ") : 0;

                            //Null check
                            if (!string.IsNullOrEmpty(names[jLoop]))
                            {
                                if (string.Compare(prevName, prevIndex, nextName, nextIndex, 50) > 0)
                                {
                                    names[jLoop] = nextName;
                                    names[jLoop + 1] = prevName;
                                }
                            }
                        }
                    }

                    foreach (string line in names)
                        Console.WriteLine(line);

                    WriteFile(names);
                }
                else
                    Console.WriteLine("File not fould.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Write output into file
        /// </summary>
        /// <param name="data"></param>
        private static void WriteFile(string[] data)
        {
            try
            {
                var fileName = ConfigurationManager.AppSettings["outputfile"];

                // Check if file already exists. If yes, delete it.     
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }

                File.WriteAllLines(fileName, data);
                Console.WriteLine("----------------------");
                Console.WriteLine($"File output file saved : {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in write file");
            }
        }
    }
}
