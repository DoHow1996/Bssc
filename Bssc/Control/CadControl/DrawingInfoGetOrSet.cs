using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;
//CAD开发库
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.DatabaseServices;

//CAD开发基础库
using BaseLibrary.Runtime;
using BaseLibrary.ResultData;
using BaseLibrary.ExtensionMethod;
using BaseLibrary.Filter;
using Bssc.Models.ModelsV.ProjectModelsV;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Bssc.Models.ModelsV;
using Bssc.Control.Tools;
using Bssc.Models.ModelsV.SourceModelsV;
using Bssc.ViewForms.AffiliateForms;

namespace Bssc.Control.CadControl
{
    public static class DrawingInfoGetOrSet
    {
        /// <summary>
        /// 获取图面信息
        /// </summary>
        /// <returns></returns>
        public static List<SubSModelV> GetSubSModelVs(TreeNode node)
        {

            List<SubSModelV> subSModelVs1 = new List<SubSModelV>();

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            ///List<SubSModelV> subSModelVs = new List<SubSModelV>();

            #region 数据
            TreeNode bridgeNode = node.Parent;
            TreeNode roadNode = bridgeNode.Parent;
            var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == roadNode.Name).FirstOrDefault();
            var bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == bridgeNode.Name).FirstOrDefault();
            var roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault(); ;
            var subSModelVs = bridgeModelV.subSModelVs;

            string roadLayer = roadSourceModelV.Designation + roadSourceModelV.Id;
            string fkxLayer = roadSourceModelV.Designation + "分跨线标记";

            subSModelVs.ForEach(aa => subSModelVs1.Add(aa));

            subSModelVs.Clear();

            ///清除下部结构List信息
            //for (int i = 0; i < subSModelVs.Count; i++)
            //{
            //    subSModelVs[i].Id = node.Name;
            //    subSModelVs[i].distance = 0;
            //    subSModelVs[i].PierNum = "";
            //    subSModelVs[i].Mark = 0;
            //    subSModelVs[i].Angle = 0;
            //}
            #endregion



            using (DocumentLock docLock = doc.LockDocument())
            {
                using (Transaction trans = doc.TransactionManager.StartTransaction())
                {
                    List<Entity> ents = db.Ex_GetLayerAllEntitys(roadLayer);
                    //路线曲线
                    List<Curve> curves = new List<Curve>();

                    for (int i = 0; i < ents.Count; i++)
                    {
                        curves.Add((Curve)ents[i]);
                    }
                    ents.Clear();

                    ents = db.Ex_GetLayerAllEntitys(fkxLayer);
                    //分跨线块
                    List<BlockReference> brs = new List<BlockReference>();
                    for (int i = 0; i < ents.Count; i++)
                    {
                        brs.Add((BlockReference)ents[i]);
                    }

                    for (int i = 0; i < brs.Count; i++)
                    {
                        Point3d valuePM = brs[i].Position;
                        var curvesTemp = (from aa in curves orderby ((aa.GetClosestPointTo(valuePM, false)).DistanceTo(valuePM)) ascending select aa).ToList<Curve>();
                        //离分跨线块最近的路线
                        Curve curveTemp = curvesTemp[0];

                        TypedValueList brValueList = brs[i].Ex_GetXData("分跨线标记");
                        string FkxUnitId = Convert.ToString(brValueList[4].Value);


                        List<string> fkxUnitIds = (from aa in subSModelVs1 select aa.FkxUnitId).ToList();

                        if ((curveTemp.GetClosestPointTo(valuePM, false)).DistanceTo(valuePM) < 2)
                        {
                            TypedValueList valuesTemp = curveTemp.Ex_GetXData(curveTemp.Layer);
                            string strTemp = Convert.ToString(valuesTemp[2].Value);
                            strTemp = strTemp.Substring(1, strTemp.Length - 2);
                            string[] strsTemp = strTemp.Split(',');
                            double zhMinTemp = Convert.ToDouble(strsTemp[0]);
                            double zhMaxTemp = Convert.ToDouble(strsTemp[1]);

                            double lengthTemp = curveTemp.GetDistAtPoint(curveTemp.GetClosestPointTo(valuePM, false));

                            double zh = Math.Round(zhMinTemp + lengthTemp, 2);
                            //Vector3d curveNormalVector = curveTemp.getCurveNormalVector(curveTemp.GetClosestPointTo(valuePM, false));
                            Vector3d curveTanVector = curveTemp.Ex_GetTangentVector(curveTemp.GetClosestPointTo(valuePM, false));
                            double angelTemp = curveTanVector.GetAngleTo(new Vector3d(1, 0, 0));
                            if (curveTanVector.Y < 0)
                            {
                                angelTemp = Math.PI * 2 - angelTemp;
                            }

                            double rotation = Math.Round((brs[i].Rotation - angelTemp - Math.PI / 2) * 180 / Math.PI, 3);
                            if (rotation < 0)
                            {
                                rotation = rotation + 360;
                            }
                            if (rotation > 360)
                            {
                                rotation = rotation - 360;
                            }
                            

                            Vector3d qVector = curveTemp.Ex_GetTangentVector(curveTemp.GetClosestPointTo(valuePM, false));

                            Vector3d vz = qVector.RotateBy(-Math.PI / 2 + rotation * Math.PI / 180, Vector3d.ZAxis);
                            Vector3d vf = qVector.RotateBy(Math.PI / 2 + rotation * Math.PI / 180, Vector3d.ZAxis);


                            Line lineZ = new Line(new Point3d(valuePM.X + vz.X * 100 / vz.Length, valuePM.Y + vz.Y * 100 / vz.Length, 0), valuePM);
                            Line lineF = new Line(new Point3d(valuePM.X + vf.X * 100 / vf.Length, valuePM.Y + vf.Y * 100 / vf.Length, 0), valuePM);

                            //lineZ.ColorIndex = 5;
                            //lineF.ColorIndex = 6;
                            //doc.Database.Ex_AddToModelSpace(lineZ);
                            //doc.Database.Ex_AddToModelSpace(lineF);

                            TypedValue[] values = new TypedValue[1];
                            values.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
                            SelectionFilter filter = new SelectionFilter(values);

                            PromptSelectionResult psr = ed.SelectAll(filter);

                            SelectionSet sSet = psr.Value;

                            List<BlockReference> pierBlocks = new List<BlockReference>();
                            List<BlockReference> pileBlocks = new List<BlockReference>();

                            using (DocumentLock docLock1 = doc.LockDocument())
                            {
                                using (Transaction trans1 = doc.TransactionManager.StartTransaction())
                                {

                                    foreach (ObjectId id in sSet.GetObjectIds())
                                    {
                                        BlockReference br = id.GetObject(OpenMode.ForRead) as BlockReference;

                                        if (br.Position.DistanceTo(lineZ.GetClosestPointTo(br.Position, false)) < 0.01||
                                            br.Position.DistanceTo(lineF.GetClosestPointTo(br.Position, false)) < 0.01)
                                        {
                                            if (br.Name.Contains("pier"))
                                            {
                                                TypedValueList valuesTemp1 = br.Ex_GetXData("墩位标记");
                                                string strTemp1 = Convert.ToString(valuesTemp1[2].Value);

                                                if (strTemp1 == "辅墩" || strTemp1 == "主墩")
                                                {
                                                    pierBlocks.Add(br);
                                                }
                                            }

                                            if (br.Name.Contains("foundation"))
                                            {
                                                TypedValueList valuesTemp1 = br.Ex_GetXData("墩位标记");
                                                string strTemp1 = Convert.ToString(valuesTemp1[2].Value);

                                                if (strTemp1 == "辅墩" || strTemp1 == "主墩")
                                                {
                                                    pileBlocks.Add(br);
                                                }
                                            }
                                        }

                                        

                                    }
                                    trans1.Commit();
                                }
                            }

                            for (int j = 0; j < pierBlocks.Count; j++)
                            {

                                string pierArrangeUnitId = "";

                                bool isMainPier = true;

                                if (pierBlocks[j].Name.Contains("pier"))
                                {
                                    TypedValueList valuesTemp1 = pierBlocks[j].Ex_GetXData("墩位标记");
                                    string strTemp1 = Convert.ToString(valuesTemp1[2].Value);
                                    pierArrangeUnitId = Convert.ToString(valuesTemp1[6].Value);

                                    if (strTemp1 == "辅墩")
                                    {
                                        isMainPier = true;
                                    }
                                    else
                                    {
                                        isMainPier = false;
                                    }
                                }

                                double dis1 = pierBlocks[j].Position.DistanceTo(lineZ.GetClosestPointTo(pierBlocks[j].Position, false));
                                double dis2 = pierBlocks[j].Position.DistanceTo(lineF.GetClosestPointTo(pierBlocks[j].Position, false));

                                var tempSubsmodelvs = subSModelVs1.Where(aa => aa.FkxUnitId == FkxUnitId && aa.PierArrangeUnitId == pierArrangeUnitId).ToList() ;

                                SubSModelV tempSubsmodelv = null;

                                if (tempSubsmodelvs.Count != 0)
                                {
                                    tempSubsmodelv = tempSubsmodelvs.First();
                                }

                                if (dis1 <= dis2)
                                {
                                    double dis = pierBlocks[j].Position.DistanceTo(valuePM);
                                    Vector3d cV = curveTemp.Ex_GetTangentVector(curveTemp.GetClosestPointTo(pierBlocks[j].Position, false));
                                    double angleA = pierBlocks[j].Rotation;
                                    double angleB = brs[i].Rotation;

                                    PierSourceModelV pierSourceModelV = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == pierBlocks[j].Name).FirstOrDefault();

                                    SubSModelV subSModelV = new SubSModelV();

                                    if (tempSubsmodelv != null)
                                    {
                                        subSModelV = tempSubsmodelv;
                                    }


                                    subSModelV.Mark = Math.Round(zh,3);
                                    subSModelV.Angle = Math.Round(rotation, 3);
                                    subSModelV.PierName = pierBlocks[j].Name;
                                    subSModelV.FoundationName = pileBlocks[j].Name;
                                    subSModelV.IsTransitionalPier = pierSourceModelV.IsTransitionalPier;
                                    subSModelV.PierEccentricDistance = Math.Round(dis,3);
                                    subSModelV.PierAngle = Math.Round((angleA - angleB - Math.PI) * 180 / Math.PI,3);
                                    subSModelV.IsAuxiliaryPier = isMainPier ? 1 : 0;
                                    subSModelV.FkxUnitId = FkxUnitId;
                                    subSModelV.PierArrangeUnitId = pierArrangeUnitId;
                                    subSModelVs.Add(subSModelV);

        
                                }
                                
                                if (dis1 > dis2)
                                {
                                    
                                    double dis = -pierBlocks[j].Position.DistanceTo(valuePM);
                                    Vector3d cV = curveTemp.Ex_GetTangentVector(curveTemp.GetClosestPointTo(pierBlocks[j].Position, false));
                                    double angleA = pierBlocks[j].Rotation;
                                    double angleB = brs[i].Rotation;

                                    PierSourceModelV pierSourceModelV = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == pierBlocks[j].Name).FirstOrDefault();

                                    SubSModelV subSModelV = new SubSModelV();

                                    if (tempSubsmodelv != null)
                                    {
                                        subSModelV = tempSubsmodelv;
                                    }

                                    subSModelV.Mark = Math.Round(zh, 3);
                                    subSModelV.Angle = Math.Round(rotation, 3);
                                    subSModelV.PierName = pierBlocks[j].Name;
                                    subSModelV.FoundationName = pileBlocks[j].Name;
                                    subSModelV.IsTransitionalPier = pierSourceModelV.IsTransitionalPier;
                                    subSModelV.PierEccentricDistance = Math.Round(dis, 3);
                                    subSModelV.PierAngle = Math.Round((angleA - angleB - Math.PI) * 180 / Math.PI, 3);
                                    subSModelV.IsAuxiliaryPier = isMainPier ? 1 : 0;
                                    subSModelV.FkxUnitId = FkxUnitId;
                                    subSModelV.PierArrangeUnitId = pierArrangeUnitId;
                                    subSModelVs.Add(subSModelV);
                                }
                            }
                        }
                    }

                    subSModelVs.Distinct();

                    subSModelVs = (from aa in subSModelVs where (aa.Mark > bridgeModelV.StartMark && aa.Mark < bridgeModelV.EndMark)
                                   orderby aa.Mark ascending , aa.IsAuxiliaryPier ascending
                                   select aa).ToList();

                    //subSModelVs = subSModelVs.OrderBy(aa => aa.Mark).ThenBy(aa => aa.IsAuxiliaryPier).ToList();
                    
                    ///墩号起始号 用来标记墩号的增加
                    int dh = 1;
                    int affdh = 0;
                    for (int i = 0; i < subSModelVs.Count; i++)
                    {
                        
                        if (i == 0)
                        {
                            subSModelVs[i].distance = 0;
                            subSModelVs[i].PierNum = getDh(dh)+ "_0";
                        }
                        else
                        {
                            if (subSModelVs[i].Mark == subSModelVs[i - 1].Mark)
                            {
                                affdh = affdh + 1;
                                subSModelVs[i].PierNum = subSModelVs[i - 1].PierNum.Split('_')[0] + "_" + affdh;
                                subSModelVs[i].distance = subSModelVs[i].Mark - subSModelVs[i - 1].Mark;
                            }
                            else
                            {
                                affdh = 0;
                                dh = dh + 1;
                                subSModelVs[i].PierNum = getDh(dh) + "_0";
                                subSModelVs[i].distance = subSModelVs[i].Mark - subSModelVs[i - 1].Mark;
                            }
                        }
                        subSModelVs[i].PierNumber = subSModelVs[i].PierNum;

                        if (subSModelVs[i].PierAngle < 0)
                        {
                            subSModelVs[i].PierAngle = subSModelVs[i].PierAngle + 360;
                        }

                        if (subSModelVs[i].Angle < 0)
                        {
                            subSModelVs[i].Angle = subSModelVs[i].Angle + 360;
                        }

                        if (subSModelVs[i].CapsAngle < 0)
                        {
                            subSModelVs[i].CapsAngle = subSModelVs[i].CapsAngle + 360;
                        }

                        subSModelVs[i].Id = node.Name;
                        subSModelVs[i].FkxId = "Fkx" + node.Name;
                        //subSModelVs[i].FkxUnitId = "FkxUnitId" + UUIDUtil.Get32UUID();
                        subSModelVs[i].PierArrangeId = "PierArrange" + node.Name;
                        //subSModelVs[i].PierArrangeUnitId = "PierArrangeUnitId" + UUIDUtil.Get32UUID();
                        
                    }

                    trans.Commit();
                }
            }
            return subSModelVs;
        }

        public static List<SubSModelV> GetStartSubsModelVs(TreeNode node)
        {
            List<SubSModelV> subSModelVs1 = new List<SubSModelV>();

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            #region 数据
            TreeNode bridgeNode = node.Parent;
            TreeNode roadNode = bridgeNode.Parent;
            var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == roadNode.Name).FirstOrDefault();
            var bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == bridgeNode.Name).FirstOrDefault();
            var roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault(); ;
            var subSModelVs = bridgeModelV.subSModelVs;

            string roadLayer = roadSourceModelV.Designation + roadSourceModelV.Id;
            string fkxLayer = roadSourceModelV.Designation + "分跨线标记";

            subSModelVs.Clear();

            ///清除下部结构List信息
            //for (int i = 0; i < subSModelVs.Count; i++)
            //{
            //    subSModelVs[i].Id = node.Name;
            //    subSModelVs[i].distance = 0;
            //    subSModelVs[i].PierNum = "";
            //    subSModelVs[i].Mark = 0;
            //    subSModelVs[i].Angle = 0;
            //}
            #endregion

            using (DocumentLock docLock = doc.LockDocument())
            {
                using (Transaction trans = doc.TransactionManager.StartTransaction())
                {
                    List<Entity> ents = db.Ex_GetLayerAllEntitys(roadLayer);
                    //路线曲线
                    List<Curve> curves = new List<Curve>();

                    for (int i = 0; i < ents.Count; i++)
                    {
                        curves.Add((Curve)ents[i]);
                    }
                    ents.Clear();

                    ents = db.Ex_GetLayerAllEntitys(fkxLayer);
                    //分跨线块
                    List<BlockReference> brs = new List<BlockReference>();
                    for (int i = 0; i < ents.Count; i++)
                    {
                        brs.Add((BlockReference)ents[i]);
                    }

                    for (int i = 0; i < brs.Count; i++)
                    {
                        Point3d valuePM = brs[i].Position;
                        var curvesTemp = (from aa in curves orderby ((aa.GetClosestPointTo(valuePM, false)).DistanceTo(valuePM)) ascending select aa).ToList<Curve>();
                        //离分跨线块最近的路线
                        Curve curveTemp = curvesTemp[0];


                        if ((curveTemp.GetClosestPointTo(valuePM, false)).DistanceTo(valuePM) < 2)
                        {
                            TypedValueList valuesTemp = curveTemp.Ex_GetXData(curveTemp.Layer);
                            string strTemp = Convert.ToString(valuesTemp[2].Value);
                            strTemp = strTemp.Substring(1, strTemp.Length - 2);
                            string[] strsTemp = strTemp.Split(',');
                            double zhMinTemp = Convert.ToDouble(strsTemp[0]);
                            double zhMaxTemp = Convert.ToDouble(strsTemp[1]);

                            double lengthTemp = curveTemp.GetDistAtPoint(curveTemp.GetClosestPointTo(valuePM, false));

                            double zh = Math.Round(zhMinTemp + lengthTemp, 2);
                            //Vector3d curveNormalVector = curveTemp.getCurveNormalVector(curveTemp.GetClosestPointTo(valuePM, false));
                            Vector3d curveTanVector = curveTemp.Ex_GetTangentVector(curveTemp.GetClosestPointTo(valuePM, false));
                            double angelTemp = curveTanVector.GetAngleTo(new Vector3d(1, 0, 0));
                            if (curveTanVector.Y < 0)
                            {
                                angelTemp = Math.PI * 2 - angelTemp;
                            }

                            double rotation = Math.Round((brs[i].Rotation - angelTemp - Math.PI / 2) * 180 / Math.PI, 3);
                            if (rotation < 0)
                            {
                                rotation = rotation + 360;
                            }
                            if (rotation > 360)
                            {
                                rotation = rotation - 360;
                            }
                            #region 不用的代码
                            //Vector3d qVector = curveTemp.Ex_GetTangentVector(curveTemp.GetClosestPointTo(valuePM, false));

                            //Vector3d vz = qVector.RotateBy(-Math.PI / 2 + rotation * Math.PI / 180, Vector3d.ZAxis);
                            //Vector3d vf = qVector.RotateBy(Math.PI / 2 + rotation * Math.PI / 180, Vector3d.ZAxis);


                            //Line lineZ = new Line(new Point3d(valuePM.X + vz.X * 100 / vz.Length, valuePM.Y + vz.Y * 100 / vz.Length, 0), valuePM);
                            //Line lineF = new Line(new Point3d(valuePM.X + vf.X * 100 / vf.Length, valuePM.Y + vf.Y * 100 / vf.Length, 0), valuePM);

                            //TypedValue[] values = new TypedValue[1];
                            //values.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
                            //SelectionFilter filter = new SelectionFilter(values);

                            //PromptSelectionResult psr = ed.SelectAll(filter);

                            //SelectionSet sSet = psr.Value;

                            //List<BlockReference> pierBlocks = new List<BlockReference>();
                            //List<BlockReference> pileBlocks = new List<BlockReference>();

                            //using (DocumentLock docLock1 = doc.LockDocument())
                            //{
                            //    using (Transaction trans1 = doc.TransactionManager.StartTransaction())
                            //    {

                            //        foreach (ObjectId id in sSet.GetObjectIds())
                            //        {
                            //            BlockReference br = id.GetObject(OpenMode.ForRead) as BlockReference;

                            //            if (br.Position.DistanceTo(lineZ.GetClosestPointTo(br.Position, false)) < 0.01 ||
                            //                br.Position.DistanceTo(lineF.GetClosestPointTo(br.Position, false)) < 0.01)
                            //            {
                            //                if (br.Name.Contains("pier"))
                            //                {
                            //                    TypedValueList valuesTemp1 = br.Ex_GetXData("墩位标记");
                            //                    string strTemp1 = Convert.ToString(valuesTemp1[2].Value);

                            //                    if (strTemp1 == "辅墩" || strTemp1 == "主墩")
                            //                    {
                            //                        pierBlocks.Add(br);
                            //                    }
                            //                }

                            //                if (br.Name.Contains("foundation"))
                            //                {
                            //                    TypedValueList valuesTemp1 = br.Ex_GetXData("墩位标记");
                            //                    string strTemp1 = Convert.ToString(valuesTemp1[2].Value);

                            //                    if (strTemp1 == "辅墩" || strTemp1 == "主墩")
                            //                    {
                            //                        pileBlocks.Add(br);
                            //                    }
                            //                }
                            //            }



                            //        }
                            //        trans1.Commit();
                            //    }
                            //}

                            //for (int j = 0; j < pierBlocks.Count; j++)
                            //{

                            //    bool isMainPier = true;

                            //    if (pierBlocks[j].Name.Contains("pier"))
                            //    {
                            //        TypedValueList valuesTemp1 = pierBlocks[j].Ex_GetXData("墩位标记");
                            //        string strTemp1 = Convert.ToString(valuesTemp1[2].Value);

                            //        if (strTemp1 == "辅墩")
                            //        {
                            //            isMainPier = true;
                            //        }
                            //        else
                            //        {
                            //            isMainPier = false;
                            //        }
                            //    }

                            //    double dis1 = pierBlocks[j].Position.DistanceTo(lineZ.GetClosestPointTo(pierBlocks[j].Position, false));
                            //    double dis2 = pierBlocks[j].Position.DistanceTo(lineF.GetClosestPointTo(pierBlocks[j].Position, false));

                            //    if (dis1 <= dis2)
                            //    {
                            //        double dis = pierBlocks[j].Position.DistanceTo(valuePM);
                            //        Vector3d cV = curveTemp.Ex_GetTangentVector(curveTemp.GetClosestPointTo(pierBlocks[j].Position, false));
                            //        double angleA = pierBlocks[j].Rotation;
                            //        double angleB = brs[i].Rotation;

                            //        PierSourceModelV pierSourceModelV = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == pierBlocks[j].Name).FirstOrDefault();

                            //        SubSModelV subSModelV = new SubSModelV();

                            //        subSModelV.Mark = Math.Round(zh, 3);
                            //        subSModelV.Angle = Math.Round(rotation, 3);
                            //        subSModelV.PierName = pierBlocks[j].Name;
                            //        subSModelV.FoundationName = pileBlocks[j].Name;
                            //        subSModelV.IsTransitionalPier = pierSourceModelV.IsTransitionalPier;
                            //        subSModelV.PierEccentricDistance = Math.Round(dis, 3);
                            //        subSModelV.PierAngle = Math.Round((angleA - angleB - Math.PI) * 180 / Math.PI, 3);
                            //        subSModelV.IsAuxiliaryPier = isMainPier ? 1 : 0;
                            //        subSModelVs.Add(subSModelV);
                            //    }

                            //    if (dis1 > dis2)
                            //    {

                            //        double dis = -pierBlocks[j].Position.DistanceTo(valuePM);
                            //        Vector3d cV = curveTemp.Ex_GetTangentVector(curveTemp.GetClosestPointTo(pierBlocks[j].Position, false));
                            //        double angleA = pierBlocks[j].Rotation;
                            //        double angleB = brs[i].Rotation;

                            //        PierSourceModelV pierSourceModelV = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == pierBlocks[j].Name).FirstOrDefault();

                            //        SubSModelV subSModelV = new SubSModelV();

                            //        subSModelV.Mark = Math.Round(zh, 3);
                            //        subSModelV.Angle = Math.Round(rotation, 3);
                            //        subSModelV.PierName = pierBlocks[j].Name;
                            //        subSModelV.FoundationName = pileBlocks[j].Name;
                            //        subSModelV.IsTransitionalPier = pierSourceModelV.IsTransitionalPier;
                            //        subSModelV.PierEccentricDistance = Math.Round(dis, 3);
                            //        subSModelV.PierAngle = Math.Round((angleA - angleB - Math.PI) * 180 / Math.PI, 3);
                            //        subSModelV.IsAuxiliaryPier = isMainPier ? 1 : 0;
                            //        subSModelVs.Add(subSModelV);
                            //    }
                            //}
                            #endregion



                            SubSModelV subSModelV = new SubSModelV();

                            subSModelV.Mark = Math.Round(zh, 3);
                            subSModelV.Angle = Math.Round(rotation, 3);
                            //subSModelV.PierName = pierBlocks[j].Name;
                            subSModelV.PierName = GlobalData.sourceModelV.pierSourceModelVs.First().Id;
                            //subSModelV.FoundationName = pileBlocks[j].Name;
                            subSModelV.FoundationName = GlobalData.sourceModelV.foundationSourceModelVs.First().Id;
                            //subSModelV.IsTransitionalPier = pierSourceModelV.IsTransitionalPier;
                            subSModelV.IsTransitionalPier = GlobalData.sourceModelV.pierSourceModelVs.First().IsTransitionalPier;
                            subSModelV.PierEccentricDistance = 0;
                            subSModelV.PierAngle = 0;
                            subSModelV.IsAuxiliaryPier = 0;
                            subSModelVs.Add(subSModelV);

                        }
                    }




                    subSModelVs.Distinct();

                    subSModelVs = (from aa in subSModelVs
                                   where (aa.Mark > bridgeModelV.StartMark && aa.Mark < bridgeModelV.EndMark)
                                   orderby aa.Mark ascending
                                   select aa).ToList();

                    //subSModelVs = subSModelVs.OrderBy(aa => aa.Mark).ThenBy(aa => aa.IsAuxiliaryPier).ToList();

                    ///墩号起始号 用来标记墩号的增加
                    int dh = 1;
                    int affdh = 0;
                    for (int i = 0; i < subSModelVs.Count; i++)
                    {

                        if (i == 0)
                        {
                            subSModelVs[i].distance = 0;
                            subSModelVs[i].PierNum = getDh(dh) + "_0";
                        }
                        else
                        {
                            if (subSModelVs[i].Mark == subSModelVs[i - 1].Mark)
                            {
                                affdh = affdh + 1;
                                subSModelVs[i].PierNum = subSModelVs[i - 1].PierNum.Split('_')[0] + "_" + affdh;
                                subSModelVs[i].distance = subSModelVs[i].Mark - subSModelVs[i - 1].Mark;
                            }
                            else
                            {
                                affdh = 0;
                                dh = dh + 1;
                                subSModelVs[i].PierNum = getDh(dh) + "_0";
                                subSModelVs[i].distance = subSModelVs[i].Mark - subSModelVs[i - 1].Mark;
                            }
                        }
                        subSModelVs[i].PierNumber = subSModelVs[i].PierNum;

                        if (subSModelVs[i].PierAngle < 0)
                        {
                            subSModelVs[i].PierAngle = subSModelVs[i].PierAngle + 360;
                        }

                        if (subSModelVs[i].Angle < 0)
                        {
                            subSModelVs[i].Angle = subSModelVs[i].Angle + 360;
                        }

                        if (subSModelVs[i].CapsAngle < 0)
                        {
                            subSModelVs[i].CapsAngle = subSModelVs[i].CapsAngle + 360;
                        }

                        subSModelVs[i].Id = node.Name;
                        subSModelVs[i].FkxId = "Fkx" + node.Name;
                        subSModelVs[i].FkxUnitId = "FkxUnitId" + UUIDUtil.Get32UUID();
                        subSModelVs[i].PierArrangeId = "PierArrange" + node.Name;
                        subSModelVs[i].PierArrangeUnitId = "PierArrangeUnitId" + UUIDUtil.Get32UUID();
                        subSModelVs[i].IsTurn = "否";

                    }

                    trans.Commit();
                }
            }

            DrawingInfoGetOrSet.SetSubSModelVs(subSModelVs, node);

            return subSModelVs;
        }


        public static void GetHoleInfo(List<SubSModelV> subSModelVs,int rowIndex)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;
            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();

            Polyline polyline = new Polyline();
            ObjectId polyId;

            for (int i = rowIndex; i < subSModelVs.Count; i++)
            {
                using (DocumentLock docLock = doc.LockDocument())
                {
                    using (DBTrans dBTrans = new DBTrans())
                    {
                        SubSModelV subSModelV = subSModelVs[i];
                        if (subSModelV.Id == null)
                        {
                            continue;
                        }
                        //获取对应的墩块
                        string pierId = subSModelV.PierName;
                        string foundId = subSModelV.FoundationName;
                        string pierArrangeId = subSModelV.PierArrangeUnitId;
                        //获取所有块
                        TypedValue[] values = new TypedValue[1];
                        values.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
                        SelectionFilter filter = new SelectionFilter(values);
                        PromptSelectionResult psr = ed.SelectAll(filter);
                        SelectionSet sSet = psr.Value;

                        BlockReference pierBr = null;
                        BlockReference foundBr = null;

                        foreach (ObjectId id in sSet.GetObjectIds())
                        {
                            BlockReference br = id.GetObject(OpenMode.ForRead) as BlockReference;
                            TypedValueList typedValues = br.Ex_GetXData("墩位标记");
                            if (br.Name.Contains("pier"))
                            {
                                if (pierArrangeId == Convert.ToString(typedValues[6].Value))
                                {
                                    pierBr = br;
                                }
                            }

                            if (br.Name.Contains("foundation"))
                            {
                                if (pierArrangeId == Convert.ToString(typedValues[6].Value))
                                {
                                    foundBr = br;
                                }
                            }
                        }

                        Point3d insertP = pierBr.Position;

                        polyline = new Polyline();

                        polyline.AddVertexAt(0, new Point2d(insertP.X - 5, insertP.Y), 1, 2, 2);
                        polyline.AddVertexAt(1, new Point2d(insertP.X + 5, insertP.Y), 1, 2, 2);
                        polyline.AddVertexAt(2, new Point2d(insertP.X - 5, insertP.Y), 1, 2, 2);
                        polyline.ColorIndex = 1;
                        polyId = db.Ex_AddToModelSpace(polyline);

                        if (pierBr != null)
                        {
                            pierBr.Highlight();
                        }

                        if (foundBr != null)
                        {
                            foundBr.Highlight();
                        }
                    }
                }
                using (DocumentLock docLock1 = doc.LockDocument())
                {
                    using (DBTrans dbtrans = new DBTrans())
                    {
                        PromptSelectionOptions pso = new PromptSelectionOptions();
                        pso.MessageForAdding = "请选择勘探孔对应的块";

                        TypedValue[] values = new TypedValue[1];
                        values.SetValue(new TypedValue(1001, "ExplorationHole"), 0);
                        SelectionFilter filter = new SelectionFilter(values);

                        PromptSelectionResult psr = ed.GetSelection(pso, filter);
                        if (psr.Status != PromptStatus.OK)
                        {
                            if (polyId != null)
                            {
                                polyId.Ex_Erase();
                            }
                            return;
                        }
                        SelectionSet sSet = psr.Value;

                        string result = "";
                        foreach (var id in sSet.GetObjectIds())
                        {
                            TypedValueList list = id.Ex_GetXData("ExplorationHole");
                            string unitId = (string)list[1].Value;
                            result = unitId;
                            //result = GlobalData.sourceModelV.explorSourceModelV.explorationSourceModelVs.Where(aa => aa.UnitId == unitId).First().Num;
                        }
                        var holeUnitId = GlobalData.sourceModelV.explorSourceModelV.explorationSourceModelVs.Where(aa => aa.UnitId == result).First().UnitId;

                        var holeNum = GlobalData.sourceModelV.explorSourceModelV.explorationSourceModelVs.Where(aa => aa.UnitId == result).First().Num;
                        var strs = GlobalData.sourceModelV.explorSourceModelV.soiLayerSourceModelVs.Where(aa => aa.HoleNum == holeNum).Select(aa => aa.SoilLayerNum).ToList();

                        SelectSoilLayer selectSoilLayer = new SelectSoilLayer(strs);
                        selectSoilLayer.StartPosition = FormStartPosition.CenterScreen;
                        selectSoilLayer.ShowDialog();

                        var s1 = selectSoilLayer.s1;
                        var s2 = selectSoilLayer.s2;
                        var s3 = selectSoilLayer.s3;

                        subSModelVs[i].HoleNum = holeUnitId;
                        subSModelVs[i].PileFoundationBearingLayerNumber = s1;
                        subSModelVs[i].EnlargeBase1stHoldingLayerNumber = s2;
                        subSModelVs[i].EnlargeBase2ndHoldingLayerNumber = s3;

                        polyId.Ex_Erase();
                    }
                }
            }
        }
        
        /// <summary>
        /// 获取钻孔编号
        /// </summary>
        /// <returns></returns>
        public static string[] GetHoleInfo()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            Autodesk.AutoCAD.Internal.Utils.SetFocusToDwgView();
            using (DBTrans dbtrans = new DBTrans())
            {
                PromptSelectionOptions pso = new PromptSelectionOptions();
                pso.MessageForAdding = "请选择勘探孔对应的块";

                TypedValue[] values = new TypedValue[1];
                values.SetValue(new TypedValue(1001, "ExplorationHole"), 0);
                SelectionFilter filter = new SelectionFilter(values);

                PromptSelectionResult psr = ed.GetSelection(pso, filter);
                if (psr.Status != PromptStatus.OK)
                {
                    return null;
                }
                SelectionSet sSet = psr.Value;

                string result = "";
                foreach (var id in sSet.GetObjectIds())
                {
                    TypedValueList list = id.Ex_GetXData("ExplorationHole");
                    string unitId = (string)list[1].Value;
                    result = unitId;
                    //result = GlobalData.sourceModelV.explorSourceModelV.explorationSourceModelVs.Where(aa => aa.UnitId == unitId).First().Num;
                }
                var holeNum = GlobalData.sourceModelV.explorSourceModelV.explorationSourceModelVs.Where(aa => aa.UnitId == result).First().Num;
                var strs = GlobalData.sourceModelV.explorSourceModelV.soiLayerSourceModelVs.Where(aa => aa.HoleNum == holeNum).Select(aa => aa.SoilLayerNum).ToList();

                SelectSoilLayer selectSoilLayer = new SelectSoilLayer(strs);
                selectSoilLayer.ShowDialog();

                var s1 = selectSoilLayer.s1;
                var s2 = selectSoilLayer.s2;
                var s3 = selectSoilLayer.s3;

                return new string[] { result, s1, s2, s3};
            }
            
        }

        /// <summary>
        /// 更新图面信息
        /// </summary>
        /// <param name="subSModelVs">下部结构数据</param>
        /// <param name="node">下部结构节点</param>
        public static void SetSubSModelVs(List<SubSModelV> subSModelVs,TreeNode node)
        {

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Editor ed = doc.Editor;

            TreeNode bridgeNode = node.Parent;
            TreeNode roadNode = bridgeNode.Parent;
            var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == roadNode.Name).FirstOrDefault();
            var bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == bridgeNode.Name).FirstOrDefault();
            var roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault(); ;
            var pqxSourceModelVs = roadSourceModelV.pqxSourceModelVs;

            using (DocumentLock docLock = doc.LockDocument())
            {
                using (DBTrans dBTrans = new DBTrans())
                {
                    //获取墩块
                    List<Entity> ents = db.Ex_GetLayerAllEntitys("墩位标记" + roadSourceModelV.Id);
                    //获取分跨线
                    List<Entity> ents1 = db.Ex_GetLayerAllEntitys(roadSourceModelV.Designation + "分跨线标记");

                    //获取道路中心线
                    List<Entity> entsTemp = db.Ex_GetLayerAllEntitys(roadSourceModelV.Designation + roadSourceModelV.Id);

                    if (entsTemp.Count == 0)
                    {
                        doc.DrawPqx(roadSourceModelV);
                    }
                    entsTemp.Clear();
                    entsTemp = db.Ex_GetLayerAllEntitys(roadSourceModelV.Designation + roadSourceModelV.Id);
                    List<Curve> curves = new List<Curve>();
                    foreach (var item in entsTemp)
                    {
                        curves.Add((Curve)item);
                    }

                    for (int i = 0; i < ents.Count; i++)
                    {
                        ents[i].ObjectId.Ex_Erase();
                    }

                    for (int i = 0; i < ents1.Count; i++)
                    {
                        ents1[i].ObjectId.Ex_Erase();
                    }

                    for (int i = 0; i < subSModelVs.Count; i++)
                    {
                        //1.根据桩号确定法线方向
                        SubSModelV subSModelV = subSModelVs[i];
                        double mark = subSModelV.Mark;

                        int curvesI = 0;
                        double zhMin = 0;
                        double zhMax = 0;
                        for (int j = 0; j < curves.Count; j++)
                        {
                            TypedValueList values = curves[j].Ex_GetXData(curves[j].Layer);
                            string str = Convert.ToString(values[2].Value);
                            str = str.Substring(1, str.Length - 2);
                            string[] strs = str.Split(',');
                            zhMin = Convert.ToDouble(strs[0]);
                            zhMax = Convert.ToDouble(strs[1]);

                            if (mark > zhMin && mark < zhMax)
                            {
                                curvesI = j;
                                break;
                            }
                        }

                        //切线斜率
                        Point3d insertP = curves[curvesI].GetPointAtDist(mark - zhMin);
                        Vector3d vector = curves[curvesI].Ex_GetTangentVector(insertP);
                        
                        //获取切线角度和法线角度
                        double angle = vector.GetAngleTo(new Vector3d(1, 0, 0));
                        if (vector.Y < 0)
                        {
                            angle = 2 * Math.PI - angle;
                        }
                        double normalAngle = angle + Math.PI / 2;
                        //根据法线角度计算得出分跨线的旋转角
                        double fkxAngle = normalAngle + subSModelV.Angle * Math.PI / 180;

                        //2.法线方向和分跨线斜交角绘制分跨线（注意配置XDATA数据）
                        if (subSModelV.IsAuxiliaryPier == 0)
                        {
                            ObjectId blockId = doc.EX_AddBlockToModelSpace("分跨线标记", insertP, 1,
                                        fkxAngle, roadSourceModelV.Designation + "分跨线标记");
                            TypedValueList list = new TypedValueList();
                            list.Add(new TypedValue(1000, roadSourceModelV.Designation + roadSourceModelV.Id));
                            list.Add(new TypedValue(1000, roadSourceModelV.Designation));
                            list.Add(new TypedValue(1000, roadSourceModelV.Id));
                            list.Add(new TypedValue(1000, subSModelV.FkxUnitId));

                            blockId.Ex_AddXData("分跨线标记", list);
                        }
                        



                        

                        //3.根据分跨桩号 分跨线斜交角 墩的偏心距 墩的斜交角等数据绘制墩（注意配置XDATA数据 和 区分主墩辅墩）

                        Point3d pierInsertP = insertP + vector.RotateBy(-Math.PI / 2 + subSModelV.Angle * Math.PI / 180, Vector3d.ZAxis)
                            * subSModelV.PierEccentricDistance / vector.Length;
                        double pierAngle = fkxAngle + subSModelV.PierAngle * Math.PI / 180 + Math.PI;

                        ObjectId id;
                        ObjectId id1;
                        id = doc.EX_AddBlockToModelSpace(subSModelV.PierName, pierInsertP,
                                1, pierAngle, "墩位标记" + roadSourceModelV.Id);

                        id1 = doc.EX_AddBlockToModelSpace(subSModelV.FoundationName, pierInsertP,
                                1, pierAngle, "墩位标记" + roadSourceModelV.Id);

                        TypedValueList list1 = new TypedValueList();
                        list1.Add(new TypedValue(1000, roadSourceModelV.Designation + roadSourceModelV.Id));
                        if (subSModelV.IsAuxiliaryPier == 0)
                        {
                            list1.Add(new TypedValue(1000, "主墩"));
                        }
                        else
                        {
                            list1.Add(new TypedValue(1000, "辅墩"));
                        }

                        list1.Add(new TypedValue(1000, roadSourceModelV.Designation));
                        list1.Add(new TypedValue(1000, roadSourceModelV.Id));
                        list1.Add(new TypedValue(1000, bridgeModelV.Id));
                        list1.Add(new TypedValue(1000, subSModelV.PierArrangeUnitId));

                        id.Ex_AddXData("墩位标记", list1);
                        id1.Ex_AddXData("墩位标记", list1);

                    }
                }

            }


        }



        private static string getDh(int num)
        {
            if (num < 10)
            {
                return "0" + num;
            }
            else
            {
                return "" + num;
            }
        }

    }
}
