﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;
using CoordinatesMapWF.Domain.Models;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

namespace CoordinatesMapWF.App
{
    public partial class MapForm : Form
    {
        public MapForm()
        {
            InitializeComponent();
        }

        private void MapForm_Load(object sender, EventArgs e)
        {
            InitializeComponent();
        }

        private void gMapControl_Load(object sender, EventArgs e)
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerAndCache;
            gMapControl1.MapProvider = GMap.NET.MapProviders.GoogleMapProvider.Instance;
            gMapControl1.MinZoom = 2;
            gMapControl1.MaxZoom = 16;
            gMapControl1.Zoom = 4;
            gMapControl1.Position = new GMap.NET.PointLatLng(66.4169575018027, 94.25025752215694);
            gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            gMapControl1.CanDragMap = true;
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.ShowCenter = false;
            gMapControl1.ShowTileGridLines = false;
            
            // Example coordinate.
            var coordinate = new Coordinate()
            {
                Id = Guid.NewGuid(),
                Latitude = 66.4169575018027,
                Longitude = 94.25025752215694
            };
            var points = new List<Coordinate> { coordinate };
            gMapControl1.Overlays.Add(GetOverlayMarkers(points, "GroupsMarkers"));
        }
        
        private GMarkerGoogle GetMarker(Coordinate coordinate)
        {
            GMarkerGoogle mapMarker = new GMarkerGoogle(
                new GMap.NET.PointLatLng(coordinate.Latitude, coordinate.Longitude), GMarkerGoogleType.red);
            mapMarker.ToolTip = new GMap.NET.WindowsForms.ToolTips.GMapRoundedToolTip(mapMarker);
            mapMarker.ToolTipText = coordinate.Id.ToString();
            mapMarker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            return mapMarker;
        }
        
        private GMapOverlay GetOverlayMarkers(List<Coordinate> coordinates, string name)
        {
            GMapOverlay gMapMarkers = new GMapOverlay(name);
            foreach (var coordinate in coordinates)
            {
                gMapMarkers.Markers.Add(GetMarker(coordinate));
            }
            return gMapMarkers;
        }
    }
}
