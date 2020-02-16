using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Models;

namespace xMatrix.ViewModels
{
    public class ReportsViewModel : INotifyPropertyChanged
    {
        private readonly IGoalRepo _repo;
        private readonly IPersonRepo _personRepo;
        private List<Goal> _goals = new List<Goal>();
        private List<Person> _people = new List<Person>();

        private List<ReportItem> _levelOneReport = new List<ReportItem>();

        public event PropertyChangedEventHandler PropertyChanged;

        public List<ReportItem> LevelOneReport
        {
            get { return _levelOneReport; }
            set
            {
                _levelOneReport = value;
                OnPropertyChanged(nameof(LevelOneReport));
            }
        }

        private List<ReportItem> _levelTwoReport;

        public List<ReportItem> LevelTwoReport
        {
            get { return _levelTwoReport; }
            set
            {
                _levelTwoReport = value;
                OnPropertyChanged(nameof(LevelTwoReport));
            }
        }

        private List<ReportItem> _levelThreeReport;

        public List<ReportItem> LevelThreeReport
        {
            get { return _levelThreeReport; }
            set
            {
                _levelThreeReport = value;
                OnPropertyChanged(nameof(LevelThreeReport));
            }
        }


        public ReportsViewModel(
            IGoalRepo repo,
            IPersonRepo personRepo)
        {
            _repo = repo;
            _personRepo = personRepo;
            _goals = _repo.GetAllGoals();
            _people = _personRepo.GetAllPeople();
            _repo.NewData += OnNewRepoData;
            LevelOneReport = GenerateLevelOneReport();
            LevelTwoReport = GenerateLevelTwoReport();
            LevelThreeReport = GenerateLevelThreeReport();
        }

        private List<ReportItem> GenerateLevelThreeReport()
        {
            var result = new List<ReportItem>();
            var people = _people;
            foreach (var person in people)
            {
                foreach (var initiativeOne in _goals.Where(x => x.GoalType == GoalType.InitiativesThree))
                {
                    if (initiativeOne.RelatesPerson.Contains(person.Id))
                    {
                        var reportItem = new ReportItem();
                        reportItem.Person = person;
                        reportItem.Goal = initiativeOne;
                        result.Add(reportItem);
                    }
                }
            }
            return result;
        }

        private List<ReportItem> GenerateLevelTwoReport()
        {
            var result = new List<ReportItem>();
            var people = _people;
            foreach (var person in people)
            {
                foreach (var initiativeOne in _goals.Where(x => x.GoalType == GoalType.InitiativesTwo))
                {
                    if (initiativeOne.RelatesPerson.Contains(person.Id))
                    {
                        var reportItem = new ReportItem();
                        reportItem.Person = person;
                        reportItem.Goal = initiativeOne;
                        result.Add(reportItem);
                    }
                }
            }
            return result;
        }

        private List<ReportItem> GenerateLevelOneReport()
        {
            var result = new List<ReportItem>();
            var people = _people;
            foreach (var person in people)
            {
                foreach (var initiativeOne in _goals.Where(x => x.GoalType == GoalType.InitiativesOne))
                {
                    if (initiativeOne.RelatesPerson.Contains(person.Id))
                    {
                        var reportItem = new ReportItem();
                        reportItem.Person = person;
                        reportItem.Goal = initiativeOne;
                        result.Add(reportItem);
                    }
                }
            }
            return result;
        }

        private void OnNewRepoData(object sender, RepoEventArgs eventArgs)
        {
            _goals = eventArgs.Goals;
            _people = _personRepo.GetAllPeople();
            LevelOneReport = GenerateLevelOneReport();
            LevelTwoReport = GenerateLevelTwoReport();
            LevelThreeReport = GenerateLevelThreeReport();
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
