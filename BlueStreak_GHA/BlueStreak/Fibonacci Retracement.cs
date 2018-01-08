using System;
using System.Collections.Generic;

using Grasshopper.Kernel;
using Rhino.Geometry;

namespace BlueStreak
{
    public class Fibonacci_Retracement : GH_Component
    {
        /// <summary>
        /// Initializes a new instance of the Fibonacci_Retracement class.
        /// </summary>
        public Fibonacci_Retracement()
          : base("Fibonacci_Retracement", "Fib Retracement",
              "Display Fibonacci Retracement datums",
              "BlueStreak", "Input")
        {
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            pManager.AddPointParameter("Low Point", "Low", "Lowest point in a graph", GH_ParamAccess.item);
            pManager.AddPointParameter("High Point", "High", "Highest point in a graph", GH_ParamAccess.item);
            pManager.AddNumberParameter("Extend Lines","Extend","",GH_ParamAccess.item);

            pManager[2].Optional = true;


        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            pManager.AddLineParameter("Fib618n", "Fib618n", "", GH_ParamAccess.item);
            pManager.AddLineParameter("Fib382", "Fib382", "", GH_ParamAccess.item);
            pManager.AddLineParameter("Fib500", "Fib500", "", GH_ParamAccess.item);
            pManager.AddLineParameter("Fib618", "Fib618", "", GH_ParamAccess.item);
            pManager.AddLineParameter("Fib1618", "Fib1618", "", GH_ParamAccess.item);

        }

        Point3d LowPoint;
        Point3d HighPoint;

        double Extensions;



        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object is used to retrieve from inputs and store in outputs.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            DA.GetData(0, ref LowPoint);
            DA.GetData(1, ref HighPoint);
            DA.GetData(2, ref Extensions);


            if (HighPoint.X < LowPoint.X)
            {
                Extensions = Extensions * -1;
            }

            double xDisplacement = HighPoint.X - LowPoint.X + (Extensions * 2);

            double dailyLow = LowPoint.Y;
            double dailyHigh = HighPoint.Y;

            double fib618n = dailyLow - (dailyHigh - dailyLow) * 0.618;
            double fib382 = dailyLow + (dailyHigh - dailyLow) * 0.382;
            double fib500 = dailyLow + (dailyHigh - dailyLow) * 0.5;
            double fib618 = dailyLow + (dailyHigh - dailyLow) * 0.618;
            double fib1618 = dailyLow + (dailyHigh - dailyLow) * 0.618;

            Vector3d xVec = new Vector3d(10, 0, 0);

            Point3d ptFib618n = new Point3d((LowPoint.X - Extensions), fib618n, 0);
            Point3d ptFib382 = new Point3d((LowPoint.X - Extensions), fib382, 0);
            Point3d ptFib500 = new Point3d((LowPoint.X - Extensions), fib500, 0);
            Point3d ptFib618 = new Point3d((LowPoint.X - Extensions), fib618, 0);
            Point3d ptFib1618 = new Point3d((LowPoint.X - Extensions), fib1618, 0);

            Line lnFib618n = new Line(ptFib618n, xVec, xDisplacement);
            Line lnFib382 = new Line(ptFib382, xVec, xDisplacement);
            Line lnFib500 = new Line(ptFib500, xVec, xDisplacement);
            Line lnFib618 = new Line(ptFib618, xVec, xDisplacement);
            Line lnFib1618 = new Line(ptFib1618, xVec, xDisplacement);

            DA.SetData(0, lnFib618n);
            DA.SetData(1, lnFib382);
            DA.SetData(2, lnFib500);
            DA.SetData(3, lnFib618);
            DA.SetData(4, lnFib1618);

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
                return BlueStreak.Properties.Resources.Icon_Fibonacci;
            }
        }

        /// <summary>
        /// Gets the unique ID for this component. Do not change this ID after release.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("e96e2bc2-d277-475a-b544-6f428332447c"); }
        }
    }
}