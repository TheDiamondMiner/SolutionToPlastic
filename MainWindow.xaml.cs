using SolutionToPlastic.ViewModels;
using SolutionToPlastic.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
    }

    public class ViewModel : PsudeoViewModel, INotifyPropertyChanged
    {
        private string dataforstate = File.ReadAllText("StateData.csv");

        public Dictionary<int, string> stateName = new Dictionary<int, string>();
        public List<string> plasticRecycledAmount = new List<string>();
        public List<string> percentageRecycled = new List<string>();

        public event PropertyChangedEventHandler? PropertyChanged;
        public ICommand Switcher { get; }

        public ViewModel()
        {
            Switcher = new ViewSwitcher(this);
            currentview.ViewChanged += Child_ViewChanged;
            SetUpDataForState();
        }

        private PsudeoViewModel currentview = new ViewModels.Introduction();

        public PsudeoViewModel CurrentView
        {
            get { return currentview; }
            set {
                currentview = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentView"));
                currentview.ViewChanged += Child_ViewChanged;
            }
        }

        private void Child_ViewChanged(object? sender, NotifyChangeView e)
        {
            Switcher.Execute(NotifyChangeView.NextPage);
        }

        private void SetUpDataForState()
        {
            string[] arr = dataforstate.Split(',');
            for(int i = 0; i < 31;)
            {
                stateName.Add(i, arr[i]);
                plasticRecycledAmount.Add(arr[i + 1]);
                plasticRecycledAmount.Add(arr[i + 2]);
                percentageRecycled.Add(arr[i + 3]);
                i += 4;
            }
        }
    }

    public class NotifyChangeView : EventArgs {
        public static string NextPage { get; set; }

        public NotifyChangeView(string page)
        {
            NextPage = page;
        }
    }

    public class PsudeoViewModel {
        public event EventHandler<NotifyChangeView> ViewChanged;

        public void RaiseEvent(string nextpage)
        {
            ViewChanged.Invoke(this, new NotifyChangeView(nextpage));
        }
    }

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
    public class Introduction : PsudeoViewModel {
        public ICommand PageSwitch { get; }

        public Introduction()
        {
            PageSwitch = new NotifyParentForPageSwitch(this);
        }
    }

    public class PercentagePage : PsudeoViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
    }
    public class ProjectsDone : PsudeoViewModel {}   // Team Seas and Swatchda bharat etc
    public class InventionsMade : PsudeoViewModel {} // Inventions made as a solution to plastic
    public class PlasticComposition : PsudeoViewModel {}
    public class ThankYouCredits : PsudeoViewModel {}

    public class NotifyParentForPageSwitch : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public Introduction vm;

        public NotifyParentForPageSwitch(Introduction vm)
        {
            this.vm = vm;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            vm.RaiseEvent(parameter.ToString());
        }
    }

    public class PrepareDataForMap : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            throw new NotImplementedException();
        }
    }
}
