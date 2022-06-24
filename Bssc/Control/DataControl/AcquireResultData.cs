using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using BaseLibrary.ExtensionMethod;
using BaseLibrary.Runtime;
using Bssc.Control.Tools;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.ProjectModelsV;
using Bssc.Models.ModelsV.ResultModelsV;
using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.DataControl
{
    public class AcquireResultData
    {
        /// <summary>
        /// 保留小数位
        /// </summary>
        const int RD = 3;


        /// <summary>
        /// 桥梁的节点
        /// </summary>
        TreeNode node;

        public BridgeModelV bridgeModelV;
        public RoadModelV roadModelV;
        public SourceModelV sourceModelV;

        public List<SuperSResultView> _superSResultViews;
        public List<SupportResultView> _supportResultViews;
        public List<SubSResultView> _subSResultViews;
        public List<SupportResultDataView> _supportResultDataViews;
        public List<SupportNumView> _supportNumViews;
        public List<SdsSteelNumView> _sdsSteelNumViews;
        public List<XdsSteelNumView> _xdsSteelNumViews;
        public List<PilePositionView> _pilePositionViews;
        public List<PierParmView> _pierParmViews;


        public AcquireResultData(TreeNode node)
        {
            this.node = node;

            TreeNode roadNode = node.Parent;
            roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == roadNode.Name).FirstOrDefault();
            bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();
            var aatemp = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault();
            sourceModelV = GlobalData.sourceModelV;

            _superSResultViews = GetSuperSResultViews();
            _subSResultViews = GetSubSResultViews();
            _supportResultViews = GetSupportResultViews();
            _supportResultDataViews = GetSupportResultDataViews();
            _supportNumViews = GetSupportNumViews();
            _sdsSteelNumViews = GetSdsSteelNumViews();
            _xdsSteelNumViews = GetXdsSteelNumViews();
            _pilePositionViews = GetPilePositionViews();
            _pierParmViews = GetPierParmViews();
        }

        /// <summary>
        /// 获取上部结构的视图结果
        /// </summary>
        /// <returns></returns>
        private List<SuperSResultView> GetSuperSResultViews()
        {
            List<SuperSResultView> superSResultViews = new List<SuperSResultView>();

            List<SuperSModelV> superSModels = bridgeModelV.superSModelVs;

            for (int i = 0; i < superSModels.Count; i++)
            {
                BeamSourceModelV beamSourceModelV = sourceModelV.beamSourceModelVs.Where(aa => aa.Id == superSModels[i].BeamId).FirstOrDefault();
                SuperSResultView superSResultView = new SuperSResultView()
                {
                    ChainNum = superSModels[i].UniteNum,
                    StartPierNum = superSModels[i].StartPierNum,
                    EndPierNum = superSModels[i].EndPierNum,
                    SpanNum = superSModels[i].SpanNum,
                    Designation = beamSourceModelV.Designation,
                    ComponentClass = beamSourceModelV.Type,
                    OverlayThickness = beamSourceModelV.OverlayThickness,
                    SideBeamPierHeight = beamSourceModelV.SideBeamPierHeight,
                    MidBeamPierHeight = beamSourceModelV.MidBeamPierHeight,
                    StartPointPierAddHeight = beamSourceModelV.StartPointPierAddHeight,
                    EndPointPierAddHeight = beamSourceModelV.EndPointPierAddHeight,
                    StrartPierBeamHeight = beamSourceModelV.StartPointPierAddHeight + beamSourceModelV.SideBeamPierHeight,
                    EndPierBeamHeight = beamSourceModelV.EndPointPierAddHeight + beamSourceModelV.SideBeamPierHeight,
                    BottomBoardCrossSlope = beamSourceModelV.BottomBoardCrossSlope
                };
                superSResultViews.Add(superSResultView);
            }

            return superSResultViews;
        }
        /// <summary>
        /// 下部结构
        /// </summary>
        /// <returns></returns>
        private List<SubSResultView> GetSubSResultViews()
        {
            List<SubSResultView> subSResultViews = new List<SubSResultView>();
            List<SubSModelV> subSModelVs = bridgeModelV.subSModelVs;
            List<SupportSModelV> supportSModelVs = bridgeModelV.supportSModelVs;
            List<SuperSModelV> superSModelVs = bridgeModelV.superSModelVs;
            for (int i = 0; i < subSModelVs.Count; i++)
            {
                SubSModelV subSModelV = subSModelVs[i];
                SubSResultView subSResultView = new SubSResultView();
                subSResultView.dongxuhao = Convert.ToInt16(subSModelV.PierNum.Split('_')[0]);
                subSResultView.dongtaihao = subSModelV.PierNum;
                subSResultView.zhuanghao = subSModelV.Mark;
                subSResultView.kuajing = subSModelV.distance;
                subSResultView.lizhuxinghao = sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault().Designation;
                subSResultView.shifoufanxiang = subSModelV.IsTurn;
                subSResultView.jichuxinghao = sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == subSModelV.FoundationName).FirstOrDefault().Designation;
                subSResultView.zhizuoxitongyushegaodong = Math.Round(subSModelV.SupportSystemPredefineHeight, RD);
                subSResultView.chengtaizuixiaomaishenbiaozhunzhi = Math.Round(subSModelV.CapsDepth, RD);
                subSResultView.chengtaizhuanjiao = Math.Round(subSModelV.CapsAngle, RD);
                subSResultView.xiejiaojiao = Math.Round(subSModelV.Angle, RD);
                subSResultView.zuankongbianhao = sourceModelV.explorSourceModelV.explorationSourceModelVs.Where(aa => aa.UnitId == subSModelV.HoleNum).FirstOrDefault().Num;
                subSResultView.shifoufuzhudong = subSModelV.IsAuxiliaryPier;
                subSResultView.shifougudingdong =
                     supportSModelVs.Where(aa => aa.PierNum == subSModelV.PierNum).Where(aa => aa.LimitDirection == "GD").ToList().Count;
                subSResultView.liangdinghengpozuo = Math.Round(RoadDataHandle.GetCgSlope(subSModelV.Mark, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().cgxSourceModelVs)[0], RD);
                subSResultView.liangdinghengpoyou = Math.Round(RoadDataHandle.GetCgSlope(subSModelV.Mark, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().cgxSourceModelVs)[1], RD);

                SuperSModelV superSModelV = new SuperSModelV();
                for (int j = 0; j < superSModelVs.Count; j++)
                {
                    int currentPierNum = Convert.ToInt16(subSModelV.PierNum.Split('_')[0]);
                    int superLPierNum = Convert.ToInt16(superSModelVs[j].StartPierNum.Split('_')[0]);
                    int superFPierNum = Convert.ToInt16(superSModelVs[j].EndPierNum.Split('_')[0]);
                    if (currentPierNum >= superLPierNum && currentPierNum <= superFPierNum)
                    {
                        superSModelV = superSModelVs[j];
                    }
                }
                BeamSourceModelV beamSourceModelV = GlobalData.sourceModelV.beamSourceModelVs.Where(aa => aa.Id == superSModelV.BeamId).FirstOrDefault();
                if (beamSourceModelV.BottomBoardCrossSlope == 0)
                {
                    subSResultView.liangdihengpozuo = 0;
                    subSResultView.liangdihengpoyou = 0;
                }
                else
                {
                    subSResultView.liangdihengpozuo = Math.Round(subSResultView.liangdinghengpozuo, RD);
                    subSResultView.liangdihengpoyou = Math.Round(subSResultView.liangdinghengpoyou, RD);
                }

                subSResultView.dimianhengpozuo = Math.Round(RoadDataHandle.GetCgSlope(subSModelV.Mark, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == bridgeModelV.AffRoadSourceModelVId).FirstOrDefault().cgxSourceModelVs)[0], RD);
                subSResultView.dimianhengpoyou = Math.Round(RoadDataHandle.GetCgSlope(subSModelV.Mark, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == bridgeModelV.AffRoadSourceModelVId).FirstOrDefault().cgxSourceModelVs)[1], RD);

                var foundationSourceModelV = sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == subSModelV.FoundationName).First();
                if (foundationSourceModelV.Type == "扩大基础")
                {
                    subSResultView.kuodajichuzuixiaofutushendong = Math.Round(subSModelV.CapsDepth, RD);
                    subSResultView.kuodajichuruchilicengzuixiaozhi = Math.Round(sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == subSModelV.FoundationName).First().ExpandTheMinimumValueOfTheFoundationIntoTheBearingLayer, RD);
                }
                else
                {
                    subSResultView.kuodajichuzuixiaofutushendong = 0;
                    subSResultView.kuodajichuruchilicengzuixiaozhi = 0;
                }

                subSResultView.kuodajichuzuixiaobiaogao = Math.Round(subSModelV.MinimumElevationOfExpandFoundation, RD);
                subSResultView.zhuangjichilicengbianhao = subSModelV.PileFoundationBearingLayerNumber;
                subSResultView.kuodajichudi1chilicengbianhao = subSModelV.EnlargeBase1stHoldingLayerNumber;
                subSResultView.kuodajichudi2chilicengbianhao = subSModelV.EnlargeBase2ndHoldingLayerNumber;
                subSResultView.jichuleixing = sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == subSModelV.FoundationName).FirstOrDefault().Type;
                subSResultView.shifouguodongdong = subSModelV.IsTransitionalPier;

                List<SupportSModelV> supportSModelVsTemp = supportSModelVs.Where(aa => aa.PierNum == subSModelV.PierNum).ToList();
                ///计算横向偏心距
                double sum = supportSModelVsTemp.Sum(aa => aa.DistanceBetweenSupportAndCenterline);
                subSResultView.hengxiangpianxinjue = Math.Round(supportSModelVsTemp.Sum(aa => aa.DistanceBetweenSupportAndCenterline) / supportSModelVsTemp.Count, RD);

                ///计算纵向偏心距
                if (subSModelV.IsTransitionalPier == 1)
                {
                    //subSResultView.zongxiangpianxinjuf = supportSModelVsTemp.;
                    //四个就是2排2列
                    if (supportSModelVsTemp.Count == 4)
                    {
                        subSResultView.zongxiangpianxinjuf = Math.Round(((supportSModelVsTemp[0].SupportReactionDeadLoad * supportSModelVsTemp[0].DistanceBetweenSupportAndSpanline
                             + supportSModelVsTemp[1].SupportReactionDeadLoad * supportSModelVsTemp[1].DistanceBetweenSupportAndSpanline)
                             - (supportSModelVsTemp[2].SupportReactionDeadLoad * supportSModelVsTemp[2].DistanceBetweenSupportAndSpanline
                             + supportSModelVsTemp[3].SupportReactionDeadLoad * supportSModelVsTemp[3].DistanceBetweenSupportAndSpanline))
                             / (supportSModelVsTemp[0].SupportReactionDeadLoad + supportSModelVsTemp[1].SupportReactionDeadLoad
                             + supportSModelVsTemp[2].SupportReactionDeadLoad + supportSModelVsTemp[3].SupportReactionDeadLoad), RD);
                    }
                    else if (supportSModelVsTemp.Count == 3)
                    {
                        //正品字形
                        if (subSModelV.IsTurn == "否")
                        {
                            subSResultView.zongxiangpianxinjuf = Math.Round(((supportSModelVsTemp[0].SupportReactionDeadLoad * supportSModelVsTemp[0].DistanceBetweenSupportAndSpanline
                             + supportSModelVsTemp[1].SupportReactionDeadLoad * supportSModelVsTemp[1].DistanceBetweenSupportAndSpanline)
                             - (supportSModelVsTemp[2].SupportReactionDeadLoad * supportSModelVsTemp[2].DistanceBetweenSupportAndSpanline))
                             / (supportSModelVsTemp[0].SupportReactionDeadLoad + supportSModelVsTemp[1].SupportReactionDeadLoad
                             + supportSModelVsTemp[2].SupportReactionDeadLoad), RD);
                        }
                        //反品字形
                        else
                        {
                            subSResultView.zongxiangpianxinjuf = Math.Round(((supportSModelVsTemp[0].SupportReactionDeadLoad * supportSModelVsTemp[0].DistanceBetweenSupportAndSpanline)
                             - (supportSModelVsTemp[1].SupportReactionDeadLoad * supportSModelVsTemp[1].DistanceBetweenSupportAndSpanline
                             + supportSModelVsTemp[2].SupportReactionDeadLoad * supportSModelVsTemp[2].DistanceBetweenSupportAndSpanline))
                             / (supportSModelVsTemp[0].SupportReactionDeadLoad + supportSModelVsTemp[1].SupportReactionDeadLoad
                             + supportSModelVsTemp[2].SupportReactionDeadLoad), RD);
                        }
                    }
                    else if (supportSModelVsTemp.Count == 2)
                    {
                        subSResultView.zongxiangpianxinjuf = Math.Round(((supportSModelVsTemp[0].SupportReactionDeadLoad * supportSModelVsTemp[0].DistanceBetweenSupportAndSpanline)
                             - (supportSModelVsTemp[1].SupportReactionDeadLoad * supportSModelVsTemp[1].DistanceBetweenSupportAndSpanline))
                             / (supportSModelVsTemp[0].SupportReactionDeadLoad + supportSModelVsTemp[1].SupportReactionDeadLoad), RD);
                    }

                }
                else
                {
                    subSResultView.zongxiangpianxinjuf = 0;
                }

                subSResultView.zongxiangpianxinjuf = Math.Round(-subSResultView.zongxiangpianxinjuf * 1000, RD);

                subSResultView.qiaodongzhongxinzhuanghao = Math.Round(subSModelV.Mark + subSResultView.hengxiangpianxinjue * Math.Tan(subSModelV.Angle * Math.PI / 180)
                    + subSResultView.zongxiangpianxinjuf * 0.001, RD);

                subSResultView.Xzuobiao = Math.Round(RoadDataHandle.xyf_xy(subSResultView.qiaodongzhongxinzhuanghao, 0, 0, 2, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().pqxSourceModelVs), RD);
                subSResultView.Yzuobiao = Math.Round(RoadDataHandle.xyf_xy(subSResultView.qiaodongzhongxinzhuanghao, 0, 0, 1, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().pqxSourceModelVs), RD);

                subSResultView.daolushejixianchuqiaomiangaocheng = Math.Round(RoadDataHandle.jqsqx(subSResultView.qiaodongzhongxinzhuanghao, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().sqxSourceModelVs), RD);

                subSResultView.qiexiangzongpo = Math.Round((RoadDataHandle.jqsqx(subSResultView.qiaodongzhongxinzhuanghao + 0.01, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().sqxSourceModelVs)
                    - RoadDataHandle.jqsqx(subSResultView.qiaodongzhongxinzhuanghao, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().sqxSourceModelVs)) * 100 * 100, RD);

                subSResultView.shejidimiangao = Math.Round(RoadDataHandle.jqsqx(subSResultView.qiaodongzhongxinzhuanghao, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == bridgeModelV.AffRoadSourceModelVId).FirstOrDefault().sqxSourceModelVs), RD);


                var yy1 = RoadDataHandle.xyf_xy(subSResultView.qiaodongzhongxinzhuanghao + 0.01, 0, 0, 1, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().pqxSourceModelVs);
                var yy2 = RoadDataHandle.xyf_xy(subSResultView.qiaodongzhongxinzhuanghao, 0, 0, 1, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().pqxSourceModelVs);
                var xx1 = RoadDataHandle.xyf_xy(subSResultView.qiaodongzhongxinzhuanghao + 0.01, 0, 0, 2, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().pqxSourceModelVs);
                var xx2 = RoadDataHandle.xyf_xy(subSResultView.qiaodongzhongxinzhuanghao + 0, 0, 0, 2, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().pqxSourceModelVs);

                double qxfwj = Math.Round(Math.Atan(((RoadDataHandle.xyf_xy(subSResultView.qiaodongzhongxinzhuanghao + 0.01, 0, 0, 1, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().pqxSourceModelVs)
                    - RoadDataHandle.xyf_xy(subSResultView.qiaodongzhongxinzhuanghao, 0, 0, 1, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().pqxSourceModelVs)) /
                    (RoadDataHandle.xyf_xy(subSResultView.qiaodongzhongxinzhuanghao + 0.01, 0, 0, 2, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().pqxSourceModelVs) -
                    RoadDataHandle.xyf_xy(subSResultView.qiaodongzhongxinzhuanghao, 0, 0, 2, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().pqxSourceModelVs)))), RD);

                subSResultView.qiexianfangweijiao = Math.Round(qxfwj > 0 ? qxfwj : 2 * Math.PI + qxfwj, RD);

                double lianggao = 0;
                if (_superSResultViews.Where(aa => aa.ChainNum == supportSModelVsTemp[0].ChainNum).First().StartPierNum == supportSModelVsTemp[0].PierNum)
                {
                    lianggao = _superSResultViews.Where(aa => aa.ChainNum == supportSModelVsTemp[0].ChainNum).First().StrartPierBeamHeight;
                }
                else
                {
                    if (_superSResultViews.Where(aa => aa.ChainNum == supportSModelVsTemp[0].ChainNum).First().EndPierNum == supportSModelVsTemp[0].PierNum)
                    {
                        lianggao = _superSResultViews.Where(aa => aa.ChainNum == supportSModelVsTemp[0].ChainNum).First().EndPierBeamHeight;
                    }
                    else
                    {
                        lianggao = _superSResultViews.Where(aa => aa.ChainNum == supportSModelVsTemp[0].ChainNum).First().MidBeamPierHeight;
                    }
                }

                subSResultView.dongdingbiaogaoA = Math.Round(subSResultView.daolushejixianchuqiaomiangaocheng - lianggao - subSResultView.zhizuoxitongyushegaodong, RD);

                subSResultView.lizhuhoudong = Math.Round(sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault().TopLLongitudinalThickness, RD);

                if (subSResultView.hengxiangpianxinjue < 0)
                {
                    subSResultView.chengtaimaishenjisuangaodong = Math.Round(subSResultView.chengtaizuixiaomaishenbiaozhunzhi -
                        subSResultView.dimianhengpozuo * subSResultView.hengxiangpianxinjue / 100, RD);
                }
                else
                {
                    subSResultView.chengtaimaishenjisuangaodong = Math.Round(subSResultView.chengtaizuixiaomaishenbiaozhunzhi +
                        subSResultView.dimianhengpoyou * subSResultView.hengxiangpianxinjue / 100, RD);
                }

                subSResultView.lizhujisuangaodong = Math.Round(subSResultView.dongdingbiaogaoA - (subSResultView.shejidimiangao - subSResultView.chengtaimaishenjisuangaodong), RD);
                subSResultView.lizhucaiyonggaodongH = Math.Round(Math.Max(ceiling(subSResultView.lizhujisuangaodong, 0.1),
                    sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).FirstOrDefault().VariableHeight), RD);
                subSResultView.chengtaidingbiaogaoB = Math.Round(subSResultView.dongdingbiaogaoA - subSResultView.lizhucaiyonggaodongH, RD);
                subSResultView.chengtai_qiaotaigailianghoudong = Math.Round(sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == subSModelV.FoundationName).FirstOrDefault().CapsThickness, RD);
                subSResultView.chengtaidibiaogaoC = Math.Round(subSResultView.chengtaidingbiaogaoB - subSResultView.chengtai_qiaotaigailianghoudong, RD);

                var explorationSourceModelV = sourceModelV.explorSourceModelV.explorationSourceModelVs.Where(aa => aa.UnitId == subSModelV.HoleNum).First();

                subSResultView.zhuangjichilicengdingmianbiaogao =
                     Math.Round(Convert.ToDouble(sourceModelV.explorSourceModelV.soiLayerSourceModelVs.Where(aa => aa.HoleNum == explorationSourceModelV.Num && aa.SoilLayerNum == subSModelV.PileFoundationBearingLayerNumber).FirstOrDefault().TopLayerElevation), RD);
                //subSResultView.zhuangjichilicengdingmianbiaogao = 
                //    Convert.ToDouble(sourceModelV.explorSourceModelV.soiLayerSourceModelVs.Where(aa => aa.UnitExplorationUnitId == subSModelV.HoleNum && aa.SoilLayerNum == subSResultView.zhuangjichilicengbianhao).FirstOrDefault().TopLayerElevation);
                subSResultView.zhuangjing = Math.Round(sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == subSModelV.FoundationName).FirstOrDefault().pileRadius, RD);
                subSResultView.ruchilicengshendong = Math.Round(subSResultView.zhuangjing * 1.5, RD);
                subSResultView.chudingzhuangdibiaogao = Math.Round(subSResultView.zhuangjichilicengdingmianbiaogao - subSResultView.ruchilicengshendong, RD);
                subSResultView.chudingzhuangchang = Math.Round(subSResultView.chengtaidibiaogaoC - subSResultView.chudingzhuangdibiaogao, RD);
                subSResultView.shishezhuangchang = Math.Round(ceiling(Math.Max(5 * subSResultView.zhuangjing, subSResultView.chudingzhuangchang), 1), RD);
                subSResultView.zhuangdibiaogaoD = Math.Round(subSResultView.chengtaidibiaogaoC - subSResultView.shishezhuangchang, RD);
                subSResultView.kuojidi1chilicengdingmianbiaogao =
                     Math.Round(Convert.ToDouble(sourceModelV.explorSourceModelV.soiLayerSourceModelVs.Where(aa => aa.HoleNum == explorationSourceModelV.Num && aa.SoilLayerNum == subSModelV.EnlargeBase1stHoldingLayerNumber).FirstOrDefault().TopLayerElevation), RD);
                subSResultView.kuojidi2chilicengdingmianbiaogao =
                     Math.Round(Convert.ToDouble(sourceModelV.explorSourceModelV.soiLayerSourceModelVs.Where(aa => aa.HoleNum == explorationSourceModelV.Num && aa.SoilLayerNum == subSModelV.EnlargeBase2ndHoldingLayerNumber).FirstOrDefault().TopLayerElevation), RD);

                //subSResultView.kuojidi1chilicengdingmianbiaogao =
                //    Convert.ToDouble(sourceModelV.explorSourceModelV.soiLayerSourceModelVs.Where(aa => aa.UnitExplorationUnitId == subSModelV.HoleNum && aa.SoilLayerNum == subSResultView.kuodajichudi1chilicengbianhao).FirstOrDefault().TopLayerElevation);
                //subSResultView.kuojidi2chilicengdingmianbiaogao =
                //    Convert.ToDouble(sourceModelV.explorSourceModelV.soiLayerSourceModelVs.Where(aa => aa.UnitExplorationUnitId == subSModelV.HoleNum && aa.SoilLayerNum == subSResultView.kuodajichudi2chilicengbianhao).FirstOrDefault().TopLayerElevation);
                if (subSResultView.jichuleixing == "扩大基础")
                {
                    subSResultView.kuojichilicengbiaogao1 = Math.Round(subSResultView.kuojidi1chilicengdingmianbiaogao, RD);
                    subSResultView.kuojichilicengbiaogao2 = Math.Round(subSResultView.kuojidi2chilicengdingmianbiaogao, RD);
                    subSResultView.jichuhoudong = Math.Round(subSResultView.chengtai_qiaotaigailianghoudong, RD);
                    subSResultView.kuojijidibiaogao_anchiliceng = Math.Round(subSResultView.kuojidi1chilicengdingmianbiaogao - subSResultView.kuodajichuruchilicengzuixiaozhi, RD);
                    if (subSResultView.hengxiangpianxinjue < 0)
                    {
                        subSResultView.kuojijidibiaogao_anfutu = Math.Round(subSResultView.shejidimiangao -
                            (-subSResultView.dimianhengpozuo * subSResultView.hengxiangpianxinjue) / 100 - subSResultView.kuodajichuzuixiaofutushendong, RD);
                    }
                    else
                    {
                        subSResultView.kuojijidibiaogao_anfutu = Math.Round(subSResultView.shejidimiangao -
                            (subSResultView.dimianhengpoyou * subSResultView.hengxiangpianxinjue) / 100 - subSResultView.kuodajichuzuixiaofutushendong, RD);
                    }
                    subSResultView.chusuanjidibiaogao =
                         Math.Round(Math.Min(Math.Min(subSResultView.kuojijidibiaogao_anchiliceng, subSResultView.kuojijidibiaogao_anfutu), subSResultView.kuodajichuzuixiaobiaogao), RD);
                    subSResultView.jinrudidongchilicengshijidibiaogao =
                         Math.Round(subSResultView.chusuanjidibiaogao < subSResultView.kuojichilicengbiaogao2
                        ?
                        Math.Min(subSResultView.chusuanjidibiaogao, subSResultView.kuojichilicengbiaogao2 - subSResultView.kuodajichuruchilicengzuixiaozhi)
                        :
                        subSResultView.chusuanjidibiaogao, RD);
                    subSResultView.jidingbiaogao = Math.Round(subSResultView.jinrudidongchilicengshijidibiaogao + subSResultView.jichuhoudong, RD);
                    subSResultView.dongzhujisuangaodong = Math.Round(subSResultView.dongdingbiaogaoA - subSResultView.jidingbiaogao, RD);
                    subSResultView.lizhucaiyonggaodongH1 = Math.Round(ceiling(subSResultView.dongzhujisuangaodong, 0.1), RD);
                    subSResultView.jichudingbiaogaoB = Math.Round(subSResultView.dongdingbiaogaoA - subSResultView.lizhucaiyonggaodongH1, RD);
                    subSResultView.jichudibiaogaoC = Math.Round(subSResultView.jichudingbiaogaoB - subSResultView.jichuhoudong, RD);
                }
                subSResultViews.Add(subSResultView);
            }
            return subSResultViews;
        }


        /// <summary>
        /// 获取支座系统数据
        /// </summary>
        /// <returns></returns>
        private List<SupportResultView> GetSupportResultViews()
        {
            List<SupportResultView> supportResultViews = new List<SupportResultView>();
            List<SupportSModelV> supportSModelVs = bridgeModelV.supportSModelVs;
            List<SuperSModelV> superSModelVs = bridgeModelV.superSModelVs;
            List<SubSModelV> subSModelVs = bridgeModelV.subSModelVs;
            for (int i = 0; i < supportSModelVs.Count; i++)
            {
                SupportSModelV supportSModelV = supportSModelVs[i];
                SupportResultView supportResultView = new SupportResultView();

                supportResultView.duntaihao = supportSModelV.PierNum;
                supportResultView.weizhi = supportSModelV.Position;
                supportResultView.lianxuhao = supportSModelV.ChainNum;
                supportResultView.dongxuhao = Convert.ToInt16(supportSModelV.PierNum.Split('_')[0]);
                supportResultView.zhizuoxitongyushegao = supportSModelV.SupportSystemPredefineHeight;
                supportResultView.zhzuoyudaoluzhongxinxianjuli = supportSModelV.DistanceBetweenSupportAndCenterline;
                supportResultView.zhzuojufenhuaxianjuli = supportSModelV.DistanceBetweenSupportAndSpanline;
                supportResultView.shangdianshihoudong = supportSModelV.ThicknessOfUpperStone;
                supportResultView.shangdianshizuixiaobaoluochanga = supportSModelV.MinimumEnvelopeLengthOfUpperStone;
                supportResultView.shangdianshizuixiaobaoluokuanb = supportSModelV.MinimumEnvelopeWidthOfUpperStone;
                supportResultView.zhifanlihengzai = supportSModelV.SupportReactionDeadLoad;
                supportResultView.zuidabiaozhunzuhefanli = supportSModelV.MaximumStandardCombinationReaction;
                supportResultView.zuixiaobiaozhunzuhefanli = supportSModelV.MinimumStandardCombinationReaction;
                supportResultView.nixuanzhzuochengzaili = supportSModelV.ProposedSupportBearingCapacity;
                supportResultView.gudingfangxiang = supportSModelV.LimitDirection;
                supportResultView.zhzuoxinghao = supportSModelV.SupportType;
                supportResultView.daoluzhongxinxianshejigao = Math.Round(
                RoadDataHandle.jqsqx(subSModelVs.Where(aa => aa.PierNum == supportSModelV.PierNum).First().Mark, GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().sqxSourceModelVs), RD);
                supportResultView.puzhuanghou = _superSResultViews.Where(aa => aa.ChainNum == supportSModelV.ChainNum).First().OverlayThickness;

                if (_superSResultViews.Where(aa => aa.ChainNum == supportSModelV.ChainNum).First().StartPierNum == supportSModelV.PierNum)
                {
                    supportResultView.lianggao = Math.Round(_superSResultViews.Where(aa => aa.ChainNum == supportSModelV.ChainNum).First().StrartPierBeamHeight, RD);

                }
                else
                {
                    if (_superSResultViews.Where(aa => aa.ChainNum == supportSModelV.ChainNum).First().EndPierNum == supportSModelV.PierNum)
                    {
                        supportResultView.lianggao = Math.Round(_superSResultViews.Where(aa => aa.ChainNum == supportSModelV.ChainNum).First().EndPierBeamHeight, RD);
                    }
                    else
                    {
                        supportResultView.lianggao = Math.Round(_superSResultViews.Where(aa => aa.ChainNum == supportSModelV.ChainNum).First().MidBeamPierHeight, RD);
                    }

                }

                if (supportResultView.zhzuoyudaoluzhongxinxianjuli < 0)
                {
                    supportResultView.liangdihengpo = Math.Round(_subSResultViews.Where(aa => aa.dongtaihao == supportResultView.duntaihao).FirstOrDefault().liangdihengpozuo, RD);
                }
                else
                {
                    supportResultView.liangdihengpo = Math.Round(_subSResultViews.Where(aa => aa.dongtaihao == supportResultView.duntaihao).FirstOrDefault().liangdihengpoyou, RD);
                }

                ///求纵坡
                supportResultView.zongpo = Math.Round(_subSResultViews.Where(aa => aa.dongtaihao == supportResultView.duntaihao).FirstOrDefault().qiexiangzongpo, RD);
                ///横向偏心距
                supportResultView.hengxiangpianxinjue = Math.Round(_subSResultViews.Where(aa => aa.dongtaihao == supportResultView.duntaihao).FirstOrDefault().hengxiangpianxinjue, RD);
                supportResultView.zongxiangpianxinjuf = Math.Round(_subSResultViews.Where(aa => aa.dongtaihao == supportResultView.duntaihao).FirstOrDefault().zongxiangpianxinjuf, RD);
                supportResultView.zhizuochuliangdigaocha = Math.Round(supportResultView.lianggao + supportResultView.puzhuanghou +
                    Math.Abs(supportResultView.zhzuoyudaoluzhongxinxianjuli) * supportResultView.liangdihengpo * 0.01, RD);

                supportResultViews.Add(supportResultView);
            }
            for (int i = 0; i < supportResultViews.Count; i++)
            {
                SupportResultView supportResultView = supportResultViews[i];
                supportResultView.zhizuochuzuidagaocha = Math.Round(supportResultViews.Where(aa => aa.duntaihao == supportResultView.duntaihao)
                    .OrderByDescending(aa => aa.zhizuochuliangdigaocha).FirstOrDefault().zhizuochuliangdigaocha, RD);
                supportResultView.zhizuoxitonggaodong = Math.Round(supportResultView.zhizuoxitongyushegao + supportResultView.zhizuochuzuidagaocha
                    - supportResultView.zhizuochuliangdigaocha + supportResultView.zhzuojufenhuaxianjuli * supportResultView.zongpo / 100, RD);
                SupportSourceModelV supportSourceModelV = GlobalData.supportSourceModelVs.Where(aa => aa.designation == supportResultView.zhzuoxinghao).FirstOrDefault();
                supportResultView.zhizuogaodongmm = Math.Round(supportSourceModelV.support_height, RD);
                supportResultView.zhizuoxiadianshihoudong = Math.Round(supportResultView.zhizuoxitonggaodong * 1000 - supportResultView.zhizuogaodongmm - supportResultView.shangdianshihoudong, RD);
                supportResultView.shifougudingdong = supportResultView.gudingfangxiang == "GD" ? 1 : 0;


                string sdshd1 = (supportResultView.shangdianshizuixiaobaoluokuanb > supportSourceModelV.top_board_width_a
                        ? supportResultView.shangdianshizuixiaobaoluokuanb : ceiling((supportSourceModelV.top_board_width_a + 100), 10))
                        + "*" +
                        (supportResultView.shangdianshizuixiaobaoluochanga > supportSourceModelV.top_board_length_a1_z100
                        ? supportResultView.shangdianshizuixiaobaoluochanga : ceiling((supportSourceModelV.top_board_length_a1_z100 + 100), 10));

                string sdshd2 = (supportResultView.shangdianshizuixiaobaoluochanga > supportSourceModelV.top_board_length_a1_z100
                                        ? supportResultView.shangdianshizuixiaobaoluochanga : ceiling((supportSourceModelV.top_board_length_a1_z100 + 100), 10))
                                        + "*" +
                                        (supportResultView.shangdianshizuixiaobaoluokuanb > supportSourceModelV.top_board_width_a
                                        ? supportResultView.shangdianshizuixiaobaoluokuanb : ceiling((supportSourceModelV.top_board_width_a + 100), 10));

                if (_subSResultViews.Where(aa => aa.dongtaihao == supportResultView.duntaihao).First().shifougudingdong > 0)
                {
                    if (supportSModelVs.Where(aa => aa.PierNum == supportResultView.duntaihao).Count() > 1)
                    {
                        if (supportResultView.weizhi == 2)
                        {
                            supportResultView.shangdianshichicunab = sdshd1;
                        }
                        else
                        {
                            supportResultView.shangdianshichicunab = sdshd2;
                        }
                    }
                    else
                    {
                        supportResultView.shangdianshichicunab = sdshd2;
                    }
                }
                else
                {
                    supportResultView.shangdianshichicunab = sdshd2;
                }

                supportResultView.xiadianshichicunc = ceiling(supportSourceModelV.bottom_board_length_c + 100, 50);
                supportResultView.xiadianshichicuncc = supportResultView.xiadianshichicunc + "*" + supportResultView.xiadianshichicunc;
                supportResultView.zhizuosuoxuqiaodonghoudong = Math.Round((supportResultView.xiadianshichicunc / 2 +
                    Math.Abs(supportResultView.zhzuojufenhuaxianjuli * 1000 - supportResultView.zongxiangpianxinjuf)) * 2 + 100, RD);
                supportResultView.qiaodongshunqiaoxianghoudong = Math.Round(
                    _subSResultViews.Where(aa => aa.dongtaihao == supportResultView.duntaihao).FirstOrDefault().lizhuhoudong * 1000, RD);
                supportResultView.qiaodongkuandongshifoumanzuxuyao = supportResultView.qiaodongshunqiaoxianghoudong > supportResultView.zhizuosuoxuqiaodonghoudong ? "是" : "否";
            }

            return supportResultViews;
        }
        /// <summary>
        /// 支座数据一览表
        /// </summary>
        /// <returns></returns>
        public List<SupportResultDataView> GetSupportResultDataViews()
        {
            List<SupportResultDataView> supportResultDataViews = new List<SupportResultDataView>();

            for (int i = 0; i < _supportResultViews.Count; i++)
            {
                SupportResultView supportResultView = _supportResultViews[i];
                SupportResultDataView supportResultDataView = new SupportResultDataView();
                supportResultDataView.dunhao = supportResultView.duntaihao;
                supportResultDataView.zhizuoweizhi = "支座" + supportResultView.weizhi;
                supportResultDataView.zhizuoxinghao = supportResultView.zhzuoxinghao;
                SupportSourceModelV supportSourceModelV = GlobalData.supportSourceModelVs.Where(aa => aa.designation == supportResultView.zhzuoxinghao).FirstOrDefault();
                supportResultDataView.zhizuogaoduh = supportSourceModelV.support_height;
                supportResultDataView.zhizuoshangdianshichicuna = supportSourceModelV.top_board_length_a1_z150;
                supportResultDataView.zhizuoshangdianshichicunb = supportSourceModelV.top_cushion_width_b_preset;
                supportResultDataView.zhizuoshangdianshichicunc = supportSourceModelV.bottom_cushion_size;
                supportResultDataView.zhizuoshangdianshichicund = supportSourceModelV.bottom_cushion_size;
                supportResultDataView.mingyizhizuoxitonggaoduh1 = supportResultView.zhizuoxitongyushegao * 1000;
                supportResultDataView.shijizhizuoxitonggaoduh2 = supportResultView.zhizuoxitonggaodong * 1000;
                supportResultDataView.shangdianshihoudu = supportResultView.shangdianshihoudong;
                supportResultDataView.xiadianshihoudu = supportResultView.zhizuoxiadianshihoudong;
                supportResultDataView.dianshihunningtuC50 = ((supportResultDataView.zhizuoshangdianshichicuna * supportResultDataView.zhizuoshangdianshichicunb) * supportResultDataView.shangdianshihoudu
                    + (supportResultDataView.zhizuoshangdianshichicunc * supportResultDataView.zhizuoshangdianshichicund) * supportResultDataView.xiadianshihoudu) / 1000.0 / 1000.0 / 1000.0;
                supportResultDataViews.Add(supportResultDataView);
            }

            return supportResultDataViews;
        }
        /// <summary>
        /// 支座数量表
        /// </summary>
        /// <returns></returns>
        private List<SupportNumView> GetSupportNumViews()
        {
            List<SupportNumView> supportNumViews = new List<SupportNumView>();
            List<TempSupportData> tempSupportDatas = new List<TempSupportData>();


            for (int i = 0; i < _supportResultViews.Count; i++)
            {
                SupportResultView supportResultView = _supportResultViews[i];
                TempSupportData tempSupportData = new TempSupportData();
                tempSupportData.zzxh = supportResultView.zhzuoxinghao;
                SuperSResultView superSResultView = _superSResultViews.Where(aa => aa.ChainNum == supportResultView.lianxuhao).First();
                if (superSResultView.ComponentClass.Contains("钢"))
                {
                    tempSupportData.isgxl = 1;
                }
                else
                {
                    tempSupportData.ishnt = 1;
                }
                tempSupportDatas.Add(tempSupportData);
            }

            for (int i = 0; i < tempSupportDatas.Count; i++)
            {
                TempSupportData tempSupportData = tempSupportDatas[i];
                if (supportNumViews.Where(aa => aa.zhizuoxinghao == tempSupportData.zzxh).Count() > 0)
                {
                    SupportNumView supportNumView = supportNumViews.Where(aa => aa.zhizuoxinghao == tempSupportData.zzxh).First();
                    supportNumView.zongshuliang += 1;
                    supportNumView.ganhgxiangliangshuliang += tempSupportData.isgxl;
                    supportNumView.hunningtushuliang += tempSupportData.ishnt;
                }
                else
                {
                    SupportNumView supportNumView = new SupportNumView();
                    supportNumView.zhizuoxinghao = tempSupportData.zzxh;
                    supportNumView.zongshuliang += 1;
                    supportNumView.ganhgxiangliangshuliang += tempSupportData.isgxl;
                    supportNumView.hunningtushuliang += tempSupportData.ishnt;
                    supportNumViews.Add(supportNumView);
                }
            }
            return supportNumViews;
        }

        private List<SdsSteelNumView> GetSdsSteelNumViews()
        {
            List<SdsSteelNumView> sdsSteelNumViews = new List<SdsSteelNumView>();

            for (int i = 0; i < _supportNumViews.Count; i++)
            {
                SdsSteelNumView sdsSteelNumView = new SdsSteelNumView();

                sdsSteelNumView.zhizuoxinghao = _supportNumViews[i].zhizuoxinghao;
                SupportSourceModelV supportSourceModelV = GlobalData.supportSourceModelVs.Where(aa => aa.designation == sdsSteelNumView.zhizuoxinghao).FirstOrDefault();
                sdsSteelNumView.shangdianshichicuna = supportSourceModelV.top_board_length_a1_z150;
                sdsSteelNumView.shangdianshichicunb = supportSourceModelV.top_cushion_width_b_preset;
                sdsSteelNumView.shangzuobanchicun = sdsSteelNumView.shangdianshichicuna + "×" + sdsSteelNumView.shangdianshichicunb;
                sdsSteelNumView.shangzuobangeshu = _supportNumViews[i].zongshuliang;
                sdsSteelNumView.mzhi = ceiling((sdsSteelNumView.shangdianshichicunb - 80) / 100.0, 1);
                sdsSteelNumView.nzhi = ceiling((sdsSteelNumView.shangdianshichicuna - 80) / 100.0, 1);
                sdsSteelNumView.n1gangjingchangdu = sdsSteelNumView.shangdianshichicuna + 1220;
                sdsSteelNumView.n2gangjingchangdu = sdsSteelNumView.shangdianshichicunb + 1220;
                sdsSteelNumView.n3gangjingchangdu = sdsSteelNumView.shangdianshichicuna - 80;
                sdsSteelNumView.n4gangjingchangdu = sdsSteelNumView.shangdianshichicunb - 80;
                sdsSteelNumView.danzhong = 0.888;
                sdsSteelNumView.zongzhong = ((sdsSteelNumView.mzhi + 1) * (sdsSteelNumView.n1gangjingchangdu + sdsSteelNumView.n3gangjingchangdu)
                    + (sdsSteelNumView.nzhi + 1) * (sdsSteelNumView.n2gangjingchangdu + sdsSteelNumView.n4gangjingchangdu)) / 1000.0
                    * sdsSteelNumView.danzhong * sdsSteelNumView.shangzuobangeshu;
                sdsSteelNumViews.Add(sdsSteelNumView);
            }

            return sdsSteelNumViews;
        }

        private List<XdsSteelNumView> GetXdsSteelNumViews()
        {
            List<XdsSteelNumView> xdsSteelNumViews = new List<XdsSteelNumView>();
            for (int i = 0; i < _supportNumViews.Count; i++)
            {
                XdsSteelNumView xdsSteelNumView = new XdsSteelNumView();

                xdsSteelNumView.zhizuoxinghao = _supportNumViews[i].zhizuoxinghao;
                SupportSourceModelV supportSourceModelV = GlobalData.supportSourceModelVs.Where(aa => aa.designation == xdsSteelNumView.zhizuoxinghao).FirstOrDefault();
                xdsSteelNumView.xiadianshichicunc = supportSourceModelV.bottom_cushion_size;
                xdsSteelNumView.xiadianshichicund = supportSourceModelV.bottom_cushion_size;
                xdsSteelNumView.dianshichicun = xdsSteelNumView.xiadianshichicunc + "*" + xdsSteelNumView.xiadianshichicund;
                xdsSteelNumView.dianshigeshu = _supportNumViews[i].zongshuliang;
                xdsSteelNumView.mzhi = ceiling((xdsSteelNumView.xiadianshichicund - 80) / 100.0, 1);
                xdsSteelNumView.nzhi = ceiling((xdsSteelNumView.xiadianshichicunc - 80) / 100.0, 1);
                xdsSteelNumView.n1gangjingchangdu = xdsSteelNumView.xiadianshichicunc + 1220;
                xdsSteelNumView.n2gangjingchangdu = xdsSteelNumView.xiadianshichicund + 1220;
                xdsSteelNumView.n3gangjingchangdu = xdsSteelNumView.xiadianshichicunc - 80;
                xdsSteelNumView.n4gangjingchangdu = xdsSteelNumView.xiadianshichicund - 80;
                xdsSteelNumView.danzhong = 0.888;
                xdsSteelNumView.zongzhong = ((xdsSteelNumView.mzhi + 1) * (xdsSteelNumView.n1gangjingchangdu + xdsSteelNumView.n3gangjingchangdu)
                    + (xdsSteelNumView.nzhi + 1) * (xdsSteelNumView.n2gangjingchangdu + xdsSteelNumView.n4gangjingchangdu) / 1000.0
                     * xdsSteelNumView.danzhong * xdsSteelNumView.dianshigeshu);
                xdsSteelNumViews.Add(xdsSteelNumView);
            }
            return xdsSteelNumViews;
        }

        private List<PilePositionView> GetPilePositionViews()
        {

            List<PilePositionView> pilePositionViews = new List<PilePositionView>();
            List<SubSModelV> subSModelVs = bridgeModelV.subSModelVs;
            List<PqxSourceModelV> pqxSourceModelVs = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == roadModelV.RoadSourceModelId).FirstOrDefault().pqxSourceModelVs;
            for (int i = 0; i < subSModelVs.Count; i++)
            {
                SubSModelV subSModelV = subSModelVs[i];
                ///分跨线基点位置
                Point3d p0 = new Point3d(RoadDataHandle.xyf_xy(subSModelV.Mark,0,0,1, pqxSourceModelVs),
                    RoadDataHandle.xyf_xy(subSModelV.Mark, 0, 0, 2, pqxSourceModelVs),0);
                ///墩位基点位置
                double angelTemp = Math.Atan((RoadDataHandle.xyf_xy(subSModelV.Mark + 0.01, 0, 0, 2, pqxSourceModelVs)
                    - RoadDataHandle.xyf_xy(subSModelV.Mark, 0, 0, 2, pqxSourceModelVs))/
                    (RoadDataHandle.xyf_xy(subSModelV.Mark + 0.01, 0, 0, 1, pqxSourceModelVs)
                    - RoadDataHandle.xyf_xy(subSModelV.Mark, 0, 0, 1, pqxSourceModelVs)));
                double rotation1 = subSModelV.Angle * Math.PI / 180 + Math.PI / 2 + angelTemp;
                Point3d p1 = new Point3d(p0.X + subSModelV.PierEccentricDistance * Math.Cos(rotation1),
                    p0.Y + subSModelV.PierEccentricDistance * Math.Sin(rotation1),0);
                List<Point3d> point3Ds = new List<Point3d>();
                FoundationSourceModelV fs = GlobalData.sourceModelV.foundationSourceModelVs.Where(aa => aa.Id == subSModelV.FoundationName).First();
                List<Position> positions = fs.PileCenterPoints.GetPositions();
                for (int j = 0; j < positions.Count; j++)
                {
                    Point3d p = new Point3d(positions[j].X + p1.X, positions[j].Y + p1.Y, 0);
                    point3Ds.Add(p.RotateBy(subSModelV.PierAngle * Math.PI / 180 + Math.PI + rotation1, Vector3d.ZAxis, p1));
                }
                for (int j = 0; j < point3Ds.Count; j++)
                {
                    PilePositionView pilePositionView = new PilePositionView();
                    pilePositionView.duntaihao = subSModelV.PierNum;
                    pilePositionView.dunxuhao = subSModelV.PierNum;
                    pilePositionView.zhuangjibianhao = j;
                    pilePositionView.zhuangjizuobiaoX = point3Ds[j].X;
                    pilePositionView.zhuangjizuobiaoY = point3Ds[j].Y;
                    pilePositionViews.Add(pilePositionView);
                }
                
            }

            return pilePositionViews;
        }

        private List<PierParmView> GetPierParmViews()
        {
            List<PierParmView> pierParmViews = new List<PierParmView>();

            for (int i = 0; i < _subSResultViews.Count; i++)
            {
                SubSResultView subSResultView = _subSResultViews[i];
                PierParmView pierParmView = new PierParmView();
                pierParmView.dunxuhao = subSResultView.dongxuhao;
                pierParmView.duntaihao = subSResultView.dongtaihao;
                pierParmView.zhuanghao = subSResultView.zhuanghao;
                pierParmView.qiaodunxinghao = subSResultView.lizhuxinghao;
                pierParmView.jichuxinghao = subSResultView.jichuxinghao;
                pierParmView.shifouguodudun = subSResultView.shifouguodongdong;
                pierParmView.jichuleixing = subSResultView.jichuleixing;
                pierParmView.xzuobiao = subSResultView.Xzuobiao;
                pierParmView.yzuobiao = subSResultView.Yzuobiao;
                pierParmView.qiexianfangweijiao = subSResultView.qiexianfangweijiao;
                pierParmView.dundingbiaogaoA = subSResultView.dongdingbiaogaoA;
                pierParmView.chengtaidingbiaogaoB = subSResultView.chengtaidingbiaogaoB;
                pierParmView.chengtaidingbiaogaoC = subSResultView.chengtaidibiaogaoC;
                pierParmView.jichudingbiaogaoB = subSResultView.jichudingbiaogaoB;
                pierParmView.jichudibiaogaoC = subSResultView.jichudibiaogaoC;
                pierParmView.lizhugaoduH = subSResultView.lizhucaiyonggaodongH;
                pierParmView.zhuangdibiaogaoD = subSResultView.zhuangdibiaogaoD;
                pierParmView.zhuangchang = subSResultView.shishezhuangchang;
                pierParmView.hengxiangpianxinju = subSResultView.hengxiangpianxinjue;
                pierParmView.zongxiangpianxinju = subSResultView.zongxiangpianxinjuf;
                List<SupportResultView> supportResultViews = _supportResultViews.Where(aa => aa.duntaihao == subSResultView.dongtaihao).ToList();
                SubSModelV subSModelV = bridgeModelV.subSModelVs.Where(aa => aa.PierNum == subSResultView.dongtaihao).First();
                PierSourceModelV pierSourceModelV = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == subSModelV.PierName).First(); 
                if (supportResultViews.Count == 1)
                {
                    pierParmView.zhizuojufenkuaxianjuliA = 0;
                    pierParmView.zhizuojufenkuaxianjuliB = 0;
                }
                else if (supportResultViews.Count == 2)
                {
                    pierParmView.zhizuojufenkuaxianjuliA = supportResultViews[0].zhzuojufenhuaxianjuli;
                    pierParmView.zhizuojufenkuaxianjuliB = supportResultViews[0].zhzuojufenhuaxianjuli;
                }
                else if (supportResultViews.Count == 3)
                {
                    if (subSModelV.IsTurn == "是")
                    {
                        pierParmView.zhizuojufenkuaxianjuliA = supportResultViews[0].zhzuojufenhuaxianjuli;
                        pierParmView.zhizuojufenkuaxianjuliB = supportResultViews[2].zhzuojufenhuaxianjuli + supportResultViews[1].zhzuojufenhuaxianjuli;
                    }
                    else
                    {
                        pierParmView.zhizuojufenkuaxianjuliA = supportResultViews[0].zhzuojufenhuaxianjuli + supportResultViews[1].zhzuojufenhuaxianjuli;
                        pierParmView.zhizuojufenkuaxianjuliB = supportResultViews[2].zhzuojufenhuaxianjuli;
                    }
                }
                else if (supportResultViews.Count == 4)
                {
                    pierParmView.zhizuojufenkuaxianjuliA = supportResultViews[0].zhzuojufenhuaxianjuli + supportResultViews[1].zhzuojufenhuaxianjuli;
                    pierParmView.zhizuojufenkuaxianjuliB = supportResultViews[2].zhzuojufenhuaxianjuli + supportResultViews[3].zhzuojufenhuaxianjuli;
                }
                pierParmViews.Add(pierParmView);
            }

            return pierParmViews;
        }



        private double ceiling(double a, double b)
        {
            if (a % b == 0)
            {
                return a;
            }
            else
            {
                return Math.Ceiling(a / b) * b;
            }
        }
    }



    class TempSupportData
    {
        public string zzxh;
        public int num;
        public int isgxl = 0;
        public int ishnt = 0;
    }
}
