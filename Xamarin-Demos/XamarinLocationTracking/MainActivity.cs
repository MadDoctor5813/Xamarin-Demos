using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Gms.Maps;
using Android.Support.V4.App;
using Android.Util;

namespace XamarinLocationTracking
{
    [Activity(Label = "XamarinLocationTracking", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback
    {
        MapFragment mapFragment;
        Button trackingToggleButton;
        GoogleMap trackingMap;

        bool isTracking = false;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //build the map fragment
            mapFragment = MapFragment.NewInstance();
            Android.App.FragmentTransaction tx = FragmentManager.BeginTransaction();
            tx.Add(Resource.Id.RootLayout, mapFragment);
            tx.Commit();

            //grab the layout elements
            trackingToggleButton = FindViewById<Button>(Resource.Id.TrackingButton);
            trackingToggleButton.Click += OnToggleTracking;
            mapFragment.GetMapAsync(this);
        }

        private void OnToggleTracking(object sender, EventArgs e)
        {
            isTracking = !isTracking;
            if (isTracking)
            {
                OnStartTracking();
            }
            else
            {
                OnEndTracking();
            }
        }

        private void OnStartTracking()
        {
            throw new NotImplementedException();
        }

        private void OnEndTracking()
        {
            throw new NotImplementedException();
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            trackingMap = googleMap;
            Log.Debug("LocationTrackingApp", "MapFragment ready");
            trackingMap.MoveCamera(CameraUpdateFactory.NewLatLng(new Android.Gms.Maps.Model.LatLng(0.0, 0.0)));
        }
    }
}

