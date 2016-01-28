using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Lab1___Neural_Network.Components
{
    class FileReader
    {
        public static bool FileExist(string FilePath)
        {
            if (System.IO.File.Exists(FilePath))
            {
                return true;
            }
            return false;
        }

        private static string[,] ReadFile(string FilePath)
        {
            if (FileExist(FilePath))
            {
                int nOfRows = CountFileRows(FilePath);
                int nOfColumns = CountFileColumns(FilePath);
                string[,] fileData = new string[nOfRows, nOfColumns];
                System.IO.StreamReader file = new System.IO.StreamReader(FilePath);

                for (int i = 0; i < nOfRows; i++)
                {
                    string[] tmpData = file.ReadLine().Split(' ');
                   
                    for (int j = 0; j < nOfColumns; j++)
                    {
                        fileData[i, j] = tmpData[j];
                    }
                }

                file.Close();

                return fileData;
            }
            return null;
        }

        private static int CountFileRows(string FilePath)
        {
            if (FileExist(FilePath))
            {
                return System.IO.File.ReadLines(FilePath).Count();
            }
            return 0;
        }

        private static int CountFileColumns(string FilePath)
        {
            if (FileExist(FilePath))
            {
                return System.IO.File.ReadLines(FilePath).First().Split(' ').Count();
            }
            return 0;
        }

        private static bool CriticalIndexInRange(int[] CriticalInputIndex, string FilePath)
        {
            for (int i = 0; i < CriticalInputIndex.Count(); i++)
            {
                if (CriticalInputIndex[i] < 0 || CriticalInputIndex[i] > CountFileColumns(FilePath))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Reads a file and stores it to the format used in Etch-depth prediction NN.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="nOfInputs"></param>
        /// <param name="CriticalInputs"></param>
        /// <param name="CriticalInputIndex"></param>
        /// <returns></returns>
        public static string[,] CollectFileData(string FilePath, int nOfInputs, bool CriticalInputs, int[] CriticalInputIndex)
        {
            if (FileExist(FilePath) && nOfInputs <= CountFileRows(FilePath) && CriticalInputIndex.Count() <= nOfInputs && CriticalIndexInRange(CriticalInputIndex, FilePath))
            {
                string[,] RawData = ReadFile(FilePath);
                int nOfDataSets = CountFileRows(FilePath);
                string[,] FileData = new string[nOfInputs, nOfDataSets];

                if (CriticalInputs)
                {
                    for (int i = 0; i <= nOfDataSets; i++)
                    {
                        for (int j = 0; j <= nOfInputs; j++)
                        {
                            FileData[i, j] = RawData[i, CriticalInputIndex[j]];
                        }
                    }
                }
                else
                {
                    FileData = RawData;
                }

                return FileData;
            }
            else
            {
                // Do nothing
                return null;
            }
        }
    }
}
