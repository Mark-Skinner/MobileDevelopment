using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.Lang;

using WillowTree.NameGame.Droid;
using WillowTree.NameGame.Core.Models;

namespace WillowTree.NameGame.Core.Adapters
{
    public class NameGameAdapter : BaseAdapter<Profile>
    {
        List<Profile> profiles;
        Activity context;

        public NameGameAdapter(Activity Context, List<Profile> Profiles) : base()
        {
            context = Context;
            profiles = Profiles;
        }

        public override long GetItemId(int Index)
        {
            return Index;
        }

        public override Profile this[int Index]
        {
            get { return profiles[Index]; }
        }

        public override int Count
        {
            get { return profiles.Count; }
        }

        public override View GetView(int Index, View View, ViewGroup Parent)
        {
            View view = View;
            if (view == null) // no view for list item exists, so create it
                view = context.LayoutInflater.Inflate(Resource.Layout.picture_item, null);

            Profile profile = profiles[Index];
            ImageView iView = view.FindViewById<ImageView>(Resource.Id.picture_image);
            TextView tView = view.FindViewById<TextView>(Resource.Id.picture_text);
            if (!string.IsNullOrEmpty(profile.Headshot.Url))
            {
                tView.Visibility = ViewStates.Gone;
                
                iView.SetMaxWidth(profile.Headshot.Width);
                iView.SetMaxHeight(profile.Headshot.Height);
                iView.SetImageBitmap(GetBitmap(profile.Headshot.Url));
                iView.SetOnClickListener(new Droid.Listeners.ProfileImageClickListener(profile));
            }
            else
            {
                iView.Visibility = ViewStates.Gone;

                tView.SetMaxWidth(profile.Headshot.Width);
                tView.SetMaxHeight(profile.Headshot.Height);
                tView.Text = profile.Headshot.AltText;
                tView.SetOnClickListener(new Droid.Listeners.ProfileImageClickListener(profile));
            }
            
            return view;
        }
        
        private Android.Graphics.Bitmap GetBitmap(string Url)
        {
            try
            {
                byte[] image_data = new byte[0];
                using (HttpClient Client = new HttpClient())
                {
                    Client.Timeout = new TimeSpan(50000000); // 5 seconds
                    image_data = Client.GetByteArrayAsync(Url).Result;
                }

                return Android.Graphics.BitmapFactory.DecodeByteArray(image_data, 0, image_data.Length);
            }
            catch (HttpRequestException)
            {
                // Failed to send request to data url
                throw;
            }
            catch (System.Exception)
            {
                // any other exceptions..
                throw;
            }
        }
    }
}