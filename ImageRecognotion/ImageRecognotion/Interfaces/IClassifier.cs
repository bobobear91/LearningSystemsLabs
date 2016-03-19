using System.Collections.Generic;

namespace ImageRecognotion.Interfaces
{
    public interface IClassifier
    {
        void Train(IEnumerable<Observation> traningSet);
        string Predict(int[] pixels);
    }

}
