using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Lab_Assignment_3.GA
{
    class TravelingSalesmanAlgorithm
    {
        #region Variables
        // Algorithm
        private List<DNA> population;
        private int DNA_Size;
        private Point[] currentBestRoute;
        private double currentBestFitness;
        private int currentIteration;

        public List<DNA> Population
        {
            get { return population; }
            set { population = value; }
        }

        // Configuration
        private int populationSize;
        private int reproductionVolume;
        private double mutationChance;
        private List<Point> cityLayout;

        public int PopulationSize
        {
            get { return populationSize; }
            set {
                // only allow an even population.
                if (value % 2 != 0)
                {
                    populationSize = value + 1;
                }
                else
                {
                    populationSize = value;
                }
            }
        }
        public int ReproductionVolume
        {
            get { return reproductionVolume; }
            set {
                // only allow an even reproductionVolume.
                if (value % 2 != 0)
                {
                    reproductionVolume = value + 1;
                }
                else
                {
                    reproductionVolume = value;
                }
            }
        }
        public double MutationChance
        {
            get { return mutationChance; }
            set {
                // only allow values between 0-1 as mutationChance.
                if (value > 1)
                {
                    mutationChance = 1;
                }
                else if (value < 0)
                {
                    mutationChance = 0;
                }
                else
                {
                    mutationChance = value;
                }
            }
        }
        public List<Point> CityLayout
        {
            get { return cityLayout; }
            set {
                cityLayout = value;
                DNA_Size = cityLayout.Count;
            }
        }
        
        // Control
        bool abortAlgorithm;
        bool isRunning;
        bool doSlowDown;
        private Object ReadLock = new Object();

        public bool IsRunning
        {
            get { return isRunning; }
        }
        public bool DoSlowDown
        {
            get { return doSlowDown; }
            set { doSlowDown = value; }
        }

        Random rnd;
        #endregion

        #region Initialize
        public TravelingSalesmanAlgorithm(List<Point> cityLayout, int populationSize = 100, int reproductionVolume = 80, double mutationChance = 0.2)
        {
            // Set Algorithm Configuration
            this.cityLayout = cityLayout;
            this.PopulationSize = populationSize;
            this.ReproductionVolume = reproductionVolume;
            this.MutationChance = mutationChance;
            this.DNA_Size = cityLayout.Count;

            rnd = new Random();

            // Initialize Random Defualt Population
            InitializePopulation();

            // Set Control variables
            abortAlgorithm = false;
            isRunning = false;
            currentIteration = 0;
            currentBestFitness = double.MaxValue;
            doSlowDown = false;
        }
        private void InitializePopulation()
        {
            population = new List<DNA>();
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(CreateRandomDNA());
            }
        }
        private DNA CreateRandomDNA()
        {
            List<int> indexList = new List<int>();
            for (int i = 0; i < cityLayout.Count; i++)
            {
                indexList.Add(i);
            }

            // Initialize a new DNA string with the values from indexList in random order. 
            int[] DNA_String = new int[DNA_Size];
            for (int i = 0; i < DNA_Size; i++)
            {
                int nextIndex = rnd.Next(indexList.Count - 1);
                DNA_String[i] = indexList[nextIndex];
                indexList.RemoveAt(nextIndex);
            }
            return new DNA(DNA_String);
        }
        #endregion

        #region User-Control Functions
        public void StartAlgorithm(int maxIterations)
        {
            Action doGeneticAlgorithm = () =>
            {
                abortAlgorithm = false;
                isRunning = true;
                MainAlgorithm(maxIterations);
                isRunning = false;

                Application.Current.Dispatcher.Invoke(AlgorithmIsDone_Event, new Object[] { population[GetBestIndividualIndex()].Fitness });
            };

            if (!isRunning)
            {
                Thread GA = new Thread(() => doGeneticAlgorithm());
                GA.Start();
            }
        }
        public void StopAlgorithm()
        {
            abortAlgorithm = true;
        }
        public void ResetPopulation()
        {
            population.Clear();
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(CreateRandomDNA());
            }
        }
        #endregion

        #region Algorithm
        private void MainAlgorithm(int maxIterations)
        {
            int iteration = 0;
            // Main Algorithm iteration. 
            while (iteration <= maxIterations && !abortAlgorithm)
            {
                // Compute Fitness for current population. 
                for (int i = 0; i < PopulationSize; i++)
                {
                    population[i].Fitness = ComputeFitness(population[i].DNA_String);
                }


                    currentBestRoute = GetRoute(GetBestIndividualIndex());
                    currentBestFitness = population[GetBestIndividualIndex()].Fitness;
                    currentIteration = iteration;

                // Sorts the population by fitness, creates offspring, and removes the worst individuals. 
                ReproducePopulation();
                // Force mutation
                MutateDuplicates();

                iteration++;

                if (doSlowDown)
                {
                    Thread.Sleep(100);
                }
            } 
        }
        private void ReproducePopulation()
        {
            // Sorts the current population by fitness. 
            population.Sort();

            // Create as many children as defined in ReproductionVolume
            List<DNA> offsprings = new List<DNA>();
            for (int i = 0; i < reproductionVolume; i++)
            {
                // Get random index for the top half of the population.
                int parentA_Index = rnd.Next(PopulationSize / 2);
                int parentB_Index = rnd.Next(PopulationSize / 2);

                offsprings.Add(Reproduce(population[parentA_Index], population[parentB_Index]));
            }

            // Replace worst population with new children
            for (int i = 0; i < reproductionVolume; i++)
            {
                population[(populationSize - 1) - i] = offsprings[i];
            }
        }
        private DNA Reproduce(DNA parentA, DNA parentB)
        {
            // Create a list with available DNA
            List<int> AvailabeDNA = new List<int>();
            for (int i = 0; i < DNA_Size; i++)
            {
                AvailabeDNA.Add (parentA.DNA_String[i]);
            }

            // Create offspring crossover DNA
            DNA offspring = new DNA(DNA_Size);
            for (int i = 0; i < DNA_Size; i++)
            {
                // Should parentA or parentB be used for this DNA index? 
                double rnd_parentValue = rnd.NextDouble();
                if (rnd_parentValue > 0.5)
                {
                    // Use parentA
                    offspring.DNA_String[i] = parentA.DNA_String[i];
                    AvailabeDNA.Remove(offspring.DNA_String[i]);
                }
                else
                {
                    // Use parentB
                    offspring.DNA_String[i] = parentB.DNA_String[i];
                    AvailabeDNA.Remove(offspring.DNA_String[i]);
                }
            }

            // Correct offspring DNA - Remove duplicates
            for (int i = 0; i < DNA_Size; i++)
            {
                for (int j = 0; j < DNA_Size; j++)
                {
                    if (i != j && offspring.DNA_String[i] == offspring.DNA_String[j])
                    {
                        offspring.DNA_String[i] = AvailabeDNA[0];
                        AvailabeDNA.RemoveAt(0);
                        break;
                    }
                }
            }

            // Check if Mutation should be done
            double rnd_mutationValue = rnd.NextDouble();
            if (rnd_mutationValue < mutationChance)
            {
                offspring = MutateDNA(offspring);
            }

            return offspring;
        }
        private DNA MutateDNA(DNA dna)
        {
            int rnd_mutation = rnd.Next(3);
            switch (rnd_mutation)
            {
                case 0:
                    dna.Mutation_SwapDNA();
                    break;
                case 1:
                    dna.Mutation_MoveDNA();
                    break;
                case 2:
                    dna.Mutation_ReverseDNARange();
                    break;
            }
            return dna;
        }
        private void MutateDuplicates()
        {
            // Force mutate duplicates
            for (int i = 0; i < populationSize; i++)
            {
                for (int j = 0; j < populationSize; j++)
                {
                    if (i != j && population[i].isDNAEqual(population[j]))
                    {
                        population[j] = MutateDNA(population[j]);
                    }
                }
            }
        }
        #endregion

        #region Helpers
        private double ComputeFitness(int[] individualDNA)
        {
            double distanceSum = 0;
            // Compute all but the last euclidean distance to avoid if-last-statement in for-loop.
            for (int i = 0; i < cityLayout.Count - 1; i++)
            {
                distanceSum += ComputeEuclideanDistance(cityLayout[individualDNA[i]], cityLayout[individualDNA[i + 1]]);
            }
            // Compute extra last distance to get distance back "home".
            distanceSum += ComputeEuclideanDistance(cityLayout[individualDNA.Length - 1], cityLayout[individualDNA[0]]);
            return (distanceSum > double.MaxValue) ? double.MaxValue : distanceSum;
        }
        private int GetBestIndividualIndex()
        {
            int bestIndex = 0;
            for (int i = 0; i < populationSize; i++)
            {
                if (population[i].Fitness < population[bestIndex].Fitness)
                {
                    bestIndex = i;
                }
            }
            return bestIndex;
        }
        public Point[] GetBestRoute()
        {
            lock (ReadLock)
            {
                return currentBestRoute;
            }
        }
        private Point[] GetRoute(int index)
        {
            // Get the best index and route order.
            int[] RouteOrder = population[index].DNA_String;
            // Initialize route Point array
            Point[] route = new Point[DNA_Size + 1];
            // Get all point coordinates in the order of routeOrder
            for (int i = 0; i < DNA_Size; i++)
            {
                route[i] = cityLayout[RouteOrder[i]];
            }
            // Add the first point from routeOrder to the last point of the route. 
            route[DNA_Size] = cityLayout[RouteOrder[0]];
            return route;
        }
        public Point GetBestFitness()
        {
                return new Point(currentIteration, population[0].Fitness);
        }
        #endregion

        #region Math - Helpers
        private double ComputeEuclideanDistance(Point a, Point b)
        {
            double deltaX = b.X - a.X;
            double deltaY = b.Y - a.Y;

            double powDeltaX = deltaX * deltaX;
            double powDeltaY = deltaY * deltaY;

            return Math.Sqrt(powDeltaX + powDeltaY);
        }
        #endregion

        #region Events
        public delegate void AlgorithmIsDone(double bestFitness);
        public event AlgorithmIsDone AlgorithmIsDone_Event;
        #endregion
    }
}
