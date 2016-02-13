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
        private FuzzyLogicRuleBook rulebook;
        #endregion

        #region Properties
        public FuzzyLogicRuleBook Rules
        {
            get { return rulebook; }
            set { rulebook = value; }
        }

        #endregion

        #region Constructor

        #endregion


    }

    /// <summary>
    /// 
    /// </summary>
    class LinguisticVariable
    {

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

    /// <summary>
    /// 
    /// </summary>
    class FuzzyLogicRule
    {
        #region Variables
        private string rule = string.Empty;
        private double value = 0;
        #endregion

        #region Properties
        public string Rule
        {
            get { return rule; }
            set { rule = value; }
        }

        public double Value
        {
            get { return value; }
            set { this.value = value; }
        }
        #endregion

        #region Constructor
        public FuzzyLogicRule(string ruletext)
        {
            this.rule = ruletext;
        }
        public FuzzyLogicRule(string ruletext, double value)
        {
            this.rule = ruletext;
            this.value = value;
        }
        #endregion
    }

}
