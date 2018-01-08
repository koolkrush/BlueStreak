using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace BlueStreak
{
    public class BlueStreakComponent : GH_Component
    {
        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public BlueStreakComponent()
          : base("Ticker", "Ticker",
              "Retrieves Cryptocurrency Value",
              "BlueStreak", "Input")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddTextParameter("Input Currency", "From", "Currency code to convert (i.e. BTC, ETH, USD..etc)", GH_ParamAccess.item);
            pManager.AddTextParameter("Output Currency", "To", "Currency code to convert to (i.e. BTC, ETH, USD..etc)", GH_ParamAccess.item);
            pManager.AddTextParameter("Market (Optional)", "Market", "Retrieve data from a specific market(i.e. Coinbase, Bitstamp..etc)", GH_ParamAccess.item);
            pManager.AddBooleanParameter("Activate","Fetch","Activates Ticker",GH_ParamAccess.item);

            pManager[2].Optional = true;

        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {

            pManager.AddTextParameter("Currency Value","Result","Cryptocurrency Value (Updates every 10 seconds)",GH_ParamAccess.item);

        }

        string Market;
        string inputCurrency;
        string outputCurrency;
        Boolean active;

        string cryptoValue;

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            DA.GetData(0, ref inputCurrency);
            DA.GetData(1, ref outputCurrency);
            DA.GetData(2, ref Market);
            DA.GetData(3, ref active);

            //Generate URL
            string address;
            string path1 = "https://min-api.cryptocompare.com/data/price?fsym=";
            string path2 = "&tsyms=";
            string path3 = "&e=";

            if (Market == null)
            {
                string url = path1 + inputCurrency + path2 + outputCurrency;
                address = url;
            }
            else
            {
                string url2 = path1 + inputCurrency + path2 + outputCurrency + path3 + Market;
                address = url2;
            }

            //Download data from URL and Parse
            System.Net.WebClient client = new System.Net.WebClient();
            string downloadedData = client.DownloadString(address);
            string[] words = downloadedData.Split(':', '}');


            /*
            //Timer
            if (active)
            {
                ghDocument = OnPingDocument();
                ghDocument.SolutionEnd += documentSolutionsEnd;
                cryptoValue = words[1];
            }
            else
            {
                cryptoValue = null;
            }
            */
            if (active)
            {
                cryptoValue = words[1];
            }

            DA.SetData(0, cryptoValue);

        }

        /*
        //Timer
        GH_Document ghDocument;
        System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        void documentSolutionsEnd(object sender, GH_SolutionEventArgs e)
        {
            ghDocument.SolutionEnd -= documentSolutionsEnd;
            myTimer.Interval = 10000;
            myTimer.Tick += myTimerTick;
            myTimer.Start();
        }
        void myTimerTick(object sender, EventArgs e)
        {
            myTimer.Tick -= myTimerTick;
            myTimer.Stop();
            ghDocument.NewSolution(true);
        }
        */



        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return BlueStreak.Properties.Resources.Icon_Ticker;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("53db3b12-d1fa-4db1-a07c-a0bd2c1ef308"); }
        }
    }
}
