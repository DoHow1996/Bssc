using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Autodesk.AutoCAD.DatabaseServices;
using BaseLibrary.ExtensionMethod;
using Autodesk.AutoCAD.Geometry;
using BaseLibrary.ResultData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.GraphicsSystem;
using Bssc.ViewForms.AffiliateForms;
using Bssc.Models.ModelsV.SourceModelsV;
using View = Autodesk.AutoCAD.GraphicsSystem.View;
using Bssc.Control.DataControl;

namespace Bssc.Control.CadControl
{
    public static class PreViewOperation
    {
        /// <summary>
        /// 布墩界面的预览
        /// </summary>
        /// <param name="previewControl"></param>
        /// <param name="foundationSourceModelV"></param>
        /// <param name="pierSourceModelV"></param>
        public static void ArrangePierPreView(this PreviewControl previewControl, FoundationSourceModelV foundationSourceModelV,PierSourceModelV pierSourceModelV)
        {
            try
            {
                View view = previewControl.view;
                Model model = previewControl.model;
                view.EraseAll();


                Polyline polyline1 = new Polyline();
                Point3dCollection pCols = new Point3dCollection();

                Polyline polyline = new Polyline();
                Point3dCollection pCols1 = new Point3dCollection();

                if (foundationSourceModelV != null)
                {
                    List<Position> pileCenterPositions = foundationSourceModelV.PileCenterPoints.GetPositions();
                    List<Position> CapsSectionPositions = foundationSourceModelV.CapsSectionPoints.GetPositions();
                    for (int i = 0; i < CapsSectionPositions.Count; i++)
                    {
                        pCols.Add(new Point3d(CapsSectionPositions[i].X, CapsSectionPositions[i].Y, 0));
                    }
                    polyline1.Ex_CreatePolyline(pCols);
                    polyline1.Closed = true;

                    List<Circle> cirs = new List<Circle>();

                    for (int i = 0; i < pileCenterPositions.Count; i++)
                    {
                        cirs.Add(new Circle(new Point3d(pileCenterPositions[i].X,
                            pileCenterPositions[i].Y, 0), Vector3d.ZAxis, foundationSourceModelV.pileRadius / 2));
                    }

                    view.Add(polyline1, model);

                    for (int i = 0; i < cirs.Count; i++)
                    {
                        view.Add(cirs[i], model);
                    }
                }

                if (pierSourceModelV != null)
                {
                    double width = pierSourceModelV.BottomTransverseWidth;
                    double height = pierSourceModelV.BottomLongitudinalThickness;
                    double radius = pierSourceModelV.PierRounderCornerRadius;

                    pCols1.Add(new Point3d(width / 2, -(height / 2 - radius), 0));
                    pCols1.Add(new Point3d(width / 2, (height / 2 - radius), 0));
                    pCols1.Add(new Point3d(width / 2 - radius, (height / 2), 0));
                    pCols1.Add(new Point3d(-(width / 2 - radius), (height / 2), 0));
                    pCols1.Add(new Point3d(-(width / 2), (height / 2 - radius), 0));
                    pCols1.Add(new Point3d(-width / 2, -(height / 2 - radius), 0));
                    pCols1.Add(new Point3d(-(width / 2 - radius), -(height / 2), 0));
                    pCols1.Add(new Point3d(width / 2 - radius, -(height / 2), 0));

                    polyline.Ex_CreatePolyline(pCols1);
                    polyline.Closed = true;
                    polyline.SetBulgeAt(1, Math.Tan(Math.PI / 2 / 4));
                    polyline.SetBulgeAt(3, Math.Tan(Math.PI / 2 / 4));
                    polyline.SetBulgeAt(5, Math.Tan(Math.PI / 2 / 4));
                    polyline.SetBulgeAt(7, Math.Tan(Math.PI / 2 / 4));

                    view.Add(polyline, model);
                }

                Extents3d extents3D = polyline1.GeometricExtents;
                Point3d midPoint = extents3D.MinPoint.Ex_GetMidPointTo(extents3D.MaxPoint);
                double xx = extents3D.MaxPoint.X - extents3D.MinPoint.X;
                double yy = extents3D.MaxPoint.Y - extents3D.MinPoint.Y;
                Extents3d Extends3DTemp = new Extents3d(new Point3d(midPoint.X - xx, midPoint.Y - yy, 0), new Point3d(midPoint.X + xx, midPoint.Y + yy, 0));
                previewControl.extents = Extends3DTemp;
                previewControl.outZoom();
            }
            catch (Exception)
            {

            }
            

        }

        /// <summary>
        /// 基础配置界面的预览
        /// </summary>
        /// <param name="previewControl"></param>
        /// <param name="foundationSourceModelV"></param>
        /// <param name="pierSourceModelV"></param>
        public static void FoundationConfigPreView(this PreviewControl previewControl, FoundationSourceModelV foundationSourceModelV)
        {
            try
            {
                List<Entity> ents = new List<Entity>();
                View view = previewControl.view;
                Model model = previewControl.model;
                try
                {
                    view.EraseAll();
                }
                catch (Exception)
                {

                }
                view.EraseAll();

                Polyline polyline1 = new Polyline();
                Point3dCollection pCols = new Point3dCollection();

                if (foundationSourceModelV != null)
                {
                    List<Position> pileCenterPositions = foundationSourceModelV.PileCenterPoints.GetPositions();
                    List<Position> CapsSectionPositions = foundationSourceModelV.CapsSectionPoints.GetPositions();
                    for (int i = 0; i < CapsSectionPositions.Count; i++)
                    {
                        pCols.Add(new Point3d(CapsSectionPositions[i].X, CapsSectionPositions[i].Y, 0));
                    }
                    polyline1.Ex_CreatePolyline(pCols);
                    polyline1.Closed = true;

                    ents.Add(polyline1);

                    List<Circle> cirs = new List<Circle>();

                    for (int i = 0; i < pileCenterPositions.Count; i++)
                    {
                        cirs.Add(new Circle(new Point3d(pileCenterPositions[i].X,
                            pileCenterPositions[i].Y, 0), Vector3d.ZAxis, foundationSourceModelV.pileRadius / 2));
                    }

                    view.Add(polyline1, model);

                    for (int i = 0; i < cirs.Count; i++)
                    {
                        ents.Add(cirs[i]);
                        view.Add(cirs[i], model);
                    }
                }

                List<Extents3d> extents3Ds = new List<Extents3d>();
                extents3Ds = ents.Select(aa => aa.GeometricExtents).ToList();
                
                double minX = extents3Ds.OrderBy(aa => aa.MinPoint.X).First().MinPoint.X;
                double minY = extents3Ds.OrderBy(aa => aa.MinPoint.Y).First().MinPoint.Y;
                double maxX = extents3Ds.OrderByDescending(aa => aa.MinPoint.X).First().MaxPoint.X;
                double maxY = extents3Ds.OrderByDescending(aa => aa.MinPoint.Y).First().MaxPoint.Y;
                Extents3d extents3D = new Extents3d(new Point3d(minX,minY,0),new Point3d(maxX,maxY,0));
                //Extents3d extents3D = polyline1.GeometricExtents;
                Point3d midPoint = extents3D.MinPoint.Ex_GetMidPointTo(extents3D.MaxPoint);
                double xx = extents3D.MaxPoint.X - extents3D.MinPoint.X;
                double yy = extents3D.MaxPoint.Y - extents3D.MinPoint.Y;
                Extents3d Extends3DTemp = new Extents3d(new Point3d(midPoint.X - xx, midPoint.Y - yy, 0), new Point3d(midPoint.X + xx, midPoint.Y + yy, 0));
                previewControl.extents = Extends3DTemp;
                previewControl.outZoom();
            }
            catch (Exception)
            {

            }
            

        }

        /// <summary>
        /// 墩配置界面上的预览
        /// </summary>
        /// <param name="previewControl"></param>
        /// <param name="foundationSourceModelV"></param>
        /// <param name="pierSourceModelV"></param>
        public static void PierConfigPreView(this PreviewControl previewControl, PierSourceModelV pierSourceModelV)
        {
            try
            {
                View view = previewControl.view;
                Model model = previewControl.model;
                try
                {
                    view.EraseAll();
                }
                catch (Exception)
                {

                }
                view.EraseAll();

                Polyline polyline = new Polyline();
                Point3dCollection pCols1 = new Point3dCollection();


                if (pierSourceModelV != null)
                {
                    double width = pierSourceModelV.BottomTransverseWidth;
                    double height = pierSourceModelV.BottomLongitudinalThickness;
                    double radius = pierSourceModelV.PierRounderCornerRadius;

                    pCols1.Add(new Point3d(width / 2, -(height / 2 - radius), 0));
                    pCols1.Add(new Point3d(width / 2, (height / 2 - radius), 0));
                    pCols1.Add(new Point3d(width / 2 - radius, (height / 2), 0));
                    pCols1.Add(new Point3d(-(width / 2 - radius), (height / 2), 0));
                    pCols1.Add(new Point3d(-(width / 2), (height / 2 - radius), 0));
                    pCols1.Add(new Point3d(-width / 2, -(height / 2 - radius), 0));
                    pCols1.Add(new Point3d(-(width / 2 - radius), -(height / 2), 0));
                    pCols1.Add(new Point3d(width / 2 - radius, -(height / 2), 0));

                    polyline.Ex_CreatePolyline(pCols1);
                    polyline.Closed = true;
                    polyline.SetBulgeAt(1, Math.Tan(Math.PI / 2 / 4));
                    polyline.SetBulgeAt(3, Math.Tan(Math.PI / 2 / 4));
                    polyline.SetBulgeAt(5, Math.Tan(Math.PI / 2 / 4));
                    polyline.SetBulgeAt(7, Math.Tan(Math.PI / 2 / 4));

                    double x = pierSourceModelV.BottomTransverseWidth /4;
                    double y = pierSourceModelV.BottomLongitudinalThickness / 4;

                    switch (pierSourceModelV.SupportArrangement )
                    {
                        case "单支座":
                            view.Add(GetPolyline(0, 0),model);
                            break;
                        case "1排2列":
                            view.Add(GetPolyline(x, 0), model);
                            view.Add(GetPolyline(-x, 0), model);
                            break;
                        case "2排1列":
                            view.Add(GetPolyline(0, y), model);
                            view.Add(GetPolyline(0, -y), model);
                            break;
                        case "品字形":
                            view.Add(GetPolyline(x, -y), model);
                            view.Add(GetPolyline(-x, -y), model);
                            view.Add(GetPolyline(0, y), model);
                            break;
                        case "2排2列":
                            view.Add(GetPolyline(x, -y), model);
                            view.Add(GetPolyline(-x, -y), model);
                            view.Add(GetPolyline(x, y), model);
                            view.Add(GetPolyline(-x, y), model);
                            break;
                        default:
                            break;
                    }

                    view.Add(polyline, model);
                }

                Extents3d extents3D = polyline.GeometricExtents;
                Point3d midPoint = extents3D.MinPoint.Ex_GetMidPointTo(extents3D.MaxPoint);
                double xx = extents3D.MaxPoint.X - extents3D.MinPoint.X;
                double yy = extents3D.MaxPoint.Y - extents3D.MinPoint.Y;
                Extents3d Extends3DTemp = new Extents3d(new Point3d(midPoint.X - xx, midPoint.Y - yy, 0), new Point3d(midPoint.X + xx, midPoint.Y + yy, 0));
                previewControl.extents = Extends3DTemp;
                previewControl.outZoom();
            }
            catch (Exception)
            {

            }    
        }

        private static Polyline GetPolyline(double x,double y)
        {
            double length = 0.1;
            Polyline polyline = new Polyline();
            Point3dCollection pCol = new Point3dCollection();
            pCol.Add(new Point3d(x - length, y + length, 0));
            pCol.Add(new Point3d(x + length, y + length, 0));
            pCol.Add(new Point3d(x + length, y - length, 0));
            pCol.Add(new Point3d(x - length, y - length, 0));
            polyline.Ex_CreatePolyline(pCol);
            polyline.Closed = true;
            return polyline;
        }

    }
}
