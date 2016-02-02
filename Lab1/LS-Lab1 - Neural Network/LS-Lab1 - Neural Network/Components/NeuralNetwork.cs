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
        private double[,] ihWeights;
        private double[] hBiases;
        private double[] hOutputs;

        private double[,] hoWeights;
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

        private static double[,] MakeMatrix(int rows, int columns, double v)
        {
            throw new NotImplementedException();
        }

        private void InitializeWeights()
        {
            throw new NotImplementedException();
        }

        public void SetWeights(double[] weights)
        {
            throw new NotImplementedException();
        }
        public double[] GetWeights()
        {
            throw new NotImplementedException();
        }

        public double[] ComputeOutputs(double[] xValues)
        {
            throw new NotImplementedException();
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
