using OrderService.Extensions;
using OrderService.Api.Client;
using OrderService.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Extended;
using Xamarin.Forms.Xaml;

namespace OrderService.Pages.Notification
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsPage : ContentPage
    {
        public static readonly BindableProperty IsLoadingProperty =
            BindableProperty.Create(nameof(IsBusy), typeof(bool), typeof(NotificationsPage), default(bool));

        public static readonly BindableProperty ItemsProperty =
            BindableProperty.Create(nameof(Items), typeof(InfiniteScrollCollection<NotificationResponse>), typeof(NotificationsPage), default(InfiniteScrollCollection<NotificationResponse>));

        public static readonly BindableProperty NotificationsResponseProperty =
            BindableProperty.Create(nameof(NotificationsResponse), typeof(IEnumerable<NotificationResponse>), typeof(NotificationsPage), default(IEnumerable<NotificationResponse>));

        public static readonly BindableProperty CurrentPageProperty =
            BindableProperty.Create(nameof(CurrentPage), typeof(int), typeof(NotificationsPage), 0);

        public static readonly BindableProperty HasNextProperty =
            BindableProperty.Create(nameof(HasNext), typeof(bool), typeof(NotificationsPage), default(bool));

        private const int _top = 10;

        private int _skip => Items != null ? Items.Count() : 0;

        public NotificationsPage()
        {
            InitializeComponent();

            getItems().
               ContinueWith(t =>
               {
                   SetValue(NotificationsResponseProperty, t);
                   Items.AddRange(t.Result);
               },
               TaskContinuationOptions.NotOnFaulted);

            Items = new InfiniteScrollCollection<NotificationResponse>
            {
                OnCanLoadMore = () => HasNext,
                OnLoadMore = async () =>
                {
                    IsBusy = true;
                    var result = await getItems();
                    IsBusy = false;
                    return result;
                }
            };
        }

        public bool IsLoading
        {
            get { return (bool)GetValue(IsLoadingProperty); }
            set
            {
                SetValue(IsLoadingProperty, value);
            }
        }

        public InfiniteScrollCollection<NotificationResponse> Items
        {
            get { return (InfiniteScrollCollection<NotificationResponse>)GetValue(ItemsProperty); }
            set
            {
                SetValue(ItemsProperty, value);
            }
        }

        public IEnumerable<NotificationResponse> NotificationsResponse
        {
            get { return (IEnumerable<NotificationResponse>)GetValue(NotificationsResponseProperty); }
            set
            {
                SetValue(NotificationsResponseProperty, value);
            }
        }

        public int CurrentPage
        {
            get { return (int)GetValue(CurrentPageProperty); }
            set
            {
                SetValue(CurrentPageProperty, value);
            }
        }

        public bool HasNext
        {
            get { return (bool)GetValue(HasNextProperty); }
            set
            {
                SetValue(HasNextProperty, value);
            }
        }

        private async Task<IEnumerable<NotificationResponse>> getItems()
        {
            try
            {
                var data = await DependencyService.Get<IUserService>().GetNotificationsAsync(_top, _skip);
                data = data.Where(x => x.State == NotificationState.New);
                HasNext = data.Count() > 0;
                return data;
            }
            catch (Exception ex)
            {
                throw ex.Failin();
            }
        }
    }
}