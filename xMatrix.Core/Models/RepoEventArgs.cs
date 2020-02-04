using System;
using System.Collections.Generic;
using System.Text;

namespace xMatrix.Core.Models
{
    public class RepoEventArgs : EventArgs
    {
        public List<Goal> Goals { get; set; }
    }
}
