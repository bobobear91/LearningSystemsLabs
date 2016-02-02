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
            throw new NotImplementedException();
        }
        public static double[] SoftMax(double[] oSums)
        {
            throw new NotImplementedException();
        }

        public double[] Train(double[,] TrainingData, int maxEpochs, double learnRate, double momentum)
        {
            throw new NotImplementedException();
        }

        private void Shuffle(int[] sequence)
        {
            throw new NotImplementedException();
        }
        private double Error(double[,] trainData)
        {
            throw new NotImplementedException();
        }

        public double Accuracy(double[,] testData)
        {
            throw new NotImplementedException();
        }
        private static int MaxIndex(double[] vector)
        {
            throw new NotImplementedException();
        }
    }
}
