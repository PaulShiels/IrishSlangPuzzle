using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.IO;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace IrishSlangPuzzle
{
    public partial class MainPage : PhoneApplicationPage, INotifyPropertyChanged
    {
        // Data context for the local database
        public static IrishSlangCtx db;

        // Define an observable collection property that controls can bind to.
        private ObservableCollection<UserTbl> _user_tbl;
        public ObservableCollection<UserTbl> UserTbl
        {
            get
            {
                return _user_tbl;
            }
            set
            {
                if (_user_tbl != value)
                {
                    _user_tbl = value;
                    NotifyPropertyChanged("UserTbl");
                }
            }
        }
        //private List<User> usersList = new List<User>();

        public MainPage()
        {
            InitializeComponent();

            // Connect to the database and instantiate data context.
            db = new IrishSlangCtx(IrishSlangCtx.DBConnectionString);

            // Data context and observable collection are children of the main page.
            this.DataContext = this;

            User u = new User();
            u.name = "Paddy";
            u.points = 0;
            UserTbl tdi = new UserTbl();
            tdi.UserName = u.name;
            tdi.Points = u.points;
            db.UserTable.InsertOnSubmit(tdi);
            db.SubmitChanges();

        }

        public MainPage(string buttomName)
        {
            InitializeComponent();

            //if (App.Current.users.Count() > 0)
            //    foreach (User us in App.Current.users)
            //    {
            //        lbxUsers.Items.Add(us.ToString());
            //    }
            btnAddUser.Content = buttomName;

            //var q = from p in App.db.User_tbls
            //        select p.UserName;

            //foreach (var un in q)
            //{
            //    lbxUsers.Items.Add(un);
            //}


            //User user = new User();
            //user.name = "Joe";
            //ToDoItem item = new ToDoItem();
            //item.UserName = user.name;
            //IrishSlangDB.ToDoItems.InsertOnSubmit(item);
            //IrishSlangDB.SubmitChanges();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Define the query to gather all of the to-do items.
            var toDoItemsInDB = from UserTbl todo in db.UserTable
                                select todo;
            var users = from u in db.UserTable
                        select u;

            lbxUsers.ItemsSource = users;

            //foreach (var user in users)
            {
                //User nu = new User();
                //nu.userId = item.UserId;
                //nu.name = item.UserName;
                //nu.points = item.Points;
                //usersList.Add(nu);
                //App.Current.users.Add(nu);
                
            }
            //lbxUsers.Items.Add(toDoItemsInDB.FirstOrDefault());

            // Execute the query and place the results into a collection.
            //ToDoItems = new ObservableCollection<ToDoItem>(toDoItemsInDB);

            // Call the base method.
            base.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            

            // Save changes to the database.
            db.SubmitChanges();
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new CreateUser();
        }

        private void lbxUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            var user = (from u in db.UserTable
                       where u == lbxUsers.SelectedItem
                       select u).SingleOrDefault();

            User us = new User();
            us.name = user.UserName;
            us.points = user.Points;

            AddPuzzleToList(us);
            this.Content = App.Current.imgPuzzle[App.Current.puzzleIndex];
            App.Current.puzzleIndex++;
            
        }  
      
        private void AddPuzzleToList(User us)
        {
            App.Current.imgPuzzle.Add(new ImageWord(us, "Images\\Car1.jpg", "Isn't that a fine ______", "Boat", "Animal", "Beast", "Yock", "Box", "Dog", 4));
            App.Current.imgPuzzle.Add(new ImageWord(us, "Images\\Clout.jpg", "Johnny got a ______ on the head last night on his way home.", "Shock", "Knock", "Tap", "Touch", "Clout", "Pat", 5));
            App.Current.imgPuzzle.Add(new ImageWord(us, "Images\\ClaneMad.jpg", "That man is away ______ mad.", "Crazy", "Angry", "Clane", "Tale", "Way", "Awful", 3));
        }
    }

    #region Database

    public class IrishSlangCtx : DataContext
    {
        // Specify the connection string as a static, used in main page and app.xaml.
        public static string DBConnectionString = "Data Source=isostore:/IrishSlangPuzzle.sdf";

        // Pass the connection string to the base class.
        public IrishSlangCtx(string connectionString)
            : base(connectionString)
        { }

        // Specify a single table for the to-do items.
        public Table<UserTbl> UserTable;
    }


    [Table]
    public class UserTbl : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property and database column.
        private int _userId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int UserId
        {
            get
            {
                return _userId;
            }
            set
            {
                if (_userId != value)
                {
                    NotifyPropertyChanging("UserId");
                    _userId = value;
                    NotifyPropertyChanged("UserId");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _userName;

        [Column]
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                if (_userName != value)
                {
                    NotifyPropertyChanging("UserName");
                    _userName = value;
                    NotifyPropertyChanged("UserName");
                }
            }
        }

        // Define completion value: private field, public property and database column.
        private int _points;

        [Column]
        public int Points
        {
            get
            {
                return _points;
            }
            set
            {
                if (_points != value)
                {
                    NotifyPropertyChanging("Points");
                    _points = value;
                    NotifyPropertyChanged("Points");
                }
            }
        }
        // Version column aids update performance.
        //[Column(IsVersion = true)]
        //private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }

    #endregion
}