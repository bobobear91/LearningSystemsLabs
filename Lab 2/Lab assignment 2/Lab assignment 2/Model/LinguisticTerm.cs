using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_assignment_2.Model
{

    public class LinguisticTerm
    {
        #region Variables
        private string name = string.Empty;
        private double currentValue = 0;

        private MembershipFunctionCollection membershipFunctionCollection = new MembershipFunctionCollection();

        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = !string.IsNullOrEmpty(value) ? value : name; }
        }
        #endregion

        #region Constructor
        public LinguisticTerm(string name)
        {
            this.name = name;
        }

        public LinguisticTerm(string name, MembershipFunctionCollection membershipFunctionCollection)
        {
            this.Name = name;
            this.MembershipFunctionCollection = membershipFunctionCollection;
        }

        public double Input
        {
            get { return currentValue; }
            set { currentValue = value; }
        }
        public MembershipFunctionCollection MembershipFunctionCollection
        {
            get { return membershipFunctionCollection; }
            set { membershipFunctionCollection = value; }
        }

        #endregion

        public double Fuzzify(string membershipFunctionName)
        {
            MembershipFunction membershipFunction = this.membershipFunctionCollection.Find(membershipFunctionName);

            if ((membershipFunction.X0 <= this.currentValue) && (this.currentValue < membershipFunction.X1))
                return (this.currentValue - membershipFunction.X0) / (membershipFunction.X1 - membershipFunction.X0);
            else if ((membershipFunction.X1 <= this.currentValue) && (this.currentValue <= membershipFunction.X2))
                return 1;
            else if ((membershipFunction.X2 < this.currentValue) && (this.currentValue <= membershipFunction.X3))
                return (membershipFunction.X3 - this.currentValue) / (membershipFunction.X3 - membershipFunction.X2);
            else
                return 0;
        }

        /// <summary>
        /// Returns the minimum value of the linguistic variable.
        /// </summary>
        /// <returns>The minimum value of the linguistic variable.</returns>
        public double MinValue()
        {
            double minValue = this.membershipFunctionCollection[0].X0;

            for (int i = 1; i < this.membershipFunctionCollection.Count; i++)
            {
                if (this.membershipFunctionCollection[i].X0 < minValue)
                    minValue = this.membershipFunctionCollection[i].X0;
            }

            return minValue;
        }

        /// <summary>
        /// Returns the maximum value of the linguistic variable.
        /// </summary>
        /// <returns>The maximum value of the linguistic variable.</returns>
        public double MaxValue()
        {
            double maxValue = this.membershipFunctionCollection[0].X3;

            for (int i = 1; i < this.membershipFunctionCollection.Count; i++)
            {
                if (this.membershipFunctionCollection[i].X3 > maxValue)
                    maxValue = this.membershipFunctionCollection[i].X3;
            }

            return maxValue;
        }

        /// <summary>
        /// Returns the difference between MaxValue() and MinValue().
        /// </summary>
        /// <returns>The difference between MaxValue() and MinValue().</returns>
        public double Range()
        {
            return this.MaxValue() - this.MinValue();
        }
    }

}
