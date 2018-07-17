using Windows.UI.Xaml.Controls;
using MvvmCross.WindowsUWP.Platform;
using MvvmCross.Core.ViewModels;
using System;

namespace WillowTree.NameGame.Win
{
    internal class Setup : MvxWindowsSetup
    {
        private Frame rootFrame;

        public Setup(Frame rootFrame) : base(rootFrame)
        {
            this.rootFrame = rootFrame;
        }

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
    }
}