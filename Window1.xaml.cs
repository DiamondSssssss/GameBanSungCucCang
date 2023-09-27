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
using System.Windows.Shapes;

namespace Moving_cube
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void easy(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            

            // Set the DataFromWindow1 property on the main instance
            main.DataFromWindow1 = 1;
            main.Show();
            // Show the main window
        }

        private void hard(object sender, RoutedEventArgs e)
        {
            MainWindow main = new MainWindow();
            main.Show();

            // Set the DataFromWindow1 property on the main instance
            main.DataFromWindow1 = 0.1;

            // Show the main window
            
        }
    }
}
