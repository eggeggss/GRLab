using GRCustom.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace GRLab.Page
{
    public partial class GRListViewPage : ContentPage
    {
        private List<Item> _AllList = new List<Item>();
        private List<Item> _list = new List<Item>();

        private int CurrentIndex = 1;
        //get 3 rows
        private const int BatchIndex = 3;
        
        public GRListViewPage()
        {
            InitializeComponent();
            
            _AllList.Add(new Item() { Source = @"b02.jpg" });
            _AllList.Add(new Item() { Source = @"b01.jpg" });
            _AllList.Add(new Item() { Source = @"b03.jpg" });
            _AllList.Add(new Item() { Source = @"b04.jpg" });
            _AllList.Add(new Item() { Source = @"b05.jpg" });
            _AllList.Add(new Item() { Source = @"b06.jpg" });
            _AllList.Add(new Item() { Source = @"b07.jpg"});
            _AllList.Add(new Item() { Source = @"b08.jpg"});
            _AllList.Add(new Item() { Source = @"b09.jpg" });
            _AllList.Add(new Item() { Source = @"b10.jpg" });
            _AllList.Add(new Item() { Source = @"b11.jpg"});
            _AllList.Add(new Item() { Source = @"b12.jpg"});
            _AllList.Add(new Item() { Source = @"b13.jpg"});
            _AllList.Add(new Item() { Source = @"b14.jpg" });
            _AllList.Add(new Item() { Source = @"b15.jpg"});
            _AllList.Add(new Item() { Source = @"b16.jpg" });

            //每次取3筆
             LoadNextData();
            this.listview.ItemList = _list;
            this.listview.LoadData();


            //下拉到底要做什麼? 再取3筆
            this.listview.LoadEventHandler += () => 
            {
               
                LoadNextData();

                this.listview.ItemList = _list;
                this.listview.LoadData();


            };
            
        }

        //每次更新CurrentIndex,若大於_AllList.Count 則取_AllList.Count
        private void LoadNextData()
        {
            int maxCount = BatchIndex + CurrentIndex;

            if (maxCount > _AllList.Count)
                maxCount = _AllList.Count + 1;
            
            for (int i = CurrentIndex; i < maxCount; i++)
            {
                _list.Add(_AllList[i - 1]);
            }

            CurrentIndex = maxCount;

            //this.listview.ItemList = _list;
            //this.listview.ItemsSource = null;
           // this.listview.ItemsSource = _list;
        }

       

    }
    public class Item
    {
        public String Source { set; get; }

    }
}
