using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Threading;
using System.Windows.Media.Imaging;

namespace IrishSlangPuzzle
{
    public partial class ImageWord : PhoneApplicationPage
    {
        public DispatcherTimer clock = new DispatcherTimer();
        public User u { get; set; }
        private int skipTimer { get; set; }
        public string imgSource { get; set; }
        public string sentance { get; set; }
        public List<string> answersList { get; set; }
        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public string answer4 { get; set; }
        public string answer5 { get; set; }
        public string answer6 { get; set; }
        public int correctAns { get; set; }

        public ImageWord()
        {
            InitializeComponent();
        }

        public ImageWord(User u, string imgSrc, string sentance, string ans1, string ans2, string ans3, string ans4, string ans5, string ans6, int correctAns)
        {
            InitializeComponent();
            answersList = new List<string>();
            this.u = u;
            this.imgSource = imgSrc;
            this.sentance = sentance;
            this.answer1 = ans1;
            this.answer2 = ans2;
            this.answer3 = ans3;
            this.answer4 = ans4;
            this.answer5 = ans5;
            this.answer6 = ans6;
            this.correctAns = correctAns;

            updateCurrentWindow();
            skipTimer = 5;
            clock.Tick+=new EventHandler(clock_Tick);
            clock.Interval= new TimeSpan(0,0,1);
            clock.Start();
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            if (skipTimer > 0)
            {
                skipTimer--;
                btnSolve.Content = String.Format("Solve {0}", skipTimer);
            }
            else
            {
                btnSolve.Content = "Solve";
                btnSkip.IsEnabled = true;
                btnSolve.IsEnabled = true;
            }
        }

        private void updateCurrentWindow()
        {
            btnSkip.IsEnabled = false;
            btnSolve.IsEnabled = false;
            txtPlayer.Text = u.name;
            txtPoints.Text = u.points.ToString();
            Image.Source = new BitmapImage(new Uri(imgSource, UriKind.Relative));
            txtSentance.Text = sentance;
            ans1.Text = answer1;
            ans2.Text = answer2;
            ans3.Text = answer3;
            ans4.Text = answer4;
            ans5.Text = answer5;
            ans6.Text = answer6;
            AddAnswersToList();
        }

        

        private void AddAnswersToList()
        {
            answersList.Add(answer1);
            answersList.Add(answer2);
            answersList.Add(answer3);
            answersList.Add(answer4);
            answersList.Add(answer5);
            answersList.Add(answer6);
        }

        private void setOpacityZindex(FrameworkElement e1, FrameworkElement e2, FrameworkElement e3, FrameworkElement e4, FrameworkElement e5, FrameworkElement e6, int opacity, int zIndex)
        {
            e1.Opacity = opacity;
            Canvas.SetZIndex(e1, zIndex);
            e2.Opacity = opacity;
            Canvas.SetZIndex(e2, zIndex);
            e3.Opacity = opacity;
            Canvas.SetZIndex(e3, zIndex);
            e4.Opacity = opacity;
            Canvas.SetZIndex(e4, zIndex);
            e5.Opacity = opacity;
            Canvas.SetZIndex(e6, zIndex);
            e6.Opacity = opacity;
            Canvas.SetZIndex(e6, zIndex);
        }

        

        private void CheckAnswer(int btnNum)
        {
            if (correctAns == btnNum)
                MessageBox.Show("You're Right!!");
        }

        #region answer button click events
        private void ans1_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(1);
        }

        private void ans2_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(2);
        }

        private void ans3_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(3);
        }

        private void ans4_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(4);
        }

        private void ans5_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(5);
        }

        private void ans6_Click(object sender, RoutedEventArgs e)
        {
            CheckAnswer(6);
        }
        #endregion

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            txtInstruction.Opacity = 0.1;
            Image.Opacity = 0.2;
            txtSentance.Opacity = 0;
            setOpacityZindex(btnAns1, btnAns2, btnAns3, btnAns4, btnAns5, btnAns6, 0, -2);

            txtSure.Visibility = Visibility.Visible;
            spnl.Visibility = Visibility.Visible;
            btnSkip.Visibility = Visibility.Collapsed;
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            txtInstruction.Opacity = 1;
            Image.Opacity = 1;
            txtSure.Visibility = Visibility.Collapsed;
            spnl.Visibility = Visibility.Collapsed;
            btnSkip.Visibility = Visibility.Visible;
            txtSentance.Opacity = 1;
            setOpacityZindex(btnAns1, btnAns2, btnAns3, btnAns4, btnAns5, btnAns6, 1, 1);
        }

        private void btnSkip_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new ImageWord(u, "Images\\Lion1.jpg", "Would you take a look at the  ______", "Beast", "Wee Dinasor", "Big Ket", "Buck", "Pet", "Dog", 3);
        }

    }
}