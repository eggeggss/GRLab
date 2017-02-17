using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GRCustom.View
{
 
    public class GRListView<T> : ListView
    {
        public ActivityIndicator _activityindicator;
        public Label _waitlabel;
        StackLayout _waitlayout;
        private const int footerHeight = 80;

        public List<T> ItemList = new List<T>();

        private bool isLoading;

        public event Action LoadEventHandler;

        private Color _footerBackgroundColor, _loadingTextColor, _completeTextColor;


        public Color FooterBackgroundColor
        {
            set
            {
                this._footerBackgroundColor = value;
            }
            get
            {               
                return this._footerBackgroundColor;
            }
        }

        public Color LoadingTextColor
        {
            set
            {
                this._loadingTextColor = value;
            }
            get
            {
              
                   
                return this._loadingTextColor;
            }
        }

        public Color CompleteTextColor
        {
            set
            {
                this._completeTextColor = value;
            }
            get
            {
              
                  

                return this._completeTextColor;
            }
        }

        private String _loadingText;
        public String LoadingText
        {
            set
            {
                _loadingText = value;
            }
            get
            {
                if (String.IsNullOrEmpty(_loadingText))
                {
                    _loadingText = "Load more..";
                }
                return _loadingText;
            }
        }

        private String _completeText;
        public String CompleteText
        {
            set
            {
                _completeText = value;
            }
            get
            {
                if (String.IsNullOrEmpty(_completeText))
                    _completeText = "Complete2!";
                return _completeText;
            }
        }


        public GRListView():base(ListViewCachingStrategy.RecycleElement)
        {
            
            this._footerBackgroundColor = Color.FromHex("#C0C0C0");
            this._loadingTextColor = Color.Yellow;
            _completeTextColor = Color.White;

            _waitlabel = new Label()
            {
                BackgroundColor = FooterBackgroundColor,
                TextColor = CompleteTextColor,
                HeightRequest = footerHeight / 2,
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                FontAttributes = FontAttributes.Bold
            };

            _activityindicator = new ActivityIndicator()
            {
                BackgroundColor = FooterBackgroundColor,
                Color = CompleteTextColor,
                HeightRequest = footerHeight / 2,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand
            };

            _waitlayout = new StackLayout();
            _waitlayout.Spacing = 0;
            _waitlayout.HeightRequest = footerHeight;

            _waitlayout.Children.Add(_activityindicator);
            _waitlayout.Children.Add(_waitlabel);

            _waitlayout.BackgroundColor = FooterBackgroundColor;

            this.Footer = _waitlayout;

            this.ItemAppearing += (sender, e) =>
            {
                LoadMore(sender, e);
            };
        }


        public void LoadMore(object sender, ItemVisibilityEventArgs e)
        {
            List<T> _list = (List<T>)this.ItemsSource;
            if (isLoading || _list.Count == 0)
                return;
       
            if (EqualityComparer<T>.Default.Equals((T)e.Item, _list[_list.Count - 1]))
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    ItemLoadInitial();

                    await Task.Delay(2000);

                    if (LoadEventHandler != null)
                    {
                        LoadEventHandler();
                        LoadData();
                    }


                    ItemLoadComplete();
                });
                //LoadMoreItem();
            }
            //hit bottom!
        }

        public void LoadData()
        {
            Device.BeginInvokeOnMainThread(()=> 
            {
                this.ItemsSource = this.ItemList;

            });
        }

        private void ItemLoadInitial()
        {
            _activityindicator.Opacity = 1;
            _activityindicator.IsVisible = true;
            _activityindicator.IsRunning = true;
            _waitlabel.Text = LoadingText;
            _waitlabel.TextColor = LoadingTextColor;
            _waitlayout.HeightRequest = footerHeight;
            _waitlayout.Opacity = 1;
            isLoading = true;
        }

        //刷完的狀態
        private void ItemLoadComplete()
        {
            _activityindicator.Opacity = 1;
            _activityindicator.IsVisible = false;
            _activityindicator.IsRunning = false;
            _waitlabel.Text = CompleteText;
            _waitlabel.TextColor = CompleteTextColor;
            isLoading = false;
        }

    }


}
