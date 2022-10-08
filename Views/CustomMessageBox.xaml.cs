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
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }

    public class MessageBoxViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private int percentRecycled = 0;
        private string[] statedetails = { "STATE:", "PLASTIC RECYCLED (KG):", "PLASTIC RECYCLED (TON):", "YEAR: 2021-2022" };
        
        public string StateName
        {
            get { return statedetails[0]; }
            set { 
                statedetails[0] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StateName"));
            }
        }
        public string RecycledKG
        {
            get { return statedetails[1]; }
            set
            {
                statedetails[1] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RecycledKG"));
            }
        }
        public string RecycledTON
        {
            get { return statedetails[2]; }
            set
            {
                statedetails[2] = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("RecycledTON"));
            }
        }
        public string Percent
        {
            get { return $"{PercentRecycled}%"; }
            set { 
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Percent"));
            }
        }
        public int PercentRecycled
        {
            get { return percentRecycled; }
            set
            {
                percentRecycled = value;
                Percent = percentRecycled.ToString();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PercentRecycled"));
            }
        }
    }
}
