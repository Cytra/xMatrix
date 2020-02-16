using System.Configuration;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System.Windows.Controls;
using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;
using xMatrix.Models;
using xMatrix.UserControls;
using xMatrix.Core.Services;
using xMatrix.Core.Models;

namespace xMatrix.ViewModels
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel(ISnackbarMessageQueue snackbarMessageQueue)
        {
            _allItems = GenerateDemoItems(snackbarMessageQueue);
            FilterItems(null);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private string _searchKeyword;
        private ObservableCollection<DemoItem> _allItems;
        private ObservableCollection<DemoItem> _demoItems;
        private DemoItem _selectedItem;


        public string SearchKeyword
        {
            get => _searchKeyword;
            set
            {
                _searchKeyword = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DemoItems)));
                FilterItems(_searchKeyword);
            }
        }

        public ObservableCollection<DemoItem> DemoItems
        {
            get => _demoItems;
            set
            {
                _demoItems = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(DemoItems)));
            }
        }

        public DemoItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                if (value == null || value.Equals(_selectedItem)) return;

                _selectedItem = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SelectedItem)));
            }
        }


        private ObservableCollection<DemoItem> GenerateDemoItems(ISnackbarMessageQueue snackbarMessageQueue)
        {
            var repo = new GoalRepo();

            if (snackbarMessageQueue == null)
                throw new ArgumentNullException(nameof(snackbarMessageQueue));

            return new ObservableCollection<DemoItem>
            {

               new DemoItem("Matrix", new LevelOneMatrix { DataContext = new LevelOneMatrixViewModel( 
                   repo, 
                   new LevelOneMatrixService(
                       new MatrixService(new MatrixGridService(), GoalType.LongTerm, GoalType.OneYear, GoalType.ShortTerm, GoalType.Monthly), 
                           new MatrixGridService()), 
                           new IdService()) } ,
                    new []
                    {
                        DocumentationLink.WikiLink("Button-Styles", "Buttons"),
                        DocumentationLink.StyleLink("Button"),
                        DocumentationLink.StyleLink("PopupBox"),
                        DocumentationLink.ApiLink<PopupBox>()
                    })
                    {
                        VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
                    },

               //new DemoItem("Level 2", new LevelTwoMatrix { DataContext = new LevelTwoMatrixViewModel() } ,
               //     new []
               //     {
               //         DocumentationLink.WikiLink("Button-Styles", "Buttons"),
               //         DocumentationLink.StyleLink("Button"),
               //         DocumentationLink.StyleLink("PopupBox"),
               //         DocumentationLink.ApiLink<PopupBox>()
               //     })
               //     {
               //         VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
               //     },

               //new DemoItem("Input", new UserInput { DataContext = new UserInputViewModel(repo, new IdService()) } ,
               //     new []
               //     {
               //         DocumentationLink.WikiLink("Button-Styles", "Buttons"),
               //         DocumentationLink.StyleLink("Button"),
               //         DocumentationLink.StyleLink("PopupBox"),
               //         DocumentationLink.ApiLink<PopupBox>()
               //     })
               //     {
               //         VerticalScrollBarVisibilityRequirement = ScrollBarVisibility.Auto
               //     },

            };
        }

        private void FilterItems(string keyword)
        {
            var filteredItems =
                string.IsNullOrWhiteSpace(keyword) ?
                _allItems :
                _allItems.Where(i => i.Name.ToLower().Contains(keyword.ToLower()));

            DemoItems = new ObservableCollection<DemoItem>(filteredItems);
        }
    }
}