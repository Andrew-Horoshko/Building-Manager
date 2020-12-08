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
    /// Логика взаимодействия для Bills.xaml
    /// </summary>
    public partial class Bills : Window
    {
        public Bills(string owner ,string apartment)
        {
            InitializeComponent();
            Person.Text = owner;
            Flat.Text = apartment.Trim();
        }
    }
}
