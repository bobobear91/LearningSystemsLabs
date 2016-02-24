using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab_assignment_2.Model
{
    /// <summary>
    /// 
    /// </summary>
    class FuzzyLogicRule
    {
        #region Variables
        /// <summary>
        /// 
        /// </summary>
        private string rule = string.Empty;

        /// <summary>
        /// 
        /// </summary>
        private double value = 0;

        /// <summary>
        /// 
        /// </summary>
        private string rulePlainText = string.Empty;
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

        public string RuleText
        {
            get { return rulePlainText; }
            set { rulePlainText = value; }
        }
        #endregion

        #region Constructor
        public FuzzyLogicRule(string ruletext)
        {
            this.rule = Validate(ruletext);
            this.rulePlainText = ruletext;
        }
        public FuzzyLogicRule(string ruletext, double value)
        {
            this.rule = (ruletext);
            this.rulePlainText = ruletext;
            this.value = value;
        }
        #endregion



        #region Public Methods
        /// <summary>
        /// Returns the conditions of the rule. 
        /// The part of the rule between IF and THEN. 
        /// </summary>
        /// <returns>The conditions of the rule.</returns> 
        public string Conditions()
         { 
             return this.rule.Substring(this.rule.IndexOf("IF ") + 3, this.rule.IndexOf(" THEN") - 3); 
         }
        #endregion

        #region Private Methods
        private string Validate(string text)
        {
            //
            int count = 0;
            //Get the first position of the paranthesis
            int position = text.IndexOf("(");
            string[] tokens = text.Replace("(", "").Replace(")", "").Split();

            while (position >= 0)
            {
                count++;
                position = text.IndexOf("(", position + 1);
            }

            //Back checking if all parenthesis matches, equal amount of '(' and ')'
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
}
