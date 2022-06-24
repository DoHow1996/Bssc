using Bssc.Control.DataControl;
using Bssc.Control.DataControl.SaveAndLoadData;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.ResultModelsV;
using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DynamicDataGridViewTool;

namespace Bssc.ViewForms.AffiliateForms
{
    public partial class ResultDataForm : Form
    {
        TreeNode node;
        AcquireResultData acquireResultData;

        public ResultDataForm(TreeNode node)
        {
            InitializeComponent();

            this.node = node;
            //try
            //{
                this.acquireResultData = new AcquireResultData(node);

                ResultModel resultModel = new ResultModel()
                {
                    subSResultViews = acquireResultData._subSResultViews,
                    superSResultViews = acquireResultData._superSResultViews,
                    supportResultViews = acquireResultData._supportResultViews,
                    foundationSourceModelVs = GlobalData.sourceModelV.foundationSourceModelVs,
                    pierSourceModelVs = GlobalData.sourceModelV.pierSourceModelVs,
                    beamSourceModelVs = GlobalData.sourceModelV.beamSourceModelVs,
                    supportResultDataViews = acquireResultData._supportResultDataViews,
                    supportNumViews = acquireResultData._supportNumViews,
                    sdsSteelNumViews = acquireResultData._sdsSteelNumViews,
                    xdsSteelNumViews = acquireResultData._xdsSteelNumViews,
                    pilePositionViews = acquireResultData._pilePositionViews,
                    pierParmViews = acquireResultData._pierParmViews
                };
                SaveData.Export2Excel<ResultModel>(resultModel, "结果总表");
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show("已有数据不能生成结果纵表，请检查是否遗漏数据项");
            //}
            //finally
            //{
            //    this.Dispose();
            //}

        }

        private void skinTreeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            switch (e.Node.Text)
            {

                case "上部结构":

                    DynamicDgv.GenerateDynamicDgv<SuperSResultView>(skinDataGridView, acquireResultData._superSResultViews);
                    break;
                case "下部结构":
                    DynamicDgv.GenerateDynamicDgv<SubSResultView>(skinDataGridView, acquireResultData._subSResultViews);
                    break;
                case "支座系统":
                    DynamicDgv.GenerateDynamicDgv<SupportResultView>(skinDataGridView, acquireResultData._supportResultViews);
                    break;
                case "基础":
                    DynamicDgv.GenerateDynamicDgv<FoundationSourceModelV>(skinDataGridView, GlobalData.sourceModelV.foundationSourceModelVs);
                    break;
                case "桥墩":
                    DynamicDgv.GenerateDynamicDgv<PierSourceModelV>(skinDataGridView, GlobalData.sourceModelV.pierSourceModelVs);
                    break;
                case "主梁":
                    DynamicDgv.GenerateDynamicDgv<BeamSourceModelV>(skinDataGridView, GlobalData.sourceModelV.beamSourceModelVs);
                    break;
                case "支座数据一览表":
                    DynamicDgv.GenerateDynamicDgv<SupportResultDataView>(skinDataGridView, acquireResultData._supportResultDataViews);
                    break;
                case "支座数量表":
                    DynamicDgv.GenerateDynamicDgv<SupportNumView>(skinDataGridView, acquireResultData._supportNumViews);
                    break;
                case "上垫石钢筋数量表":
                    DynamicDgv.GenerateDynamicDgv<SdsSteelNumView>(skinDataGridView, acquireResultData._sdsSteelNumViews);
                    break;
                case "下垫石钢筋数量表":
                    DynamicDgv.GenerateDynamicDgv<XdsSteelNumView>(skinDataGridView, acquireResultData._xdsSteelNumViews);
                    break;
                case "桩基坐标表":
                    DynamicDgv.GenerateDynamicDgv<PilePositionView>(skinDataGridView, acquireResultData._pilePositionViews);
                    break;
                case "桥墩结构数据表":
                    DynamicDgv.GenerateDynamicDgv<PierParmView>(skinDataGridView, acquireResultData._pierParmViews);
                    break;
                default:
                    break;
            }
        }

        private void skinTreeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }

    
}
