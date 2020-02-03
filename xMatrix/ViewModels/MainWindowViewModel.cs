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
            if (snackbarMessageQueue == null)
                throw new ArgumentNullException(nameof(snackbarMessageQueue));

            return new ObservableCollection<DemoItem>
            {

               new DemoItem("Level 1", new LevelOneMatrix { DataContext = new LevelOneMatrixViewModel() } ,
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

               new DemoItem("Level 2", new LevelTwoMatrix { DataContext = new LevelTwoMatrixViewModel() } ,
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

               new DemoItem("Input", new UserInput { DataContext = new UserInputViewModel() } ,
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