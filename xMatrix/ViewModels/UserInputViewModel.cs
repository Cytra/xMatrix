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
        private List<string> _goalTypes = new List<string>() { GoalType.LongTerm, GoalType.InitiativesOne, GoalType.OneYear, GoalType.ShortTerm };

        public List<string> GoalTypes
        {
            get { return _goalTypes; }
            set { _goalTypes = value; }
        }

        private List<Goal> _goals = new List<Goal>();

        public List<Goal> Goals
        {
            get { return _goals; }
            set
            {
                _goals = value;
                OnPropertyChanged(nameof(Goals));
            }
        }

        private List<Goal> _allRelatedGoals = new List<Goal>();

        public List<Goal> AllRelatedGoals
        {
            get { return _allRelatedGoals; }
            set
            {
                _allRelatedGoals = value;
                OnPropertyChanged(nameof(AllRelatedGoals));
            }
        }


        private List<Goal> _relatedGoals = new List<Goal>();

        public List<Goal> RelatedGoals
        {
            get { return _relatedGoals; }
            set
            {
                _relatedGoals = value;
                OnPropertyChanged(nameof(RelatedGoals));
            }
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

        private Goal _selectedRemoveRelatedGoal;

        public Goal SelectedRemoveRelatedGoal
        {
            get { return _selectedRemoveRelatedGoal; }
            set { _selectedRemoveRelatedGoal = value; }
        }


        private string _newGoalName;

        public string NewGoalName
        {
            get { return _newGoalName; }
            set
            {
                _newGoalName = value;
                OnPropertyChanged(nameof(NewGoalName));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private string _newGoalType;

        public string NewGoalType
        {
            get { return _newGoalType; }
            set
            {
                _newGoalType = value;
                OnPropertyChanged(nameof(NewGoalName));
            }
        }

        public ICommand AddNewGoal { get; set; }
        public ICommand AddRelatedGoal { get; set; }
        public ICommand RemoveRelatedGoal { get; set; }
        public ICommand DeleteGoal { get; set; }

        public UserInputViewModel(IGoalRepo repo, IidService idService)
        {
            _repo = repo;
            _idService = idService;
            AddNewGoal = new DelegateCommand(ExcecuteAddNewGoal, CanExcecuteAddNewGoal);
            AddRelatedGoal = new DelegateCommand(ExcecuteAddRelatedGoal, CanExcecuteAddRelatedGoal);
            RemoveRelatedGoal = new DelegateCommand(ExcecuteRemoveRelatedGoal, CanExcecuteRemoveRelatedGoal);
            DeleteGoal = new DelegateCommand(ExcecuteDeleteGoal, CanExcecuteDeleteGoal);
            Goals = _repo.GetAllGoals();
        }

        private void SubscribeToRepoEvent()
        {

        }

        private bool CanExcecuteDeleteGoal()
        {
            if (SelectedGoal == null)
            {
                return false;
            }
            return true;
        }

        private void ExcecuteDeleteGoal()
        {

            foreach(var goal in Goals)
            {
                goal.Relates.Remove(SelectedGoal.Id);
            }
            Goals.Remove(SelectedGoal);

            _repo.SaveGoals(Goals);
            Goals = _repo.GetAllGoals();
        }

        private bool CanExcecuteRemoveRelatedGoal()
        {
            if (SelectedRemoveRelatedGoal == null)
            {
                return false;
            }
            return true;
        }

        private void ExcecuteRemoveRelatedGoal()
        {
            SelectedGoal.Relates.Remove(SelectedRemoveRelatedGoal.Id);
            _repo.SaveGoals(Goals);
            Goals = _repo.GetAllGoals();
        }

        private bool CanExcecuteAddRelatedGoal()
        {
            if (SelectedRelatedGoal == null)
            {
                return false;
            }
            return true;
        }

        private void ExcecuteAddRelatedGoal()
        {
            SelectedGoal.Relates.Add(SelectedRelatedGoal.Id);
            _repo.SaveGoals(Goals);
            Goals = _repo.GetAllGoals();
            AllRelatedGoals = new List<Goal>();
            RelatedGoals = new List<Goal>();
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
            Goals = _repo.GetAllGoals();
            NewGoalName = "";
        }

        private void UpdateAllRelatedGoals()
        {
            if (SelectedGoal == null)
            {
                AllRelatedGoals = new List<Goal>();
            }
            else
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
                        AllRelatedGoals = Goals.Where(x => x.GoalType == GoalType.InitiativesOne).ToList();
                        break;
                    case GoalType.InitiativesOne:
                        AllRelatedGoals = new List<Goal>();
                        break;
                }

                SelectedGoal.Relates.ForEach(x => AllRelatedGoals.RemoveAll(y => y.Id == x));
                var relatedGoals = new List<Goal>();
                SelectedGoal.Relates.ForEach(x => relatedGoals.Add(Goals.FirstOrDefault(y => y.Id == x)));
                RelatedGoals = relatedGoals;
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
