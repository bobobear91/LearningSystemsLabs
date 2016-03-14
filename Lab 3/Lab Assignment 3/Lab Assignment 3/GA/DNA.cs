using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_Assignment_3.GA
{
    class DNA : IEquatable<DNA> , IComparable<DNA>
    {
        // DNA - string. The individuals DNA representing the Route of the TravelingSalesman-Algorithm. 
        int[] dna_String;
        // Fitness. The individuals Fitness-value, contains the total distance for the Route of the DNA. 
        double fitness;
        public int[] DNA_String
        {
            get { return dna_String; }
            set { dna_String = value; }
        }
        public double Fitness
        {
            get { return fitness; }
            set
            {
                fitness = (fitness > double.MaxValue) ? double.MaxValue : value;
            }
        }

        Random rnd;

        public DNA()
        {
            fitness = double.MaxValue;
            rnd = new Random();
        }
        public DNA(int[] dna_String)
        {
            this.dna_String = dna_String;
            fitness = double.MaxValue;
            rnd = new Random();
        }
        public DNA(int dna_Size)
        {
            this.dna_String = new int[dna_Size];
            fitness = double.MaxValue;
            rnd = new Random();
        }

        // Three Different Mutation methods to counter complex DNA "faults": Swap, Move, Reverse'Range'.
        public void Mutation_SwapDNA()
        {
            if (dna_String == null) throw new ArgumentNullException("DNA String");

            int indexA = rnd.Next(dna_String.Length);
            int indexB = rnd.Next(dna_String.Length);

            int tmp = dna_String[indexA];
            dna_String[indexA] = dna_String[indexB];
            dna_String[indexB] = tmp;
        }
        public void Mutation_MoveDNA()
        {
            if (dna_String == null) throw new ArgumentNullException("DNA String");

            int startIndex = rnd.Next(dna_String.Length);
            int tmp = dna_String[startIndex];
            for (int i = startIndex; i < dna_String.Length - 1; i++)
            {
                dna_String[i] = dna_String[i + 1]; 
            }
            dna_String[dna_String.Length - 1] = tmp;
        }
        public void Mutation_ReverseDNARange()
        {
            if (dna_String == null) throw new ArgumentNullException("DNA String");

            int startIndex = rnd.Next(dna_String.Length);
            int endIndex = rnd.Next(startIndex, dna_String.Length);

            // Extract part of DNA to reverse.
            int[] reverseRange = new int[endIndex - startIndex];
            int index = startIndex;
            for (int i = 0; i < reverseRange.Length; i++)
            {
                reverseRange[i] = dna_String[startIndex + i];
            }
            reverseRange = reverseRange.Reverse().ToArray();

            // Insert the new reversed DNA part. 
            for (int i = 0; i < reverseRange.Length; i++)
            {
                dna_String[startIndex + i] = reverseRange[i];
            }
        }

        #region Interface Implementation
        //public bool Equals(DNA other)
        //{
        //    if (other == null) return false;

        //    if (this.fitness == other.fitness) return true;
        //    else return false;
        //}
        public bool Equals(DNA other)
        {
            if (other == null) return false;

            for (int i = 0; i < dna_String.Length; i++)
            {
                if (dna_String[i] != other.dna_String[i])
                {
                    return false;
                }
            }
            return true;
        }
        public bool isDNAEqual(DNA other)
        {
            if (other == null) return false;

            for (int i = 0; i < dna_String.Length; i++)
            {
                if (dna_String[i] != other.dna_String[i])
                {
                    return false;
                }
            }
            return true;
        }

        public int CompareTo(DNA other)
        {
            if (other == null) return 1;
            else return this.fitness.CompareTo(other.fitness);
        }
        #endregion
    }
}
