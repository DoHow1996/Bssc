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
using Bssc.Models.ModelsV.ProjectModelsV;
using Bssc.Control.Tools;

namespace Bssc.Control.CadControl
{
    public class ArrangePierAndFoundation
    {

        Point3d p;
        double rotation;
        RoadSourceModelV roadSourceModelV;
        BridgeModelV bridgeModelV;
        PierSourceModelV pierSourceModelV;
        FoundationSourceModelV foundationSourceModelV;
        string blockName;
        string blockName1;
        bool isMain;

        public ArrangePierAndFoundation(Point3d p, double rotation, RoadSourceModelV roadSourceModelV, BridgeModelV bridgeModelV,
            PierSourceModelV pierSourceModelV, FoundationSourceModelV foundationSourceModelV,bool isMain)
        {
            this.p = p;
            this.rotation = rotation + Math.PI;
            this.roadSourceModelV = roadSourceModelV;
            this.foundationSourceModelV = foundationSourceModelV;
            this.pierSourceModelV = pierSourceModelV;
            this.blockName = pierSourceModelV.Id;
            this.blockName1 = foundationSourceModelV.Id;
            this.bridgeModelV = bridgeModelV;
            this.isMain = isMain;
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

                    ed.WriteMessage("\n请单击相应位置进行布墩");

                    BlockReference br1 = db.Ex_GetBlockReference(blockName);
                    BlockReference br2 = db.Ex_GetBlockReference(blockName1);
                    //SnapEntityJigForBudun blockJig = new SnapEntityJigForBudun(br,p,rotation);
                    SnapEntityJigForArrangePier blockJig = new SnapEntityJigForArrangePier(br1, br2, p, rotation);
                    PromptResult resJig = ed.Drag(blockJig);

                    if (resJig.Status == PromptStatus.Cancel)
                    {

                        isFlag = false;
                    }

                    if (resJig.Status == PromptStatus.OK)
                    {
                        ObjectId id;
                        ObjectId id1;
                        id = doc.EX_AddBlockToModelSpace(blockName, blockJig.m_position,
                                1, rotation, "墩位标记" + roadSourceModelV.Id);

                        id1 = doc.EX_AddBlockToModelSpace(blockName1, blockJig.m_position,
                                1, rotation, "墩位标记" + roadSourceModelV.Id);



                        TypedValueList list = new TypedValueList();
                        list.Add(new TypedValue(1000, roadSourceModelV.Designation + roadSourceModelV.Id));
                        if (isMain)
                        {
                            list.Add(new TypedValue(1000, "主墩"));
                        }
                        else
                        {
                            list.Add(new TypedValue(1000, "辅墩"));
                        }
                        
                        list.Add(new TypedValue(1000, roadSourceModelV.Designation));
                        list.Add(new TypedValue(1000, roadSourceModelV.Id));
                        list.Add(new TypedValue(1000, bridgeModelV.Id));
                        list.Add(new TypedValue(1000, "PierArrangeUnitId" + UUIDUtil.Get32UUID()));

                        id.Ex_AddXData("墩位标记", list);
                        id1.Ex_AddXData("墩位标记", list);


                        isFlag = true;

                        Application.UpdateScreen();
                    }

                    trans.Commit();
                }
                Lock.Dispose();

            }

            return true;

        }

    }
}
