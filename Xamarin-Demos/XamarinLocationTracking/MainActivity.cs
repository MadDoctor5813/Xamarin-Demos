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
using Android.Locations;
using System.Collections.Generic;
using Android.Gms.Maps.Model;
using Android.Graphics;

namespace XamarinLocationTracking
{
    [Activity(Label = "XamarinLocationTracking", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity, IOnMapReadyCallback
    {
        MapFragment mapFragment;
        ToggleButton trackingToggleButton;
        GoogleMap trackingMap;

        TrackingLocationListener trackingLocationListener;

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

            trackingLocationListener = new TrackingLocationListener(this);
        }

        protected override void OnStart()
        {
            base.OnStart();
            //grab the layout elements
            trackingToggleButton = FindViewById<ToggleButton>(Resource.Id.TrackingButton);
            trackingToggleButton.CheckedChange += OnToggleTracking;
            mapFragment.GetMapAsync(this);
        }

        public void ClearMarkersAndPolyline()
        {
            trackingMap.Clear();
        }

        public void DrawLocations(List<Location> locations)
        {
            PolylineOptions opts = new PolylineOptions();
            var latLngBuilder = new LatLngBounds.Builder();
            foreach (Location loc in locations)
            {
                opts.Add(new LatLng(loc.Latitude, loc.Longitude));
                latLngBuilder.Include(new LatLng(loc.Latitude, loc.Longitude));
                trackingMap.AddMarker(new MarkerOptions().SetPosition(new LatLng(loc.Latitude, loc.Longitude)));
            }
            opts.InvokeColor(Color.Red);
            trackingMap.AddPolyline(opts);
            trackingMap.MoveCamera(CameraUpdateFactory.NewLatLngBounds(latLngBuilder.Build(), 200));
            

        }

        private void OnToggleTracking(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            trackingLocationListener.IsTracking = e.IsChecked;
            if (e.IsChecked)
            {
                trackingLocationListener.OnStartTracking();
            }
            else
            {
                trackingLocationListener.OnEndTracking();
            }
        }

        public void OnMapReady(GoogleMap googleMap)
        {
            trackingMap = googleMap;
            trackingMap.UiSettings.MyLocationButtonEnabled = true;
            trackingMap.MyLocationEnabled = true;
        }
    }
}

