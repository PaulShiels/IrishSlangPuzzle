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

namespace IrishSlangPuzzle
{
    public partial class MainPage : PhoneApplicationPage
    {
        public string buttomName { get; set; }

        public MainPage()
        {
            InitializeComponent();
            User u = new User();
            if (App.Current.users.Count() > 0)
                foreach (User us in App.Current.users)
                {
                    lbxUsers.Items.Add(us.ToString());
                }
            
        }

        public MainPage(string buttomName)
        {
            InitializeComponent();
            User u = new User();
            if (App.Current.users.Count() > 0)
                foreach (User us in App.Current.users)
                {
                    lbxUsers.Items.Add(us.ToString());
                }
            btnAddUser.Content = buttomName;
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new CreateUser();
        }

        
    }
}