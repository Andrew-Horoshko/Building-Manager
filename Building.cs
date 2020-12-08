using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BuidingManager
{
    class Building
    {
        private bool IsPriceChanged = false;
        private SetPrices pricewindow;
        private DataGrid MonthTable;
        private DataGrid BuildingStats;
        private List<string> FileName = new List<string>() { "Choose Year", "2001", "2000", "template" };
        private string filenames;
        private string chosenMonth;
        private string chosenYear;
        private  string file;
        private string[] SeparatedTextFromFile;
        private string TextFromFile;
        private ComboBox comboBox_month;
        private List<Apartment> BuildingApartments = new List<Apartment>(6);
        private Dictionary<string, int> currMonthIndex = new Dictionary<string, int>();
        private readonly string[] monthArray = { "January", "February" , "March", "April" , "May" , "June", "July", "August", "September",
            "October" ,"November","December"};
        private readonly string[] shortenedMonthArray = { "Jan", "Fb" , "Mr", "Apr" , "May" , "Jun", "Jul", "Aug", "Sp",
            "Ot" ,"Nv","Dc"};
        private List<string> monthWithoutData  = new List<string>(10);
         public Building()
        {

            for (int i = 0; i < monthArray.Length; i++)
            {
                currMonthIndex.Add(monthArray[i], i * 51);
            }
           
        }
      
        public void LinkToTable(System.Windows.Controls.DataGrid BuildingStats)
        {
            this.BuildingStats = BuildingStats;
        }
        private void PrintOutData()
        {
            BuildingApartments.Clear();
            BuildingStats.ItemsSource = "";
            if (SeparatedTextFromFile[currMonthIndex[chosenMonth]] == chosenMonth)
            {

                for (int i = 0; i < 10; i++)
                {
                    BuildingApartments.Add(new Apartment(int.Parse(SeparatedTextFromFile[currMonthIndex[chosenMonth] + 1 + (i * 5)]),
                        SeparatedTextFromFile[currMonthIndex[chosenMonth] + 2 + (i * 5)], SeparatedTextFromFile[currMonthIndex[chosenMonth] + 3 + (i * 5)],
               double.Parse(SeparatedTextFromFile[currMonthIndex[chosenMonth] + 4 + (i * 5)]), double.Parse(SeparatedTextFromFile[currMonthIndex[chosenMonth] + 5 + (i * 5)])));

                }
                BuildingStats.ItemsSource = BuildingApartments;
            }
        }
        public void YearInitialize (ComboBox Year)
        {
            Year.ItemsSource = FileName;
            for (int i = 0; i < FileName.Count; i++) filenames += FileName[i] + ",";
            Year.SelectedIndex = 0;
        }
        public void OpenFile(ComboBox comboBox ,TextBox NewFileName)
        {
            if(MonthTable!= null)MonthTable.ItemsSource = "";
            if (comboBox.SelectedIndex != 0 && comboBox.SelectedIndex > 0)
            {
                this.chosenYear = FileName[comboBox.SelectedIndex];
                if (chosenYear == "template")
                {
                    NewFileName.IsReadOnly = false;
                    NewFileName.Text = "New File Name";

                }
                else
                {
                    NewFileName.IsReadOnly = true;
                    NewFileName.Text = "";
                }
                file = "";
                file = @"C:\Users\Bob\Desktop\3kurs\BuidingManager\" + chosenYear + ".txt";
                TextFromFile = File.ReadAllText(file);
                SeparatedTextFromFile = TextFromFile.Split(' ');
                comboBox_month.SelectedIndex = 0;
                
            }
        }
        public void ChangeMonth(System.Windows.Controls.ComboBox comboBox_month)
        {
            this.comboBox_month = comboBox_month;
            System.Windows.Controls.ComboBoxItem month = (System.Windows.Controls.ComboBoxItem)comboBox_month.SelectedItem;
            if (comboBox_month.SelectedIndex != 0)
            {
               chosenMonth = month.Content.ToString();
                PrintOutData();
            }
            else
            {
                
                BuildingApartments.Clear();
                if(BuildingStats != null)BuildingStats.ItemsSource = "";
                
            }
        }
        public void ManageFile(TextBox NewFileName ,ComboBox Year)
        {
            int currWorkingMonth = currMonthIndex[chosenMonth];
            for (int i = 0; i < 10; i++)
            {
                SeparatedTextFromFile[currWorkingMonth + 2 + (i * 5)] = BuildingApartments[i].OwnersName;
                SeparatedTextFromFile[currWorkingMonth + 3 + (i * 5)] = BuildingApartments[i].OwnersSurname;
                SeparatedTextFromFile[currWorkingMonth + 4 + (i * 5)] = BuildingApartments[i].WaterUsedInCuboMetres.ToString();
                SeparatedTextFromFile[currWorkingMonth + 5 + (i * 5)] = BuildingApartments[i].ElectrisityUsedInKiloWats.ToString();
                if (i == 9) SeparatedTextFromFile[currWorkingMonth + 5 + (i * 5)] += "\n";
            }
            GetPrice();
        }
        public void Save(TextBox NewFileName, ComboBox Year)
        {
            GetPrice();
            string newFileName = @"C:\Users\Bob\Desktop\3kurs\BuidingManager\";
            if (chosenYear != "template")
            {
                newFileName += chosenYear + "copy";

                Year.ItemsSource = "";
                FileName.Clear();
                Year.ItemsSource = FileName;
                filenames += chosenYear + "copy" + ",";
                string[] temp = filenames.Split(',');
                for (int i = 0; i < temp.Length; i++) if (temp[i] != "") FileName.Add(temp[i]);
                Year.SelectedIndex = 0;


            }
            else
            {
                newFileName += NewFileName.Text.ToString();

                Year.ItemsSource = "";
                FileName.Clear();
                Year.ItemsSource = FileName;

                filenames += NewFileName.Text.ToString() + ",";
                string[] temp = filenames.Split(',');
                for (int i = 0; i < temp.Length; i++) if (temp[i] != "") FileName.Add(temp[i]);
                Year.SelectedIndex = 0;

            }
            newFileName += ".txt";
            System.IO.FileStream fstream = File.Create(newFileName);
            for (int i = 0; i < SeparatedTextFromFile.Length; i++)
            {
                byte[] array = System.Text.Encoding.Default.GetBytes(SeparatedTextFromFile[i] + " ");
                // запись массива байтов в файл
                fstream.Write(array, 0, array.Length);

            }
        }
        public void DeleteFile(ComboBox Year)
        {
          
            if(chosenYear != "template" && chosenYear != "2000" && chosenYear != "2001" && chosenYear != "Choose Year")
            {
                File.Delete(file);
                Year.ItemsSource = "";
                FileName.Clear();
                Year.ItemsSource = FileName;
                string[] temp = filenames.Split(',');
                filenames = "";
             
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i] != chosenYear && temp[i]!= "")
                    {
                        FileName.Add(temp[i]);
                        filenames += temp[i] + ",";
                    }
                }
                Year.SelectedIndex = 0;
            }
        }
        public void NoDataForMonth(DataGrid MonthTable)
        {
            this.MonthTable = MonthTable;
            MonthTable.ItemsSource = "";
            monthWithoutData.Clear();
            for(int i = 0;i < 10; i++) monthWithoutData.Add("");
            for (int i = 0 , j = 0; i < 10; i++)
            {
                if (double.Parse(SeparatedTextFromFile[currMonthIndex[monthArray[j]] + 4 + (i * 5)]) == 0)
                {
                    monthWithoutData[i] += shortenedMonthArray[j] + " ";
                }
             
                if( i == 9 && j != 11)
                { 
                    i = -1;
                    j++;
                }
            }   
                for(int i = 0; i < 10; i++) if(monthWithoutData[i] == "")  monthWithoutData[i] += "none";
           
            MonthTable.ItemsSource = monthWithoutData;
        }
        private void GetPrice()
        {
            if (IsPriceChanged)
            {
                SeparatedTextFromFile[613] = pricewindow.WaterPrice.Text + "\n";
                SeparatedTextFromFile[615] = pricewindow.ElectrisityPrice.Text;
                pricewindow.Close();
                IsPriceChanged = false;
            }
        }
        public void SetPrice()
        {
            pricewindow = new SetPrices();
            pricewindow.Show();
            IsPriceChanged = true;
        }
        public void BillCalculation()
        {
            string[] resultingOwnerAndFlat = WhoSpentLess().Split(',');
            
            Bills windowbills = new Bills(resultingOwnerAndFlat[1] , resultingOwnerAndFlat[0]);
            windowbills.Show();
        }
        private string WhoSpentMore( double [] waterBills ,double [] electrisityBills)
        {
            int whospenttheleast = 0;
            string owner = "";
            for (int i = 0; i < 10; i++) if (waterBills[i] + electrisityBills[i]  < waterBills[whospenttheleast]
                    +electrisityBills[whospenttheleast]) whospenttheleast = i;
            owner += SeparatedTextFromFile[currMonthIndex[monthArray[0]] + 2 + (whospenttheleast * 5)] + " " +
                SeparatedTextFromFile[currMonthIndex[monthArray[0]] + 3 + (whospenttheleast * 5)];
            return owner;
        }
        private string WhoSpentLess()
        {
            string results = "";
            string filetemp;
            string TextFromFiletemp;
            int spentthemost = 0;
            string[] SeparatedTextFromFiletemp;
            string[] openedYears =  filenames.Split(',');
            double[] waterBills = new double[10];
            double[] electrisityBills = new double[10];
            double[] yearwaterBills = new double[10];
            double[] yearelectrisityBills = new double[10];
            for (int m = 0; m < openedYears.Length; m++)
            {
                if (openedYears[m] != "template" && openedYears[m] != "Choose Year")
                {
                    filetemp = "";
                    filetemp = @"C:\Users\Bob\Desktop\3kurs\BuidingManager\" + "2000" + ".txt";
                    TextFromFiletemp = File.ReadAllText(filetemp);
                    SeparatedTextFromFiletemp = TextFromFiletemp.Split(' ');

                    for (int i = 0, j = 0; i < 10; i++)
                    {
                        yearwaterBills[i] += double.Parse(SeparatedTextFromFile[currMonthIndex[monthArray[j]] + 4 + (i * 5)])
                             * double.Parse(SeparatedTextFromFile[613]);
                        yearelectrisityBills[i] += double.Parse(SeparatedTextFromFile[currMonthIndex[monthArray[j]] + 5 + (i * 5)])
                             * double.Parse(SeparatedTextFromFile[615]);
                        if (i == 9 && j != 11)
                        {
                            i = -1;
                            j++;
                        }
                    }
                    if (openedYears[m] == chosenYear) results += WhoSpentMore(yearwaterBills, yearelectrisityBills);
                    for (int i = 0; i < 10;i++)
                    {
                        waterBills[i] += yearwaterBills[i];
                        electrisityBills[i] = yearelectrisityBills[i];
                        yearelectrisityBills[i] = 0;
                        yearwaterBills[i] = 0;
                    }
                }
            }
            for (int i = 0; i < 10; i++) if (waterBills[i] + electrisityBills[i] > waterBills[spentthemost]
                      + electrisityBills[spentthemost]) spentthemost = i;
         
            string flat = SeparatedTextFromFile[currMonthIndex["January"] + 1 + (spentthemost * 5)] + "," +results;
            return flat;
        }
    }
}
