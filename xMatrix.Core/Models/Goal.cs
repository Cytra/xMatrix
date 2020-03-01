using System;
using System.Collections.Generic;
using System.Text;

namespace xMatrix.Core.Models
{
    public class Goal : ModelBase
    {
        public string GoalType { get; set; }
        public bool Deleted { get; set; }
        public List<int> RelatesGoals { get; set; } = new List<int>();
        public List<int> RelatesPerson { get; set; } = new List<int>();
        public List<int> RelatedDepartments { get; set; } = new List<int>();
    }
}
