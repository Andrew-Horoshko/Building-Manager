using System.Windows;

namespace BuidingManager
{
    /// <summary>
    /// Window for showing usage records
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
