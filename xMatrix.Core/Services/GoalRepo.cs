using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Models;

namespace xMatrix.Core.Services
{
    public class GoalRepo : IGoalRepo
    {

        private const string _goalListName = "Goals";
        private readonly string _fileLocation;
        public GoalRepo()
        {
            _fileLocation = $"{AppDomain.CurrentDomain.BaseDirectory}/{_goalListName}.json";
        }

        public List<Goal> GetAllGoals()
        {
            var result = new List<Goal>();
            if (File.Exists(_fileLocation))
            {
                var goalString = File.ReadAllText(_fileLocation);
                result = JsonConvert.DeserializeObject<List<Goal>>(goalString);
            }
            return result;
        }

        public void SaveGoals(List<Goal> goals)
        {
            File.WriteAllText(_fileLocation, JsonConvert.SerializeObject(goals));
            var repoEventArgs = new RepoEventArgs();
            repoEventArgs.Goals = goals;
            OnNewData(repoEventArgs);
        }

        protected virtual void OnNewData(RepoEventArgs e)
        {
            EventHandler<RepoEventArgs> handler = NewData;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public event EventHandler<RepoEventArgs> NewData;
    }
}
