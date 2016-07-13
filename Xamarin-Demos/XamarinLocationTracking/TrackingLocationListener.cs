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
using Android.Locations;
using Android.Util;

namespace XamarinLocationTracking
{
    class TrackingLocationListener : Java.Lang.Object, ILocationListener
    {

        private Context context;

        public TrackingLocationListener(Context context)
        {
            this.context = context;
        }

        public void OnLocationChanged(Location location)
        {
            
        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            //Log.Debug(Resource.GetString(Resource.String.AppLogId), )
        }
    }
}