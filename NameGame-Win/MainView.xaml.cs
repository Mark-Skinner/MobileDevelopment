using MvvmCross.WindowsUWP.Views;
using WillowTree.NameGame.Core.Models;
using WillowTree.NameGame.Core.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace WillowTree.NameGame.Win
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainView : MvxWindowsPage
    {
        public new MainViewModel ViewModel
        {
            get { return base.ViewModel as MainViewModel; }
            set { base.ViewModel = value; }
        }

        public MainView()
        {
            this.InitializeComponent();
        }
    }
}
