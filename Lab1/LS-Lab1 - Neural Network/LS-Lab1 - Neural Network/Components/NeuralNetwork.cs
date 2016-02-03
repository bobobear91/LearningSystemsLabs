using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LS_Lab1___Neural_Network.Components
{
    class NeuralNetwork
    {
        //https://visualstudiomagazine.com/Articles/2015/04/01/Back-Propagation-Using-C.aspx?Page=1

        //Variables
        int numInput;
        int numHidden;
        int numOutput;

        private double[] inputs;
        private double[][]ihWeights;
        private double[] hBiases;
        private double[] hOutputs;

        private double[][] hoWeights;
        private double[] oBiases;
        private double[] outputs;

        private Random rnd;

        public int NumberInputs
        {
            get { return numInput; }
        }

        public int NumberHidden
        {
            get { return numHidden; }
        }

        public int NumberOutput
        {
            get
            {
                return numOutput;
            }
        }

        public NeuralNetwork(int numInput, int numHidden, int numOutput)
        {
            // Initialize ANN configuration
            this.numInput = numInput;
            this.numHidden = numHidden;
            this.numOutput = numOutput;

            // Initialize input layer
            this.inputs = new double[numInput];

            // Initialize input->hidden layer + bias
            this.ihWeights = MakeMatrix(numInput, numHidden, 0.0);
            this.hBiases = new double[numHidden];
            this.hOutputs = new double[numHidden];

            // Initialize hidden->output layer + bias
            this.hoWeights = MakeMatrix(numHidden, numOutput, 0.0);
            this.oBiases = new double[numOutput];
            this.outputs = new double[numOutput];

            // Init, random gen. 
            this.rnd = new Random(0); // TODO: Add seed? 
            // Setup initial weights for ANN, will initialize all weights and biases.
            InitializeWeights();
        }

        private static double[][] MakeMatrix(int rows, int columns, double value)
        {
            // Initialize rows of jagged array (y)
            double[][] result = new double[rows][];

            // Initialize columns of jagged array (x)
            for (int y = 0; y < rows; y++)
            {
                result[y] = new double[columns];
            }

            // Initialize jagged array with default values.
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    result[y][x] = value;
                }
            }

            return result;
        }

        private void InitializeWeights()
        {
            // Calculate the total number of weights in ANN
            int numWeights = (numInput * numHidden) + (numHidden * numOutput) + numHidden + numOutput;
            double[] initialWeights = new double[numWeights];

            // Initialize weights with random values. 
            for (int i = 0; i < initialWeights.Length; i++)
            {
                initialWeights[i] = (0.001 - 0.0001) * rnd.NextDouble() + 0.0001; //TODO: avoid arbitrary constants? 
            }
        }

        public void SetWeights(double[] weights)
        {
            // Calculate number of expected weights with current ANN configuration
            int numWeights = (numInput * numHidden) + (numHidden * numOutput) + numHidden + numOutput;
            // If, calculated number of weights doesnt match the length of input array -> throw exception. 
            if (weights.Length != numWeights)
            {
                throw new Exception("SetWeights, weight number mismatch!");
            }

            // int k, for iterating through weights[]
            int k = 0;

            // Set input->hidden weights.
            for (int y = 0; y < numInput; y++)
            {
                for (int x = 0; x < numHidden; x++)
                {
                    ihWeights[y][x] = weights[k++];
                }
            }
            // Set hiddenBiases weights
            for (int x = 0; x < numHidden; x++)
            {
                hBiases[x] = weights[k++];
            }
            // Set hidden->output weights.
            for (int y = 0; y < numHidden; y++)
            {
                for (int x = 0; x < numOutput; x++)
                {
                    hoWeights[y][x] = weights[k++];
                }
            }
            // Set outputBiases wieghts.
            for (int x = 0; x < numOutput; x++)
            {
                oBiases[x] = weights[k++];
            }
        }
        public double[] GetWeights()
        {
            // Calculate number of weights for ANN.
            int numWeights = (numInput * numHidden) + (numHidden * numOutput) + numHidden + numOutput;
            double[] result = new double[numWeights];

            // int k is for iterating through result array.
            int k = 0;
            // Get all inpute->hidden weights.
            for (int y = 0; y < ihWeights.Length; y++)
            {
                for (int x = 0; x < ihWeights[0].Length; x++)
                {
                    result[k++] = ihWeights[y][x];
                }
            }
            // Get all hiddenBiases.
            for (int x = 0; x < hBiases.Length; x++)
            {
                result[k++] = hBiases[x];
            }
            // Get all hidden->output weights.
            for (int y = 0; y < hoWeights.Length; y++)
            {
                for (int x = 0; x < hoWeights[0].Length; x++)
                {
                    result[k++] = hoWeights[y][x];
                }
            }
            // Get all outputBiases.
            for (int x = 0; x < oBiases.Length; x++)
            {
                result[k++] = oBiases[x];
            }
            return result;
        }

        public double[] ComputeOutputs(double[] inputValues)
        {
            double[] hSums = new double[numHidden];
            double[] oSums = new double[numOutput];

            // Pass the input values to the input layer
            for (int x = 0; x < inputValues.Length; x++)
            {
                this.inputs[x] = inputValues[x];
            }

            // Apply the weights of input->hidden as a factor and pass the values to hidden layer.
            for (int y = 0; y < numHidden; y++)
            {
                for (int x = 0; x < numInput; x++)
                {
                    hSums[y] += this.inputs[x] * this.ihWeights[y][x];
                }
            }

            // Add the hiddenBias to the hidden layer sums. 
            for (int x = 0; x < numHidden; x++)
            {
                hSums[x] += this.hBiases[x];
            }

            // Apply the HyperTan to hsums and pass the values to hidden->output layer
            for (int x = 0; x < numHidden; x++)
            {
                this.hOutputs[x] = HyperTan(hSums[x]); // hard coded
            }

            // Apply the weights of hidden->output as a factor and pass the values to output layer.
            for (int y = 0; y < numOutput; y++)
            {
                for (int x = 0; x < numHidden; x++)
                {
                    oSums[y] += hOutputs[x] * hoWeights[y][x];
                }
            }

            // Add the outputBias to the output layer sums.
            for (int x = 0; x < numOutput; x++)
            {
                oSums[x] += oBiases[x];
            }

            double[] softOut = SoftMax(oSums);
            Array.Copy(softOut, outputs, softOut.Length);

            double[] retResult = new double[numOutput];
            Array.Copy(this.outputs, retResult, retResult.Length);
            return retResult;
        }
        public static double HyperTan(double x)
        {
            if (x < -20.0) return -1.0;
            else if (x > 20.0) return 1.0;
            else return Math.Tanh(x);
        }
        public static double[] SoftMax(double[] oSums)
        {
            // Somewhat hardcoded. 
            // Does all output nodes at once instead of individually. 
            // This means we dont have to re-compute scale each time.

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

        public double[] Train(double[][] TrainingData, int maxEpochs, double learnRate, double momentum)
        {
            // train using back-prop
            // back-prop specific arrays
            double[][] hoGrads = MakeMatrix(numHidden, numOutput, 0.0); // hidden-to-output weight gradients
            double[] obGrads = new double[numOutput];                   // output bias gradients

            double[][] ihGrads = MakeMatrix(numInput, numHidden, 0.0);  // input-to-hidden weight gradients
            double[] hbGrads = new double[numHidden];                   // hidden bias gradients

            double[] oSignals = new double[numOutput];                  // local gradient output signals - gradients w/o associated input terms
            double[] hSignals = new double[numHidden];                  // local gradient hidden node signals

            // back-prop momentum specific arrays 
            double[][] ihPrevWeightsDelta = MakeMatrix(numInput, numHidden, 0.0);
            double[] hPrevBiasesDelta = new double[numHidden];
            double[][] hoPrevWeightsDelta = MakeMatrix(numHidden, numOutput, 0.0);
            double[] oPrevBiasesDelta = new double[numOutput];

            int epoch = 0;
            double[] xValues = new double[numInput]; // inputs
            double[] tValues = new double[numOutput]; // target values
            double derivative = 0.0;
            double errorSignal = 0.0;

            int[] sequence = new int[TrainingData.Length];  
            for (int i = 0; i < sequence.Length; ++i)
                sequence[i] = i;

            int errInterval = maxEpochs / 10; // interval to check error
            while (epoch < maxEpochs)
            {
                ++epoch;

                if (epoch % errInterval == 0 && epoch < maxEpochs)
                {
                    double trainErr = Error(TrainingData);
                    Console.WriteLine("epoch = " + epoch + "  error = " +
                      trainErr.ToString("F4"));
                    //Console.ReadLine();
                }

                Shuffle(sequence); // visit each training data in random order
                for (int ii = 0; ii < TrainingData.Length; ++ii)
                {
                    int idx = sequence[ii];
                    Array.Copy(TrainingData[idx], xValues, numInput);
                    Array.Copy(TrainingData[idx], numInput, tValues, 0, numOutput);
                    ComputeOutputs(xValues); // copy xValues in, compute outputs 

                    // indices: i = inputs, j = hiddens, k = outputs

                    // 1. compute output node signals (assumes softmax)
                    for (int k = 0; k < numOutput; ++k)
                    {
                        errorSignal = tValues[k] - outputs[k];  // Wikipedia uses (o-t)
                        derivative = (1 - outputs[k]) * outputs[k]; // for softmax
                        oSignals[k] = errorSignal * derivative;
                    }

                    // 2. compute hidden-to-output weight gradients using output signals
                    for (int j = 0; j < numHidden; ++j)
                        for (int k = 0; k < numOutput; ++k)
                            hoGrads[j][k] = oSignals[k] * hOutputs[j];

                    // 2b. compute output bias gradients using output signals
                    for (int k = 0; k < numOutput; ++k)
                        obGrads[k] = oSignals[k] * 1.0; // dummy assoc. input value

                    // 3. compute hidden node signals
                    for (int j = 0; j < numHidden; ++j)
                    {
                        derivative = (1 + hOutputs[j]) * (1 - hOutputs[j]); // for tanh
                        double sum = 0.0; // need sums of output signals times hidden-to-output weights
                        for (int k = 0; k < numOutput; ++k)
                        {
                            sum += oSignals[k] * hoWeights[j][k]; // represents error signal
                        }
                        hSignals[j] = derivative * sum;
                    }

                    // 4. compute input-hidden weight gradients
                    for (int i = 0; i < numInput; ++i)
                        for (int j = 0; j < numHidden; ++j)
                            ihGrads[i][j] = hSignals[j] * inputs[i];

                    // 4b. compute hidden node bias gradients
                    for (int j = 0; j < numHidden; ++j)
                        hbGrads[j] = hSignals[j] * 1.0; // dummy 1.0 input

                    // == update weights and biases

                    // update input-to-hidden weights
                    for (int i = 0; i < numInput; ++i)
                    {
                        for (int j = 0; j < numHidden; ++j)
                        {
                            double delta = ihGrads[i][j] * learnRate;
                            ihWeights[i][j] += delta; // would be -= if (o-t)
                            ihWeights[i][j] += ihPrevWeightsDelta[i][j] * momentum;
                            ihPrevWeightsDelta[i][j] = delta; // save for next time
                        }
                    }

                    // update hidden biases
                    for (int j = 0; j < numHidden; ++j)
                    {
                        double delta = hbGrads[j] * learnRate;
                        hBiases[j] += delta;
                        hBiases[j] += hPrevBiasesDelta[j] * momentum;
                        hPrevBiasesDelta[j] = delta;
                    }

                    // update hidden-to-output weights
                    for (int j = 0; j < numHidden; ++j)
                    {
                        for (int k = 0; k < numOutput; ++k)
                        {
                            double delta = hoGrads[j][k] * learnRate;
                            hoWeights[j][k] += delta;
                            hoWeights[j][k] += hoPrevWeightsDelta[j][k] * momentum;
                            hoPrevWeightsDelta[j][k] = delta;
                        }
                    }

                    // update output node biases
                    for (int k = 0; k < numOutput; ++k)
                    {
                        double delta = obGrads[k] * learnRate;
                        oBiases[k] += delta;
                        oBiases[k] += oPrevBiasesDelta[k] * momentum;
                        oPrevBiasesDelta[k] = delta;
                    }

                } // each training item

            } // while
            double[] bestWts = GetWeights();
            return bestWts;
        }

        private void Shuffle(int[] sequence)
        {
            // Swaps the values for the sequence randomly.
            for (int i = 0; i < sequence.Length; i++)
            {
                int r = this.rnd.Next(i, sequence.Length);
                int tmp = sequence[r];
                sequence[r] = sequence[i];
                sequence[i] = tmp;
            }
        }
        private double Error(double[][] trainData)
        {
            // for training
            // Average squared error per training item
            double sumSquaredError = 0.0;
            double[] xValues = new double[numInput];
            double[] tValues = new double[numOutput];

            // Sums up the total error value for all trainingData. 
            for (int i = 0; i < trainData.Length; i++)
            {
                Array.Copy(trainData[i], xValues, numInput);
                Array.Copy(trainData[i], numInput, tValues, 0, numOutput);
                double[] yValues = this.ComputeOutputs(xValues);
                for (int j = 0; j < numOutput; j++)
                {
                    double err = tValues[j] - yValues[j];
                    sumSquaredError += err * err;
                }
            }
            return sumSquaredError / trainData.Length;
        }

        public double Accuracy(double[][] testData)
        {
            // for testing
            // Precentage correct using winner-takes all.
            int numCorrect = 0;
            int numWrong = 0;
            double[] xValues = new double[numInput];
            double[] tValues = new double[numOutput];
            double[] yValues;

            for (int i = 0; i < testData.Length; i++)
            {
                Array.Copy(testData[i], xValues, numInput);
                Array.Copy(testData[i], numInput, tValues, 0, numOutput);
                yValues = this.ComputeOutputs(xValues);
                int maxIndex = MaxIndex(yValues);
                int tMaxIndex = MaxIndex(tValues);

                if (maxIndex == tMaxIndex)
                {
                    numCorrect++;
                }
                else
                {
                    numWrong++;
                }        
            }
            return (numCorrect) / (numCorrect + numWrong);
        }
        private static int MaxIndex(double[] vector)
        {
            int bigIndex = 0;
            double biggestVal = vector[0];
            for (int i = 0; i < vector.Length; i++)
            {
                if (vector[i] > biggestVal)
                {
                    biggestVal = vector[i];
                    bigIndex = i;
                }
            }
            return bigIndex;
        }
    }
}
