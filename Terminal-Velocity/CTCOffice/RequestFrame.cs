using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Utility;
using Interfaces;

namespace CTCOffice
{
    public partial class RequestFrame : UserControl
    {
        private string _ptc;
        private List<IRequest> requests;
        private int current, counter;
        private TestingTrackController _ptco;

        public RequestFrame(string primaryTrackController, TestingTrackController tc)
        {
            InitializeComponent();

            _ptc = primaryTrackController;
            _ptco = tc;

            _ptco.RequestRec += new EventHandler<RequestEventArgs>(_ptco_RequestRec);

            current = 0;
            counter = 0;
            requests = new List<IRequest>();
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

            //disable for now
            _btnToFile.Enabled = false;
            
        }

        void _ptco_RequestRec(object sender, RequestEventArgs e)
        {
            newRequest(e.Request);
        }

        private void clearRequest()
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

        public void newRequest(IRequest request)
        {
            //add new request to list
            requests.Add(request);
            //set counter var
            counter = requests.Count;
            //set current index
            current = counter-1;
            if (current < 0)
            {
                current = 0;
            }
            //set label
            _lblCount.Text = current.ToString();
            //populate table
            clearRequest();
            setRequest(requests[current]);
        }

        private void setRequest(IRequest request)
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

        private void _btnPrev_Click(object sender, EventArgs e)
        {
            _btnNext.Enabled = true;
            current--;
            if (current < 0)
            {
                current = 0;
                _btnPrev.Enabled = false;
            }
            _lblCount.Text = current.ToString();

            if (requests.Count != 0)
            {
                setRequest(requests[current]);
            }
        }

        private void _btnNext_Click(object sender, EventArgs e)
        {
            _btnPrev.Enabled = true;
            current++;
            if (current >= requests.Count)
            {
                current = requests.Count - 1;
                _btnNext.Enabled = false;
            }
            _lblCount.Text = current.ToString();

            if (requests.Count != 0)
            {
                setRequest(requests[current]);
            }
        }
    }
}
