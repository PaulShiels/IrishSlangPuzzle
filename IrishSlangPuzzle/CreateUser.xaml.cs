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
    public partial class CreateUser : PhoneApplicationPage
    {

        public CreateUser()
        {
            InitializeComponent();            
        }

        private void btnSubmitName_Click(object sender, RoutedEventArgs e)
        {
            User u = new User();
            u.name = txtUserName.Text;
            u.points = 0;
            //db.User_tbl.InsertOnSubmit(u);
            //App.Current.users.Add(u);

            //this.Content = new ImageWord(u, "Images\\Car1.jpg", "Isn't that a fine ______", "Boat", "Animal", "Beast", "Yock", "Box", "Dog", 4);
            //this.Content = new MainPage("New user added");
            

            //WriteUserToFile(u);
            //this.Content = new MainPage();
            //Start Game
        }

    }
}