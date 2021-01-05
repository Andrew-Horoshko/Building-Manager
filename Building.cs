using Microsoft.Win32;
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
        private bool IsPriceChanged = false; //variable to keep track of initialized Bill window objects
        private SetPrices pricewindow; // window variable for SetPrices window
        private DataGrid MonthTable; // Our DataGrid for month that don't have values 
        private DataGrid BuildingStats; // Main DataGrid where we store and edit our information about each apartment
        private  List<string> FileName = new List<string>() { "Choose Year", "Choose custom","2001", "2000", "template" }; // List for all opened files that can be choosen in ComboBox ,including default 
        private string filenames; // string which we use to add or remove years from ComboBox
        private string chosenMonth; // current chosen in application month
        private string chosenYear; // curent chosen year from ComboBox
        private  string file; // file name 
        private string[] SeparatedTextFromFile; // each word or number read from file and splited
        private string TextFromFile;// String containing every character from file ,including spaces
        private ComboBox comboBox_month; // ComboBox which user uses to select the month to be shown
        private ComboBox Year;// ComboBox which user uses to select the year to be shown
        private List<Apartment> BuildingApartments = new List<Apartment>(6); // List of objects of type Apartment class which we bind to our DataGrid BuildingStats
        private Dictionary<string, int> currMonthIndex = new Dictionary<string, int>(); // Dictionary with values of month names and keys with the index of each month in seperated string array
        private readonly string[] monthArray = { "January", "February" , "March", "April" , "May" , "June", "July", "August", "September",
            "October" ,"November","December"}; // array with month names for currMonthIndex Dictionary
        private readonly string[] shortenedMonthArray = { "Jan", "Fb" , "Mr", "Apr" , "May" , "Jun", "Jul", "Aug", "Sp",
            "Ot" ,"Nv","Dc"}; // Array with short month names which we use to show the month without information about electrisity or water usage
        private List<string> monthWithoutData  = new List<string>(10); // list  which stores all the month for each apartment that have got no information
         private  Bills windowbills;// window for the person who spent the least and the apartment who's owner spent the most
         //Constructor which initializes our Dictionary
        public Building()
        {

            for (int i = 0; i < monthArray.Length; i++)
            {
                currMonthIndex.Add(monthArray[i], i * 51);
            }
           
        }
       // Getting the table from window1 
        public void LinkToTable(DataGrid BuildingStats)
        {
            this.BuildingStats = BuildingStats;
        }
        // Printing the list of apartment information for chosen month and year
        private void PrintOutData()
        {
            try
            {
                BuildingApartments.Clear();    // Clearing our DataGrid from old info
                BuildingStats.ItemsSource = "";
                // Getting new info
                if (SeparatedTextFromFile != null && SeparatedTextFromFile[currMonthIndex[chosenMonth]] == chosenMonth   ) 
                {

                    for (int i = 0; i < 10; i++)
                    {
                        BuildingApartments.Add(new Apartment(int.Parse(SeparatedTextFromFile[currMonthIndex[chosenMonth] + 1 + (i * 5)]),
                            SeparatedTextFromFile[currMonthIndex[chosenMonth] + 2 + (i * 5)], SeparatedTextFromFile[currMonthIndex[chosenMonth] + 3 + (i * 5)],
                   double.Parse(SeparatedTextFromFile[currMonthIndex[chosenMonth] + 4 + (i * 5)]), double.Parse(SeparatedTextFromFile[currMonthIndex[chosenMonth] + 5 + (i * 5)])));

                    }
                    // Outputing new info to DataGrid
                    BuildingStats.ItemsSource = BuildingApartments;
                }
                else
                {
                    comboBox_month.SelectedIndex = 0;
                }
            }
            catch
            {
                MessageBox.Show("Error The file was corrupted", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        // Initializing the string containing names of years from the ComboBox (which we transfer through parameter)
        //Linking the ComboBox to the list with current allowed years and default choice
        public void YearInitialize (ComboBox Year)
        {
            this.Year = Year;
            Year.ItemsSource = FileName;
            for (int i = 0; i < FileName.Count; i++) filenames += FileName[i] + ",";
            //Setting the default choice
            Year.SelectedIndex = 0;
        }
        // Opening the chosen file and reading it into our variables
        public void OpenFile(ComboBox comboBox ,TextBox NewFileName)
        {
            try
            {  // Tracking if we already linked 
                if (MonthTable != null) MonthTable.ItemsSource = "";
                // Making sure the year is chosen and not the default answer
                if (comboBox.SelectedIndex != 0 && comboBox.SelectedIndex > 0 && comboBox.SelectedIndex != 1)
                {
                    
                    //saving the chosen year name (file name)
                    this.chosenYear = FileName[comboBox.SelectedIndex];
                    //allowing and showing text box for making new file and saving it
                    if (chosenYear[0] != 'C')
                    {
                        if (chosenYear == "template")
                        {
                            NewFileName.IsReadOnly = false;
                            NewFileName.Text = "New File Name";

                        }
                        //else checking the field for name is unavailable
                        else
                        {
                            NewFileName.IsReadOnly = true;
                            NewFileName.Text = "";
                        }
                        // Making the file name
                        file = "";
                        file = @"C:\Users\Bob\Desktop\3kurs\BuidingManager\" + chosenYear + ".txt";
                        //Reading and assigning text to variables
                    }
                    else
                    {
                        file = "";
                        file = chosenYear;
                    }
                    TextFromFile = File.ReadAllText(file);
                    SeparatedTextFromFile = TextFromFile.Split(' ');
                    //Setting month ComboBox to default choice
                    comboBox_month.SelectedIndex = 0;

                }
                else if(comboBox.SelectedIndex == 1)
                {
                    OpenFileDialog openFileDialog = new OpenFileDialog();
                    if (openFileDialog.ShowDialog() == true)
                        MessageBox.Show(openFileDialog.FileName);

                    Year.ItemsSource = "";
                    FileName.Clear();
                    Year.ItemsSource = FileName;

                    filenames += openFileDialog.FileName + ",";
                    string[] temp = filenames.Split(',');
                    for (int i = 0; i < temp.Length; i++) if (temp[i] != "") FileName.Add(temp[i]);
                    Year.SelectedIndex = 0;
                  
                }
            }
            catch
            {
                MessageBox.Show("Error Couldn't open  file ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Changing the month that is currently outputed to DataGrid 
        public void ChangeMonth(ComboBox comboBox_month)
        {
            this.comboBox_month = comboBox_month;
           ComboBoxItem month = (ComboBoxItem)comboBox_month.SelectedItem;
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
        //Editing opened File  and saving current changes in application
        public void ManageFile()
        {
            if (Year != null && Year.SelectedIndex != 0)
            {
                int currWorkingMonth = currMonthIndex[chosenMonth];
                //Getting values ,parsing into double when needed
                for (int i = 0; i < 10; i++)
                {
                    SeparatedTextFromFile[currWorkingMonth + 2 + (i * 5)] = BuildingApartments[i].OwnersName;
                    SeparatedTextFromFile[currWorkingMonth + 3 + (i * 5)] = BuildingApartments[i].OwnersSurname;
                    SeparatedTextFromFile[currWorkingMonth + 4 + (i * 5)] = BuildingApartments[i].WaterUsedInCuboMetres.ToString();
                    SeparatedTextFromFile[currWorkingMonth + 5 + (i * 5)] = BuildingApartments[i].ElectrisityUsedInKiloWats.ToString();
                    if (i == 9) SeparatedTextFromFile[currWorkingMonth + 5 + (i * 5)] += "\n";
                }
                // Getting user price 
                GetPrice();
            }
            else
            {
                MessageBox.Show("Ooops ,sounds like there's a problem ,your missing a file ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Saving all the edits into a new file 
        public void Save(TextBox NewFileName, ComboBox Year)
        {
            if (Year != null && Year.SelectedIndex != 0)
            {
                //Getting the price if it was changed
                GetPrice();
                string newFileName = @"C:\Users\Bob\Desktop\3kurs\BuidingManager\";
                //in case the chosen file is template getting the name of the new file from the field
                //Othervise naming it a copy of chosen year
                if (chosenYear != "template")
                {
                    Year.ItemsSource = "";
                    FileName.Clear();
                    Year.ItemsSource = FileName;
                    if (chosenYear[0] != 'C')
                    {
                        newFileName += chosenYear + "copy";
                        filenames += chosenYear + "copy" + ",";
                    }
                    else
                    {
                        newFileName = chosenYear;
                    }


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
                //Creating a file with chosen name and outputing the info
                try
                {
                    System.IO.FileStream fstream = File.Create(newFileName);



                    for (int i = 0; i < SeparatedTextFromFile.Length; i++)
                    {
                        byte[] array = System.Text.Encoding.Default.GetBytes(SeparatedTextFromFile[i] + " ");
                        // запись массива байтов в файл
                        fstream.Write(array, 0, array.Length);

                    }
                }
                catch
                {
                    MessageBox.Show("Error Couldn't create a file ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Ooops ,sounds like there's a problem ,your missing a file ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Deleting files from application and from the pc (those that where created by user
        public void DeleteFile(ComboBox Year)
        {
            if (Year != null && Year.SelectedIndex != 0)
            {
                try
                {
                    if (chosenYear != "template" && chosenYear != "2000" && chosenYear != "2001" && chosenYear != "Choose Year")
                    {
                        MessageBoxResult result = MessageBox.Show("You sure ,you really want to delete this file?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (result == MessageBoxResult.Yes)
                        {
                            File.Delete(file);
                            Year.ItemsSource = "";
                            FileName.Clear();
                            Year.ItemsSource = FileName;
                            string[] temp = filenames.Split(',');
                            filenames = "";

                            for (int i = 0; i < temp.Length; i++)
                            {
                                if (temp[i] != chosenYear && temp[i] != "")
                                {
                                    FileName.Add(temp[i]);
                                    filenames += temp[i] + ",";
                                }
                            }
                            Year.SelectedIndex = 0;
                            comboBox_month.SelectedIndex = 0;
                        }

                    }
                    else
                    {
                        MessageBox.Show("Hey,stop that , you can't delete those!! ", "Hey ,man ,i'm sorry ,but no.", MessageBoxButton.OK, MessageBoxImage.Warning);

                    }
                }

                catch
                {
                    MessageBox.Show("Error You haven't chosen a file ...", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                MessageBox.Show("Ooops ,sounds like there's a problem ,your missing a file ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Getting the month without data and showing them it the dedicated DataGrid
        public void NoDataForMonth(DataGrid MonthTable)
        {
            if (Year != null && Year.SelectedIndex != 0)
            {
                this.MonthTable = MonthTable;
                MonthTable.ItemsSource = "";
                monthWithoutData.Clear();
                for (int i = 0; i < 10; i++) monthWithoutData.Add("");
                for (int i = 0, j = 0; i < 10; i++)
                {
                    if (double.Parse(SeparatedTextFromFile[currMonthIndex[monthArray[j]] + 4 + (i * 5)]) == 0)
                    {
                        monthWithoutData[i] += shortenedMonthArray[j] + " ";
                    }

                    if (i == 9 && j != 11)
                    {
                        i = -1;
                        j++;
                    }
                }
                for (int i = 0; i < 10; i++) if (monthWithoutData[i] == "") monthWithoutData[i] += "none";

                MonthTable.ItemsSource = monthWithoutData;
            }
        
            else
            {
                MessageBox.Show("Ooops ,sounds like there's a problem ,your missing a file ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    //Saving the user set prices for water and electrisity for current year
    private void GetPrice()
        {
            try
            {
                if (IsPriceChanged)
                {
                    if (double.Parse(pricewindow.WaterPrice.Text) < 20 && double.Parse(pricewindow.WaterPrice.Text) > 0 &&
                        double.Parse(pricewindow.ElectrisityPrice.Text) > 0 && double.Parse(pricewindow.ElectrisityPrice.Text) < 20)
                    {
    
                        SeparatedTextFromFile[613] = pricewindow.WaterPrice.Text + "\n";
                        SeparatedTextFromFile[615] = pricewindow.ElectrisityPrice.Text;
                    }
                    else
                    {
                        MessageBox.Show("Error Incorect data  ,number sould be above 0 and below 20 ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                        pricewindow.Close();
                    IsPriceChanged = false;
                }
            }
            catch
            {
                MessageBox.Show("Error Incorect data format , try a number ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
        }
        //Allowing user to set prices for water and electrisity
        public void SetPrice()
        {
            if (Year != null && Year.SelectedIndex != 0)
            {
                try
                {
                    pricewindow = new SetPrices();
                    pricewindow.Show();
                    IsPriceChanged = true;
                }
                catch
                {
                    MessageBox.Show("Ooops ,sounds like there's a problem ,your missing a file ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Ooops ,sounds like there's a problem ,your missing a file ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        //Calculating who spent the least and the other way around
        public void BillCalculation()
        {
            string[] resultingOwnerAndFlat = WhoSpentMore().Split(',');
            if (chosenYear == "template") resultingOwnerAndFlat[1] = "Incorect File";
            windowbills = new Bills(resultingOwnerAndFlat[1] , resultingOwnerAndFlat[0]);
            windowbills.Show();
            
        }
        //Calculating who spent the least in chosen year
        private string WhoSpentLess( double [] waterBills ,double [] electrisityBills)
        {
            int whospenttheleast = 0;
            string owner = "";
            for (int i = 0; i < 10; i++) if (waterBills[i] + electrisityBills[i]  < waterBills[whospenttheleast]
                    +electrisityBills[whospenttheleast]) whospenttheleast = i;
            owner += SeparatedTextFromFile[currMonthIndex[monthArray[0]] + 2 + (whospenttheleast * 5)] + " " +
                SeparatedTextFromFile[currMonthIndex[monthArray[0]] + 3 + (whospenttheleast * 5)];
            return owner;
        }
        //Calculating who spent the most in all uploaded years
        private string WhoSpentMore()
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
                    try
                    {
                        filetemp = "";
                        filetemp = @"C:\Users\Bob\Desktop\3kurs\BuidingManager\" + "2000" + ".txt";
                        TextFromFiletemp = File.ReadAllText(filetemp);
                        SeparatedTextFromFiletemp = TextFromFiletemp.Split(' ');
                    }
                    catch
                    {
                        MessageBox.Show("Error Could not open file ", "Error" ,MessageBoxButton.OK    ,MessageBoxImage.Error);
                        return "";
                    }
                    try
                    {
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
                    }
                    catch
                    {
                        MessageBox.Show("Error Invalid data ,check your files ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return "";
                    }
                    try
                    {
                        if (openedYears[m] == chosenYear) results += WhoSpentLess(yearwaterBills, yearelectrisityBills);
                    }
                    catch
                    {
                        MessageBox.Show("Error Bad Data ", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return "";
                    }
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
