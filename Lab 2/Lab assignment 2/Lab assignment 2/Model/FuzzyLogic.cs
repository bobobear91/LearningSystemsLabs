using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_assignment_2.Model
{
    //https://github.com/MicheleBertoli/DotFuzzy
    /// <summary>
    /// 
    /// </summary>
    class FuzzyLogic
    {
        #region Variables
        private FuzzyLogicRuleBook rulebook = new FuzzyLogicRuleBook();
        //private Collection<FuzzyLogicRule> rulebook = new Collection<FuzzyLogicRule>();
        private Collection<LinguisticTerm> linguisticVariableCollection = new Collection<LinguisticTerm>();
        private string consequent = String.Empty;

        #endregion

        #region Properties
        public FuzzyLogicRuleBook Rules
        {
            get { return rulebook; }
            set { rulebook = value; }
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
        public LinguisticTerm FindLingustics(string linguisticVariableName)
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
        #endregion

        private LinguisticTerm GetConsequent()
        {
            return this.FindLingustics(this.consequent);
        }

        private double Parse(string text)
        {
            int counter = 0;
            int firstMatch = 0;

            if (!text.StartsWith("("))
            {
                string[] tokens = text.Split();
                return FindLingustics(tokens[0]).Fuzzify(tokens[2]);
            }

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
                            string substring = text.Substring(firstMatch + 1, i - firstMatch - 1);
                            string substringBrackets = text.Substring(firstMatch, i - firstMatch + 1);
                            int length = substringBrackets.Length;
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

    }


    /// <summary>
    /// 
    /// </summary>
    class FuzzyLogicRuleBook : Collection<FuzzyLogicRule>
    {
        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Constructor

        #endregion

        #region Methods
        private string[] TokenizeString(string text)
        {
            return null;
        }

        private string Validate(string text)
        {
            int count = 0;
            int position = text.IndexOf("(");
            string[] tokens = text.Replace("(", "").Replace(")", "").Split();

            while (position >= 0)
            {
                count++;
                position = text.IndexOf("(", position + 1);
            }

            position = text.IndexOf(")");
            while (position >= 0)
            {
                count--;
                position = text.IndexOf(")", position + 1);
            }

            if (count > 0)
                throw new Exception("missing right parenthesis: " + text);
            else if (count < 0)
                throw new Exception("missing left parenthesis: " + text);

            if (tokens[0] != "IF")
                throw new Exception("'IF' not found: " + text);

            if (tokens[tokens.Length - 4] != "THEN")
                throw new Exception("'THEN' not found: " + text);

            if (tokens[tokens.Length - 2] != "IS")
                throw new Exception("'IS' not found: " + text);

            for (int i = 2; i < (tokens.Length - 5); i = i + 2)
            {
                if ((tokens[i] != "IS") && (tokens[i] != "AND") && (tokens[i] != "OR"))
                    throw new Exception("Syntax error: " + tokens[i]);
            }

            return text;
        }
        #endregion
    }



    public class MembershipFunction
    {
        #region Private Properties

        private string name = String.Empty;
        private double x0 = 0;
        private double x1 = 0;
        private double x2 = 0;
        private double x3 = 0;
        private double value = 0;

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MembershipFunction()
        {
        }

        /// <param name="name">The name that identificates the membership function.</param>
        public MembershipFunction(string name)
        {
            this.Name = name;
        }

        /// <param name="name">The name that identificates the linguistic variable.</param>
        /// <param name="x0">The value of the (x0, 0) point.</param>
        /// <param name="x1">The value of the (x1, 1) point.</param>
        /// <param name="x2">The value of the (x2, 1) point.</param>
        /// <param name="x3">The value of the (x3, 0) point.</param>
        public MembershipFunction(string name, double x0, double x1, double x2, double x3)
        {
            this.Name = name;
            this.X0 = x0;
            this.X1 = x1;
            this.X2 = x2;
            this.X3 = x3;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// The name that identificates the membership function.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// The value of the (x0, 0) point.
        /// </summary>
        public double X0
        {
            get { return x0; }
            set { x0 = value; }
        }

        /// <summary>
        /// The value of the (x1, 1) point.
        /// </summary>
        public double X1
        {
            get { return x1; }
            set { x1 = value; }
        }

        /// <summary>
        /// The value of the (x2, 1) point.
        /// </summary>
        public double X2
        {
            get { return x2; }
            set { x2 = value; }
        }

        /// <summary>
        /// The value of the (x3, 0) point.
        /// </summary>
        public double X3
        {
            get { return x3; }
            set { x3 = value; }
        }

        /// <summary>
        /// The value of membership function after evaluation process.
        /// </summary>
        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Calculate the centroid of a trapezoidal membership function.
        /// </summary>
        /// <returns>The value of centroid.</returns>
        public double Centorid()
        {
            double a = this.x2 - this.x1;
            double b = this.x3 - this.x0;
            double c = this.x1 - this.x0;

            return ((2 * a * c) + (a * a) + (c * b) + (a * b) + (b * b)) / (3 * (a + b)) + this.x0;
        }

        /// <summary>
        /// Calculate the area of a trapezoidal membership function.
        /// </summary>
        /// <returns>The value of area.</returns>
        public double Area()
        {
            double a = this.Centorid() - this.x0;
            double b = this.x3 - this.x0;

            return (this.value * (b + (b - (a * this.value)))) / 2;
        }

        #endregion
    }

    public class MembershipFunctionCollection : Collection<MembershipFunction>
    {
        #region Public Methods

        /// <summary>
        /// Finds a linguistic variable in a collection.
        /// </summary>
        /// <param name="linguisticVariableName">Linguistic variable name.</param>
        /// <returns>The linguistic variable, if founded.</returns>
        public MembershipFunction Find(string linguisticVariableName)
        {
            MembershipFunction linguisticVariable = null;

            foreach (MembershipFunction variable in this)
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
