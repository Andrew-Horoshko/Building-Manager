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

namespace BuidingManager
{
    /// <summary>
    /// Window for user to see information about where to write to if questions appear
    /// </summary>
    public partial class Email : Window
    {
        public Email()
        {
            InitializeComponent();
        }

        private void Button_Click_Copy(object sender, RoutedEventArgs e)
        {

            //copying the email adress for user prfisiency
            Clipboard.SetText("safer.andy@gmail.com");
        }

        private void Button_Click_Ok(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Letter(object sender, RoutedEventArgs e)
        {
            SendLetter windowLetter = new SendLetter();
            windowLetter.Show();
            this.Close();

        }
    }
}
