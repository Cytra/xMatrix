using System;
using System.Collections.Generic;
using System.Text;
using xMatrix.Core.Models;

namespace xMatrix.Core.Interfaces
{
    public interface IGoalRepo
    {
        List<Goal> GetAllGoals();
        void SaveGoals(List<Goal> goals);

        event EventHandler<RepoEventArgs> NewData;
    }
}
