using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using xMatrix.Core.Interfaces;
using xMatrix.Core.Models;

namespace xMatrix.ViewModels
{
    public class LevelOneMatrixViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private readonly IGoalRepo _repo;
        private readonly ILevelOneMatrixService _levelOneMatrixService;


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

        public LevelOneMatrixViewModel(IGoalRepo repo, ILevelOneMatrixService levelOneMatrixService)
        {
            _repo = repo;
            _levelOneMatrixService = levelOneMatrixService;
            _repo.NewData += OnNewRepoData;
            GoalList = _repo.GetAllGoals();
            RectItems = _levelOneMatrixService.GenerateRectList(GoalList);
            Polygons = _levelOneMatrixService.GeneratePolygonList(GoalList);
        }

        public void OnNewRepoData(object sender, RepoEventArgs eventArgs)
        {

            GoalList = eventArgs.Goals;
            RectItems = null;
            Polygons = null;
            RectItems = new List<RectItem>();
            Polygons = new List<Polygon>();
            RectItems = _levelOneMatrixService.GenerateRectList(GoalList);
            Polygons = _levelOneMatrixService.GeneratePolygonList(GoalList);
        }

        protected virtual void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
