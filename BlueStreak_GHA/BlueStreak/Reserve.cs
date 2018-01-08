using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace BlueStreak
{
    public class Reserve : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Reserve class.
        /// </summary>
        public Reserve()
          : base("Reserve", "Reserve",
              "Stores Cryptocurrency Value",
              "BlueStreak", "Input")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddBooleanParameter("Reset", "Reset", "Empties stored values", GH_ParamAccess.item);
            pManager.AddNumberParameter("Currency Value", "Value", "Currency value to store", GH_ParamAccess.item);
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddNumberParameter("Crypto Values", "Values", "Stored Cryptocurrency values", GH_ParamAccess.list);
            pManager.AddNumberParameter("Stored Count", "Count", "Series of Cryptocurrency values stored. Use this along with the (Values) output to create a graph", GH_ParamAccess.list);

        }

        bool reset;
        double btValue;

        //declare list to store latest value
        List<int> latestValue = new List<int>();
        List<int> valueList = new List<int>();
        List<double> btValues = new List<double>();
        int starter = 1;



        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {

            DA.GetData(0, ref reset);
            DA.GetData(1, ref btValue);

            if (reset)
            {
                latestValue.Clear();
                btValues.Clear();
                valueList.Clear();
            }

            //add starter value to latest value list
            latestValue.Add(starter);

            //add bt values to the list
            btValues.Add(btValue);

            //create a loop that empties list and adds 1 to last number
            for (int i = 0; i < latestValue.Count; i++)
            {

                int counter = latestValue[0] + 1;
                latestValue.Clear();
                latestValue.Add(counter);

                //add values in the form of list
                valueList.Add(latestValue[0]);

            }

            //axisX = valueList;
           // Bitcoin_Stored_Values = btValues;

            DA.SetDataList(0, valueList);
            DA.SetDataList(1, btValues);
        }




        /// <summary>
        /// Provides an Icon for the component.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                //You can add image files to your project resources and access them like this:
                // return Resources.IconForThisComponent;
                return BlueStreak.Properties.Resources.Icon_Reserve;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("6156e3ea-b675-4203-bc10-32f5c977d1ad"); }
        }
    }
}