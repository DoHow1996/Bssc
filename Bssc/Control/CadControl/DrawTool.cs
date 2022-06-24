using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using BaseLibrary.ExtensionMethod;
using Autodesk.AutoCAD.EditorInput;
using Bssc.Models.ModelsV.SourceModelsV;

using BaseLibrary.ResultData;
using Bssc.Control.DataControl;
using Bssc.Control.Tools;
using BaseLibrary.Runtime;
using Bssc.Models.ModelsV.ProjectModelsV;
using EI;
using EICDPoint = EI.EICDPoint;

namespace Bssc.Control.CadControl
{
    public static class DrawTool
    {
        /// <summary>
        /// 绘制平曲线
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="roadSourceModelV">道路数据</param>
        public static void DrawPqx(this Document doc,RoadSourceModelV roadSourceModelV)
        {

            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();

            Database db = doc.Database;
            Editor ed = doc.Editor;

            ObjectIdCollection idsTemp = db.Ex_GetLayerAllObjectIds(roadSourceModelV.Designation + roadSourceModelV.Id);
            
            if (idsTemp.Count == 0)
            {

                EICurveFactory eICurveFactory = EIFactoryProducer.CreateFactory(EICurveType.EICD);
                //var aa1 = eICurveFactory.Load(roadSourceModelV.PqxFileName);
                eICurveFactory.LoadData(roadSourceModelV.pqxSourceModelVs);
                
                List<List<EICDPoint>> list = eICurveFactory.GetGeoEICurve();
                using (DocumentLock docLock = doc.LockDocument())
                {
                    using (DBTrans dBTrans = new DBTrans())
                    {
                        foreach (List<EICDPoint> points in list)
                        {
                            Polyline polyline = new Polyline();
                            for (int i = 0; i < points.Count; i++)
                            {
                                polyline.AddVertexAt(i, new Point2d(points[i].Y, points[i].X), points[i].Bulge, 0, 0);
                            }
                            TypedValueList typedValues = new TypedValueList();
                            typedValues.Add(new TypedValue(1000, roadSourceModelV.Designation));
                            typedValues.Add(new TypedValue(1000, "(" + points.First().Mark + "," + points.Last().Mark + ")"));
                            polyline.Ex_AddXData(roadSourceModelV.Designation + roadSourceModelV.Id, typedValues, db);
                            db.Ex_AddToModelSpace(polyline, roadSourceModelV.Designation + roadSourceModelV.Id);
                        }
                    }
                }
                #region 以前的代码
                //EICDCurve EICD = new EICDCurve();
                //EICD.CreateEICDCurveByFile(roadSourceModelV.PqxFileName);
                //EICD.Print();
                //using (DocumentLock docLock = doc.LockDocument())
                //{
                //    using (Transaction trans = doc.TransactionManager.StartTransaction())
                //    {
                //        db.Ex_AddLayer(roadSourceModelV.Designation + roadSourceModelV.Id);
                //        BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);
                //        BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                //        List<Entity> polylines = EICD.DrawCADEntity(roadSourceModelV.Designation + roadSourceModelV.Id, roadSourceModelV.Designation);
                //        //List<Entity> polylines = EICD.DrawCADEntity();
                //        bool stu = true;
                //        foreach (Entity polyline in polylines)
                //        {
                //            if (stu)
                //            {
                //                polyline.ColorIndex = 10;
                //                polyline.Layer = roadSourceModelV.Designation + roadSourceModelV.Id;
                //                stu = false;
                //            }
                //            else
                //            {
                //                polyline.ColorIndex = 90;
                //                polyline.Layer = roadSourceModelV.Designation + roadSourceModelV.Id;
                //                stu = true;
                //            }

                //            btr.AppendEntity(polyline);
                //            trans.AddNewlyCreatedDBObject(polyline, true);

                //        }
                //        trans.Commit();
                //    }
                //}
                #endregion

            }


            List<Entity> ents = HostApplicationServices.WorkingDatabase.Ex_GetLayerAllEntitys(roadSourceModelV.Designation + roadSourceModelV.Id);

            List<Point3d> pMaxs = new List<Point3d>();
            List<Point3d> pMins = new List<Point3d>();

            for (int i = 0; i < ents.Count; i++)
            {
                Extents3d extents = ents[i].GeometricExtents;
                pMaxs.Add(extents.MaxPoint);
                pMins.Add(extents.MinPoint);
            }

            var pMaxsTemp = (from aa in pMaxs orderby aa.Y descending select aa).ToList<Point3d>();
            var pMinsTemp = (from aa in pMins orderby aa.Y ascending select aa).ToList<Point3d>();

            Point3d pMax = pMaxsTemp[0];
            Point3d pMin = pMinsTemp[0];

            ViewTableRecord vtr = new ViewTableRecord();
            vtr.Width = pMax.X - pMin.X;
            vtr.Height = pMax.Y - pMin.Y;
            vtr.CenterPoint = new Point2d(pMin.X + (pMax.X - pMin.X) / 2, pMin.Y + (pMax.Y - pMin.Y) / 2);
            ed.SetCurrentView(vtr);
            ed.Regen();

        }

        /// <summary>
        /// 进行分跨
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="roadSourceModelV">道路数据</param>
        public static void DivideSpan(this Document doc, RoadSourceModelV roadSourceModelV)
        {
            Database db = doc.Database;
            Editor ed = doc.Editor;
            List<Entity> ents = db.Ex_GetLayerAllEntitys(roadSourceModelV.Designation + roadSourceModelV.Id);
            List<Curve> curves = new List<Curve>();
            for (int i = 0; i < ents.Count; i++)
            {
                curves.Add((Curve)ents[i]);
            }
            ArrangeDivideSpanLine arrangeDivideSpanLine = new ArrangeDivideSpanLine(curves,roadSourceModelV);
            arrangeDivideSpanLine.addPier();
        }

        public static void ArrangePier(this Document doc, RoadSourceModelV roadSourceModelV,BridgeModelV bridgeModelV,PierSourceModelV pierSourceModelV, FoundationSourceModelV foundationSourceModelV,bool isMain)
        {
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
            TypedValue[] values = new TypedValue[2];
            values.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
            values.SetValue(new TypedValue((int)DxfCode.LayerName, roadSourceModelV.Designation + "分跨线标记"), 1);
            SelectionFilter filter = new SelectionFilter(values);

            Editor ed = doc.Editor;
            Database db = doc.Database;
            bool isFlag = true;
            while (isFlag)
            {
                PromptSelectionOptions pso = new PromptSelectionOptions();
                pso.MessageForAdding = "请选择分跨线";
                PromptSelectionResult psr = ed.GetSelection(pso, filter);
                if (psr.Status != PromptStatus.OK)
                {
                    isFlag = false;
                    return;
                }
                SelectionSet sSet = psr.Value;

                BlockReference br = null;

                using (DocumentLock docLock = doc.LockDocument())
                {
                    using (Transaction trans = db.TransactionManager.StartTransaction())
                    {
                        foreach (ObjectId id in sSet.GetObjectIds())
                        {
                            br = id.GetObject(OpenMode.ForWrite) as BlockReference;
                        }
                        trans.Commit();
                    }
                }

                if (pierSourceModelV != null && foundationSourceModelV != null)
                {
                    ArrangePierAndFoundation arrangePierAndFoundation 
                        = new ArrangePierAndFoundation(br.Position, br.Rotation, roadSourceModelV, bridgeModelV, pierSourceModelV, foundationSourceModelV,isMain);
                    arrangePierAndFoundation.TSTSnapEntityJig();
                }

            }

            



        }

        /// <summary>
        /// 绘制勘探孔
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="explorationSourceModelV">勘探孔数据</param>
        public static void PlotExplorationHole(this Document doc,List<ExplorationSourceModelV> explorationSourceModelVs)
        {

            if (explorationSourceModelVs.Count == 0)
            {
                return;
            }

            Database db = doc.Database;
            Editor ed = doc.Editor;

            using (DocumentLock docLock = doc.LockDocument())
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    for (int i = 0; i < explorationSourceModelVs.Count; i++)
                    {
                        string UnitId = explorationSourceModelVs[i].UnitId;
                        string Num = explorationSourceModelVs[i].Num;
                        string Type = explorationSourceModelVs[i].Type;
                        double PointX = Convert.ToDouble(explorationSourceModelVs[i].PointX);
                        double PointY = Convert.ToDouble(explorationSourceModelVs[i].PointY);

                        Point3d pInsert = new Point3d(PointX, PointY, 0);

                        BlockTable bt = (BlockTable)db.BlockTableId.GetObject(OpenMode.ForRead);
                        if (!bt.Has(Type))
                        {
                            ed.WriteMessage("请手动添加" + Type + "块");
                            Type = "当前无该钻孔块";
                        }

                        ObjectId blockId = db.Ex_InsertBlockReference("0", Type, pInsert, new Scale3d(1, 1, 1), 0);
                        TypedValueList values = new TypedValueList();
                        values.Add(new TypedValue(1000, UnitId));
                        blockId.Ex_AddXData("ExplorationHole", values);

                    }
                    trans.Commit();
                }
                
            }
        }

        /// <summary>
        /// 创建墩块
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="pierSourceModelV"></param>
        public static void CreatePierBlock(this Document doc,PierSourceModelV pierSourceModelV)
        {

            Database db = doc.Database;

            double width = pierSourceModelV.BottomTransverseWidth;
            double height = pierSourceModelV.BottomLongitudinalThickness;
            double radius = pierSourceModelV.PierRounderCornerRadius;

            Polyline polyline = new Polyline();
            Point3dCollection pCols = new Point3dCollection();

            pCols.Add(new Point3d(width / 2, -(height / 2 - radius), 0));
            pCols.Add(new Point3d(width / 2, (height / 2 - radius), 0));
            pCols.Add(new Point3d(width / 2 - radius, (height / 2), 0));
            pCols.Add(new Point3d(-(width / 2 - radius), (height / 2), 0));
            pCols.Add(new Point3d(-(width / 2), (height / 2 - radius), 0));
            pCols.Add(new Point3d(-width / 2, -(height / 2 - radius), 0));
            pCols.Add(new Point3d(-(width / 2 - radius), -(height / 2), 0));
            pCols.Add(new Point3d(width / 2 - radius, -(height / 2), 0));

            polyline.Ex_CreatePolyline(pCols);
            polyline.Closed = true;
            polyline.SetBulgeAt(1, Math.Tan(Math.PI / 2 / 4));
            polyline.SetBulgeAt(3, Math.Tan(Math.PI / 2 / 4));
            polyline.SetBulgeAt(5, Math.Tan(Math.PI / 2 / 4));
            polyline.SetBulgeAt(7, Math.Tan(Math.PI / 2 / 4));

            using (DocumentLock doclock = doc.LockDocument())
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    db.Ex_AddBlockTableRecord(pierSourceModelV.Id, true, polyline);
                    trans.Commit();
                }
            }

        }

        /// <summary>
        /// 创建基础块
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="foundationSourceModelV"></param>
        public static void CreateFoundationBlock(this Document doc,FoundationSourceModelV foundationSourceModelV)
        {
            #region 向CAD中建立块
            List<Position> pileCenterPositions = foundationSourceModelV.PileCenterPoints.GetPositions();
            List<Position> CapsSectionPositions = foundationSourceModelV.CapsSectionPoints.GetPositions();

            Database db = doc.Database;

            List<Entity> ents = new List<Entity>();

            Polyline polyline = new Polyline();
            Point3dCollection pCols = new Point3dCollection();

            for (int i = 0; i < CapsSectionPositions.Count; i++)
            {
                pCols.Add(new Point3d(CapsSectionPositions[i].X,CapsSectionPositions[i].Y, 0));
            }



            polyline.Ex_CreatePolyline(pCols);
            polyline.Closed = true;

            Line l1 = new Line(new Point3d(-6, 0, 0), new Point3d(6, 0, 0));
            Line l2 = new Line(new Point3d(0, 4, 0), new Point3d(0, -4, 0));
            l1.Linetype = "DASHED";
            l1.LinetypeScale = 0.1;
            l2.Linetype = "DASHED";
            l2.LinetypeScale = 0.1;
            ents.Add(l1);
            ents.Add(l2);

            ents.Add(polyline);

            for (int i = 0; i < pileCenterPositions.Count; i++)
            {
                ents.Add(new Circle(new Point3d(pileCenterPositions[i].X,pileCenterPositions[i].Y, 0), Vector3d.ZAxis, foundationSourceModelV.pileRadius / 2));
            }

            using (DocumentLock doclock = doc.LockDocument())
            {
                using (Transaction trans = db.TransactionManager.StartTransaction())
                {
                    ObjectId id = db.Ex_AddBlockTableRecord(foundationSourceModelV.Id, ents, true);
                    trans.Commit();
                }
            }
            #endregion
        }

        /// <summary>
        /// 创建平曲线
        /// </summary>
        /// <param name="pqxSourceModelVs"></param>
        public static void CreatePqx(this List<PqxSourceModelV> pqxSourceModelVs)
        {
            
            Document doc = Application.DocumentManager.MdiActiveDocument;
            using (DocumentLock docLock = doc.LockDocument())
            {
                using (DBTrans dBTrans = new DBTrans())
                {
                    Point3dCollection point3DCollection = new Point3dCollection();
                    for (int i = 0; i < 10000; i++)
                    {
                        double px = RoadDataHandle.xyf_xy(i, 0, 0, 1, pqxSourceModelVs);
                        double py = RoadDataHandle.xyf_xy(i, 0, 0, 2, pqxSourceModelVs);
                        point3DCollection.Add(new Point3d(px, py, 0));
                    }
                    Polyline polyline = new Polyline();
                    polyline.Ex_CreatePolyline(point3DCollection);
                    doc.Database.Ex_AddToModelSpace(polyline);
                }
            }
            
        }
    }

    

}

