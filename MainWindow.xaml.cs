using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;



namespace Moving_cube
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    { 
    bool goup, godown, goleft, goright;
        int playerHealth = 100;
        int test = 1;
        int limit = 100;
        int playerspeed = 12;
        int speed = 5;
        int bossHealth = 100;
        int bossspeed = 3;
        string facing = "up";
        int enemycounter = 10;
        double damage;
        Rect playerHitBox;
        Random rand = new Random();
        List<Rectangle> itemstoremove = new List<Rectangle>();

        DispatcherTimer gameTimer = new DispatcherTimer();
        

        public MainWindow()
        {
            InitializeComponent();
            
            damage = 0.1;
            myCanvas.Focus();

            gameTimer.Tick += gameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Start();
        }
        public double DataFromWindow1 { get; set; }
        
        
        /*private void GameTimerEvent(object? sender, EventArgs e)
        {
            if (playerHealth > 1)
            {
                pHealth.Value = playerHealth;
            }
            if (goleft == true && Canvas.GetLeft(player) > 5)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerspeed);
            }
            if (goright == true && Canvas.GetLeft(player) + (player.Width + 20) < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerspeed);
            }
            if (goup == true && Canvas.GetTop(player) > 5)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - playerspeed);
            }
            if (godown == true && Canvas.GetTop(player) + (player.Height * 2) < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + playerspeed);
            }
        }*/


        // You can access DataFromWindow1 in Window2's methods or bindings

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goleft = false;
            }
            if (e.Key == Key.Right)
            {
                goright = false;
            }
            if (e.Key == Key.Up)
            {
                goup = false;
            }
            if (e.Key == Key.Down)
            {
                godown = false;
            }
            if (e.Key == Key.Space)
            {

                {
                    ImageBrush playerbullet = new ImageBrush();
                    playerbullet.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/poop.png"));

                    Rectangle newBullet = new Rectangle
                    {
                        Tag = "bullet",
                        Height = 30,
                        Width = 15,
                        //Fill = Brushes.White,
                        //Stroke = Brushes.Red
                        Fill= playerbullet
                    };
                    // place the bullet on top of the player location
                    Canvas.SetTop(newBullet, Canvas.GetTop(player) - newBullet.Height);
                    // place the bullet middle of the player image
                    Canvas.SetLeft(newBullet, Canvas.GetLeft(player) + player.Width / 2);
                    // add the bullet to the screen
                    myCanvas.Children.Add(newBullet);
                }
            }

        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                goleft = true;
                facing = "left";
            }
            if (e.Key == Key.Right)
            {
                goright = true;
                facing = "right";
            }
            if (e.Key == Key.Up)
            {
                goup = true;
                facing = "up";
            }
            if (e.Key == Key.Down)
            {
                godown = true;
                facing = "down";
            }

        }
        private void makeEnemies()
        {

            // make a new rectangle called new enemy
            // this rectangle has a enemy tag, height 50 and width 56 pixels
            // background fill is assigned to the randomly generated enemy sprite from the switch statement above
            ImageBrush minions = new ImageBrush();
            minions.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/meteor.jpg"));
            Rectangle newEnemy = new Rectangle
            {
                Tag = "minion",
                Height = 50,
                Width = 56,
                Fill= minions
                //Fill = Brushes.Blue,
                //Stroke = Brushes.Red
            };
            Canvas.SetTop(newEnemy, -100); // set the top position of the enemy to -100
            // randomly generate the left position of the enemy
            Canvas.SetLeft(newEnemy, rand.Next(30, 430));
            // add the enemy object to the screen
            myCanvas.Children.Add(newEnemy);
            // garbage collection
            GC.Collect(); // collect any unused resources for this game
        }

        private void gameEngine(object sender, EventArgs e)
        {
            
            double damagereal = 0.1;
            playerHitBox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);
            enemycounter--;
            
            if (enemycounter < 0)
            {
                makeEnemies(); // run the make enemies function
                enemycounter = limit; //reset the enemy counter to the limit integer
            }

            if (goleft == true && Canvas.GetLeft(player) > 5)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - playerspeed);
            }
            if (goright == true && Canvas.GetLeft(player) + (player.Width + 20) < Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + playerspeed);
            }
            if (goup == true && Canvas.GetTop(player) > 5)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) - playerspeed);
            }
            if (godown == true && Canvas.GetTop(player) + (player.Height * 2) < Application.Current.MainWindow.Height)
            {
                Canvas.SetTop(player, Canvas.GetTop(player) + playerspeed);
            }
            {
                
                {
                    ImageBrush bossbullets = new ImageBrush();
                    bossbullets.ImageSource = new BitmapImage(new Uri("pack://application:,,,/image/bossbu.png"));
                    Rectangle bossBullet = new Rectangle
                    {
                        Tag = "bossbullet",
                        Height = 60,
                        Width = 60,
                        //Fill = Brushes.Green,
                        //Stroke = Brushes.Red,
                        Fill= bossbullets

                    };
                    // place the bullet on top of the player location
                    Canvas.SetTop(bossBullet, Canvas.GetTop(boss) - bossBullet.Height);
                    // place the bullet middle of the player image
                    Canvas.SetLeft(bossBullet, Canvas.GetLeft(boss) + boss.Width / 2);
                    // add the bullet to the screen
                    if (Canvas.GetLeft(boss) == rand.Next(0, 1000) || Canvas.GetLeft(boss) == rand.Next(0, 1000) || Canvas.GetLeft(boss) == rand.Next(0, 1000) || Canvas.GetLeft(boss) == rand.Next(0, 1000) || Canvas.GetLeft(boss) == rand.Next(0, 1000) || Canvas.GetLeft(boss) == rand.Next(0, 1000) || Canvas.GetLeft(boss) == rand.Next(0, 1000) || Canvas.GetLeft(boss) == rand.Next(0, 1000) || Canvas.GetLeft(boss) == rand.Next(0, 1000) || Canvas.GetLeft(boss) == rand.Next(0, 1000) || Canvas.GetLeft(boss) == rand.Next(0, 1000) || Canvas.GetLeft(boss) == rand.Next(0, 1000))
                    {
                        myCanvas.Children.Add(bossBullet);
                    }
                }
                Canvas.SetLeft(boss, Canvas.GetLeft(boss) - speed);

                if (Canvas.GetLeft(boss) < 5 || Canvas.GetLeft(boss) + (boss.Width * 2) > Application.Current.MainWindow.Width)
                {
                    speed = -speed;
                }



            }
            foreach (var x in myCanvas.Children.OfType<Rectangle>())
            {
                // if any rectangle has the tag bullet in it
                if (x is Rectangle && (string)x.Tag == "bullet")
                {
                    // move the bullet rectangle towards top of the screen
                    Canvas.SetTop(x, Canvas.GetTop(x) - 20);
                    // make a rect class with the bullet rectangles properties
                    Rect bullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    // check if bullet has reached top part of the screen
                    if (Canvas.GetTop(x) < 10)
                    {
                        // if it has then add it to the item to remove list
                        itemstoremove.Add(x);
                    }
                    // run another for each loop inside of the main loop this one has a local variable called y
                    foreach (var z in myCanvas.Children.OfType<Rectangle>())
                    {
                        // if y is a rectangle and it has a tag called enemy
                        if (z is Rectangle && (string)z.Tag == "minion")
                        {
                            // make a local rect called enemy and put the enemies properties into it
                            Rect minion = new Rect(Canvas.GetLeft(z), Canvas.GetTop(z), z.Width, z.Height);
                            // now check if bullet and enemy is colliding or not
                            // if the bullet is colliding with the enemy rectangle
                            if (bullet.IntersectsWith(minion))
                            {
                                itemstoremove.Add(x); // remove bullet
                                itemstoremove.Add(z); // remove enemy

                            }
                        }
                        
                    }
                    

                    
                    foreach (var y in myCanvas.Children.OfType<Rectangle>())
                    {
                        // if y is a rectangle and it has a tag called enemy
                        //if (y is Rectangle && (string)y.Tag == "enemy")
                        {
                            // make a local rect called enemy and put the enemies properties into it
                            Rect enemy = new Rect(Canvas.GetLeft(boss), Canvas.GetTop(boss), boss.Width, boss.Height);
                            // now check if bullet and enemy is colliding or not
                            // if the bullet is colliding with the enemy rectangle
                            if (bullet.IntersectsWith(enemy))
                            {
                                itemstoremove.Add(x); // remove bullet
                                damagereal = DataFromWindow1;
                                bosshealth.Value -= damagereal;
                            }
                        }
                    }
                }
                if (x is Rectangle && (string)x.Tag == "bossbullet")
                {
                    // if we find a rectangle with the enemy tag
                    Canvas.SetTop(x, Canvas.GetTop(x) + 10); // move the enemy downwards
                                                             // make a new enemy rect for enemy hit box
                    Rect bossbullet = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    // first check if the enemy object has gone passed the player meaning
                    // its gone passed 700 pixels from the top
                    if (Canvas.GetTop(x) + 150 > 700)
                    {
                        // if so first remove the enemy object
                        itemstoremove.Add(x);

                    }

                    // outside the second loop lets check for the enemy again
                    if (playerHitBox.IntersectsWith(bossbullet))
                    {

                        itemstoremove.Add(x); // remove the enemy object
                        pHealth.Value -= 10;
                    }
                }
                if (x is Rectangle && (string)x.Tag == "minion")
                {
                    // if we find a rectangle with the enemy tag
                    Canvas.SetTop(x, Canvas.GetTop(x) + 5); // move the enemy downwards
                                                             // make a new enemy rect for enemy hit box
                    Rect minion = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    // first check if the enemy object has gone passed the player meaning
                    // its gone passed 700 pixels from the top
                    if (Canvas.GetTop(x) + 150 > 700)
                    {
                        // if so first remove the enemy object
                        itemstoremove.Add(x);
                        pHealth.Value -= 5;
                        bosshealth.Value += 5;

                    }
                    // if the player hit box and the enemy is colliding 
                    if (playerHitBox.IntersectsWith(minion))
                    {
                        pHealth.Value -= 5;
                       
                        itemstoremove.Add(x); // remove the enemy object
                    }



                }
                // outside the second loop lets check for the enemy again

                // if the player hit box and the enemy is colliding 

            }
            if (bosshealth.Value==0)
            {
                gameTimer.Stop(); // stop the main timer
                
                MessageBox.Show("Well Done Star Captain!" + Environment.NewLine + "You have destroyed the Alien ships");
                // show the message box with the message inside of it
            }
            if(pHealth.Value == 0)
            {
                gameTimer.Stop(); // stop the main timer

                MessageBox.Show("Skill Issue For real"  + Environment.NewLine + "Get gud ");
            }
            foreach (Rectangle y in itemstoremove)
            {
                // remove them permanently from the canvas
                myCanvas.Children.Remove(y);
            }
        }
    }
}
