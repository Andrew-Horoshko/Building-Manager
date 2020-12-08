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
    public partial class SetPrices : Window
    {
        public SetPrices()
        {
            InitializeComponent();
        
        }

        private void WaterPrice_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (WaterPrice.Text == "Water Price") WaterPrice.Text = "";
        }

        private void WaterPrice_MouseLeave(object sender, MouseEventArgs e)
        {
            if (WaterPrice.Text == "") WaterPrice.Text = "Water Price";
        }

        private void ElectrisityPrice_MouseEnter(object sender, MouseEventArgs e)
        {
            if (ElectrisityPrice.Text == "Electrisity Price") ElectrisityPrice.Text = "";
        }

        private void ElectrisityPrice_MouseLeave(object sender, MouseEventArgs e)
        {
            if (ElectrisityPrice.Text == "") ElectrisityPrice.Text = "Electrisity Price";
        }

        private void Button_Click_Prices(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
