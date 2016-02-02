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
                // Returns true if file exists. 
                return true;
            }
            return false;
        }

        /// <summary>
        /// Reads a file, Assuming symetric column/row formated NN Traning/Test Data. 
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private static string[,] ReadFileToArray(string FilePath)
        {
            if (FileExist(FilePath))
            {
                // Fetches information of Row/Column-size to determine the size of Data array.
                int nOfRows = CountFileRows(FilePath);
                int nOfColumns = CountFileColumns(FilePath);
                string[,] fileData = new string[nOfRows, nOfColumns]; 
                // Create streamReader, Open file.
                System.IO.StreamReader file = new System.IO.StreamReader(FilePath);

                // Iterator to fill 'fileData'-array with values from formatted textfile. 
                for (int y = 0; y < nOfRows; y++)
                {
                    // Splits a row into array elements.
                    string[] tmpData = file.ReadLine().Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                    for (int x = 0; x < nOfColumns; x++)
                    {
                        tmpData[x] = tmpData[x].Replace('.', ',');
                        // Stores temporary created tmpData in fileData array. 
                        fileData[y, x] = tmpData[x];
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
        /// Reads a file, and returns the number of 'Rows', assuming ' ' divides file data.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <returns></returns>
        private static int CountFileRows(string FilePath)
        {
            if (FileExist(FilePath))
            {
                // IF file Exists, returns the number of lines read. 
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
                // If file exists, returns the number of columns that exist in the first line of the specified file.
                return System.IO.File.ReadLines(FilePath).First().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Count();
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
            // Iterates through 'CriticalInputIndex'-array, making sure that the range of its values is within accepted values. 
            for (int i = 0; i < CriticalInputIndex.Count(); i++)
            {
                // Checking value, accepted value should be bigger than '0' and smaller than the number of columns in file.
                if (CriticalInputIndex[i] < 0 || CriticalInputIndex[i] > CountFileColumns(FilePath))
                {
                    // Returns false, if any value is bigger or smaller than specified range.
                    return false;
                }
            }
            // Returns true, if all values pass the range check. 
            return true;
        }

        /// <summary>
        /// Reads a file and stores it to the format used in Etch-depth prediction NN.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="nOfInputs"></param>
        /// <returns></returns>
        public static double[,] CollectInputFileData(string FilePath, int nOfInputs)
        {
            if (FileExist(FilePath) && nOfInputs <= CountFileRows(FilePath))
            {
                string[,] RawData = ReadFileToArray(FilePath);
                int nOfDataSets = CountFileRows(FilePath);
                double[,] FileData = new double[nOfDataSets, nOfInputs];

                try
                {
                    // Creates a FileData-array with all elements from RawData-array.
                    for (int y = 0; y < CountFileRows(FilePath); y++)
                    {
                        for (int x = 0; x < CountFileColumns(FilePath); x++)
                        {
                            FileData[y, x] = Convert.ToDouble(RawData[y, x]);
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

                // Returns formatted FileData-array.
                return FileData;
            }
            return null;
        }

        /// <summary>
        /// Reads a file and stores a selected number of elements to the format used in Etch-depth prediction NN.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="nOfInputs"></param>
        /// <param name="CriticalInputIndex"></param>
        /// <returns></returns>
        public static double[,] CollectInputFileData(string FilePath, int nOfInputs, int[] CriticalInputIndex)
        {
            if (FileExist(FilePath) && nOfInputs <= CountFileRows(FilePath) && CriticalInputIndex.Count() <= nOfInputs && CriticalIndexInRange(CriticalInputIndex, FilePath))
            {
                string[,] RawData = ReadFileToArray(FilePath);
                int nOfDataSets = CountFileRows(FilePath);
                double[,] FileData = new double[nOfDataSets, nOfInputs];

                try
                {
                    // Creates a FileData-array with a selected number of elements from RawData-array
                    for (int y = 0; y < nOfDataSets; y++)
                    {
                        for (int x = 0; x < nOfInputs; x++)
                        {
                            FileData[y, x] = Convert.ToDouble(RawData[y, CriticalInputIndex[x]].ToUpper());
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

                // Returns Formatted FileData-array.
                return FileData;
            }
            return null;
        }

        /// <summary>
        /// Reads a file and stores a selected number of output elements to the format used in Etch-depth prediction NN.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="nOfOutputs"></param>
        /// <param name="SelectedOutputIndex"></param>
        /// <returns></returns>
        public static double[,] CollectOutputFileData(string FilePath, int nOfOutputs, int[] SelectedOutputIndex)
        {
            // This function does the same as "CollectInputFileData", but with a proper name. 
            return CollectInputFileData(FilePath, nOfOutputs, SelectedOutputIndex);
        }  
    }
}