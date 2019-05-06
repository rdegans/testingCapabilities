using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace testingCapabilities
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //Some problems with restrictions on height
        Rectangle sprite = new Rectangle();
        double xValue = 0;
        double yValue = 0;
        double xSpeed = 1;
        double ySpeed = 0;
        double level = 1;
        string lastKey = "";
        Rectangle[] rectangles = new Rectangle[10];
        Random rand = new Random();
        public MainWindow()
        {
            InitializeComponent();
            System.Windows.Threading.DispatcherTimer gameTimer = new System.Windows.Threading.DispatcherTimer();
            sprite.Fill = Brushes.Black;
            sprite.Height = 50;
            sprite.Width = 50;
            canvas.Children.Add(sprite);
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 1);
            for (int i = 0; i < rectangles.Length; i++)
            {
                rectangles[i] = new Rectangle();
                int horVer = rand.Next(2);
                if (horVer == 1)
                {
                    rectangles[i].Height = 50;
                    rectangles[i].Width = rand.Next(4)*50 + 100;
                }
                else
                {
                    rectangles[i].Width = 50;
                    rectangles[i].Height = rand.Next(3) * 50 + 100;
                }
                rectangles[i].Fill = Brushes.Yellow;
                canvas.Children.Add(rectangles[i]);
                Canvas.SetTop(rectangles[i], rand.Next(600));
                Canvas.SetLeft(rectangles[i], rand.Next(1200));
            }
            gameTimer.Start();
        }
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            lblOutput.Content = "";
            if (Keyboard.IsKeyDown(Key.Down))
            {
                if (yValue < 600)
                {
                    if (lastKey == "Down")
                    {
                        ySpeed += 0.01;
                    }
                    else
                    {
                        ySpeed = 1;
                    }
                }
                lastKey = "down";
                lblOutput.Content = "Going down";
            }
            else if (Keyboard.IsKeyDown(Key.Up))
            {
                if (yValue >= 0)
                {
                    if (lastKey == "up")
                    {
                        ySpeed -= 0.01;
                    }
                    else
                    {
                        ySpeed = -1;
                    }
                }
                lblOutput.Content = "Going up";
                lastKey = "up";
            }
            else
            {
                if (yValue <= 600)
                {
                    ySpeed += 0.1;
                    lblOutput.Content = "Going down";
                }
                else
                {
                    ySpeed = 0;
                }
            }
            yValue += ySpeed;
            Canvas.SetTop(sprite, yValue);
            xSpeed = level;
            xValue += xSpeed;
            Canvas.SetLeft(sprite, xValue);
            if (Canvas.GetLeft(sprite) >= 1249)
            {
                xValue = 0;
                Canvas.SetLeft(sprite, xValue);
                level++;
                for (int i = 0; i < rectangles.Length; i++)
                {
                    canvas.Children.Remove(rectangles[i]);
                    int horVer = rand.Next(1);
                    if (horVer == 2)
                    {
                        rectangles[i].Height = 50;
                        rectangles[i].Width = rand.Next(3) * 50 + 100;
                    }
                    else
                    {
                        rectangles[i].Width = 200;
                        rectangles[i].Height = rand.Next(3) * 50 + 100;
                    }
                    rectangles[i].Fill = Brushes.Yellow;
                    canvas.Children.Add(rectangles[i]);
                    Canvas.SetTop(rectangles[i], rand.Next(600));
                    Canvas.SetLeft(rectangles[i], rand.Next(600) + 300);
                }
            }
            lblOutput.Content += "Top = " + Canvas.GetTop(sprite) + "Left = " + Canvas.GetLeft(sprite) + "Level = " + level + "Speed = " + xSpeed;
        }
    }
}
