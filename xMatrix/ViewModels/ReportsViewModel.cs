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
        private readonly IDepartmentRepo _deporepo;
        private List<Goal> _goals = new List<Goal>();
        private List<Department> _departments = new List<Department>();

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
            IDepartmentRepo deporepo)
        {
            _repo = repo;
            _deporepo = deporepo;
            _goals = _repo.GetAllGoals();
            _departments = _deporepo.GetAllDepartments();
            _repo.NewData += OnNewRepoData;
            LevelOneReport = GenerateLevelOneReport();
            LevelTwoReport = GenerateLevelTwoReport();
            LevelThreeReport = GenerateLevelThreeReport();
        }

        private List<ReportItem> GenerateLevelThreeReport()
        {
            var result = new List<ReportItem>();
            var departments = _departments;
            foreach (var department in departments)
            {
                foreach (var initiativeOne in _goals.Where(x => x.GoalType == GoalType.InitiativesThree))
                {
                    if (initiativeOne.RelatedDepartments.Contains(department.Id))
                    {
                        var reportItem = new ReportItem();
                        reportItem.Department = department;
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
            var departments = _departments;
            foreach (var department in departments)
            {
                foreach (var initiativeOne in _goals.Where(x => x.GoalType == GoalType.InitiativesTwo))
                {
                    if (initiativeOne.RelatedDepartments.Contains(department.Id))
                    {
                        var reportItem = new ReportItem();
                        reportItem.Department = department;
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
            var departments = _departments;
            foreach (var department in departments)
            {
                foreach (var initiativeOne in _goals.Where(x => x.GoalType == GoalType.InitiativesOne))
                {
                    if (initiativeOne.RelatedDepartments.Contains(department.Id))
                    {
                        var reportItem = new ReportItem();
                        reportItem.Department = department;
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
