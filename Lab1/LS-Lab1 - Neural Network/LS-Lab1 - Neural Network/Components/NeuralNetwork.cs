using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            // Apply softOut to All hoSums and Store values to output-layer.
            this.output = ComputeSoftMax(hoSums);
            return this.output;

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

        public void Train(double[][] trainingData, int maxIterations, double maxAccuracy)
        {
            int iterator = 0;
            double accuracy = 0;
            double error = 0;

            // Initialize container arrays, and split training Data -> input/output
            double[][] trainingInput = new double[numInput][];
            double[][] trainingOutput = new double[numOutput][];
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

            while (iterator < maxIterations && accuracy < maxAccuracy)
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
                    AdjustWeights(output, trainingOutput[trainingSequence[i]]);
                }

                accuracy = ComputeAccuracy(trainingOutput, currentIterationOutputs);
                error = ComputeError(trainingOutput, currentIterationOutputs);

                FirePerformanceInfo(error, accuracy, iterator);
            }
            // Handle delegates. 
            if (iterator >= maxIterations && FireMaxIterationsReached != null) FireMaxIterationsReached(iterator, accuracy);
            if (accuracy >= maxAccuracy && FireMaxAccuracyReached != null) FireMaxAccuracyReached(accuracy, iterator);
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

        public delegate void TrainingPerformanceInfo(double Error, double Accuracy, int iterations);
        public delegate void TrainingMaxIterationsReached(int iterations, double accuracy);      
        public delegate void TrainingMaxAccuracyReached(double Accuracy, int iterations);

        public event TrainingPerformanceInfo FirePerformanceInfo;
        public event TrainingMaxIterationsReached FireMaxIterationsReached;
        public event TrainingMaxAccuracyReached FireMaxAccuracyReached;

        public void Test()
        {
            // Test Iteration
            // Return result
        }

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
        private double ComputeAccuracy(double[][] TargetOutput, double[][] NNOutput)
        {
            int numCorrect = 0;
            int numWrong = 0;

            for (int i = 0; i < TargetOutput.Length; i++)
            {
                int maxIndex = MaxIndex(NNOutput[i]);
                int tMaxIndex = MaxIndex(TargetOutput[i]);

                if (maxIndex == tMaxIndex)
                {
                    ++numCorrect;
                }
                else
                {
                    ++numWrong;
                }
            }
            return (numCorrect) / (numCorrect + numWrong);
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

    }
}
