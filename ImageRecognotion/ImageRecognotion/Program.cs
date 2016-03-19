using ImageRecognotion.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageRecognotion
{
    class Program
    {
        static void Main(string[] args)
        {
            var traningPath = @"trainingsample.csv";
            var traning = DataReader.ReadObservations(traningPath);

            var distance = new ManhattanDistance();
            var classifier = new BasicClassifier(distance);
            classifier.Train(traning);

            var validationPath = @"validationsample.csv";
            var validation = DataReader.ReadObservations(validationPath);

            var correct = Evaluator.Correct(validation, classifier);
            Console.WriteLine("Correctly Classified: {0:P2}", correct);
            Console.ReadLine();
        }
    }
    public class Observation
    {
        public Observation(string label, int[] pixels)
        {
            this.Label = label;
            this.Pixels = pixels;
        }
        public string Label { get; private set; }
        public int[] Pixels { get; private set; }
    }
    public class BasicClassifier : IClassifier
    {
        private IEnumerable<Observation> data;

        private readonly IDistance distance;
        
        public BasicClassifier(IDistance distance)
        {
            this.distance = distance;
        }

        public void Train(IEnumerable<Observation> trainingSet)
        {
            this.data = trainingSet;
        }

        public string Predict(int[] pixels)
        {
            Observation currentBest = null;
            var shortest = Double.MaxValue;

            foreach (Observation obs in this.data)
            {
                var dist = this.distance.Between(obs.Pixels, pixels);
                if (dist < shortest)
                {
                    shortest = dist;
                    currentBest = obs;
                }
            }
            return currentBest.Label;
        }
    }
    public class Evaluator
    {
        public static double Correct(IEnumerable<Observation> validationSet, IClassifier classifier)
        {
            return validationSet.Select(obs => Score(obs, classifier)).Average();
        }
        private static double Score(Observation obs, IClassifier classifier)
        {
            if (classifier.Predict(obs.Pixels) == obs.Label)
                return 1.0;
            else
                return 0.0;
        }
    }

    public class ManhattanDistance : IDistance
    {
        public double Between(int[] pixels1,int[] pixels2)
        {
            if (pixels1.Length != pixels2.Length)
            {
                throw new ArgumentException("Inconsistent image sizes");
            }
            var length = pixels1.Length;
            var distance = 0;
            for (int i = 0; i < length; i++)
            {
                distance += Math.Abs(pixels1[i] - pixels2[i]);
            }
            return distance;
        }
    }
    public class DataReader
    {
        private static Observation ObservationFactory(string data)
        {
            var commaSeparated = data.Split(',');
            var label = commaSeparated[0];
            var pixels = commaSeparated.Skip(1).Select(x => Convert.ToInt32(x)).ToArray();
            return new Observation(label, pixels);
        }
        public static Observation[] ReadObservations(string datapath)
        {
            var data = File.ReadAllLines(datapath).Skip(1).Select(ObservationFactory).ToArray();
            return data;
        }
    }
}
