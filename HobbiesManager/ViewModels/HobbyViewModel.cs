using HobbiesManager.Command;
using HobbiesManager.Enums;
using HobbiesManager.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace HobbiesManager.ViewModels
{
    public class HobbyViewModel : ViewModelBase
    {
        public ObservableCollection<Hobby> Hobbies { get; set; }
        public ObservableCollection<string> FilterCategories { get; set; }
        public ObservableCollection<string> NewHobbyCategories { get; set; }
        public CollectionViewSource FilteredHobbies { get; set;}

        private string _newEquipmentItem;
        public string NewEquipmentItem
        {
            get { return _newEquipmentItem; }
            set
            {
                _newEquipmentItem = value;
                RaisePropertyChanged();
            }
        }

        private Hobby _selectedHobby;
        public Hobby SelectedHobby
        {
            get { return _selectedHobby; }
            set
            {
                _selectedHobby = value;
                RaisePropertyChanged();
                RaisePropertyChanged(nameof(SelectedHobbyCategoryDescription));
                RaisePropertyChanged(nameof(SelectedHobbyEquipment));
            }
        }

        private Hobby _newHobby;
        public Hobby NewHobby
        {
            get { return _newHobby; }
            set
            {
                _newHobby = value;
                RaisePropertyChanged();
            }
        }

        private string _selectedCategoryForFilter;
        public string SelectedCategoryForFilter
        {
            get { return _selectedCategoryForFilter; }
            set
            {
                _selectedCategoryForFilter = value;
                RaisePropertyChanged();
                FilterHobbies();
            }
        }

        private object _selectedCategoryForNewHobby;
        public object SelectedCategoryForNewHobby
        {
            get { return _selectedCategoryForNewHobby; }
            set
            {
                _selectedCategoryForNewHobby = value;
                RaisePropertyChanged();
            }
        }

        public ICommand AddHobbyCommand { get; }
        public ICommand RemoveHobbyCommand { get; }
        public ICommand AddEquipmentCommand { get; }

        public HobbyViewModel()
        {
            Hobbies = new ObservableCollection<Hobby>
        {
            new Hobby {
                Name = "Flugfiske", 
                Category = HobbyCategory.Outdoor_and_Adventure,
                Equipment = new ObservableCollection<string> {"Flugspö", "VadarByxor", "Fiskekort" },
                ImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/0/06/Flyfishing.jpg/1280px-Flyfishing.jpg"
            },
            new Hobby {
                Name = "Hiphop", 
                Category = HobbyCategory.Music_and_Dance,
                Equipment = new ObservableCollection<string> {"Bekväma kläder", "Skor"},
                ImageURL = "https://upload.wikimedia.org/wikipedia/commons/e/ed/B_Boy_doing_a_freeze.jpg" 
            },
            new Hobby {
                Name= "Gå på Loppis",
                Category = HobbyCategory.Collecting_and_Antiques,
                 Equipment = new ObservableCollection<string> {"Kontanter", "Sköna skor", "Korg/Vagn"},
                ImageURL = "https://upload.wikimedia.org/wikipedia/commons/thumb/5/51/H%C3%B6torget%2C_loppmarknad%2C_2017b.jpg/1280px-H%C3%B6torget%2C_loppmarknad%2C_2017b.jpg"
            }
        };

            FilterCategories = new ObservableCollection<string> { "Alla kategorier" };
            NewHobbyCategories = new ObservableCollection<string> { "Du måste välja en kategori" };

            foreach (var category in Enum.GetValues(typeof(HobbyCategory)).Cast<HobbyCategory>())
            {
                var tempHobby = new Hobby { Category = category };
                FilterCategories.Add(tempHobby.GetCategoryDescription());
                NewHobbyCategories.Add(tempHobby.GetCategoryDescription());
            }

            FilteredHobbies = new CollectionViewSource { Source = Hobbies };
            FilteredHobbies.Filter += (s, e) =>
            {
                if (SelectedCategoryForFilter == "Alla kategorier" || SelectedCategoryForFilter == null)
                {
                    e.Accepted = true;
                    return;
                }

                var hobby = e.Item as Hobby;
                if (hobby != null)
                {
                    e.Accepted = hobby.GetCategoryDescription() == SelectedCategoryForFilter; ;
                }
            };

            FilteredHobbies.View.Refresh();

            NewHobby = new Hobby();
            SelectedCategoryForFilter = "Alla kategorier"; 
            SelectedCategoryForNewHobby = NewHobbyCategories[0];
            NewHobby.Equipment = new ObservableCollection<string>();

            AddHobbyCommand = new RelayCommand(param => AddHobby(), param => CanAddHobby());
            RemoveHobbyCommand = new RelayCommand(param => RemoveHobby(), param => CanRemoveHobby());
            AddEquipmentCommand = new RelayCommand(param => AddEquipment(), param => CanAddEquipment());
        }

        public string SelectedHobbyCategoryDescription
        {
            get
            {
                if (SelectedHobby != null)
                {
                    return SelectedHobby.GetCategoryDescription();
                }
                return string.Empty;
            }
        }

        public string SelectedHobbyEquipment 
        { 
            get 
            { 
                if (SelectedHobby != null && SelectedHobby.Equipment != null) 
                { 
                    return string.Join(", ", SelectedHobby.Equipment); 
                } 
                return string.Empty; 
            } 
        }

        private void AddHobby()
        {
            NewHobby.Category = Hobby.GetCategoryFromDescription(SelectedCategoryForNewHobby?.ToString());

            Hobbies.Add(NewHobby);

            NewHobby = new Hobby();
            SelectedCategoryForNewHobby = NewHobbyCategories[0];
            NewHobby.Equipment = new ObservableCollection<string>();
            SelectedHobby = Hobbies.Last();
        }


        private void RemoveHobby()
        {
            Hobbies.Remove(SelectedHobby);
        }

        private void FilterHobbies()
        {
            FilteredHobbies.View.Refresh();
        }

        private void AddEquipment()
        {
            if(NewHobby.Equipment == null)
            {
                NewHobby.Equipment = new ObservableCollection<string>();
            }
            NewHobby.Equipment.Add(NewEquipmentItem);
            NewEquipmentItem = string.Empty;
            RaisePropertyChanged(nameof(NewHobby));
            RaisePropertyChanged(nameof(NewHobby.Equipment));
        }

        private bool CanAddHobby()
        {
            return !string.IsNullOrWhiteSpace(NewHobby.Name) && SelectedCategoryForNewHobby != "Du måste välja en kategori";
        }


        private bool CanRemoveHobby()
        {
            return SelectedHobby != null;
        }

        private bool CanAddEquipment()
        {
            return !string.IsNullOrWhiteSpace(NewEquipmentItem);
        }

        private bool _isDropDownOpen;
        public bool IsDropDownOpen
        {
            get { return _isDropDownOpen; }
            set
            {
                if (_isDropDownOpen != value)
                {
                    _isDropDownOpen = value;
                    RaisePropertyChanged();

                    if (_isDropDownOpen)
                        OnDropDownOpened();
                    else
                        OnDropDownClosed();
                }
            }
        }

        public override void OnDropDownOpened()
        {
            if (NewHobbyCategories.Contains("Du måste välja en kategori"))
            {
                NewHobbyCategories.Remove("Du måste välja en kategori");
            }
        }

        public override void OnDropDownClosed()
        {
            if (!NewHobbyCategories.Contains("Du måste välja en kategori"))
            {
                NewHobbyCategories.Insert(0, "Du måste välja en kategori");
            }
        }
    }
}

