using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Models;

namespace xMatrix.ViewModels
{
    public class UserInputViewModel : INotifyPropertyChanged
    {
        private readonly IGoalRepo _repo;
        private readonly IidService _idService;
        private List<string> _goalTypes = new List<string>() { GoalType.LongTerm, GoalType.Monthly, GoalType.OneYear, GoalType.ShortTerm };

        public List<string> GoalTypes
        {
            get { return _goalTypes; }
            set { _goalTypes = value; }
        }

        private List<Goal> _goals;

        public List<Goal> Goals
        {
            get { return _goals; }
            set { _goals = value; }
        }

        private List<Goal> _allRelatedGoals;

        public List<Goal> AllRelatedGoals
        {
            get { return _allRelatedGoals; }
            set
            {
                _allRelatedGoals = value;
                OnPropertyChanged();
            }
        }

        private List<Goal> _relatedGoals;

        public List<Goal> RelatedGoals
        {
            get { return _relatedGoals; }
            set { _relatedGoals = value; }
        }

        private Goal _selectedGoal;

        public Goal SelectedGoal
        {
            get { return _selectedGoal; }
            set
            {
                _selectedGoal = value;
                UpdateAllRelatedGoals();
            }
        }

        private Goal _selectedAllRelatedGoal;

        public Goal SelectedAllRelatedGoal
        {
            get { return _selectedAllRelatedGoal; }
            set { _selectedAllRelatedGoal = value; }
        }

        private Goal _selectedRelatedGoal;

        public Goal SelectedRelatedGoal
        {
            get { return _selectedRelatedGoal; }
            set { _selectedRelatedGoal = value; }
        }

        private string _newGoalName;

        public string NewGoalName
        {
            get { return _newGoalName; }
            set { _newGoalName = value; }
        }

        private string _newGoalType;

        public event PropertyChangedEventHandler PropertyChanged;

        public string NewGoalType
        {
            get { return _newGoalType; }
            set { _newGoalType = value; }
        }

        public ICommand AddNewGoal { get; set; }
        public ICommand AddRelatedGoal { get; set; }
        public ICommand RemoveRelatedGoal { get; set; }

        public UserInputViewModel(IGoalRepo repo, IidService idService)
        {
            _repo = repo;
            _idService = idService;
            AddNewGoal = new DelegateCommand(ExcecuteAddNewGoal, CanExcecuteAddNewGoal);
            AddRelatedGoal = new DelegateCommand(ExcecuteAddRelatedGoal, CanExcecuteAddRelatedGoal);
            RemoveRelatedGoal = new DelegateCommand(ExcecuteRemoveRelatedGoal, CanExcecuteRemoveRelatedGoal);
            Goals = _repo.GetAllGoals();
        }

        private bool CanExcecuteRemoveRelatedGoal()
        {
            if (SelectedRelatedGoal != null)
            {
                return false;
            }
            return true;
        }

        private void ExcecuteRemoveRelatedGoal()
        {

        }

        private bool CanExcecuteAddRelatedGoal()
        {
            if (SelectedAllRelatedGoal == null)
            {
                return false;
            }
            return true;
        }

        private void ExcecuteAddRelatedGoal()
        {
            
        }

        private bool CanExcecuteAddNewGoal()
        {
            if (string.IsNullOrWhiteSpace(NewGoalName))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(NewGoalType))
            {
                return false;
            }
            return true;
        }

        private void ExcecuteAddNewGoal()
        {
            var newGoal = new Goal();
            newGoal.Id = _idService.GetFreeId(Goals);
            newGoal.Name = NewGoalName;
            newGoal.GoalType = NewGoalType;
            Goals.Add(newGoal);
            _repo.SaveGoals(Goals);
        }

        private void UpdateAllRelatedGoals()
        {
            switch (SelectedGoal.GoalType)
            {
                case GoalType.LongTerm:
                    AllRelatedGoals = Goals.Where(x => x.GoalType == GoalType.OneYear).ToList();
                    break;
                case GoalType.OneYear:
                    AllRelatedGoals = Goals.Where(x => x.GoalType == GoalType.ShortTerm).ToList();
                    break;
                case GoalType.ShortTerm:
                    AllRelatedGoals = Goals.Where(x => x.GoalType == GoalType.Monthly).ToList();
                    break;
                case GoalType.Monthly:
                    AllRelatedGoals = new List<Goal>();
                    break;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
