/*
 * Read About.txt! 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Binding.Droid.Views;
using MvvmCross.Droid.Views;

namespace WillowTree.NameGame.Droid
{
    [Activity(Label = "Name Game", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainView : MvxActivity
    {
        public static int CorrectGuesses { get; set; }
        public static int IncorrectGuesses { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.start_screen);
            
            Button button = FindViewById<Button>(Resource.Id.btn_original);
            button.Click += Button_Click;
            button = FindViewById<Button>(Resource.Id.btn_speed);
            button.Click += Button_Click;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            if (button.Id == Resource.Id.btn_original)
            {
                // open original game mode
                StartActivityForResult(new Intent(button.Context, typeof(OriginalModeView)), 0);
            }
            else if (button.Id == Resource.Id.btn_speed)
            {
                // open speed game mode
                StartActivityForResult(new Intent(button.Context, typeof(SpeedModeView)), 0);
            }

            Finish();
        }
    }
}