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
