using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using Bssc.Models.ModelsV.SourceModelsV;
using Bssc.Control.Tools;

namespace Bssc.Control.CadControl
{
    public class ArrangeDivideSpanLine
    {

        public string roadName;
        public double qszh;
        public double zzzh;

        private Document doc;
        private Database db;
        private Editor ed;

        private List<Curve> curves;

        private RoadSourceModelV roadSourceModelV;

        public ArrangeDivideSpanLine(List<Curve> curves, RoadSourceModelV roadSourceModelV)
        {
            this.doc = Application.DocumentManager.MdiActiveDocument;
            this.db = doc.Database;
            this.ed = doc.Editor;
            this.roadSourceModelV = roadSourceModelV;

            this.curves = curves;

            List<double> roadZh = new List<double>();
            for (int i = 0; i < curves.Count; i++)
            {
                TypedValueList values = curves[i].Ex_GetXData(curves[i].Layer);
                string str = Convert.ToString(values[2].Value);
                str = str.Substring(1, str.Length - 2);
                string[] strs = str.Split(',');
                roadZh.Add(Convert.ToDouble(strs[0]));
                roadZh.Add(Convert.ToDouble(strs[1]));
            }

            qszh = roadZh.Min();
            zzzh = roadZh.Max();


        }

        Polyline poly = new Polyline();


        //[CommandMethod("RT")]
        public void addPier()
        {



            bool isFalg = true;


            while (isFalg)
            {
                PromptKeywordOptions pko = new PromptKeywordOptions("\n请输入布墩方式[单击布墩(W)/桩号输入(S)/间距输入(D)/退出(ESC)]");
                pko.Keywords.Add("W");
                pko.Keywords.Add("S");
                pko.Keywords.Add("D");
                pko.Keywords.Default = "W";
                pko.AllowNone = true;
                pko.AppendKeywordsToMessage = false;
                PromptResult pr = ed.GetKeywords(pko);
                if (pr.Status == PromptStatus.OK)
                {

                    switch (pr.StringResult)
                    {
                        case "W":
                            ed.WriteMessage("\n请单击相应位置进行布墩");
                            ArrangeDivideSpanLineOperation bukuaW = new ArrangeDivideSpanLineOperation(curves, roadSourceModelV);
                            bukuaW.TSTSnapEntityJig();
                            break;

                        case "S":
                            ed.WriteMessage("\n请输入桩号进行布墩");
                            zhbd();
                            break;

                        case "D":
                            ed.WriteMessage("\n请输入间距进行(间距格式(20+3*50+20))布墩");
                            jlbd();
                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    isFalg = false;
                    return;
                }
            }
        }

        /// <summary>
        /// 桩号布墩
        /// </summary>
        private void zhbd()
        {
            bool isFalg = true;



            while (isFalg)
            {



            label: PromptDoubleOptions pdo = new PromptDoubleOptions("\n请输入桩号(" + qszh + "--" + zzzh + ")");
                PromptDoubleResult psr = ed.GetDouble(pdo);
                if (psr.Status == PromptStatus.OK)
                {
                    double zh = psr.Value;
                    if (zh > zzzh || zh < qszh)
                    {
                        ed.WriteMessage("\n桩号输入值错误，请重新输入");
                        goto label;
                    }
                    ///由DataTable计算出点的具体坐标，计算出该点法线方向


                    int curvesI = 0;
                    double zhMin = 0;
                    double zhMax = 0;
                    for (int i = 0; i < curves.Count; i++)
                    {
                        TypedValueList values = curves[i].Ex_GetXData(curves[i].Layer);
                        string str = Convert.ToString(values[2].Value);
                        str = str.Substring(1, str.Length - 2);
                        string[] strs = str.Split(',');
                        zhMin = Convert.ToDouble(strs[0]);
                        zhMax = Convert.ToDouble(strs[1]);

                        if (zh > zhMin && zh < zhMax)
                        {
                            curvesI = i;
                            break;
                        }
                    }

                    Point3d insetP = curves[curvesI].GetPointAtDist(zh - zhMin);
                    Vector3d vector = curves[curvesI].Ex_GetNormalVector(insetP);



                    using (DocumentLock docLock = doc.LockDocument())
                    {
                        using (Transaction trans = doc.TransactionManager.StartTransaction())
                        {

                            double angle = vector.GetAngleTo(new Vector3d(1, 0, 0));
                            if (vector.Y < 0)
                            {
                                angle = 2 * Math.PI - angle;
                            }

                            ObjectId blockId = doc.EX_AddBlockToModelSpace("分跨线标记", insetP, 1,
                                angle, roadSourceModelV.Designation + "分跨线标记");

                            TypedValueList list = new TypedValueList();
                            list.Add(new TypedValue(1000, roadSourceModelV.Designation + roadSourceModelV.Id));
                            list.Add(new TypedValue(1000, roadSourceModelV.Designation));
                            list.Add(new TypedValue(1000, roadSourceModelV.Id));
                            list.Add(new TypedValue(1000, "FkxUnitId" + UUIDUtil.Get32UUID()));

                            blockId.Ex_AddXData("分跨线标记", list);

                            trans.Commit();
                        }
                    }

                    isFalg = true;
                }
                else
                {
                    isFalg = false;
                    return;
                }
            }
        }

        /// <summary>
        /// 距离布墩
        /// </summary>
        private void jlbd()
        {
            bool isFlag = true;
            while (isFlag)
            {
                PromptKeywordOptions pko = new PromptKeywordOptions("\n请确定布墩参照起点[输入起始桩号(N)/点击已布墩(M)]");
                pko.Keywords.Add("N");
                pko.Keywords.Add("M");
                pko.Keywords.Default = "M";
                pko.AllowNone = true;
                pko.AppendKeywordsToMessage = false;

                #region 选择布墩点
                TypedValue[] values = new TypedValue[1];
                values.SetValue(new TypedValue((int)DxfCode.Start, "INSERT"), 0);
                SelectionFilter filter = new SelectionFilter(values);
                #endregion

                PromptResult pr = ed.GetKeywords(pko);

                double startZh = 0;

                if (pr.Status != PromptStatus.OK)
                {
                    isFlag = false;
                    return;
                }
                else
                {
                    string str = pr.StringResult;
                    switch (str)
                    {
                        case "M":
                            PromptSelectionOptions pso = new PromptSelectionOptions();
                            pso.MessageForAdding = "\n请选择布墩块";
                            PromptSelectionResult psrM = ed.GetSelection(pso, filter);
                            if (psrM.Status != PromptStatus.OK)
                            {
                                isFlag = false;
                                return;
                            }
                            SelectionSet ssetM = psrM.Value;
                            Point3d valuePM = new Point3d();
                            using (Transaction trans = db.TransactionManager.StartTransaction())
                            {
                                valuePM = ((BlockReference)ssetM.GetObjectIds()[0].GetObject(OpenMode.ForRead)).Position;
                            }

                            ///由该坐标点反算出对应的桩号
                            var curvesTemp = (from aa in curves orderby ((aa.GetClosestPointTo(valuePM, false)).DistanceTo(valuePM)) ascending select aa).ToList<Curve>();

                            Curve curveTemp = curvesTemp[0];

                            TypedValueList valuesTemp = curveTemp.Ex_GetXData(curveTemp.Layer);
                            string strTemp = Convert.ToString(valuesTemp[2].Value);
                            strTemp = strTemp.Substring(1, strTemp.Length - 2);
                            string[] strsTemp = strTemp.Split(',');
                            double zhMinTemp = Convert.ToDouble(strsTemp[0]);
                            double zhMaxTemp = Convert.ToDouble(strsTemp[1]);

                            double lengthTemp = curveTemp.GetDistAtPoint(curveTemp.GetClosestPointTo(valuePM, false));

                            startZh = zhMinTemp + lengthTemp;

                            break;

                        case "N":
                        label: PromptDoubleOptions pdo = new PromptDoubleOptions("\n请输入桩号(" + qszh + "--" + zzzh + ")");
                            PromptDoubleResult psr = ed.GetDouble(pdo);
                            if (psr.Status == PromptStatus.OK)
                            {
                                startZh = psr.Value;
                                if (startZh > zzzh || startZh < qszh)
                                {
                                    ed.WriteMessage("\n桩号输入值错误，请重新输入");
                                    goto label;
                                }
                                ///由DataTable计算出点的具体坐标，计算出该点法线方向
                                Point3d insetP = new Point3d();
                                isFlag = true;
                            }
                            else
                            {
                                isFlag = false;
                                return;
                            }
                            break;
                        default:
                            break;
                    }

                    PromptResult pr1 = ed.GetString("\n请输入布墩间距(支持a+n*b输入法)");
                    if (pr1.Status != PromptStatus.OK)
                    {
                        isFlag = false;
                        return;
                    }
                    string bdjj = pr1.StringResult;
                    List<double> numList = getDisList(bdjj);

                    double zhTemp = startZh;

                    for (int i = 0; i < numList.Count; i++)
                    {
                        zhTemp += numList[i];
                        ///布桩动作
                        ///计算布桩点的法线
                        ///绘制直线

                        int curvesI = 0;
                        double zhMinTemp = 0;
                        double zhMaxTemp = 0;
                        for (int j = 0; j < curves.Count; j++)
                        {
                            TypedValueList valuesTemp = curves[j].Ex_GetXData(curves[j].Layer);
                            string strTemp = Convert.ToString(valuesTemp[2].Value);
                            strTemp = strTemp.Substring(1, strTemp.Length - 2);
                            string[] strsTemp = strTemp.Split(',');
                            zhMinTemp = Convert.ToDouble(strsTemp[0]);
                            zhMaxTemp = Convert.ToDouble(strsTemp[1]);

                            if (zhTemp > zhMinTemp && zhTemp < zhMaxTemp)
                            {
                                curvesI = j;
                                break;
                            }
                        }


                        Point3d insetP = curves[curvesI].GetPointAtDist(zhTemp - zhMinTemp);
                        //Vector3d vector = curves[curvesI].Ex_GetPerpendicularVector(insetP);
                        //Vector3d vector = getCurveNormalVector(curves[curvesI],insetP);
                        Vector3d vector = curves[curvesI].Ex_GetNormalVector(insetP);

                        using (DocumentLock docLock = doc.LockDocument())
                        {
                            using (Transaction trans = doc.TransactionManager.StartTransaction())
                            {

                                double angle = vector.GetAngleTo(new Vector3d(1, 0, 0));
                                if (vector.Y < 0)
                                {
                                    angle = 2 * Math.PI - angle;
                                }

                                ObjectId blockId = doc.EX_AddBlockToModelSpace("分跨线标记", insetP, 1,
                                    angle, roadSourceModelV.Designation + "分跨线标记");


                                TypedValueList list = new TypedValueList();
                                list.Add(new TypedValue(1000, roadSourceModelV.Designation + roadSourceModelV.Id));
                                list.Add(new TypedValue(1000, roadSourceModelV.Designation));
                                list.Add(new TypedValue(1000, roadSourceModelV.Id));
                                list.Add(new TypedValue(1000, "FkxUnitId" + UUIDUtil.Get32UUID()));

                                blockId.Ex_AddXData("分跨线标记", list);

                                trans.Commit();
                            }
                        }


                    }
                }
            }

        }

        private List<double> getDisList(string str)
        {
            string[] strs;
            if (str.Contains('+'))
            {
                strs = str.Split('+');
            }
            else
            {
                strs = new string[] { str };
            }

            List<double> list = new List<double>();
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i].Contains('*'))
                {
                    string[] strs1 = strs[i].Split('*');
                    double aa1 = Convert.ToDouble(strs1[0]);
                    double aa2 = Convert.ToDouble(strs1[1]);
                    for (int j = 0; j < aa1; j++)
                    {
                        list.Add(aa2);
                    }
                }
                else
                {

                    list.Add(Convert.ToDouble(strs[i]));
                }
            }
            return list;
        }


    }
}
