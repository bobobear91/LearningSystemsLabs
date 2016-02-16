using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace LS_Lab1___Neural_Network.Components
{
    class NeuralNetwork
    {
        // Configuration
        int numInput;
        int numHidden;
        int numOutput;
        // Back-Propagation Configuration
        double learnRate;
        double momentum;

        // Input/Output - layers
        private double[] input;
        private double[] hOutput;
        private double[] output;

        // NN - Weights
        private double[][] ihWeights;
        private double[] hBiases;
        private double[][] hoWeights;
        private double[] oBiases;

        // Back-Propagation momentum arrays
        double[][] ihPrevWeightsDelta;
        double[] hPrevBiasesDelta;
        double[][] hoPrevWeightsDelta;
        double[] oPrevBiasesDelta;

        // state stuff. 
        bool stopTraining;
        bool stopTesting;

        private Random rnd; 

        public int NumberOfInputs
        {
            get { return numInput; }
        }
        public int NumberOfHidden
        {
            get { return numHidden; }
        }
        public int NumberOfOutputs
        {
            get { return numOutput; }
        }
        public double LearningRate
        {
            get { return learnRate; }
            set { learnRate = value; }
        }
        public double Momentum
        {
            get { return momentum; }
            set { momentum = value; }
        }
        public double[] Weights
        {
            get
            {
                // Calculate the total amount of Weights in ANN.
                int numberOfWeights = (numInput * numHidden) + (numHidden * numOutput) + numHidden + numOutput;
                double[] compressedWeights = new double[numberOfWeights];
                int iterator = 0;

                // Get all input->hidden Weights.
                for (int y = 0; y < ihWeights.Length; y++)
                {
                    for (int x = 0; x < ihWeights[0].Length; x++)
                    {
                        compressedWeights[iterator++] = ihWeights[y][x]; 
                    }
                }

                // Get all hidden-Biases.
                for (int x = 0; x < hBiases.Length; x++)
                {
                    compressedWeights[iterator++] = hBiases[x];
                }

                // Get all hidden->output Weights.
                for (int y = 0; y < hoWeights.Length; y++)
                {
                    for (int x = 0; x < hoWeights[0].Length; x++)
                    {
                        compressedWeights[iterator++] = hoWeights[y][x];
                    }
                }

                // Get all output-Biases.
                for (int x = 0; x < oBiases.Length; x++)
                {
                    compressedWeights[iterator++] = oBiases[x];
                }
                return compressedWeights;
            }
            set
            {
                // Calculate the number of expected Weights.
                int numberOfWeights = (numInput * numHidden) + (numHidden * numOutput) + numHidden + numOutput;

                if (value.Length != numberOfWeights)
                {
                    throw new Exception("SetWeights - weight number mismatch!");
                }

                int iterator = 0;

                // Set all input->hidden Weights.
                for (int y = 0; y < numInput; y++)
                {
                    for (int x = 0; x < numHidden; x++)
                    {
                        ihWeights[y][x] = value[iterator++];
                    }
                }

                // Set all hidden-Biases.
                for (int x = 0; x < numHidden; x++)
                {
                    hBiases[x] = value[iterator++];
                }

                // Set all hidden->output Weights.
                for (int y = 0; y < numHidden; y++)
                {
                    for (int x = 0; x < numOutput; x++)
                    {
                        hoWeights[y][x] = value[iterator++];
                    }
                }

                // Set all output-Biases.
                for (int x = 0; x < numOutput; x++)
                {
                    oBiases[x] = value[iterator++];
                }
            }
        }

        public NeuralNetwork(int numInput, int numHidden, int numOutput, double learnRate, double momentum)
        {
            // Initialize ANN Configuration.
            this.numInput = numInput;
            this.numHidden = numHidden;
            this.numOutput = numOutput;
            this.learnRate = learnRate;
            this.momentum = momentum;

            // Initialize Layers.
            this.input = new double[numInput];
            this.hOutput = new double[numHidden];
            this.output = new double[numOutput];

            // Initialize random generator.
            this.rnd = new Random(0);

            // Initialize Weights.
            this.ihWeights = MakeMatrix(numInput, numHidden, 0.0);
            this.hBiases = new double[numHidden];
            this.hoWeights = MakeMatrix(numHidden, numOutput, 0.0);
            this.oBiases = new double[numOutput];
            InitializeWeights();

            // Initialize Back-Propagation 'Weight' momentum arrays.
            ihPrevWeightsDelta = MakeMatrix(numInput, numHidden, 0.0);
            hPrevBiasesDelta = new double[numHidden];
            hoPrevWeightsDelta = MakeMatrix(numHidden, numOutput, 0.0);
            oPrevBiasesDelta = new double[numOutput];
        }

        private double[][] MakeMatrix(int yDimension, int xDimension, double defaultValue)
        {
            double[][] matrix = new double[yDimension][];
            for (int y = 0; y < yDimension; y++)
            {
                matrix[y] = new double[xDimension];
            }

            for (int y = 0; y < yDimension; y++)
            {
                for (int x = 0; x < xDimension; x++)
                {
                    matrix[y][x] = defaultValue;
                }
            }
            return matrix;
        }
        private void InitializeWeights()
        {
            int numberOfWeights = (numInput * numHidden) + (numHidden * numOutput) + numHidden + numOutput;
            double[] initialWeights = new double[numberOfWeights];
            for (int i = 0; i < numberOfWeights; i++)
            {
                initialWeights[i] = (0.001 - 0.0001) * rnd.NextDouble() + 0.0001;
            }
            Weights = initialWeights;
        }
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

        private double[] ComputeOutput(double[] TrainingDataInput)
        {
            double[] ihSums = new double[numHidden];
            double[] hoSums = new double[numOutput];

            // Store Input Data to input-layer
            for (int x = 0; x < numInput; x++)
            {
                this.input[x] = TrainingDataInput[x];
            }

            // Apply ihWeights as a factor to input. 
            for (int x = 0; x < numHidden; x++)
            {
                for (int y = 0; y < numInput; y++)
                {
                    ihSums[x] += this.input[y] * this.ihWeights[y][x];
                }
            }

            // Apply hBiases as term to ihSums.
            for (int x = 0; x < numHidden; x++)
            {
                ihSums[x] += this.hBiases[x];
            }

            // Apply HyperTan to ihSums and Store values to hidden-layer.
            for (int x = 0; x < numHidden; x++)
            {
                this.hOutput[x] = HyperTan(ihSums[x]);
            }

            // Apply hoWeights as a factor to hOutput. 
            for (int x = 0; x < numOutput; x++)
            {
                for (int y = 0; y < numHidden; y++)
                {
                    hoSums[x] += hOutput[y] * hoWeights[y][x];
                }
            }

            // Apply oBiases as term to hoSums.
            for (int x = 0; x < numOutput; x++)
            {
                hoSums[x] += oBiases[x];
            }

            //this.output = ComputeSoftMax(hoSums);

            for (int o = 0; o < numOutput; o++)
            {
                this.output[o] = LogSigmoid(hoSums[o]);
            }

            double[] result = new double[numOutput];
            Array.Copy(this.output, result, numOutput);

            return result;
        }
        private double HyperTan(double x)
        {
            if (x < -20.0) return -1.0;
            else if (x > 20.0) return 1.0;
            else return Math.Tanh(x);
        }
        private double[] ComputeSoftMax(double[] oSums)
        {
            double sum = 0.0;
            for (int i = 0; i < oSums.Length; i++)
            {
                sum += Math.Exp(oSums[i]);
            }

            double[] result = new double[oSums.Length];
            for (int i = 0; i < oSums.Length; i++)
            {
                result[i] = Math.Exp(oSums[i]) / sum;
            }
            return result;
        }
        private double SoftMax(double x)
        {
            double max = double.MinValue;
            max = x;

            double scale = 0.0;
            scale = Math.Exp(x - max);

            return Math.Exp(x - max) / scale;
        }
        public double LogSigmoid(double x)
        {
            if (x < -45.0) return 0.0;
            else if (x > 45.0) return 1.0;
            else return 1.0 / (1.0 + Math.Exp(-x));
        }

        public void Train(double[][] trainingData, int maxIterations, double maxAccuracy, int PerformanceEventInterval = 10, int AccuracyFilter = 15)
        {
            stopTraining = false;
            int iterator = 0;
            double accuracy = 0;
            double error = 0;

            // Normalize all trainingData.
            trainingData = NormilizeData(trainingData);

            // Initialize container arrays, and split training Data -> input/output
            double[][] trainingInput = new double[trainingData.Length][];
            double[][] trainingOutput = new double[trainingData.Length][];
            for (int y = 0; y < trainingData.Length; y++)
            {
                trainingInput[y] = new double[numInput];
                trainingOutput[y] = new double[numOutput];

                Array.Copy(trainingData[y], trainingInput[y], numInput);
                Array.Copy(trainingData[y], numInput - 1, trainingOutput[y], 0, numOutput);
            }

            // Initialize sequence
            int[] trainingSequence = new int[trainingData.Length];
            for (int x = 0; x < trainingData.Length; x++)
            {
               trainingSequence[x] = x;
            }

            while (iterator < maxIterations && accuracy < maxAccuracy && !stopTraining)
            {
                // Initialize container for iteration output.
                double[][] currentIterationOutputs = new double[trainingData.Length][];
                for (int y = 0; y < numOutput; y++)
                {
                    currentIterationOutputs[y] = new double[numOutput];
                }

                // Shuffle Sequence
                trainingSequence = ShuffleSequence(trainingSequence);

                // Compute all training examples.
                for (int i = 0; i < trainingData.Length; i++)
                {
                    currentIterationOutputs[trainingSequence[i]] = ComputeOutput(trainingInput[trainingSequence[i]]);
                    // if NN_answer != correct_answer the adjust weights
                    if (currentIterationOutputs[trainingSequence[i]] != trainingOutput[trainingSequence[i]])
                    {
                        AdjustWeights(output, trainingOutput[trainingSequence[i]]);
                    }

                    if (i == trainingData.Length-1)
                    {
                        // Fire output comparison event.
                        if (FireOutputComparison != null) FireOutputComparison(iterator, currentIterationOutputs[trainingSequence[i]][0], trainingOutput[trainingSequence[i]][0]);
                    }
                }

                // makes sure that performance event isnt fired at every iteration. 
                if (iterator % PerformanceEventInterval == 0)
                {
                    // Compute Accuracy and error value at end of iteration
                    accuracy = ComputeAccuracy(trainingOutput, currentIterationOutputs, AccuracyFilter);
                    error = ComputeError(trainingOutput, currentIterationOutputs);
                    //if (FireOutputComparison != null) FireOutputComparison(iterator, error, accuracy);
                }

                // Fire performance info event. 
                if (FirePerformanceInfo != null) FirePerformanceInfo(error, accuracy, iterator);

                iterator++;
            }
            // Handle delegates. 
            if (iterator >= maxIterations && FireMaxIterationsReached != null) FireMaxIterationsReached(iterator, accuracy);
            if (accuracy >= maxAccuracy && FireMaxAccuracyReached != null) FireMaxAccuracyReached(accuracy, iterator);
            if (FireTrainingComplete != null) FireTrainingComplete();
        }
        private void AdjustWeights(double[] NNOutput, double[] targetOutput)
        {
            // Back-Propagation specific arrays.
            double[][] ihGrads = MakeMatrix(numInput, numHidden, 0.0);  // input->hidden weight gradients
            double[] hBiasGrads = new double[numHidden];                // hidden-bias gradients

            double[][] hoGrads = MakeMatrix(numHidden, numOutput, 0.0); // hidden->output weight gradients
            double[] oBiasGrads = new double[numOutput];                // output-bias gradients

            double[] oSignals = new double[numOutput];                  // local gradient output signals - gradients w/o associated input terms
            double[] hSignals = new double[numHidden];                  // local gradient hidden node signals

            double derivative = 0.0;
            double errorsignal = 0.0;

            // indices: i = inputs, h = hiddens, o = outputs
            // 1. compute output node signals (assumes softmax as activation)
            for (int o = 0; o < numOutput; o++)                 
            {
                errorsignal = targetOutput[o] - NNOutput[o];    // equation 5.
                derivative = (1 - NNOutput[o]) * NNOutput[o];   // equation 2.
                oSignals[o] = errorsignal * derivative;         // equation 6.
            }

            // 2. compute hidden->output weight gradients using output signals
            for (int h = 0; h < numHidden; h++)
            {
                for (int o = 0; o < numOutput; o++)
                {
                    hoGrads[h][o] = oSignals[o] * hOutput[h];   // Part of equation 7.
                }
            }

            // 2b. compute output bias gradients using output signals
            for (int o = 0; o < numOutput; o++)
            {
                oBiasGrads[o] = oSignals[o] * 1.0;              // Part of equation 7.
            }

            // 3. compute hidden node signals
            for (int h = 0; h < numHidden; h++)                 // Part of equation 7.
            {
                derivative = (1 + hOutput[h]) * (1 - hOutput[h]);   //tanh
                double sum = 0.0;                                   // sum of oSignals * hidden->output weights
                for (int o = 0; o < numOutput; o++)
                {
                    sum += oSignals[o] * hoWeights[h][o];           // represent error signal for hidden layer
                }
                hSignals[h] = derivative * sum;
            }

            // 4. compute input->hidden weight gradients
            for (int i = 0; i < numInput; i++)
            {
                for (int h = 0; h < numHidden; h++)
                {
                    ihGrads[i][h] = hSignals[h] * input[i];
                }
            }

            // 4b. compute hidden node vias gradients
            for (int h = 0; h < numHidden; h++)
            {
                hBiasGrads[h] = hSignals[h] * 1.0;  // dummy 1.0 input
            }

            // Update Weights and Biases ------------------------------------------------------------------------

            // update input->hidden weights
            for (int i = 0; i < numInput; i++)
            {
                for (int h = 0;  h < numHidden; h++)
                {
                    double delta = ihGrads[i][h] * learnRate;               // equation 8.
                    ihWeights[i][h] += delta;                               // part of euqation 9.
                    ihWeights[i][h] += ihPrevWeightsDelta[i][h] * momentum; // part of equation 9.
                    ihPrevWeightsDelta[i][h] = delta;
                }
            }

            // update hidden biases
            for (int h = 0; h < numHidden; h++)
            {
                double delta = hBiasGrads[h] * learnRate;
                hBiases[h] += delta;
                hBiases[h] += hPrevBiasesDelta[h] * momentum;
                hPrevBiasesDelta[h] = delta;
            }

            // update hidden->output weights
            for (int h = 0; h < numHidden; h++)
            {
                for (int o = 0; o < numOutput; o++)
                {
                    double delta = hoGrads[h][o] * learnRate;
                    hoWeights[h][o] += delta;
                    hoWeights[h][o] += hoPrevWeightsDelta[h][o] * momentum;
                    hoPrevWeightsDelta[h][o] = delta;
                }
            }

            // compute output node biases
            for (int o = 0; o < numOutput; o++)
            {
                double delta = oBiasGrads[o] * learnRate;
                oBiases[o] += delta;
                oBiases[o] += oPrevBiasesDelta[o] * momentum;
                oPrevBiasesDelta[o] = delta;
            }
        }
        private int[] ShuffleSequence(int[] sequence)
        {
            for (int x = 0; x < sequence.Length; x++)
            {
                int index = this.rnd.Next(x, sequence.Length);
                int tmp = sequence[index];
                sequence[index] = sequence[x];
                sequence[x] = tmp;
            }

            return sequence;
        }
        public void StopTraining()
        {
            stopTraining = true;
            stopTesting = true;
        }
        #region Training delegates
        public delegate void TrainingPerformanceInfo(double Error, double Accuracy, int iterations);
        public delegate void TrainingMaxIterationsReached(int iterations, double accuracy);      
        public delegate void TrainingMaxAccuracyReached(double Accuracy, int iterations);
        public delegate void TrainingComplete();
        public delegate void OutputComparison(int iterator, double NN_Output, double t_Output);

        public event TrainingPerformanceInfo FirePerformanceInfo;
        public event TrainingMaxIterationsReached FireMaxIterationsReached;
        public event TrainingMaxAccuracyReached FireMaxAccuracyReached;
        public event TrainingComplete FireTrainingComplete;
        public event OutputComparison FireOutputComparison;
        #endregion

        public void Test(double[][] TestData, int AccuracyFilter = 15)
        {
            stopTesting = false;
            // Normalize all testdata.
            TestData = NormilizeData(TestData);
            // Initialize container arrays, and split training Data -> input/output
            double[][] TestInput = new double[TestData.Length][];
            double[][] TestOutput = new double[TestData.Length][];
            for (int y = 0; y < TestData.Length; y++)
            {
                TestInput[y] = new double[numInput];
                TestOutput[y] = new double[numOutput];

                Array.Copy(TestData[y], TestInput[y], numInput);
                Array.Copy(TestData[y], numInput - 1, TestOutput[y], 0, numOutput);
            }

            // Initialize container for iteration output.
            double[][] currentIterationOutputs = new double[TestData.Length][];
            for (int y = 0; y < TestData.Length; y++)
            {
                currentIterationOutputs[y] = new double[numOutput];
            }

            // Compute all testing examples.
            for (int i = 0; i < TestData.Length; i++)
            {
                currentIterationOutputs[i] = ComputeOutput(TestInput[i]);
                AdjustWeights(currentIterationOutputs[i], TestOutput[i]);

                if (FireOutputComparison != null) FireOutputComparison(i, currentIterationOutputs[i][0], TestOutput[i][0]);

                if (stopTesting)
                {
                    return;
                }
                Thread.Sleep(50);

            }

            double accuracy = ComputeAccuracy(TestOutput, currentIterationOutputs, AccuracyFilter);
            double error = ComputeError(TestOutput, currentIterationOutputs);

            if (FireTestingResultInfo != null) FireTestingResultInfo(accuracy, error);
            if (FirePerformanceInfo != null) FirePerformanceInfo(error, accuracy, 0);
            if (FireTestingComplete != null) FireTestingComplete();
        }
        #region Test delegates
        public delegate void TestingComplete();
        public delegate void TestingResultInfo(double accuracy, double error);

        public event TestingComplete FireTestingComplete;
        public event TestingResultInfo FireTestingResultInfo;
        #endregion

        private double ComputeError(double[][] TargetOutput, double[][] NNOutput)
        {
            double sumSquaredError = 0.0;

            for (int i = 0; i < TargetOutput.Length; i++)
            {
                for (int j = 0; j < numOutput; j++)
                {
                    double error = TargetOutput[i][j] - NNOutput[i][j];
                    sumSquaredError += error * error;
                }
            }
            return sumSquaredError / TargetOutput.Length;
        }
        private double ComputeAccuracy(double[][] TargetOutput, double[][] NNOutput, int nOfDecimals)
        {
            int numCorrect = 0;
            int numWrong = 0;
            bool answerCorrect = false;

            for (int y = 0; y < TargetOutput.Length; y++)
            {
                answerCorrect = true;
                for (int x = 0; x < TargetOutput[0].Length; x++)
                {
                    double nn_answer = Math.Round(NNOutput[y][x], nOfDecimals);
                    double t_answer = Math.Round(TargetOutput[y][x], nOfDecimals);

                    if (nn_answer != t_answer)
                    {
                        answerCorrect = false;
                        break;
                    }
                }
                if (answerCorrect)
                {
                    ++numCorrect;
                }
                else
                {
                    ++numWrong;
                }
            }
            double result = (double)(numCorrect) / ((double)numCorrect + numWrong);
            return result;
        }
        private static int MaxIndex(double[] vector)
        {
            int bigIndex = 0;
            double biggestValue = vector[0];
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] > biggestValue)
                {
                    biggestValue = vector[i];
                    bigIndex = i;
                }
            }
            return bigIndex;
        }

        public void SaveNN(string filePath)
        {
            // Create a Instance of our container class NN_Data
            NN_Data save_data = new NN_Data();
            // And store all configuration-values from NN
            save_data.NumInput = this.numInput;
            save_data.NumHidden = this.numHidden;
            save_data.NumOutput = this.numOutput;
            save_data.LearnRate = this.learnRate;
            save_data.Momentum = this.momentum;
            save_data.Weights = Weights;

            // Send obj to XML.Serialize and it will create a XML file described by NN_Data.
            Data.XML.SerializeToFile<NN_Data>(save_data, filePath);
        }
        public void LoadNN(string filePath)
        {
            // Setup a container instance of NN_Data and fetch the file
            NN_Data load_data = Data.XML.DeserializeFromFile<NN_Data>(filePath);

            // Extract the values of the container. 
            this.numInput = load_data.NumInput;
            this.numHidden = load_data.NumHidden;
            this.numOutput = load_data.NumOutput;
            this.learnRate = load_data.LearnRate;
            this.momentum = load_data.Momentum;

            // Initialize Weights.
            this.ihWeights = MakeMatrix(numInput, numHidden, 0.0);
            this.hBiases = new double[numHidden];
            this.hoWeights = MakeMatrix(numHidden, numOutput, 0.0);
            this.oBiases = new double[numOutput];
            Weights = load_data.Weights;

            // Initialize Layers.
            this.input = new double[numInput];
            this.hOutput = new double[numHidden];
            this.output = new double[numOutput];

            // Initialize random generator.
            this.rnd = new Random(0);

            // Initialize Back-Propagation 'Weight' momentum arrays.
            ihPrevWeightsDelta = MakeMatrix(numInput, numHidden, 0.0);
            hPrevBiasesDelta = new double[numHidden];
            hoPrevWeightsDelta = MakeMatrix(numHidden, numOutput, 0.0);
            oPrevBiasesDelta = new double[numOutput];
        }
    }

    [Serializable]
    public class NN_Data
    {
        [XmlElement("NumInput")]
        public int NumInput;
        [XmlElement("NumHidden")]
        public int NumHidden;
        [XmlElement("NumOutput")]
        public int NumOutput;
        [XmlElement("LearnRate")]
        public double LearnRate;
        [XmlElement("Momentum")]
        public double Momentum;
        [XmlElement("Weights")]
        public double[] Weights;
    }
}

