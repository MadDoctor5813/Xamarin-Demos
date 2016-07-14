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
using Android.Util;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Locations;
using Android.Gms.Common;

namespace XamarinLocationTracking
{
    class TrackingLocationListener : Java.Lang.Object, Android.Gms.Location.ILocationListener
    {

        private Context context;
        private GoogleApiClient googleApiClient;
        private LocationRequest locationRequest;
        private MainActivity mainActivity;

        public List<Location> Locations
        {
            get
            {
                return locations;
            }
        }

        List<Location> locations;

        public bool IsTracking { get; set; }

        public TrackingLocationListener(MainActivity activity)
        {
            this.mainActivity = activity;
            context = mainActivity.ApplicationContext;
            googleApiClient = new GoogleApiClient.Builder(context)
                .AddApi(LocationServices.API)
                .AddConnectionCallbacks(OnConnection)
                .AddOnConnectionFailedListener(OnConnectionFailed)
                .Build();
            googleApiClient.Connect();
            locationRequest = new LocationRequest()
                .SetInterval(2000)
                .SetPriority(LocationRequest.PriorityHighAccuracy);
            locations = new List<Location>();
        }

        public void OnStartTracking()
        {
            Log.Debug(context.Resources.GetString(Resource.String.AppLogId), "Starting Tracking");
            //clear the location list
            Locations.Clear();
        }

        public void OnEndTracking()
        {
            Log.Debug(context.Resources.GetString(Resource.String.AppLogId), "Ending Tracking");
        }

        public void OnConnection()
        {
            Log.Debug(context.Resources.GetString(Resource.String.AppLogId), "Google API connected.");
            LocationServices.FusedLocationApi.RequestLocationUpdates(googleApiClient, locationRequest, this);
        }

        public void OnConnectionFailed(ConnectionResult result)
        {
            Log.Debug(context.Resources.GetString(Resource.String.AppLogId), "Google API connection failed. Error code: " + result.ErrorCode);
        }

        public void OnLocationChanged(Location location)
        {
            Log.Debug(context.Resources.GetString(Resource.String.AppLogId), String.Format("Lat: {0} Lng {1}", location.Latitude, location.Longitude));
            if (Locations.Count == 0 || (Locations.Last().Latitude != location.Latitude || Locations.Last().Longitude != location.Longitude))
            {
                Locations.Add(location);
                mainActivity.DrawLocations(Locations);
            }
        }

        public void OnProviderDisabled(string provider)
        {
            Log.Debug(context.Resources.GetString(Resource.String.AppLogId), "LocListener provider disabled.");
        }

        public void OnProviderEnabled(string provider)
        {
            Log.Debug(context.Resources.GetString(Resource.String.AppLogId), "LocListener provider enabled.");
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            Log.Debug(context.Resources.GetString(Resource.String.AppLogId), "LocListener status changed.");
        }
    }
}