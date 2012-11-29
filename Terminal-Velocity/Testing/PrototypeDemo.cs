using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

using Interfaces;
using Utility;

namespace Testing
{
    public class PrototypeDemo
    {
        SimulationEnvironment.SimulationEnvironment _env;

        public PrototypeDemo()
        {
            //Underlying Classes
            // Environment object
            SimulationEnvironment.SimulationEnvironment Proto_environment = new SimulationEnvironment.SimulationEnvironment();
            _env = Proto_environment;

            // Our track circuit
            TrackController.TrackCircuit Proto_currCircuit = new TrackController.TrackCircuit(Proto_environment);
            // Next track controller's circuit
            TrackController.TrackCircuit Proto_nextCircuit = new TrackController.TrackCircuit(Proto_environment);
            // Previous track controller's circuit
            TrackController.TrackCircuit Proto_prevCircuit = new TrackController.TrackCircuit(Proto_environment);

            TrackController.TrackController Proto_prev = new TrackController.TrackController(Proto_environment, Proto_prevCircuit);
            TrackController.TrackController Proto_curr = new TrackController.TrackController(Proto_environment, Proto_currCircuit);
            TrackController.TrackController Proto_next = new TrackController.TrackController(Proto_environment, Proto_nextCircuit);

            //Create TrackModel
            TrackModel.TrackModel Proto_TrackMod = new TrackModel.TrackModel(Proto_environment);
            //Let TrackModel read in the lines before you proceed..shouldnt be done this way, but needed to stop CTC Office from faulting 
            bool Proto_res = Proto_TrackMod.provideInputFile("red.csv");
            //Console.WriteLine("Res was "+res);
            Proto_res = Proto_TrackMod.provideInputFile("green.csv");
            //Console.WriteLine("Res was " + res);


            Proto_environment.TrackModel = Proto_TrackMod;
            Proto_prev.Previous = null;
            Proto_prev.Next = Proto_curr;

            Proto_curr.Previous = Proto_prev;
            Proto_curr.Next = Proto_next;

            Proto_next.Previous = Proto_curr;
            Proto_next.Next = null;

            // Assign the same track controller to both lines
            CTCOffice.CTCOffice Proto_office = new CTCOffice.CTCOffice(Proto_environment, Proto_prev, Proto_prev);

            Proto_environment.CTCOffice = Proto_office;
            Proto_environment.PrimaryTrackControllerGreen = Proto_prev;
            Proto_environment.PrimaryTrackControllerRed = Proto_prev;

            SystemScheduler.SystemScheduler Proto_scheduler = new SystemScheduler.SystemScheduler(Proto_environment, Proto_office);

            // UNFINISHED
            #region Track form
            Form formTrack = new Form();
            UserControl controlTrack;
            controlTrack = new TrackModel.TrackModelGUI(Proto_environment, Proto_TrackMod);

            formTrack.Controls.Add(controlTrack);
            formTrack.AutoSize = true;
            formTrack.ShowDialog();
            #endregion

            // UNFINISHED
            #region Track Controller form

            Form formTrackController = new Form();
            UserControl controlTrackController;
            controlTrackController = new TrackController.TrackControllerUI(Proto_environment);

            formTrackController.Controls.Add(controlTrackController);
            formTrackController.AutoSize = true;
            formTrackController.ShowDialog();
            #endregion

            // UNFINISHED
            #region CTC Form

            Form formCTC = new Form();
            CTCOffice.CTCOfficeGUI controlCTC;
            controlCTC = new CTCOffice.CTCOfficeGUI(Proto_environment, Proto_office);

            controlCTC.ShowTrain += new EventHandler<CTCOffice.ShowTrainEventArgs>(controlCTC_ShowTrain);

            formCTC.Controls.Add(controlCTC);
            formCTC.AutoSize = true;
            formCTC.ShowDialog();

            #endregion

            // UNFINISHED
            #region Train Model form

            Form formTrain = new Form();
            UserControl controlTrain;
            controlTrain = new TrainModel.TrainGUI(Proto_environment);
            formTrain.Controls.Add(controlTrain);
            formTrain.AutoSize = true;
            formTrain.ShowDialog();

            #endregion

            // UNFINISHED
            #region Train Controller form
            /*
                    Form formTrainController = new Form();
                    UserControl controlTrainController = new UserControl();
                    */

            #endregion

            // UNFINISHED
            #region Scheduler form

            Form formScheduler = new Form();
            UserControl controlScheduler;
            controlScheduler = new SystemScheduler.SystemSchedulerGUI(Proto_environment, Proto_scheduler, Proto_office);
            formScheduler.Controls.Add(controlScheduler);
            formScheduler.AutoSize = true;
            formScheduler.ShowDialog();
            #endregion
        }

        void controlCTC_ShowTrain(object sender, CTCOffice.ShowTrainEventArgs e)
        {
            Form formTrainController = new Form();
            UserControl controlTrainController = null;
            controlTrainController = new TrainController.TrainControllerUI(e.TrainModel.TrainController, _env);
            formTrainController.Controls.Add(controlTrainController);
            formTrainController.AutoSize = true;
            formTrainController.ShowDialog();
        }
    }
}
