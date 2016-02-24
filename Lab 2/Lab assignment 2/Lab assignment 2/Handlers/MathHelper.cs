namespace Lab_assignment_2.Handlers
{
    class MathHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private double[][] NormilizeData(double[][] data)
        {
            // Compute the max and min value for each column in 'data'.
            double[] maxValues = new double[data[0].Length];
            double[] minValues = new double[data[0].Length];

            for (int x = 0; x < data[0].Length; x++)
            {
                double maxValue = double.MinValue;
                double minValue = double.MaxValue;
                for (int y = 0; y < data.Length; y++)
                {
                    if (data[y][x] > maxValue) maxValue = data[y][x];
                    if (data[y][x] < minValue) minValue = data[y][x];
                }

                maxValues[x] = maxValue;
                minValues[x] = minValue;
            }

            // Compute the normalized values for all data.
            double[][] normalizedData = new double[data.Length][];

            for (int y = 0; y < data.Length; y++)
            {
                // Initialize inner arrays on the fly.
                normalizedData[y] = new double[data[0].Length];

                for (int x = 0; x < data[0].Length; x++)
                {
                    double a = data[y][x] - minValues[x];
                    double b = maxValues[x] - minValues[x];
                    normalizedData[y][x] = a / b;
                }
            }
            return normalizedData;
        }
    }
}
