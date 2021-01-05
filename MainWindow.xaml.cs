using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace BuidingManager
{
    /// <summary>
    /// Main menu
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Button_Click_GitHub(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Andrew-Horoshko");
        }

        private void Button_Click_Instagram(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://www.instagram.com/vsegda_ystalui/");
        }


        private void Button_Click_Data(object sender, RoutedEventArgs e)
        {
            new Window1().Show();
            this.Close();
        
        }

        private void Button_Click_Mail(object sender, RoutedEventArgs e)
        {
            Email mail = new Email();
            mail.Show();
        }

        private void Button_Click_Help(object sender, RoutedEventArgs e)
        {
            Help window = new Help();
            window.Show();
        }
    }
}
