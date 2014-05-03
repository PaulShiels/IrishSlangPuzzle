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
            App.Current.users.Add(u);

            this.Content = new ImageWord();
            //this.Content = new MainPage("New user added");
            

            //WriteUserToFile(u);
            //this.Content = new MainPage();
            //Start Game
        }

        private void WriteUserToFile(User u)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Users.csv"))
                {
                    sw.WriteLine("{0}:{1}", u.name, u.points);
                }
            }
            catch (Exception e)
            {
                
                MessageBox.Show(e.ToString());
            }
            
        }
    }
}