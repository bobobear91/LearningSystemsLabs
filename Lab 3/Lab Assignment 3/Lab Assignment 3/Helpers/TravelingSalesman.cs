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

        //State
        bool stopAlgorithm;

        // Population
        int[][] population_DNA;
        double[][] population_Fitness;
        int[][] nextGenerationChildren_DNA;
        int DNASize;

        // Initialize
        public TravelingSalesman()
        { }
        private void IntítializePopulation()
        {
            throw new NotImplementedException();
        }

        // Actions
        public void Start()
        {
            throw new NotImplementedException();
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
    }
}
