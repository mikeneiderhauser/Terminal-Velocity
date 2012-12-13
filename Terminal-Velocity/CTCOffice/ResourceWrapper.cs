using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;

using Interfaces;
using Utility;
using CTCOffice;

namespace CTCOffice
{
    public class ResourceWrapper
    {
        #region Private Variables (Resources)
        private Bitmap _redLight;
        private Bitmap _yellowLight;
        private Bitmap _greenLight;

        private Bitmap _train;

        private Bitmap _unpopulated;
        private Bitmap _trackError;

        private Bitmap _redTrack;
        private Bitmap _redTrackClosed;
        private Bitmap _redTrackCrossing;
        private Bitmap _redTrackHeater;
        private Bitmap _redTrackStation;
        private Bitmap _redTrackSwitch;
        private Bitmap _redTrackTunnel;

        private Bitmap _greenTrack;
        private Bitmap _greenTrackClosed;
        private Bitmap _greenTrackCrossing;
        private Bitmap _greenTrackHeater;
        private Bitmap _greenTrackStation;
        private Bitmap _greenTrackSwitch;
        private Bitmap _greenTrackTunnel;

        private FileInfo _CTCGuide;
        
        #endregion

        #region Constructor
        public ResourceWrapper()
        {
            _redLight = Utility.Properties.Resources.red;
            _yellowLight = Utility.Properties.Resources.yellow;
            _greenLight = Utility.Properties.Resources.green;

            _train = Utility.Properties.Resources.Train;

            _unpopulated = Utility.Properties.Resources.Unpopulated;
            _trackError = Utility.Properties.Resources.TrackError;

            _redTrack = Utility.Properties.Resources.RedTrack;
            _redTrackClosed = Utility.Properties.Resources.RedTrack_Closed;
            _redTrackCrossing = Utility.Properties.Resources.RedTrack_Crossing;
            _redTrackHeater = Utility.Properties.Resources.RedTrack_Heater;
            _redTrackStation = Utility.Properties.Resources.RedTrack_Station;
            _redTrackSwitch = Utility.Properties.Resources.RedTrack_Switch;
            _redTrackTunnel = Utility.Properties.Resources.RedTrack_Tunnel;

            _greenTrack = Utility.Properties.Resources.GreenTrack;
            _greenTrackClosed = Utility.Properties.Resources.GreenTrack_Closed;
            _greenTrackCrossing = Utility.Properties.Resources.GreenTrack_Crossing;
            _greenTrackHeater = Utility.Properties.Resources.GreenTrack_Heater;
            _greenTrackStation = Utility.Properties.Resources.GreenTrack_Station;
            _greenTrackSwitch = Utility.Properties.Resources.GreenTrack_Switch;
            _greenTrackTunnel = Utility.Properties.Resources.GreenTrack_Tunnel;

            if (!File.Exists(@"CTC_Users_Guide.pdf"))
            {
                byte[] fileContainer = global::CTCOffice.Properties.Resources.CTC_Office_Users_Guide;
                File.WriteAllBytes(@"CTC_Users_Guide.pdf", fileContainer);
            }

                _CTCGuide = new FileInfo(@"CTC_Users_Guide.pdf");
            
            
            
        
        }
        #endregion

        #region Properties (Resources)

        /// <summary>
        /// Property for Red Light Bitmap (Resource)
        /// </summary>
        public Bitmap RedLight
        { get { return _redLight; } }

        /// <summary>
        /// Property for Yellow Light Bitmap (Resource)
        /// </summary>
        public Bitmap YellowLight
        { get { return _yellowLight; } }

        /// <summary>
        /// Property for Green Light Bitmap (Resource)
        /// </summary>
        public Bitmap GreenLight
        { get { return _greenLight; } }

        /// <summary>
        /// Property for Train Bitmap (Resource)
        /// </summary>
        public Bitmap Train
        { get { return _train; } }

        /// <summary>
        /// Property for Unpopulated Track Bitmap (Resource)
        /// </summary>
        public Bitmap Unpopulated
        { get { return _unpopulated; } }

        /// <summary>
        /// Property for Track Error Bitmap (Resource)
        /// </summary>
        public Bitmap TrackError
        { get { return _trackError; } }

        /// <summary>
        /// Property for Red Track Bitmap (Resource)
        /// </summary>
        public Bitmap RedTrack
        { get { return _redTrack; } }

        /// <summary>
        /// Property for Red Track Closed Bitmap (Resource)
        /// </summary>
        public Bitmap RedTrackClosed
        { get { return _redTrackClosed; } }

        /// <summary>
        /// Property for Red Track Crossing Bitmap (Resource)
        /// </summary>
        public Bitmap RedTrackCrossing
        { get { return _redTrackCrossing; } }

        /// <summary>
        /// Property for Red Track Heater Bitmap (Resource)
        /// </summary>
        public Bitmap RedTrackHeater
        { get { return _redTrackHeater; } }

        /// <summary>
        /// Property for Red Track Station Bitmap (Resource)
        /// </summary>
        public Bitmap RedTrackStation
        { get { return _redTrackStation; } }

        /// <summary>
        /// Property for Red Track Switch Bitmap (Resource)
        /// </summary>
        public Bitmap RedTrackSwitch
        { get { return _redTrackSwitch; } }

        /// <summary>
        /// Property for Red Track Tunnel Bitmap (Resource)
        /// </summary>
        public Bitmap RedTrackTunnel
        { get { return _redTrackTunnel; } }

        /// <summary>
        /// Property for Green Track Bitmap (Resource)
        /// </summary>
        public Bitmap GreenTrack
        { get { return _greenTrack; } }

        /// <summary>
        /// Property for Green Track Closed Bitmap (Resource)
        /// </summary>
        public Bitmap GreenTrackClosed
        { get { return _greenTrackClosed; } }

        /// <summary>
        /// Property for Green Track Crossing Bitmap (Resource)
        /// </summary>
        public Bitmap GreenTrackCrossing
        { get { return _greenTrackCrossing; } }

        /// <summary>
        /// Property for Green Track Heater Bitmap (Resource)
        /// </summary>
        public Bitmap GreenTrackHeater
        { get { return _greenTrackHeater; } }

        /// <summary>
        /// Property for Green Track Station Bitmap (Resource)
        /// </summary>
        public Bitmap GreenTrackStation
        { get { return _greenTrackStation; } }

        /// <summary>
        /// Property for Green Track Switch Bitmap (Resource)
        /// </summary>
        public Bitmap GreenTrackSwitch
        { get { return _greenTrackSwitch; } }

        /// <summary>
        /// Property for Green Track Tunnel Bitmap (Resource)
        /// </summary>
        public Bitmap GreenTrackTunnel
        { get { return _greenTrackTunnel; } }

        public FileInfo CTCGuide
        { get { return _CTCGuide; } }
        #endregion
    }
}
