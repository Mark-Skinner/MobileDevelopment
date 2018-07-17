using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views;

namespace WillowTree.NameGame.Droid.Listeners
{
    internal class ProfileImageClickListener : Java.Lang.Object, View.IOnClickListener
    {
        //public IntPtr Handle => return
        public Core.Models.Profile Profile { get; set; }

        public ProfileImageClickListener(Core.Models.Profile Profile)
        {
            this.Profile = Profile;
        }

        public void OnClick(View v)
        {
            // get activity
            MvxActivity Activity = GetActivity(v);
            OriginalModeView view = (OriginalModeView)Activity;

            // get model
            Core.ViewModels.MainViewModel Model = (Core.ViewModels.MainViewModel)view.ViewModel;

            // get current name textview
            TextView tv_currentName = Activity.FindViewById<TextView>(Resource.Id.textView_currentName);

            if (tv_currentName.Text.Equals(Profile.FullName))
            {
                MainView.CorrectGuesses++;
                TextView tv = Activity.FindViewById<TextView>(Resource.Id.tv_correct);
                tv.Text = "Correct: " + MainView.CorrectGuesses;
                tv.RefreshDrawableState();

                view.CurrentProfiles = Model.PickProfiles(view.Profiles, view.CurrentProfiles.Count);
                view.CurrentProfileName = view.CurrentProfiles[new Random().Next(0, view.CurrentProfiles.Count - 1)].FullName;

                // update listview with profile items
                // get listview
                ListView lv = Activity.FindViewById<ListView>(Resource.Id.listView_Pictures);

                // get current name textview
                lv.Adapter = new Core.Adapters.NameGameAdapter(view, view.CurrentProfiles);
                tv_currentName.Text = view.CurrentProfileName;
                tv_currentName.RefreshDrawableState();
            }
            else
            {
                MainView.IncorrectGuesses++;
                TextView tv = Activity.FindViewById<TextView>(Resource.Id.tv_incorrect);
                tv.Text = "Incorrect: " + MainView.IncorrectGuesses;
                tv.RefreshDrawableState();

                if (MainView.IncorrectGuesses >= 3)
                {
                    tv_currentName.Text = "Oh no, you're out of guesses!";
                    tv_currentName.RefreshDrawableState();

                    // get listview
                    ListView lv = Activity.FindViewById<ListView>(Resource.Id.listView_Pictures);
                    lv.Visibility = ViewStates.Gone;
                    lv.RefreshDrawableState();

                    // create back button
                    LinearLayout lo = Activity.FindViewById<LinearLayout>(Resource.Id.llayout_images);
                    Button back = lo.FindViewById<Button>(Resource.Id.btn_back);
                    back.Click += delegate
                    {
                        // open starting screen
                        view.StartActivityForResult(new Intent(back.Context, typeof(MainView)), 0);
                        view.Finish();
                    };
                    back.Visibility = ViewStates.Visible;
                    back.RefreshDrawableState();

                    return;
                }
                
                ImageView iv = (ImageView)v;
                iv.SetColorFilter(Android.Graphics.Color.DarkGray, Android.Graphics.PorterDuff.Mode.Multiply);
                iv.RefreshDrawableState();
                iv.SetOnClickListener(null);
            }
        }

        private void Back_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private MvxActivity GetActivity(View View)
        {
            Context context = View.Context;
            if (context.GetType().IsSubclassOf(typeof(MvxActivity)))
                return (MvxActivity)context;

            while (context.GetType().IsSubclassOf(typeof(ContextWrapper))) {
                if (context.GetType().IsSubclassOf(typeof(MvxActivity)))
                    return (MvxActivity)context;
                context = ((ContextWrapper)context).BaseContext;
            }
            return null;
        }
    }
}