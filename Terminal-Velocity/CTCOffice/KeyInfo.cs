using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace CTCOffice
{
    public partial class KeyInfo : UserControl
    {
        private ResourceWrapper _res;

        public KeyInfo(ResourceWrapper res)
        {
            InitializeComponent();
            _res = res;
            PopulateImages();
        }

        private void PopulateImages()
        {
            _pGreenTrack.Image = _res.GreenTrack;
            _pGreenTrackClosed.Image = _res.GreenTrackClosed;
            _pGreenTrackCrossing.Image = _res.GreenTrackCrossing;
            _pGreenTrackHeater.Image = _res.GreenTrackHeater;
            _pGreenTrackStation.Image = _res.GreenTrackStation;
            _pGreenTrackSwitch.Image = _res.GreenTrackSwitch;
            _pGreenTrackTunnel.Image = _res.GreenTrackTunnel;

            _pRedTrack.Image = _res.RedTrack;
            _pRedTrackClosed.Image = _res.RedTrackClosed;
            _pRedTrackCrossing.Image = _res.RedTrackCrossing;
            _pRedTrackHeater.Image = _res.RedTrackHeater;
            _pRedTrackStation.Image = _res.RedTrackStation;
            _pRedTrackSwitch.Image = _res.RedTrackSwitch;
            _pRedTrackTunnel.Image = _res.RedTrackTunnel;

            _pTrain.Image = _res.Train;
            _pTrackError.Image = _res.TrackError;
            _pUnpopulated.Image = _res.Unpopulated;
            _pGreenStatusLight.Image = _res.GreenLight;
            _pYellowStatusLight.Image = _res.YellowLight;
            _pRedStatusLight.Image = _res.RedLight;
        }
    }
}
