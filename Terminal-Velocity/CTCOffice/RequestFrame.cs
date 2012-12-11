using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Interfaces;

namespace CTCOffice
{
    public partial class RequestFrame : UserControl
    {
        private readonly string _ptc;
        private readonly TestingTrackController _ptco;

        public RequestFrame(string primaryTrackController, TestingTrackController tc)
        {
            InitializeComponent();

            _ptc = primaryTrackController;
            _ptco = tc;
            _ptco.TransferRequest += new EventHandler<EventArgs>(_ptco_TransferRequest);

            _txtPrimaryTrackController.Text = _ptc;

            _txtBlockID.ReadOnly = true;
            _txtDateTime.ReadOnly = true;
            _txtPrimaryTrackController.ReadOnly = true;
            _txtRequestType.ReadOnly = true;
            _txtTrackControllerID.ReadOnly = true;
            _txtTrainAuthority.ReadOnly = true;
            _txtTrainID.ReadOnly = true;
            _txtTrainRoute.ReadOnly = true;
            _txtTrainSpeed.ReadOnly = true;
        }

        void _ptco_TransferRequest(object sender, EventArgs e)
        {
            RequestEventArgs request = (RequestEventArgs)e;
            SetRequest(request.Request);
        }

        private void ClearRequest()
        {
            _txtBlockID.Text = "";
            _txtDateTime.Text = "";
            _txtPrimaryTrackController.Text = _ptc;
            _txtRequestType.Text = "";
            _txtTrackControllerID.Text = "";
            _txtTrainAuthority.Text = "";
            _txtTrainID.Text = "";
            _txtTrainRoute.Text = "";
            _txtTrainSpeed.Text = "";
        }

        private void SetRequest(IRequest request)
        {
            if (request.Block != null)
            {
                _txtBlockID.Text = request.Block.BlockID.ToString();
            }
            else
            {
                _txtBlockID.Text = "NULL";
            }

            if (request.IssueDateTime != null)
            {
                _txtDateTime.Text = request.IssueDateTime.ToString();
            }
            else
            {
                _txtDateTime.Text = "NULL";
            }

            _txtRequestType.Text = request.RequestType.ToString();

            _txtTrackControllerID.Text = request.TrackControllerID.ToString();

            _txtTrainAuthority.Text = request.TrainAuthority.ToString();

            _txtTrainID.Text = request.TrainID.ToString();


            if (request.TrainRoute != null)
            {
                _txtTrainRoute.Text = request.TrainRoute.RouteType.ToString();
            }
            else
            {
                _txtTrainRoute.Text = "NULL";
            }

            _txtTrainSpeed.Text = request.TrainSpeed.ToString();
        }
    }
}