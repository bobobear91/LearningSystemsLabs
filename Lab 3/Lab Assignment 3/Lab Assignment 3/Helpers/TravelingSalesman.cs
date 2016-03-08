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
            // TODO: run this on new thread
            stopAlgorithm = false;
            MainGA(maxIterations);
            // TODO: fire 'finished' event. 
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
                FireBestFitnessInformation(population_Fitness[bestIndividualIndex]);
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
            List<int> indexList = new List<int>();
            for (int i = 0; i < CityCoordinates.Length; i++)
            {
                indexList.Add(i);
            }
            throw new NotImplementedException();


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
            throw new NotImplementedException();
        }
        private double ComputeGenMeanFitnessValue()
        {
            throw new NotImplementedException();
        }
        private int GetBestFitnessIndex()
        {
            throw new NotImplementedException();
        }
        private int GetWorstFitnessIndex()
        {
            throw new NotImplementedException();
        }
        private int[] GetRoute(int index)
        {
            throw new NotImplementedException();
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
        public delegate void BestFitnessInformation(double best_fitness);
        public delegate void BestRouteInformation(int[] best_Route);
        public delegate void FinishedIterations(double best_fitness, int[] best_Route);

        public event BestFitnessInformation FireBestFitnessInformation;
        public event BestRouteInformation FireBestRouteInformation;
        public event FinishedIterations FireFinishedIterations;
    }
}
