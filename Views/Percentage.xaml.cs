using System;
using SolutionToPlastic;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SolutionToPlastic.ViewModels;

namespace SolutionToPlastic.Views
{
    public partial class Percentage : UserControl
    {
        public Percentage()
        {
            InitializeComponent();
        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var viewModel = ViewModel.viewModel;
            Ellipse state = (Ellipse)sender;
            Point mousepos = e.GetPosition(Canvas);
            double posX = mousepos.X - 5;
            double posY = mousepos.Y + 25;
            viewModel.SetData.Execute($"{posX},{posY},{state.ToolTip.ToString().ToUpper()}");
        }
    }
}