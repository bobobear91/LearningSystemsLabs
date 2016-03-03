using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_Assignment_3.Helpers
{
    class TravelingSalesman
    {
        // Configuration
        int populationSize;
        double mutationChance;
        int childrenPerGeneration;
        Point[] cityCoordinates;

        public int PopulationSize
        {
            get { return populationSize; }
            set { populationSize = value; }
        }
        public double MutationChance
        {
            get { return mutationChance; }
            set { mutationChance = value; }
        }
        public int ChildrenPerGeneration
        {
            get { return childrenPerGeneration; }
            set { childrenPerGeneration = value; }
        }
        public Point[] CityCoordinates
        {
            get { return cityCoordinates; }
            set
            {
                cityCoordinates = value;
                DNASize = cityCoordinates.Length;

                population_DNA = null;
                population_DNA = new int[populationSize][];
                for (int i = 0; i < populationSize; i++)
                {
                    population_DNA[i] = new int[DNASize];
                }
            }
        }

        //State
        bool stopAlgorithm;

        // Population
        int[][] population_DNA;
        double[] population_Fitness;
        int[][] nextGenerationChildren_DNA;
        int DNASize;

        // Initialize
        public TravelingSalesman(Point[] cityCoordinates, int populationSize = 100, double mutationChance = 0.2, int childrenPerGeneration = 50)
        {
            // Set algorithm configuration
            this.populationSize = populationSize;
            this.mutationChance = mutationChance;
            this.childrenPerGeneration = childrenPerGeneration;
            this.cityCoordinates = cityCoordinates;
            DNASize = cityCoordinates.Length;

            // Initialize population DNA
            population_DNA = new int[populationSize][];
            for (int i = 0; i < populationSize; i++)
            {
                population_DNA[i] = new int[DNASize];
            }

            // Initialize population fitness
            population_Fitness = new double[populationSize];

            // Initialize children array
            nextGenerationChildren_DNA = new int[childrenPerGeneration][];
            for (int i = 0; i < childrenPerGeneration; i++)
            {
                nextGenerationChildren_DNA[i] = new int[DNASize];
            }

            // Set state
            stopAlgorithm = false;
        }
        private void IntítializePopulation()
        {
            throw new NotImplementedException();
        }

        // Actions
        public void Start(int maxIterations)
        {
            stopAlgorithm = false;
            MainGA(maxIterations);
        }
        public void Stop()
        {
            throw new NotImplementedException();
        }
        public void Reset()
        {
            throw new NotImplementedException();
        }

        // Genetic Algorithm
        private void MainGA(int _maxIterations)
        {
            throw new NotImplementedException();
        }
        private void BreedPopulation()
        {
            throw new NotImplementedException();
        }
        private int[] CreateChild(int[] parentA, int[] parentB)
        {
            throw new NotImplementedException();
        }
        private double computePopulationFitness()
        {
            throw new NotImplementedException();
        }
        private double FindBestFitnessValue()
        {
            throw new NotImplementedException();
        }
        private int FindBestFitnessIndividualIndex()
        {
            throw new NotImplementedException();
        }

        // Math
        private double ComputeEucledianDistance(Point a, Point b)
        {
            throw new NotImplementedException();
        }

        // Delegate Events
        public delegate void BestFitnessInformation(double best_fitness);
        public delegate void BestRouteInformation(int[] best_Route);
        public delegate void FinishedIterations(double best_fitness, int[] best_Route);

        public event BestFitnessInformation FireBestFitnessInformation;
        public event BestRouteInformation FireBestRouteInformation;
        public event FinishedIterations FireFinishedIterations;
    }
}
