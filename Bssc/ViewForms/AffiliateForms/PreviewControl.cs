using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System;
using System.Drawing;
using System.Windows.Forms;
using Autodesk.AutoCAD;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.GraphicsSystem;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using View = Autodesk.AutoCAD.GraphicsSystem.View;

namespace Bssc.ViewForms.AffiliateForms
{
    public partial class PreviewControl : UserControl
    {
        #region fields
        public Device device;
        public Extents3d extents;
        public Model model;
        public Polyline polyline;
        public Point start;
        public DBText text;
        public View view;
        public double viewFactor = 1;
        #endregion



        public PreviewControl()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);

        }

        void CleanUp()
        {
            if (polyline != null)
            {
                polyline.Dispose();
                polyline = null;
            }

            if (model != null)
            {
                model.Dispose();
                model = null;
            }

            if (view != null)
            {
                // View need to be removed from device
                device.EraseAll();

                view.Dispose();
                view = null;
            }

            if (device != null)
            {
                device.Dispose();
                device = null;
            }
        }

        protected override void CreateHandle()
        {
            base.CreateHandle();

            if (DesignMode) return;

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Manager gsm = doc.GraphicsManager;

            //var descriptor = new KernelDescriptor();
            //descriptor.addRequirement(KernelDescriptor.Drawing3D);
            ////descriptor.addRequirement(UniqueString.Intern(KernelDescriptor.Drawing3D.ToString()));
            //kernel = Manager.AcquireGraphicsKernel(descriptor);
            device = gsm.CreateAutoCADDevice(Handle);
            device.OnSize(Size);

            view = new View();
            view.SetView(new Point3d(this.Size.Width / 2, this.Height / 2, 10), new Point3d(this.Size.Width / 2, this.Height / 2, 10),
                new Vector3d(0, 1, 0), this.Size.Width, this.Size.Height);
            device.Add(view);
            device.Update();

            model = gsm.CreateAutoCADModel();
            extents = new Extents3d();

            HandleDestroyed += PreviewUserControl_HandleDestroyed;
        }

        public void outZoom()
        {
            view.ZoomExtents(extents.MinPoint, extents.MaxPoint);
            RefreshView();
        }

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            RefreshView();
            view.ZoomExtents(extents.MinPoint, extents.MaxPoint);
            RefreshView();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {

            base.OnMouseDown(e);
            start = e.Location;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseDoubleClick(e);
            RefreshView();
            if (e.Button == MouseButtons.Left)
            {
                double num1 = start.X - e.Location.X;
                double num2 = start.Y - e.Location.Y;

                start = e.Location;

                view.Dolly(new Vector3d((double)(num1 * view.FieldWidth / Math.Min(this.Width, this.Height)),
                    -(double)(num2 * view.FieldHeight / Math.Min(this.Width, this.Height)), 0));

                RefreshView();
            }

        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            base.OnMouseWheel(e);
            if (e.Delta < 0)
            {

                view.SetView(new Point3d((1.0 * e.X / this.Width), (1.0 * e.Y / this.Height), 10),
                    new Point3d((1.0 * e.X / this.Width), (1.0 * e.Y / this.Height), 0),
                    view.UpVector, view.FieldWidth * 1.1, view.FieldHeight * 1.1);
                RefreshView();
            }
            else
            {



                view.SetView(new Point3d((1.0 * e.X / this.Width), (1.0 * e.Y / this.Height), 10),
                    new Point3d((1.0 * e.X / this.Width), (1.0 * e.Y / this.Height), 0),
                    view.UpVector, view.FieldWidth * 0.9, view.FieldHeight * 0.9);
                RefreshView();
            }
        }

        private void PreviewUserControl_HandleDestroyed(object sender, EventArgs e)
        {
            CleanUp();
        }

        public void RefreshView()
        {
            if (view == null) return;
            view.Invalidate();
            view.Update();
            device.Update();
        }

        private void PreviewControl_Load(object sender, EventArgs e)
        {

        }



        private Point3d getMidPoint(Point3d p1, Point3d p2)
        {
            return new Point3d((p1.X + p2.X) / 2, (p1.Y + p2.Y) / 2, 0);
        }

    }
}
