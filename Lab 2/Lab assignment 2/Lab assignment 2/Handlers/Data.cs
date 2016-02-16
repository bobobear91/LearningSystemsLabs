using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lab_assignment_2.Handlers
{
    public class Data
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
                // Returns true if the file exists.
                return true;
            }
            return false;
        }
        private static bool CheckFileEnding(string _filePath, string _fileEnd)
        {
            if (_filePath != null && _filePath.Count() >= 1)
            {
                if (_filePath.Substring(_filePath.Length - _fileEnd.Length, _fileEnd.Length) == _fileEnd)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        public static class XML
        {
            public static T DeserializeFromFile<T>(string _filePath) where T : class
            {
                if (!FileExist(_filePath))
                {
                    return null;
                }

                using (Stream stream = File.OpenRead(_filePath))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return serializer.Deserialize(stream) as T;
                }
            }
            public static void SerializeToFile<T>(T _obj, string _filename) where T : class
            {
                var serializer = new XmlSerializer(typeof(T));
                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");

                using (var writer = new StreamWriter(_filename))
                {
                    serializer.Serialize(writer, _obj, ns);
                }
            }
        }

        public static class TextFile
        {
            public static T[,] ReadFileToArray<T>(string _filePath)
            {
                if (!FileExist(_filePath))
                {
                    return null;
                }

                // Fetches information of Row/Column-size to determine the size of Data array.
                int nOfRows = CountFileRows(_filePath);
                int nOfColumns = CountFileColumns(_filePath);
                T[,] fileData = new T[nOfRows, nOfColumns];

                // Create streamReader, Open file.
                System.IO.StreamReader file = new System.IO.StreamReader(_filePath);

                // Iterator to fill 'fileData'-array with values from formatted textfile.
                for (int y = 0; y < nOfRows; y++)
                {
                    // Splits a row into array elements.
                    string[] tmpData = file.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int x = 0; x < nOfColumns; x++)
                    {
                        if (!IsNumericType(tmpData[x]))
                        {
                            tmpData[x] = tmpData[x].Replace('.', ',');
                            tmpData[x] = tmpData[x].ToUpper();
                        }
                        // Stores temporary created tmpData in fileData array.
                        fileData[y, x] = (T)Convert.ChangeType(tmpData[x], typeof(T));
                    }
                }

                // Important! Closing file after use.
                file.Close();

                // Return complete array with data from file.
                return fileData;
            }
            public static T[][] ReadFileToJaggedArray<T>(string _filePath)
            {
                if (!FileExist(_filePath))
                {
                    return null;
                }

                // Fetches information of Row/Column-size to determine the size of Data array.
                int nOfRows = CountFileRows(_filePath);
                int nOfColumns = CountFileColumns(_filePath);
                T[][] fileData = new T[nOfRows][];
                for (int y = 0; y < nOfRows; y++)
                {
                    fileData[y] = new T[nOfColumns];
                }

                // Create streamReader, Open file.
                System.IO.StreamReader file = new System.IO.StreamReader(_filePath);

                // Iterator to fill 'fileData'-array with values from formatted textfile.
                for (int y = 0; y < nOfRows; y++)
                {
                    // Splits a row into array elements.
                    string[] tmpData = file.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    for (int x = 0; x < nOfColumns; x++)
                    {
                        if (!IsNumericType(tmpData[x]))
                        {
                            tmpData[x] = tmpData[x].Replace('.', ',');
                            tmpData[x] = tmpData[x].ToUpper();
                        }
                        // Stores temporary created tmpData in fileData array.
                        fileData[y][x] = (T)Convert.ChangeType(tmpData[x], typeof(T));
                    }
                }

                // Important! Closing file after use.
                file.Close();

                // Return complete array with data from file.
                return fileData;
            }

            public static T[,] ReadFilePartToArray<T>(string _filePath, int _nOfElements, int[] _criticalIndices)
            {
                if (!FileExist(_filePath) ||
                    _nOfElements > CountFileRows(_filePath) ||
                    _criticalIndices.Count() > _nOfElements ||
                    !CriticalIndexInRange(_criticalIndices, _filePath))
                {
                    return null;
                }

                T[,] RawData = ReadFileToArray<T>(_filePath);
                int nOfDataSets = CountFileRows(_filePath);
                T[,] FileData = new T[nOfDataSets, _nOfElements];

                try
                {
                    // Creates a FileData-array with a selected number of elements from RawData-array
                    for (int y = 0; y < nOfDataSets; y++)
                    {
                        for (int x = 0; x < _nOfElements; x++)
                        {
                            FileData[y, x] = RawData[y, _criticalIndices[x]];
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
            public static T[][] ReadFilePartToJaggedArray<T>(string _filePath, int _nOfElements, int[] _criticalIndices)
            {
                if (!FileExist(_filePath) ||
                    _nOfElements > CountFileRows(_filePath) ||
                    _criticalIndices.Count() > _nOfElements ||
                    !CriticalIndexInRange(_criticalIndices, _filePath))
                {
                    return null;
                }

                T[][] RawData = ReadFileToJaggedArray<T>(_filePath);
                int nOfDataSets = CountFileRows(_filePath);
                T[][] FileData = new T[nOfDataSets][];
                for (int y = 0; y < nOfDataSets; y++)
                {
                    FileData[y] = new T[_nOfElements];
                }

                try
                {
                    // Creates a FileData-array with a selected number of elements from RawData-array
                    for (int y = 0; y < nOfDataSets; y++)
                    {
                        for (int x = 0; x < _nOfElements; x++)
                        {
                            FileData[y][x] = RawData[y][_criticalIndices[x]];
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

            public static void WriteArrayToFile<T>(string _filePath, T[,] _array, bool _overwrite)
            {
                string[] lines = new string[_array.GetLength(0)];
                for (int y = 0; y < _array.GetLength(0); y++)
                {
                    string line = "";
                    for (int x = 0; x < _array.GetLength(1); x++)
                    {
                        line += Convert.ChangeType(_array[y, x], typeof(string));
                    }
                    lines[y] = line;
                }

                if (!FileExist(_filePath) || _overwrite)
                {
                    if (!CheckFileEnding(_filePath, ".txt"))
                    {
                        _filePath += ".txt";
                    }

                    System.IO.File.WriteAllLines(_filePath, lines);
                }
            }
            public static void WriteJaggedArrayToFile<T>(string _filePath, T[][] _array, bool _overwrite)
            {
                string[] lines = new string[_array.Length];
                for (int y = 0; y < _array.Length; y++)
                {
                    string line = "";
                    for (int x = 0; x < _array[0].Length; x++)
                    {
                        line += Convert.ChangeType(_array[y][x], typeof(string));
                    }
                    lines[y] = line;
                }

                if (!FileExist(_filePath) || _overwrite)
                {
                    if (!CheckFileEnding(_filePath, ".txt"))
                    {
                        _filePath += ".txt";
                    }

                    System.IO.File.WriteAllLines(_filePath, lines);
                }
            }

            #region Private TexFile-functions
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

            private static bool IsNumericType(object o)
            {
                switch (Type.GetTypeCode(o.GetType()))
                {
                    case TypeCode.Byte:
                    case TypeCode.SByte:
                    case TypeCode.UInt16:
                    case TypeCode.UInt32:
                    case TypeCode.UInt64:
                    case TypeCode.Int16:
                    case TypeCode.Int32:
                    case TypeCode.Int64:
                    case TypeCode.Decimal:
                    case TypeCode.Double:
                    case TypeCode.Single:
                        return true;
                    default:
                        return false;
                }
            }
            #endregion
        }
    }
}
