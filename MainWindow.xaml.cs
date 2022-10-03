using SolutionToPlastic.ViewModels;
using SolutionToPlastic.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SolutionToPlastic
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            ViewModel viewModel = (ViewModel)DataContext;
            string[] viewNames = new string[6] { "Introduction", "Inventions", "Percentage", "Projects", "PlasticComposition", "ThankYou" };
            int i = 0;

            switch (e.Key)
            {
                case Key.T: //For Toggling Automation on and off
                    viewModel.Automation = !viewModel.Automation;
                    while (viewModel.Automation is true)
                    {
                        viewModel.Switcher.Execute(viewNames[i]);
                        await Task.Delay(500);
                        i++;
                        if (i > 5)
                        {
                            i = 0;
                        }
                    }
                    break;

                case Key.U:
                    break;

                case Key.Left:
                    break;
            }
        }
    }

    public class ViewModel : PsudeoViewModel, INotifyPropertyChanged
    { 
        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand Switcher { get; }
        public bool Automation = false;

        public ViewModel()
        {
            Switcher = new ViewSwitcher(this);
        }

        private PsudeoViewModel currentview = new ViewModels.Introduction();

        public PsudeoViewModel CurrentView
        {
            get { return currentview; }
            set {
                currentview = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentView"));
            }
        }
    }
    public class PsudeoViewModel { }

    public class ViewSwitcher : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public ViewModel vm;

        public ViewSwitcher(ViewModel owner)
        {
            vm = owner;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            switch(parameter.ToString())
            {
                case "Introduction":
                    vm.CurrentView = new ViewModels.Introduction();
                    break;

                case "Percentage":
                    vm.CurrentView = new PercentagePage();
                    break;

                case "Projects":
                    vm.CurrentView = new ProjectsDone();
                    break;

                case "Inventions":
                    vm.CurrentView = new InventionsMade();
                    break;

                case "PlasticComposition":
                    vm.CurrentView = new ViewModels.PlasticComposition();
                    break;

                case "ThankYou":
                    vm.CurrentView = new ThankYouCredits();
                    break;
            }
        }
    }
}

namespace SolutionToPlastic.ViewModels
{
    public class Introduction : PsudeoViewModel {}
    public class PercentagePage : PsudeoViewModel {} // For Percentage of plastic recycled by Indian states and country
    public class ProjectsDone : PsudeoViewModel {}   // Team Seas and Swatchda bharat etc
    public class InventionsMade : PsudeoViewModel {} // Inventions made as a solution to plastic
    public class PlasticComposition : PsudeoViewModel {}
    public class ThankYouCredits : PsudeoViewModel {}
}
