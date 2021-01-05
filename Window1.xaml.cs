using System.Windows;
using System.Windows.Controls;

namespace BuidingManager
{
    public delegate void DataTransferDelegate(string data);
    public partial class Window1 : Window
    {
        /// <summary>
        /// Window with our main DataGrid and endless posibilities to change arround year data
        /// </summary>

         Building currBuilding = new Building();
        public Window1()
        {
            InitializeComponent();
            currBuilding.LinkToTable(BuildingStats);
            currBuilding.YearInitialize(Year);
        }
        private void Button_Click_Go_Back(object sender, RoutedEventArgs e)
        {
           new MainWindow().Show();
            this.Close();
        }
        private void Month_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox_month = (ComboBox)sender;
           currBuilding.ChangeMonth(comboBox_month);
        }
        private void Year_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = (ComboBox)sender;
            currBuilding.OpenFile(comboBox, NewFileName);
        }
        private void Button_Click_Save(object sender, RoutedEventArgs e)
        {
            currBuilding.Save(NewFileName, Year);
        }
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            currBuilding.DeleteFile(Year);
        }

        private void Button_Click_Month(object sender, RoutedEventArgs e)
        {
            currBuilding.NoDataForMonth(MonthDataGrid);
        }

        private void Button_Click_Change(object sender, RoutedEventArgs e)
        {
            currBuilding.ManageFile();
        }

        private void Button_Click_Set_Price(object sender, RoutedEventArgs e)
        {
            currBuilding.SetPrice();
        }

        private void Button_Click_Time_Money(object sender, RoutedEventArgs e)
        {
            currBuilding.BillCalculation();
        }
    } 
}
