using OrderService.AppConstant;
using OrderService.Extensions;
using OrderService.Helpers;
using OrderService.Models.MainMenu;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace OrderService.View.MainMenu
{
    public class MainMenuViewModel : INotifyPropertyChanged
    {
        private bool _hasNewMessages;

        private ObservableCollection<MainMenuItem>? _headerItems;

        private ObservableCollection<MainMenuItem>? _menuItems;

        private int _newMessagesCount;

        private MainMenuItem? _selectedHeaderItem;

        private MainMenuItem? _selectedMenuItem;

        private string? _email;

        private string? _picture;

        public MainMenuViewModel()
        {
            LoadHeaderMenuItems();

            LoadBodyMenuItems();

            LoadUserLogged();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool HasNewMessages
        {
            get => _hasNewMessages;
            set => SetProperty(ref _hasNewMessages, value);
        }

        public ObservableCollection<MainMenuItem>? HeaderItems
        {
            set { SetProperty(ref _headerItems, value); }
            get { return _headerItems; }
        }

        public ObservableCollection<MainMenuItem>? MenuItems
        {
            set { SetProperty(ref _menuItems, value); }
            get { return _menuItems; }
        }

        public int NewMessagesCount
        {
            get => _newMessagesCount;
            set => SetProperty(ref _newMessagesCount, value);
        }

        public MainMenuItem? SelectedHeaderItem
        {
            set { SetProperty(ref _selectedHeaderItem, value); }
            get { return _selectedHeaderItem; }
        }

        public MainMenuItem? SelectedMenuItem
        {
            set { SetProperty(ref _selectedMenuItem, value); }
            get { return _selectedMenuItem; }
        }

        public string? Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }

        public string? Picture
        {
            get => _picture;
            set => SetProperty(ref _picture, value);
        }

        protected void OnPropertyChanged([CallerMemberName] string? propName = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));

        private void LoadBodyMenuItems()
        {
            MenuItems = new ObservableCollection<MainMenuItem>
            {
                new MainMenuItem
                {
                    Text = ItemType.Clients.Description(),
                    IconImageSource = FontAwesomeIcons.Users,
                    BackgroundColor = (Color)Application.Current.Resources.MergedDictionaries.FirstOrDefault()["Green4"],
                    Type = ItemType.Clients
                },
                new MainMenuItem
                {
                    Text = ItemType.Products.Description(),
                    IconImageSource = FontAwesomeIcons.ListAlt,
                    BackgroundColor = Color.FromHex("#c0392b"),
                    Type = ItemType.Products
                },
                new MainMenuItem
                {
                    Text = ItemType.Orders.Description(),
                    IconImageSource = FontAwesomeIcons.TicketAlt,
                    BackgroundColor = Color.FromHex("#2980b9"),
                    Type = ItemType.Orders
                },
                new MainMenuItem
                {
                    Text = ItemType.About.Description(),
                    IconImageSource = FontAwesomeIcons.MobileAlt,
                    BackgroundColor = (Color)Application.Current.Resources.MergedDictionaries.FirstOrDefault()["Gray1"],
                    Type = ItemType.About
                }
            };
        }

        private void LoadHeaderMenuItems()
        {
            HeaderItems = new ObservableCollection<MainMenuItem>
            {
                new MainMenuItem
                {
                    Text = ItemType.NewOs.Description(),
                    IconImageSource = FontAwesomeIcons.TicketAlt,
                    Type = ItemType.NewOs
                },
                new MainMenuItem
                {
                    Text = ItemType.NewClient.Description(),
                    IconImageSource = FontAwesomeIcons.UserPlus,
                    Type = ItemType.NewClient
                },
                new MainMenuItem
                {
                    Text = ItemType.Logout.Description(),
                    IconImageSource = FontAwesomeIcons.SignOutAlt,
                    Type = ItemType.Logout
                }
            };
        }

        private void LoadUserLogged()
        {
            Email = SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_EMAIL).Result;
            Picture = SecureStorage.GetAsync(Constants.KEY_STORAGE_USER_PICTURE).Result;
        }

        private bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string? propertyName = null)
        {
            if (Object.Equals(storage, value))
                return false;
            storage = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}