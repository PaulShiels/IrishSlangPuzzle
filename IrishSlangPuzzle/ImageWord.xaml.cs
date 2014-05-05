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
using System.Windows.Media;
using System.Threading;

namespace IrishSlangPuzzle
{
    public partial class ImageWord : PhoneApplicationPage
    {              
        public User u { get; set; }
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

        public DispatcherTimer clock = new DispatcherTimer();
        public DispatcherTimer flashTimer = new DispatcherTimer(); 
        private int skipTimer;
        private int flashCount=0;
        private bool btnSolvePressed = false;
        private Button buttonToFlash;
        private Button PressedButton;
        private int pressCounter=0;

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

            //Set up new timer to flash the answer button
            flashTimer.Tick += new EventHandler(flashTimer_Tick);
            flashTimer.Interval = new TimeSpan(0, 0, 0, 0, 200);
            buttonToFlash = GetButtonToFlash();
            //flashTimer.Start();
        }

        private void clock_Tick(object sender, EventArgs e)
        {
            buttonToFlash = GetButtonToFlash();

            if (skipTimer > 0)
            {
                btnSolve.Content = String.Format("Solve {0}", skipTimer);
                skipTimer--;
            }
            
            else if (btnSolvePressed)
            {
                ShowNextBtn();
                flashTimer.Start();
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

        private void setOpacityZindex(double opacity, int zIndex)
        {
            btnAns1.Opacity = opacity;
            Canvas.SetZIndex(btnAns1, zIndex);
            btnAns2.Opacity = opacity;
            Canvas.SetZIndex(btnAns2, zIndex);
            btnAns3.Opacity = opacity;
            Canvas.SetZIndex(btnAns3, zIndex);
            btnAns4.Opacity = opacity;
            Canvas.SetZIndex(btnAns4, zIndex);
            btnAns5.Opacity = opacity;
            Canvas.SetZIndex(btnAns5, zIndex);
            btnAns6.Opacity = opacity;
            Canvas.SetZIndex(btnAns6, zIndex);
        }        

        private void CheckAnswer(int btnNum)
        {
            if (correctAns == btnNum)
            {
                if (pressCounter == 0)
                {
                    u.points++;
                    txtPoints.Text = u.points.ToString();
                    txtSentance.Text = String.Format("You're Right!!");
                    ShowNextBtn();
                    buttonToFlash.IsEnabled = true;
                    buttonToFlash.Background = new SolidColorBrush(Colors.Green);
                    buttonToFlash.Foreground = new SolidColorBrush(Colors.Yellow);
                    pressCounter++;
                }
                
            }
            else
            {
                if (pressCounter == 0)
                {
                    txtSentance.Text = String.Format("Sorry Wrong Answer!!");
                    u.points -= 2;
                    txtPoints.Text = u.points.ToString();
                    ShowNextBtn();
                    PressedButton.IsEnabled = true;
                    PressedButton.Background = new SolidColorBrush(Colors.Red);
                    buttonToFlash.Foreground = new SolidColorBrush(Colors.Yellow);
                }
            }
        }

        #region answer button click events
        private void ans1_Click(object sender, RoutedEventArgs e)
        {
            PressedButton = btnAns1;
            CheckAnswer(1);            
        }

        private void ans2_Click(object sender, RoutedEventArgs e)
        {
            PressedButton = btnAns2;
            CheckAnswer(2);
        }

        private void ans3_Click(object sender, RoutedEventArgs e)
        {
            PressedButton = btnAns3;
            CheckAnswer(3);
        }

        private void ans4_Click(object sender, RoutedEventArgs e)
        {
            PressedButton = btnAns4;
            CheckAnswer(4);
        }

        private void ans5_Click(object sender, RoutedEventArgs e)
        {
            PressedButton = btnAns5;
            CheckAnswer(5);
        }

        private void ans6_Click(object sender, RoutedEventArgs e)
        {
            PressedButton = btnAns6;
            CheckAnswer(6);
        }
        #endregion

        private void btnSolve_Click(object sender, RoutedEventArgs e)
        {
            txtInstruction.Opacity = 0.1;
            Image.Opacity = 0.2;
            txtSentance.Opacity = 0;
            setOpacityZindex(0, -2);

            btnSolve.Visibility = Visibility.Collapsed;
            btnSolve.Opacity = 0;
            txtSure.Visibility = Visibility.Visible;
            spnl.Visibility = Visibility.Visible;
            btnSkip.Visibility = Visibility.Collapsed;
        }

        private void btnYes_Click(object sender, RoutedEventArgs e)
        {            
            if (u.points < 5)
            {
                txtSure.Text = String.Format("You must have 5 or more points to solve!");
                btnNo.Content = "OK";
                btnYes.IsEnabled = false;
                btnYes.Opacity = 0;
            }
            else
            {
                btnSolvePressed = true;
                u.points -= 5;
                txtPoints.Text = u.points.ToString();
                btnNo_Click(sender, e);
            }
            
        }

        private void btnNo_Click(object sender, RoutedEventArgs e)
        {
            txtInstruction.Opacity = 1;
            Image.Opacity = 1;
            txtSure.Visibility = Visibility.Collapsed;
            spnl.Visibility = Visibility.Collapsed;
            btnSkip.Visibility = Visibility.Visible;
            txtSentance.Opacity = 1;
            setOpacityZindex(1, 1);
        }

        private void flashTimer_Tick(object sender, EventArgs e)
        {
            switch (flashCount)
            {
                case 0:
                    buttonToFlash.Background = new SolidColorBrush(Colors.Yellow);
                    break;
                case 1:
                    buttonToFlash.Background = new SolidColorBrush(Colors.Transparent);
                    break;
                case 2:
                    buttonToFlash.Background = new SolidColorBrush(Colors.Yellow);
                    break;
                case 3:
                    buttonToFlash.Background = new SolidColorBrush(Colors.Transparent);
                    break;
                case 4:
                    buttonToFlash.Background = new SolidColorBrush(Colors.Yellow);
                    break;
                default:
                    buttonToFlash.Background = new SolidColorBrush(Colors.Green);
                    break;
            }
            
            ShowNextBtn();
            txtInstruction.Opacity = 1;
            Image.Opacity = 1;
            txtSure.Visibility = Visibility.Collapsed;
            spnl.Visibility = Visibility.Collapsed;
            btnSkip.Visibility = Visibility.Visible;
            txtSentance.Opacity = 1;
            flashCount++;

            if(flashCount >=6)
            flashTimer.Stop();
        }

        private Button GetButtonToFlash()
        {
            switch (correctAns)
            {
                case 1:
                    return btnAns1;
                case 2:
                    return btnAns2;
                case 3:
                    return btnAns3;
                case 4:
                    return btnAns4;
                case 5:
                    return btnAns5;
                case 6:
                    return btnAns6;
                default:
                    return btnAns6;
            }
        }        

        private void btnSkip_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new ImageWord(u, "Images\\Lion1.jpg", "Would you take a look at the  ______", "Beast", "Wee Dinasor", "Big Ket", "Buck", "Pet", "Dog", 3);
        }

        private void ShowNextBtn()
        {
            btnSolve.Visibility = Visibility.Collapsed;
            btnNext.Visibility = Visibility.Visible;
            Canvas.SetZIndex(btnNext, 2);
            btnSkip.IsEnabled = false;
            btnSkip.Opacity = 0;
            setOpacityZindex(0.5, 1);
            PressedButton.Opacity = 1;
            btnAns1.IsEnabled = false;
            btnAns2.IsEnabled = false;
            btnAns3.IsEnabled = false;
            btnAns4.IsEnabled = false;
            btnAns5.IsEnabled = false;
            btnAns6.IsEnabled = false;
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            this.Content = new ImageWord(u, "Images\\Lion1.jpg", "Would you take a look at the  ______", "Beast", "Wee Dinasor", "Big Ket", "Buck", "Pet", "Dog", 3);
        }


    }
}