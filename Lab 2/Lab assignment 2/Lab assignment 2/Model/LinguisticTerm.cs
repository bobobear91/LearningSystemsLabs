using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private Collection<MembershipFunction> membershipFunctionCollection = new Collection<MembershipFunction>();

        #endregion

        #region Properties
        public string Name
        {
            get { return name; }
            set { name = !string.IsNullOrEmpty(value) ? value : name; }
        }


        public double Input
        {
            get { return currentValue; }
            set { currentValue = value; }
        }
        public Collection<MembershipFunction> MembershipFunctions
        {
            get { return membershipFunctionCollection; }
            set { membershipFunctionCollection = value; }
        }
        #endregion

        #region Constructor
        public LinguisticTerm(string name)
        {
            this.name = name;
        }

        public LinguisticTerm(string name, Collection<MembershipFunction> membershipFunctionCollection)
        {
            this.Name = name;
            this.MembershipFunctions = membershipFunctionCollection;
        }


        #endregion

        #region Methods
        public double Fuzzify(string membershipFunctionName)
        {
            MembershipFunction membershipFunction = Find(membershipFunctionName);

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

        /// <summary>
        /// Finds a linguistic variable in a collection.
        /// </summary>
        /// <param name="linguisticVariableName">Linguistic variable name.</param>
        /// <returns>The linguistic variable, if founded.</returns>
        public MembershipFunction Find(string linguisticVariableName)
        {
            MembershipFunction linguisticVariable = null;

            foreach (MembershipFunction variable in MembershipFunctions)
            {
                if (variable.Name == linguisticVariableName)
                {
                    linguisticVariable = variable;
                    break;
                }
            }

            if (linguisticVariable == null)
                throw new Exception("LinguisticVariable not found: " + linguisticVariableName);
            else
                return linguisticVariable;
        }

        #endregion
    }
}
