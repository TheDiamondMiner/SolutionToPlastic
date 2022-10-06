using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace SolutionToPlastic.Views
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public static CustomMessageBox MessageBox2 = new CustomMessageBox();
        private CustomMessageBox()
        {
            InitializeComponent();
        }
    }

    public class MessageBoxViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private int percentRecycled = 0;
        private string[] statedetails = { "STATE:", "PLASTIC RECYCLED (KG):", "PLASTIC RECYCLED (TON):", "YEAR: 2021-2022" };
        
        public string[] StateDetails
        {
            get { return statedetails; }
            set { 
                statedetails = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StateDetails"));
            }
        }

        public int PercentRecycled
        {
            get { return percentRecycled; }
            set
            {
                percentRecycled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PercentRecycled"));
            }
        }

    }
}
