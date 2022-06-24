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
using Autodesk.AutoCAD.GraphicsInterface;

//CAD开发基础库
using BaseLibrary.Runtime;
using BaseLibrary.ResultData;
using BaseLibrary.ExtensionMethod;
using BaseLibrary.Filter;
using Bssc.Models.ModelsV.SourceModelsV;
using Bssc.Control.Tools;

namespace Bssc.Control.CadControl
{
    public class ArrangeDivideSpanLineOperation
    {

        private List<Curve> curves;
        private RoadSourceModelV roadData;

        public ArrangeDivideSpanLineOperation(List<Curve> curves, RoadSourceModelV roadData)
        {
            this.roadData = roadData;
            this.curves = curves;
        }

        public bool TSTSnapEntityJig()
        {

            bool isFlag = true;

            Document doc = Application.DocumentManager.MdiActiveDocument;
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            Database db = HostApplicationServices.WorkingDatabase;


            while (isFlag)
            {
                DocumentLock Lock = Application.DocumentManager.GetDocument(db).LockDocument(); //非模态模式，修改文件前进行上锁操作
                using (Transaction trans = doc.TransactionManager.StartTransaction())
                {
                    BlockTable bt = (BlockTable)trans.GetObject(db.BlockTableId, OpenMode.ForWrite);
                    BlockTableRecord btr = (BlockTableRecord)trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
                    //string blockName = "TST";
                    //if (bt.Has(blockName))
                    //{
                    //第一个状态，实现
                    ed.WriteMessage("\n请单击相应位置进行布墩");
                    SnapEntityJigForDivideSpanLine blockJig = new SnapEntityJigForDivideSpanLine(new Line(), curves);
                    PromptResult resJig = ed.Drag(blockJig);
                    if (resJig.Status == PromptStatus.Cancel)
                    {
                        blockJig.StopSnap();
                        isFlag = false;
                    }
                    if (resJig.Status == PromptStatus.OK)
                    {
                        blockJig.StopSnap();

                        ObjectId blockId;

                        double angle = blockJig.m_Direction.GetAngleTo(new Vector3d(1, 0, 0));

                        if (blockJig.m_Direction.Y < 0)
                        {
                            angle = 2 * Math.PI - angle;
                        }


                        blockId = doc.EX_AddBlockToModelSpace("分跨线标记", blockJig.m_Position, 1, angle, roadData.Designation + "分跨线标记");

                        TypedValueList list = new TypedValueList();
                        list.Add(new TypedValue(1000, roadData.Designation + roadData.Id));
                        list.Add(new TypedValue(1000, roadData.Designation));
                        list.Add(new TypedValue(1000, roadData.Id));
                        list.Add(new TypedValue(1000, "FkxUnitId" + UUIDUtil.Get32UUID()));

                        blockId.Ex_AddXData("分跨线标记", list);


                        isFlag = true;
                    }
                    trans.Commit();
                }
                Lock.Dispose();
            }

            return isFlag;
        }

    }
}
