using SolutionToPlastic.ViewModels;
using SolutionToPlastic.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        public static ViewModel viewModel = new ViewModel();
        public ICommand Switcher { get; }
        public ICommand SetData { get; }

        public ViewModel()
        {
            Switcher = new ViewSwitcher(this);
            currentview.ViewChanged += Child_ViewChanged;
            SetUpDataForState();
            SetData = new CollectDataForStates(this, new MessageBoxViewModel());
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
            int j = 0;
            for (int i = 0; i < 29; i++)
            {
                stateName.Add(i, arr[j + 0]);
                plasticRecycledAmount.Add(arr[j + 1]);
                plasticRecycledAmount.Add(arr[j + 2]);
                percentageRecycled.Add(arr[j + 3]);
                j += 4;
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
                    vm.CurrentView = new ProjectsAndInventions();
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

    public class PercentagePage : PsudeoViewModel 
    {
        private string[] countries = { "South Korea","Germany","Singapore","Netherlands","Wales"};
        private int[] percentNumber = { 91, 78, 72, 68, 63 };
        public ICommand PageSwitch { get; }

        public PercentagePage()
        {
            PageSwitch = new NotifyParentForPageSwitch(this);
        }
        public string[] Countries
        {
            get { return countries; }
        }
        public int[] Percentage
        {
            get { return percentNumber; }
        }
    }

    public class ProjectsAndInventions : PsudeoViewModel {}   // Team Seas and Swatchda bharat etc
    public class ThankYouCredits : PsudeoViewModel {}

    public class CollectDataForStates : ICommand
    {
        public ViewModel vm;
        public MessageBoxViewModel msgbox;
        public event EventHandler? CanExecuteChanged;
        public CustomMessageBox box;

        public CollectDataForStates(ViewModel vm, MessageBoxViewModel messageBox)
        {
            this.vm = vm;
            msgbox = messageBox;
            box = new CustomMessageBox { DataContext = msgbox };
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            string[] parameter2 = parameter.ToString().Split(',');
            int index = vm.stateName.FirstOrDefault(x => x.Value == parameter2[2]).Key;
            box.Top = Convert.ToDouble(parameter2[1]);
            box.Left = Convert.ToDouble(parameter2[0]);
            if (box.IsActive == false) { box.Show(); }
            msgbox.StateName = $"STATE: {vm.stateName[index]}";
            msgbox.RecycledKG = $"PLASTIC RECYCLED (KG): {vm.plasticRecycledAmount[index + 1]}";
            msgbox.RecycledTON = $"PLASTIC RECYCLED (TON) {vm.plasticRecycledAmount[index]}";
            msgbox.PercentRecycled = Convert.ToInt32(vm.percentageRecycled[index]);
        }
    }

    public class NotifyParentForPageSwitch : ICommand
    {
        public event EventHandler? CanExecuteChanged;
        public PsudeoViewModel vm;

        public NotifyParentForPageSwitch(PsudeoViewModel vm)
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
}
