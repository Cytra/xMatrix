using System;
using System.Collections.Generic;
using System.Text;

namespace xMatrix.Core.Models
{
    public class Goal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string GoalType { get; set; }
        public List<int> Relates { get; set; } = new List<int>();
    }
}
