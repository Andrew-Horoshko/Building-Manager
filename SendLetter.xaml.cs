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
using System.Windows.Shapes;

namespace BuidingManager
{
    /// <summary>
    /// Логика взаимодействия для SendLetter.xaml
    /// </summary>
    public partial class SendLetter : Window
    {
        public SendLetter()
        {
            InitializeComponent();
        }

        private void Button_Click_Cancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_Send(object sender, RoutedEventArgs e)
        {

            SmtpClient smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential("myappbuilder56@gmail.com", "buildingmanager")//The email which sends a letter. 
                };
            try
            {
                MailAddress from = new MailAddress("myappbuilder56@gmail.com", "User");
                MailAddress to = new MailAddress("andrii.horoshko.pz.2019@lpnu.ua");//The email to receive letter.
                MailMessage m = new MailMessage(from, to);
                m.Subject = "Програма, питання";
                m.Body = Letter.Text;
                smtp.Send(m);
            }
            catch 
            {
                MessageBox.Show("Error ,unable to identify", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
            finally
            {
                MessageBox.Show("It's gone ,baby!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            this.Close();
            
        }
    }
}
