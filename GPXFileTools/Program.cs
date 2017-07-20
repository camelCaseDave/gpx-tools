using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPXFileTools
{
    class Program
    {
        static void Main(string[] args)
        {
            char dataToRemove = ' ';
            IEnumerable<char> validDataToRemove = new[] { 'C', 'H', 'T' };

            while (!validDataToRemove.Contains(dataToRemove))
            {
                Console.WriteLine("\nDo you want to remove cadence (C), heart rate (H) or temperature (T)?");
                dataToRemove = Console.ReadKey().KeyChar;
            }

            Console.WriteLine("\nEnter the name of the file to read from:");
            string fileToRead = Console.ReadLine();

            if (!fileToRead.Contains(".gpx"))
                fileToRead += ".gpx";

            using (var fileStream = File.OpenRead(fileToRead))
            using (var streamWriter = new StreamWriter("new_" + fileToRead))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true, 128))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (!line.Contains("gpxtpx:hr"))
                    {
                        streamWriter.WriteLine(line);
                    }
                }
            }

            Console.WriteLine("\nAll done. Press any key to exit...");
            Console.ReadKey();
        }
    }
}

