using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Lab1___Neural_Network.Components
{
    class FileReader
    {
        /// <summary>
        /// Checks if a file at specified directory 'Exists'.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private static bool FileExist(string FilePath)
        {
            if (System.IO.File.Exists(FilePath))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reads a file, Assuming symetric column/row formated NN Traning/Test Data. 
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private static string[,] ReadFile(string FilePath)
        {
            if (FileExist(FilePath))
            {
                // Fetches information of Row/Columnsize to determine the size of Data array.
                int nOfRows = CountFileRows(FilePath);
                int nOfColumns = CountFileColumns(FilePath);
                string[,] fileData = new string[nOfRows, nOfColumns];
                // Create streamReader, Open file.
                System.IO.StreamReader file = new System.IO.StreamReader(FilePath);

                // Iterator to fill 'fileData' array with values from formatted textfile. 
                for (int i = 0; i < nOfRows; i++)
                {
                    // Splits a row into array elements.
                    string[] tmpData = file.ReadLine().Split(' ');
                   
                    for (int j = 0; j < nOfColumns; j++)
                    {
                        // Stores temporary created tmpData in fileData array. 
                        fileData[i, j] = tmpData[j];
                    }
                }
                
                // Important! Closing file after use. 
                file.Close();

                // Return complete array with data from file.
                return fileData;
            }
            // Returns 'null' if file doesnt exist. 
            return null;
        }

        /// <summary>
        /// Reads a file, and returns the number of 'Rows'.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private static int CountFileRows(string FilePath)
        {
            if (FileExist(FilePath))
            {
                return System.IO.File.ReadLines(FilePath).Count();
            }
            return 0;
        }

        /// <summary>
        /// Reads a file, and returns the number if 'Columns', Assuming symetric column/row formatted NN Training/Test Data.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private static int CountFileColumns(string FilePath)
        {
            if (FileExist(FilePath))
            {
                return System.IO.File.ReadLines(FilePath).First().Split(' ').Count();
            }
            return 0;
        }

        /// <summary>
        /// Checks if integer values in CriticalInputIndex is in range for use in 'CollectFileData' function.
        /// </summary>
        /// <param name="CriticalInputIndex"></param>
        /// <param name="FilePath"></param>
        /// <returns></returns>
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
        public static double[,] CollectFileData(string FilePath, int nOfInputs, bool CriticalInputs, int[] CriticalInputIndex) //TODO: check that double is enough to contain data.
        {
            if (FileExist(FilePath) && nOfInputs <= CountFileRows(FilePath) && CriticalInputIndex.Count() <= nOfInputs && CriticalIndexInRange(CriticalInputIndex, FilePath))
            {
                string[,] RawData = ReadFile(FilePath);
                int nOfDataSets = CountFileRows(FilePath);
                double[,] FileData = new double[nOfInputs, nOfDataSets];

                try
                {
                    if (CriticalInputs)
                    {
                        // Creates a FileData-array with a selected nuber of elements from RawData-array
                        for (int i = 0; i <= nOfDataSets; i++)
                        {
                            for (int j = 0; j <= nOfInputs; j++)
                            {
                                FileData[i, j] = Convert.ToDouble(RawData[i, CriticalInputIndex[j]]);
                            }
                        }
                    }
                    else
                    {
                        for (int i = 0; i < CountFileRows(FilePath); i++)
                        {
                            for (int j = 0; j < CountFileColumns(FilePath); i++)
                            {
                                FileData[i, j] = Convert.ToDouble(RawData[i, j]);
                            }
                        }

                    }
                }
                catch (FormatException)
                {
                    throw;
                }
                catch (OverflowException)
                {
                    throw;
                }
                catch (Exception)
                {
                    throw;
                }

                return FileData;
            }

            return null;
        }
    }
}
