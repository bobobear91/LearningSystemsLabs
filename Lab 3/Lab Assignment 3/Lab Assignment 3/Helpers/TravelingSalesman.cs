﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
                    population_DNA[i] = CreateRandomDNA();
                    population_Fitness[i] = int.MaxValue;
                }
            }
        }

        //State
        bool stopAlgorithm;

        // Population
        int[][] population_DNA;
        double[] population_Fitness;
        int DNASize;

        Random rnd;

        // Initialize
        public TravelingSalesman(Point[] cityCoordinates, int populationSize = 100, double mutationChance = 0.2, int childrenPerGeneration = 50)
        {
            // Set algorithm configuration
            this.populationSize = populationSize;
            this.mutationChance = mutationChance;
            this.childrenPerGeneration = childrenPerGeneration;
            this.cityCoordinates = cityCoordinates;
            DNASize = cityCoordinates.Length;

            rnd = new Random();

            // Initialize random default population. 
            InititializePopulation();

            // Set state
            stopAlgorithm = false;
        }
        /// <summary>
        /// Initializes/Resets the current population and fitness
        /// </summary>
        private void InititializePopulation()
        {
            // Initialize population DNA
            population_DNA = new int[populationSize][];
            for (int i = 0; i < populationSize; i++)
            {
                population_DNA[i] = CreateRandomDNA();
            }

            // Initialize population fitness
            population_Fitness = new double[populationSize];
            for (int i = 0; i < populationSize; i++)
            {
                population_Fitness[i] = double.MaxValue;
            }
        }
        /// <summary>
        /// Creates a random string of DNA filled with unique indexes between 0-numberOfCities in random order.
        /// </summary>
        /// <returns></returns>
        private int[] CreateRandomDNA()
        {
            // Initialize a list containing all indeces available for the cities. 
            List<int> indexList = new List<int>();
            for (int i = 0; i < CityCoordinates.Length; i++)
            {
                indexList.Add(i);
            }

            // Initialize a new DNA string with the values from indexList in random order. 
            int[] newDNA = new int[DNASize];
            for (int i = 0; i < DNASize; i++)
            {
                int nextIndex = rnd.Next(indexList.Count - 1);
                newDNA[i] = indexList[nextIndex];
                indexList.RemoveAt(nextIndex);
            }

            return newDNA;
        }

        // Actions
        /// <summary>
        /// Starts the Algorithm to find optimized route on new thread.
        /// </summary>
        /// <param name="maxIterations"></param>
        public void Start(int maxIterations)
        {
            Action doGeneticAlgorithm = () =>
            {
                stopAlgorithm = false;
                MainGA(maxIterations);
            };

            Thread GA = new Thread(() => doGeneticAlgorithm());
            GA.Start();
        }
        /// <summary>
        /// Stops the algorithm 
        /// </summary>
        public void Stop()
        {
            stopAlgorithm = true;
        }
        public void Reset()
        {
            InititializePopulation();
        }

        // Genetic Algorithm
        private void MainGA(int maxIterations)
        {
            int iterations = 0;
            while (iterations < maxIterations && !stopAlgorithm)
            {
                for (int i = 0; i < populationSize; i++)
                {
                    // Compute fitness of individual
                    population_Fitness[i] = computeFitness(population_DNA[i]);
                }

                // Get Best Fitness and values here --------------------------------
                int bestIndividualIndex = GetBestFitnessIndex();
                if (FireBestFitnessInformation != null) FireBestFitnessInformation(population_Fitness[bestIndividualIndex], iterations);
                if (FireBestRouteInformation != null) FireBestRouteInformation(GetRoute(bestIndividualIndex), iterations);
                //------------------------------------------------------------------

                // Create new generation / dispose current generation bad eggs. 
                InvertedCrossBreeding();
            }
        }
        private void InvertedCrossBreeding()
        {
            List<int[]> nextGenChildren = new List<int[]>();

            // Create as many children as population
            for (int i = 0; i < populationSize; i++)
            {
                nextGenChildren.Add(Breed(population_DNA[i], population_DNA[(PopulationSize - 1) - i]));
            }

            for (int i = 0; i < childrenPerGeneration; i++)
            {
                // Get a random child
                int rndIndex = rnd.Next(nextGenChildren.Count-1);
                // Find current worst parent
                int worstIndex = GetWorstFitnessIndex();
                // overwrite current worst parent with random child
                population_DNA[worstIndex] = nextGenChildren[rndIndex];
                // reset fitness value for overwritten parent
                population_Fitness[worstIndex] = 0;
                // Remove child from list of available children
                nextGenChildren.RemoveAt(rndIndex);
            }
        }
        private int[] Breed(int[] parentA, int[] parentB)
        {
            List<int> parentAList = new List<int>();
            List<int> parentBList = new List<int>();
            for (int i = 0; i < DNASize; i++)
            {
                parentAList.Add(parentA[i]);
                parentBList.Add(parentB[i]);
            }

            int[] newDNA = new int[DNASize];
            for (int i = 0; i < DNASize; i++)
            {
                double rndValue = rnd.NextDouble();
                if (rndValue > 0.5)
                {
                    newDNA[i] = parentAList[i];
                    parentAList.RemoveAt(i);
                    parentBList.Remove(parentAList[i]);
                }
                else
                {
                    newDNA[i] = parentBList[i];
                    parentBList.RemoveAt(i);
                    parentAList.Remove(parentBList[i]);
                }
            }
            return newDNA;
        }
        private double computeFitness(int[] individualDNA)
        {
            double distanceSum = 0;

            // Compute all but the last euclidean distance to avoid if-last-statement
            for (int i = 0; i < cityCoordinates.Length - 1; i++)
            {
               distanceSum += ComputeEuclideanDistance(cityCoordinates[individualDNA[i]], cityCoordinates[individualDNA[i + 1]]);
            }
            // compute extra last distance to get distance back "home"
            distanceSum += ComputeEuclideanDistance(cityCoordinates[individualDNA[individualDNA.Length - 1]], cityCoordinates[individualDNA[0]]);

            return distanceSum;
        }

        private double GetBestFitnessValue()
        {
            double bestValue = int.MaxValue;
            for (int i = 0; i < populationSize; i++)
            {
                if (population_Fitness[i] < bestValue)
                {
                    bestValue = population_Fitness[i];
                }
            }
            return bestValue;
        }
        private double ComputeGenMeanFitnessValue()
        {
            double fitnessSum = 0;
            for (int i = 0; i < PopulationSize; i++)
            {
                fitnessSum += population_Fitness[i];
            }
            return fitnessSum / PopulationSize;
        }
        private int GetBestFitnessIndex()
        {
            int bestIndex = 0;
            for (int i = 0; i < populationSize; i++)
            {
                if (population_Fitness[i] < population_Fitness[bestIndex])
                {
                    bestIndex = i;
                }
            }
            return bestIndex;
        }
        private int GetWorstFitnessIndex()
        {
            int worstIndex = 0;
            for (int i = 0; i < populationSize; i++)
            {
                if (population_Fitness[i] > population_Fitness[worstIndex])
                {
                    worstIndex = i;
                }
            }
            return worstIndex;
        }
        private Point[] GetRoute(int index)
        {
            // Get routeOrder from DNA[index]
            int[] routeOrder = population_DNA[index];
            // Initialize route Point array
            Point[] route = new Point[DNASize + 1];
            // Get all point coordinates in the order of routeOrder
            for (int i = 0; i < DNASize; i++)
            {
                route[i] = cityCoordinates[routeOrder[i]];
            }
            // Add the first point from routeOrder to the last point of the route. 
            route[DNASize] = cityCoordinates[routeOrder[0]];
            return route;
        }

        // Math
        private double ComputeEuclideanDistance(Point a, Point b)
        {
            double deltaX = b.X - a.X;
            double deltaY = b.Y - a.Y;

            double powDeltaX = deltaX * deltaX;
            double powDeltaY = deltaY * deltaY;

            return Math.Sqrt(powDeltaX + powDeltaY);
        }

        // Delegate Events
        public delegate void BestFitnessInformation(double best_fitness, int iteration);
        public delegate void BestRouteInformation(Point[] best_Route, int iteration);

        public event BestFitnessInformation FireBestFitnessInformation;
        public event BestRouteInformation FireBestRouteInformation;
    }
}
