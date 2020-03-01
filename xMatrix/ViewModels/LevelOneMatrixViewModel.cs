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

        private readonly IDepartmentRepo _depoRepo;
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
                UpdatePeople();
                UpdateAllRelatedGoals();
                UpdateAllRelatedPeople();
                UpdateDepartments();
                UpdateAllRelatedDepartment();
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

        private List<Department> _allDepartments = new List<Department>();

        public List<Department> AllDepartments
        {
            get { return _allDepartments; }
            set
            {
                _allDepartments = value;
                OnPropertyChanged(nameof(AllDepartments));
            }
        }

        private List<Department> _departments = new List<Department>();

        public List<Department> Departments
        {
            get { return _departments; }
            set
            {
                _departments = value;
                OnPropertyChanged(nameof(Departments));
            }
        }

        private List<Department> _relatedDepartment = new List<Department>();

        public List<Department> RelatedDepartment
        {
            get { return _relatedDepartment; }
            set
            {
                _relatedDepartment = value;
                OnPropertyChanged(nameof(RelatedDepartment));
            }
        }

        private Department _selectedRelatedDepartment;

        public Department SelectedRelatedDepartment
        {
            get { return _selectedRelatedDepartment; }
            set
            {
                _selectedRelatedDepartment = value;
                OnPropertyChanged(nameof(SelectedRelatedDepartment));
            }
        }

        private Department _selectedDepartment;

        public Department SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                _selectedDepartment = value;
                OnPropertyChanged(nameof(SelectedDepartment));
            }
        }

        private Department _selectedLevelTwoDepartment;

        public Department SelectedLevelTwoDepartment
        {
            get { return _selectedLevelTwoDepartment; }
            set
            {
                _selectedLevelTwoDepartment = value;
                OnPropertyChanged(nameof(SelectedLevelTwoDepartment));
            }
        }

        private Department _selectedLevelThreeDepartment;

        public Department SelectedLevelThreeDepartment
        {
            get { return _selectedLevelThreeDepartment; }
            set
            {
                _selectedLevelThreeDepartment = value;
                OnPropertyChanged(nameof(SelectedLevelThreeDepartment));
            }
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

        private string _newDepartmentName;

        public string NewDepartmentName
        {
            get { return _newDepartmentName; }
            set
            {
                _newDepartmentName = value;
                OnPropertyChanged(nameof(NewDepartmentName));
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
        public ICommand AddNewDepartment { get; set; }
        public ICommand RemoveRelatedDepartment { get; set; }
        public ICommand AddRelatedDepartment { get; set; }
        public ICommand DeleteDepartments { get; set; }

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
            IDepartmentRepo depoRepo,
            IGoalRepo repo,
            IPersonRepo personRepo,
            ILevelMatrixService levelOneMatrixService,
            ILevelMatrixService leveltwoMatrixService,
            ILevelMatrixService levelTreeMatrixService,
            IidService idService)
        {
            SelectedmatrixLevel = "One";
            _depoRepo = depoRepo;
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
            GetLevelTwoMatrix = new DelegateCommand(ExcecuteGetLevelTwoMatrix, CanExcecuteGetLevelTwoMatrix);
            GetLevelThreeMatrix = new DelegateCommand(ExcecuteGetLevelThreeMatrix, CanExcecuteGetLevelThreeMatrix);
            AddNewDepartment = new DelegateCommand(ExcecuteAddNewDepartment, CanExcecuteAddNewDepartment);
            RemoveRelatedDepartment = new DelegateCommand(ExcecuteRemoveRelatedDepartment, CanExcecuteRemoveRelatedDepartment);
            AddRelatedDepartment = new DelegateCommand(ExcecuteAddRelatedDepartment, CanExcecuteAddRelatedDepartment);
            DeleteDepartments = new DelegateCommand(ExcecuteDeleteDepartments, CanExcecuteDeleteDepartments);
            UpdateDepartments();
            Goals = _repo.GetAllGoals();
            GoalList = _repo.GetAllGoals();
            UpdatePeople();
            UpdateCanvas();
        }

        private bool CanExcecuteDeleteDepartments()
        {
            if (SelectedDepartment == null)
            {
                return false;
            }
            return true;
        }

        private void ExcecuteDeleteDepartments()
        {
            AllDepartments = _depoRepo.GetAllDepartments();
            foreach (var goal in Goals)
            {
                goal.RelatedDepartments.RemoveAll(x => x == SelectedGoal.Id);
            }
            AllDepartments.Single(x => x.Id == SelectedDepartment.Id).Deleted = true;
            _depoRepo.SaveDepartments(AllDepartments);
            AllDepartments = _depoRepo.GetAllDepartments();

            //AllDepartments = _depoRepo.GetAllDepartments();
            //AllDepartments.Single(x => x.Id == SelectedGoal.DepartmentId).Deleted = true;
            //_depoRepo.SaveDepartments(AllDepartments);
            //AllDepartments = _depoRepo.GetAllDepartments();
        }

        private bool CanExcecuteAddRelatedDepartment()
        {
            if (SelectedDepartment == null)
            {
                return false;
            }
            if (SelectedGoal == null)
            {
                return false;
            }
            return true;
        }

        private void ExcecuteAddRelatedDepartment()
        {
            Goals.Single(x => x.Id == SelectedGoal.Id).RelatedDepartments.Add(SelectedDepartment.Id);
            _repo.SaveGoals(Goals);
            UpdateDepartments();
            UpdateAllRelatedDepartment();
        }

        private void UpdateDepartments()
        {
            AllDepartments = _depoRepo.GetAllDepartments();
        }

        private void UpdateAllRelatedDepartment()
        {
            RelatedDepartment = new List<Department>();
            Departments = new List<Department>();
            if (SelectedGoal.GoalType == GoalType.ShortTerm)
            {
                Departments = AllDepartments;
                foreach (var departmentId in SelectedGoal.RelatedDepartments)
                {
                    RelatedDepartment.Add(AllDepartments.Single(x => x.Id == departmentId));
                    Departments.RemoveAll(x => x.Id == departmentId);
                }
            }
        }

        private bool CanExcecuteRemoveRelatedDepartment()
        {
            if (SelectedRelatedDepartment == null)
            {
                return false;
            }
            if (SelectedGoal == null)
            {
                return false;
            }
            return true;
        }

        private void ExcecuteRemoveRelatedDepartment()
        {
            AllDepartments = _depoRepo.GetAllDepartments();
            foreach (var goal in Goals)
            {
                goal.RelatedDepartments.RemoveAll(x => x == SelectedRelatedDepartment.Id);
            }
            AllDepartments.Single(x => x.Id == SelectedRelatedDepartment.Id).Deleted = true;
            _depoRepo.SaveDepartments(AllDepartments);
            RelatedDepartment = new List<Department>();
            SelectedRelatedDepartment = null;
            UpdateDepartments();
            UpdateAllRelatedDepartment();
        }

        private bool CanExcecuteAddNewDepartment()
        {
            if (string.IsNullOrWhiteSpace(NewDepartmentName))
            {
                return false;
            }
            return true;
        }

        private void ExcecuteAddNewDepartment()
        {
            var department = new Department();
            department.Id = _idService.GetFreeId(AllDepartments);
            department.Deleted = false;
            department.Name = NewDepartmentName;
            NewDepartmentName = "";
            AllDepartments.Add(department);
            _depoRepo.SaveDepartments(AllDepartments);
            UpdateDepartments();
        }

        private void UpdatePeople()
        {
            AllPeople = _personRepo.GetAllPeople();
            People = AllPeople;
        }

        private bool CanExcecuteAddRelatedPerson()
        {
            if (SelectedPerson == null)
            {
                return false;
            }
            if (SelectedGoal == null)
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
            newPerson.Deleted = false;
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
                goal.RelatesPerson.RemoveAll(x => x == SelectedPerson.Id);
            }

            People.Single(x => x.Id == SelectedPerson.Id).Deleted = true;
            _repo.SaveGoals(Goals);
            _personRepo.SavePeople(People);
            AllPeople = People;
        }

        private void UpdateCanvas()
        {

            switch (SelectedmatrixLevel)
            {
                case "One":
                    RectItems = _levelOneMatrixService.GenerateRectList(GoalList, AllDepartments, AllPeople, null);
                    Polygons = _levelOneMatrixService.GeneratePolygonList(GoalList, null);
                    break;
                case "Two":
                    RectItems = _leveltwoMatrixService.GenerateRectList(GoalList, AllDepartments, AllPeople, SelectedLevelTwoDepartment);
                    Polygons = _leveltwoMatrixService.GeneratePolygonList(GoalList, SelectedLevelTwoDepartment);
                    break;
                case "Three":
                    RectItems = _levelTreeMatrixService.GenerateRectList(GoalList,AllDepartments, AllPeople, SelectedLevelThreeDepartment);
                    Polygons = _levelTreeMatrixService.GeneratePolygonList(GoalList, SelectedLevelThreeDepartment);
                    break;
            }
        }

        private void ExcecuteGetLevelThreeMatrix()
        {
            SelectedmatrixLevel = "Three";
            UpdateCanvas();
        }

        private bool CanExcecuteGetLevelTwoMatrix()
        {
            if(SelectedLevelTwoDepartment == null)
            {
                return false;
            }
            return true;
        }

        private void ExcecuteGetLevelTwoMatrix()
        {
            SelectedmatrixLevel = "Two";
            UpdateCanvas();
        }

        private bool CanExcecuteGetLevelThreeMatrix()
        {
            if (SelectedLevelThreeDepartment == null)
            {
                return false;
            }
            return true;
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
            RectItems = _levelOneMatrixService.GenerateRectList(GoalList,AllDepartments, AllPeople, null);
            Polygons = _levelOneMatrixService.GeneratePolygonList(GoalList, null);
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
            Goals.Single(x => x.Id == SelectedGoal.Id).Deleted = true;

            _repo.SaveGoals(Goals);
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
            newGoal.Deleted = false;
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
            if (SelectedGoal != null)
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
