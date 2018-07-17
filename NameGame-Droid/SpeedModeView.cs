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
    [Activity(Label = "Speed", MainLauncher = false)]
    public class SpeedModeView : MvxActivity
    {
        List<Core.Models.Profile> Profiles;
        List<Core.Models.Profile> CurrentProfiles;
        const int NUM_PROFILES_PER_GUESS = 5;
        string CurrentProfileName = string.Empty;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.speed_game);

            // get progress bar
            ProgressBar pb = FindViewById<ProgressBar>(Resource.Id.progress_loader);

            // get listview
            ListView lv = FindViewById<ListView>(Resource.Id.listView_Pictures);

            // get current name textview
            TextView tv = FindViewById<TextView>(Resource.Id.textView_currentName);

            // get model
            Core.ViewModels.MainViewModel Model = (Core.ViewModels.MainViewModel)ViewModel;

            // load profiles in background
            Task.Run(async () =>
            {
                try
                {
                    // get profiles
                    Profiles = await Model.GetProfiles();
                    CurrentProfiles = Model.PickProfiles(Profiles, NUM_PROFILES_PER_GUESS);
                    CurrentProfileName = CurrentProfiles[new Random().Next(0, NUM_PROFILES_PER_GUESS - 1)].FullName;

                    // update listview with profile items
                    RunOnUiThread(new Action(() =>
                    {
                        lv.Adapter = new Core.Adapters.NameGameAdapter(this, CurrentProfiles);
                        tv.Text = CurrentProfileName;
                        pb.Visibility = ViewStates.Gone;
                        pb.RefreshDrawableState();
                        tv.RefreshDrawableState();
                    }));
                }
                catch (Exception)
                {
                    // display exception message that was thrown from GetProfiles method
                    RunOnUiThread(new Action(() =>
                    {
                        // get error box
                        TextView error = FindViewById<TextView>(Resource.Id.tv_error);
                        error.Text = "Oh no! There was a problem loading the profiles.";

                        pb.Visibility = ViewStates.Gone;
                        lv.Visibility = ViewStates.Gone;
                        error.Visibility = ViewStates.Visible;
                        pb.RefreshDrawableState();
                        lv.RefreshDrawableState();
                        error.RefreshDrawableState();
                    }));
                }
            });

        }
    }
}