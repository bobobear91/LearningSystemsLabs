using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_assignment_2.Model
{
    /// <summary>
    /// 
    /// </summary>
    class FuzzyLogic
    {
        #region Variables
        private Collection<FuzzyLogicRule> fuzzyrulebook = new Collection<FuzzyLogicRule>();
        private Collection<LinguisticTerm> linguisticVariableCollection = new Collection<LinguisticTerm>();
        private string consequent = String.Empty;

        #endregion

        #region Properties
        public Collection<FuzzyLogicRule> Rules
        {
            get { return fuzzyrulebook; }
            set { fuzzyrulebook = value; }
        }

        public Collection<LinguisticTerm> Linguistics
        {
            get { return linguisticVariableCollection; }
            set { linguisticVariableCollection = value; }
        }

        /// <summary>
        /// The consequent variable name.
        /// </summary>
        public string Consequent
        {
            get { return consequent; }
            set { consequent = value; }
        }
        #endregion

        #region Constructor

        #endregion

        #region Private Methods
        private LinguisticTerm FindLingustics(string linguisticVariableName)
        {
            LinguisticTerm linguisticVariable = null;

            foreach (LinguisticTerm variable in Linguistics)
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


        private LinguisticTerm GetConsequent()
        {
            return this.FindLingustics(this.consequent);
        }

        private double Parse(string text)
        {
            int counter = 0;
            int firstMatch = 0;

            //If text do not start with (
            if (!text.StartsWith("("))
            {
                string[] tokens = text.Split();
                return FindLingustics(tokens[0]).Fuzzification(tokens[2]);
            }

            //Get the placement of first ( and the second )
            for (int i = 0; i < text.Length; i++)
            {
                switch (text[i])
                {
                    case '(':
                        counter++;
                        if (counter == 1)
                            firstMatch = i;
                        break;

                    case ')':
                        counter--;
                        if ((counter == 0) && (i > 0))
                        {
                            //The question without brackets
                            string substring = text.Substring(firstMatch + 1, i - firstMatch - 1);
                            //The question with brackets
                            string substringBrackets = text.Substring(firstMatch, i - firstMatch + 1);
                            //Lenght of the substring with brackets
                            int length = substringBrackets.Length;

                            //Replaces Bracketssubstring on the full text WITH the answer of fuzzification  
                            text = text.Replace(substringBrackets, Parse(substring).ToString());

                            i = i - (length - 1);
                        }
                        break;

                    default:
                        break;
                }
            }

            return Evaluate(text);
        }

        private double Evaluate(string text)
        {
            string[] tokens = text.Split();
            string connective = "";
            double value = 0;
            int p = ((tokens.Length / 2) + 1);
            for (int i = 0; i <= ((tokens.Length / 2) + 1); i = i + 2)
            {
                double tokenValue = Convert.ToDouble(tokens[i]);

                switch (connective)
                {
                    case "AND":
                        if (tokenValue < value)
                            value = tokenValue;
                        break;

                    case "OR":
                        if (tokenValue > value)
                            value = tokenValue;
                        break;

                    default:
                        value = tokenValue;
                        break;
                }

                if ((i + 1) < tokens.Length)
                    connective = tokens[i + 1];
            }

            return value;
        }
        #endregion

        #region Public Methods
        //public double Fu

        public double Defuzzification()
        {

            //WHY
            double numerator = 0;
            double denominator = 0;



            // Reset values
            foreach (MembershipFunction membershipFunction in GetConsequent().MembershipFunctions)
            {
                membershipFunction.Value = 0;
            }



            // Something Somwthing Something dark side. 
            foreach (FuzzyLogicRule fuzzyRule in fuzzyrulebook)
            {
                //Fuzzification
                // INPUT: The part of the rule between IF and THEN. 
                //
                fuzzyRule.Value = Parse(fuzzyRule.Conditions());

                string[] tokens = fuzzyRule.Rule.Split();
                MembershipFunction membershipFunction = this.GetConsequent().Find(tokens[tokens.Length - 1]);

                if (fuzzyRule.Value > membershipFunction.Value)
                    membershipFunction.Value = fuzzyRule.Value;
            }

            foreach (MembershipFunction membershipFunction in this.GetConsequent().MembershipFunctions)
            {
                numerator += membershipFunction.Centorid() * membershipFunction.Area();
                denominator += membershipFunction.Area();
            }



            return numerator / denominator;
        }
        #endregion

    }
}
