using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Models;

namespace xMatrix.ViewModels
{
    public class LevelOneMatrixViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IGoalRepo _repo;
        private readonly IPersonRepo _personRepo;
        private readonly ILevelMatrixService _levelOneMatrixService;
        private readonly ILevelMatrixService _leveltwoMatrixService;
        private readonly ILevelMatrixService _levelTreeMatrixService;
        private List<RectItem> _rectItems = new List<RectItem>();

        public List<RectItem> RectItems
        {
            get { return _rectItems; }
            set
            {
                _rectItems = value;
                OnPropertyChanged(nameof(RectItems));
            }
        }

        private List<Polygon> _polygons = new List<Polygon>();

        public List<Polygon> Polygons
        {
            get { return _polygons; }
            set
            {
                _polygons = value;
                OnPropertyChanged(nameof(Polygons));
            }
        }

        public List<Goal> GoalList { get; set; } = new List<Goal>();

        private readonly IidService _idService;
        private List<string> _goalTypes = new List<string>() {
            GoalType.LongTerm,
            GoalType.OneYear,
            GoalType.ShortTerm,
            GoalType.InitiativesOne,
            GoalType.InitiativesTwo,
            GoalType.InitiativesThree};

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

        private List<Person> _people = new List<Person>();

        public List<Person> People
        {
            get { return _people; }
            set
            {
                _people = value;
                OnPropertyChanged(nameof(People));
            }
        }

        private Person _selectedPerson;

        public Person SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                _selectedPerson = value;
                OnPropertyChanged(nameof(SelectedPerson));
                UpdateAllRelatedPeople();
            }
        }

        private List<Person> _allRelatedPeople = new List<Person>();

        public List<Person> AllRelatedPeople
        {
            get { return _allRelatedPeople; }
            set
            {
                _allRelatedPeople = value;
                OnPropertyChanged(nameof(AllRelatedPeople));
            }
        }

        private Person _selectedRelatedPerson;

        public Person SelectedRelatedPerson
        {
            get { return _selectedRelatedPerson; }
            set
            {
                _selectedRelatedPerson = value;
                OnPropertyChanged(nameof(SelectedRelatedPerson));
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
                UpdatePeople();
                UpdateAllRelatedPeople();
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

        public List<Person> AllPeople { get; set; } = new List<Person>();

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

        private string _newPersonName;

        public string NewPersonName
        {
            get { return _newPersonName; }
            set
            {
                _newPersonName = value;
                OnPropertyChanged(nameof(NewPersonName));
            }
        }


        public ICommand AddNewGoal { get; set; }
        public ICommand AddRelatedGoal { get; set; }
        public ICommand RemoveRelatedGoal { get; set; }
        public ICommand DeleteGoal { get; set; }
        public ICommand GetLevelOneMatrix { get; set; }
        public ICommand GetLevelTwoMatrix { get; set; }
        public ICommand GetLevelThreeMatrix { get; set; }
        public ICommand AddNewPerson { get; set; }
        public ICommand DeletePerson { get; set; }
        public ICommand AddRelatedPerson { get; set; }

        private string _selectedmatrixLevel;

        public string SelectedmatrixLevel
        {
            get { return _selectedmatrixLevel; }
            set
            {
                _selectedmatrixLevel = value;
            }
        }

        public LevelOneMatrixViewModel(
            IGoalRepo repo,
            IPersonRepo personRepo,
            ILevelMatrixService levelOneMatrixService,
            ILevelMatrixService leveltwoMatrixService,
            ILevelMatrixService levelTreeMatrixService,
            IidService idService)
        {
            SelectedmatrixLevel = "One";
            _repo = repo;
            _personRepo = personRepo;
            _levelOneMatrixService = levelOneMatrixService;
            _leveltwoMatrixService = leveltwoMatrixService;
            _levelTreeMatrixService = levelTreeMatrixService;
            _repo.NewData += OnNewRepoData;


            _idService = idService;
            AddNewGoal = new DelegateCommand(ExcecuteAddNewGoal, CanExcecuteAddNewGoal);
            AddRelatedGoal = new DelegateCommand(ExcecuteAddRelatedGoal, CanExcecuteAddRelatedGoal);
            RemoveRelatedGoal = new DelegateCommand(ExcecuteRemoveRelatedGoal, CanExcecuteRemoveRelatedGoal);
            DeleteGoal = new DelegateCommand(ExcecuteDeleteGoal, CanExcecuteDeleteGoal);
            DeletePerson = new DelegateCommand(ExcecuteDeletePerson, CanExcecuteDeletePerson);
            AddNewPerson = new DelegateCommand(ExcecuteAddNewPerson, CanExcecuteAddNewPerson);
            AddRelatedPerson = new DelegateCommand(ExcecuteAddRelatedPerson, CanExcecuteAddRelatedPerson);
            GetLevelOneMatrix = new DelegateCommand(ExcecuteGetLevelOneMatrix);
            GetLevelTwoMatrix = new DelegateCommand(ExcecuteGetLevelTwoMatrix);
            GetLevelThreeMatrix = new DelegateCommand(ExcecuteGetLevelThreeMatrix);
            Goals = _repo.GetAllGoals();
            GoalList = _repo.GetAllGoals();
            UpdatePeople();
            AllPeople = _personRepo.GetAllPeople();
            People = AllPeople;
            UpdateCanvas();
        }

        private void UpdatePeople()
        {
            AllPeople = _personRepo.GetAllPeople();
            People = _personRepo.GetAllPeople();
        }

        private bool CanExcecuteAddRelatedPerson()
        {
            if(SelectedPerson == null)
            {
                return false;
            }
            if(SelectedGoal == null)
            {
                return false;
            }
            return true;
        }

        private void ExcecuteAddRelatedPerson()
        {
            SelectedGoal.RelatesPerson.Add(SelectedPerson.Id);
            _repo.SaveGoals(Goals);
            Goals = _repo.GetAllGoals();
            UpdateAllRelatedPeople();
            People = new List<Person>();
        }

        private bool CanExcecuteAddNewPerson()
        {
            if (string.IsNullOrWhiteSpace(NewPersonName))
            {
                return false;
            }
            return true;
        }

        private void ExcecuteAddNewPerson()
        {
            var newPerson = new Person();
            newPerson.Id = _idService.GetFreeId(People);
            newPerson.Name = NewPersonName;
            NewPersonName = "";
            People.Add(newPerson);
            _personRepo.SavePeople(People);
            AllPeople = _personRepo.GetAllPeople();
            People = AllPeople;
        }

        private bool CanExcecuteDeletePerson()
        {
            if (SelectedPerson != null)
            {
                return true;
            }
            return false;
        }

        private void ExcecuteDeletePerson()
        {
            foreach (var goal in Goals)
            {
                goal.RelatesPerson.RemoveAll(x=> x == SelectedPerson.Id);
            }

            _repo.SaveGoals(Goals);
            People.Remove(SelectedPerson);
            _personRepo.SavePeople(People);
            AllPeople = _personRepo.GetAllPeople();
            People = AllPeople;
        }

        private void UpdateCanvas()
        {

            switch (SelectedmatrixLevel)
            {
                case "One":
                    RectItems = _levelOneMatrixService.GenerateRectList(GoalList);
                    Polygons = _levelOneMatrixService.GeneratePolygonList(GoalList);
                    break;
                case "Two":
                    RectItems = _leveltwoMatrixService.GenerateRectList(GoalList);
                    Polygons = _leveltwoMatrixService.GeneratePolygonList(GoalList);
                    break;
                case "Three":
                    RectItems = _levelTreeMatrixService.GenerateRectList(GoalList);
                    Polygons = _levelTreeMatrixService.GeneratePolygonList(GoalList);
                    break;
            }
        }

        private void ExcecuteGetLevelThreeMatrix()
        {
            SelectedmatrixLevel = "Three";
            UpdateCanvas();
        }

        private void ExcecuteGetLevelTwoMatrix()
        {
            SelectedmatrixLevel = "Two";
            UpdateCanvas();
        }

        private void ExcecuteGetLevelOneMatrix()
        {
            SelectedmatrixLevel = "One";
            UpdateCanvas();
        }

        public void OnNewRepoData(object sender, RepoEventArgs eventArgs)
        {
            GoalList = eventArgs.Goals;
            Goals = eventArgs.Goals;
            RectItems = new List<RectItem>();
            Polygons = new List<Polygon>();
            RectItems = _levelOneMatrixService.GenerateRectList(GoalList);
            Polygons = _levelOneMatrixService.GeneratePolygonList(GoalList);
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

            foreach (var goal in Goals)
            {
                goal.RelatesGoals.Remove(SelectedGoal.Id);
            }
            Goals.Remove(SelectedGoal);

            _repo.SaveGoals(Goals);
            Goals = _repo.GetAllGoals();
        }

        private bool CanExcecuteRemoveRelatedGoal()
        {
            if (SelectedRelatedPerson == null)
            {
                return false;
            }
            return true;
        }

        private void ExcecuteRemoveRelatedGoal()
        {
            SelectedGoal.RelatesPerson.Remove(SelectedRelatedPerson.Id);
            _repo.SaveGoals(Goals);
            Goals = _repo.GetAllGoals();
            AllRelatedPeople = new List<Person>();
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
            SelectedGoal.RelatesGoals.Add(SelectedRelatedGoal.Id);
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
                        AllRelatedGoals = Goals.Where(x => x.GoalType == GoalType.InitiativesTwo).ToList();
                        break;
                    case GoalType.InitiativesTwo:
                        AllRelatedGoals = Goals.Where(x => x.GoalType == GoalType.InitiativesThree).ToList();
                        break;
                    case GoalType.InitiativesThree:
                        AllRelatedGoals = new List<Goal>();
                        break;
                }

                SelectedGoal.RelatesGoals.ForEach(x => AllRelatedGoals.RemoveAll(y => y.Id == x));
                var relatedGoals = new List<Goal>();
                SelectedGoal.RelatesGoals.ForEach(x => relatedGoals.Add(Goals.FirstOrDefault(y => y.Id == x)));
                RelatedGoals = relatedGoals;
            }
        }

        private void UpdateAllRelatedPeople()
        {
            if(SelectedGoal != null)
            {
                var allpeople = AllPeople;
                var people = People;
                var allRelatedPeople = new List<Person>();

                SelectedGoal.RelatesPerson.ForEach(x => people.RemoveAll(y => y.Id == x));
                SelectedGoal.RelatesPerson.ForEach(x => allRelatedPeople.Add(allpeople.FirstOrDefault(y => y.Id == x)));

                People = people;
                AllRelatedPeople = allRelatedPeople;
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
