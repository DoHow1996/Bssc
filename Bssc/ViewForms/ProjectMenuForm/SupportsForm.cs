using Bssc.Control.DataControl;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.ProjectModelsV;
using CCWin;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BaseLibrary.ExtensionMethod;
using BaseLibrary.Runtime;
using BaseLibrary.ResultData;
using BaseLibrary.ExtensionMethod;
using BaseLibrary.Filter;
using Bssc.Control.Tools;

namespace Bssc.ViewForms.ProjectMenuForm
{
    public partial class SupportsForm : Skin_Mac
    {
        public TreeNode node;
        public List<SupportSModelV> supportSModelVs;
        BridgeModelV bridgeModelV;
        public SupportsForm(TreeNode node)
        {
            InitializeComponent();
            this.node = node;

            ContextMenuWrapForDatagridview contextMenuWrapForDatagridview = new ContextMenuWrapForDatagridview(skinDataGridView1);

            TreeNode bridgeNode = node.Parent;
            TreeNode roadNode = bridgeNode.Parent;
            var roadModelV = GlobalData.projectModelV.roadModelVs.Where(aa => aa.Id == roadNode.Name).FirstOrDefault();
            bridgeModelV = roadModelV.bridgeModelVs.Where(aa => aa.Id == bridgeNode.Name).FirstOrDefault();
            supportSModelVs = bridgeModelV.subSModelVs.GetSupportSModelVs(node);
            if (bridgeModelV.supportSModelVs.Count != supportSModelVs.Count)
            {
                bridgeModelV.supportSModelVs = supportSModelVs;
            }
            BindingSource bs = new BindingSource();
            bs.DataSource = bridgeModelV.supportSModelVs;
            skinDataGridView1.DataSource = bs;

            skinDataGridView1.SetDgvColumnsReadOnlyColor();
        }


        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButton1_Click(object sender, EventArgs e)
        {
            List<SupportSModelV> supportSModelVsTemp = new List<SupportSModelV>();

            supportSModelVs.ForEach(aa => supportSModelVsTemp.Add(aa));
            supportSModelVsTemp = supportSModelVs.OrderBy(aa => aa.PierNum.Split('_')[0]).ThenBy(aa => aa.PierNum.Split('_')[1]).ThenBy(aa => aa.Position).ToList();

            List<SupportSModelV> supportSModelVsTemp1 = new List<SupportSModelV>();
            supportSModelVsTemp.ForEach(aa => supportSModelVsTemp1.Add(aa));

            SaveFileDialog file = new SaveFileDialog();
            file.Filter = "xlsx文件(*.xlsx)|*.xlsx";

            List<string> headers = new List<string>();
            #region 填充headers数据
            
            #endregion

            if (file.ShowDialog() == DialogResult.OK)
            {
                using (ExcelPackage package =
                                new ExcelPackage(new FileInfo(GeneralTool.GetDLLPath() + "支座数据表.xlsx")))
                {

                    ExcelWorksheet excelWorksheet = package.Workbook.Worksheets["支座信息提交"];

                    try
                    {

                        for (int i = 0; i < supportSModelVsTemp.Count; i++)
                        {
                            excelWorksheet.Cells[3 + i, 1].Value = supportSModelVsTemp[i].PierNum;
                            excelWorksheet.Cells[3 + i, 3].Value = supportSModelVsTemp[i].Position;
                            excelWorksheet.Cells[3 + i, 23].Value = supportSModelVsTemp[i].LimitDirection;

                            var cell = excelWorksheet.Cells[3 + i, 24];

                            cell.Formula = string.Format("={0}&V{1}&W{2}", "\"QZ\"", i + 3, i + 3);

                            supportSModelVsTemp[i].index = i;
                        }

                        int count = supportSModelVsTemp.Count;

                        List<Index> indices = new List<Index>();

                        for (int i = 0; i < count; )
                        {
                            string piernum = supportSModelVsTemp[i].PierNum;
                            var tempList = supportSModelVsTemp1.Where(aa => aa.PierNum == piernum).ToList();
                            int minIndex = supportSModelVsTemp1.Where(aa => aa.PierNum == piernum).Select(aa => aa.index).Min();
                            int maxIndex = supportSModelVsTemp1.Where(aa => aa.PierNum == piernum).Select(aa => aa.index).Max();
                            indices.Add(new Index()
                            {
                                startIndex = minIndex,
                                endIndex = maxIndex
                            });
                            for (int j = 0; j < tempList.Count; j++)
                            {
                                supportSModelVsTemp.Remove(tempList[j]);
                            }
                            count = supportSModelVsTemp.Count;
                        }

                        for (int i = 0; i < indices.Count; i++)
                        {
                            excelWorksheet.Cells[3 + indices[i].startIndex, 1, 3 + indices[i].endIndex, 1].Merge = true;
                            excelWorksheet.Cells[3 + indices[i].startIndex, 2, 3 + indices[i].endIndex, 2].Merge = true;
                            excelWorksheet.Cells[3 + indices[i].startIndex, 4, 3 + indices[i].endIndex, 4].Merge = true;
                        }


                        package.SaveAs(new FileInfo(file.FileName));
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }


            
        }

        private void skinDataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewColumn dataGridViewColumn = skinDataGridView1.Columns[e.ColumnIndex];
            DataGridViewCell dataGridViewCell = skinDataGridView1[e.ColumnIndex, e.RowIndex];
            int a = 0;
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {



            this.Close();
        }

        private void skinDataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                if (skinDataGridView1.Columns[e.ColumnIndex].HeaderText == "拟选支座承载力"
                    || skinDataGridView1.Columns[e.ColumnIndex].HeaderText == "限位方向")
                {
                    bridgeModelV.supportSModelVs[e.RowIndex].SupportType = "QZ" + bridgeModelV.supportSModelVs[e.RowIndex].ProposedSupportBearingCapacity
                        + bridgeModelV.supportSModelVs[e.RowIndex].LimitDirection;
                }
            }
        }

        /// <summary>
        /// 导入文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButton2_Click(object sender, EventArgs e)
        {


            

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "导入支座系统文件";
            openFileDialog.Filter = "xlsx文件(*.xlsx)|*.xlsx";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = openFileDialog.FileName;

                using (ExcelPackage package =
                                new ExcelPackage(new FileInfo(filePath)))
                {

                    ExcelWorksheet sheet = package.Workbook.Worksheets["支座信息提交"];
                    for (int i = 0; i < supportSModelVs.Count; i++)
                    {

                        String pierNum = Convert.ToString(sheet.Cells[i + 3, 1].Value).Trim();
                        int position = Convert.ToInt16(sheet.Cells[i + 3, 3].Value);

                        
                        var tempdata = bridgeModelV.supportSModelVs.Where(aa => aa.PierNum == pierNum && aa.Position == position).First();
                        int index = bridgeModelV.supportSModelVs.IndexOf(tempdata);


                        string s1 = Convert.ToString(sheet.Cells[i + 3, 5].Value).Trim();
                        bridgeModelV.supportSModelVs[index].DistanceBetweenSupportAndCenterline = Convert.ToDouble(GeneralTool.GetNum(s1));
                        s1 = Convert.ToString(sheet.Cells[i + 3, 6].Value).Trim();
                        bridgeModelV.supportSModelVs[index].DistanceBetweenSupportAndSpanline = Convert.ToDouble(GeneralTool.GetNum(s1));
                        s1 = Convert.ToString(sheet.Cells[i + 3, 20].Value).Trim();
                        bridgeModelV.supportSModelVs[index].MaximumStandardCombinationReaction = Convert.ToDouble(GeneralTool.GetNum(s1));
                        s1 = Convert.ToString(sheet.Cells[i + 3, 21].Value).Trim();
                        bridgeModelV.supportSModelVs[index].MinimumStandardCombinationReaction = Convert.ToDouble(GeneralTool.GetNum(s1));
                        s1 = Convert.ToString(sheet.Cells[i + 3, 22].Value).Trim();
                        bridgeModelV.supportSModelVs[index].ProposedSupportBearingCapacity = Convert.ToDouble(GeneralTool.GetNum(s1));
                        s1 = Convert.ToString(sheet.Cells[i + 3, 23].Value).Trim();
                        bridgeModelV.supportSModelVs[index].LimitDirection = Convert.ToString(s1);
                        s1 = Convert.ToString(sheet.Cells[i + 3, 24].Value).Trim();
                        bridgeModelV.supportSModelVs[index].SupportType = Convert.ToString(s1);

                    }
                }
            }

            

        }

        private void  ExamineSupportsData(List<SupportSModelV> supportSModels,out List<string> pierNums)
        {
            pierNums = new List<string>();
            bool isRight = true;
            List<SupportSModelV> supportSModelsTemp = new List<SupportSModelV>();
            supportSModels.ForEach(aa => supportSModelsTemp.Add(aa));
            while (isRight)
            {
                var tempList = supportSModelsTemp.Where(aa => aa.PierNum == supportSModelsTemp.First().PierNum).ToList();
                tempList = tempList.OrderBy(aa => aa.Position).ToList();
                string pierNum = tempList.First().PierNum;
                string pierName = bridgeModelV.subSModelVs.Where(aa => aa.PierNum == pierNum).First().PierName;
                string isTurn = bridgeModelV.subSModelVs.Where(aa => aa.PierNum == pierNum).First().IsTurn;
                var pierModel = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == pierName).First();
                string supportArrangement = pierModel.SupportArrangement;
                switch (supportArrangement)
                {
                    case "1排2列":
                        if (Math.Abs(tempList[0].DistanceBetweenSupportAndCenterline - tempList[1].DistanceBetweenSupportAndCenterline) != pierModel.SupportTransverseSpacing)
                        {
                            pierNums.Add(tempList[0].PierNum);
                        }
                        break;
                    case "2排2列":

                        if (Math.Abs(tempList[0].DistanceBetweenSupportAndCenterline - tempList[1].DistanceBetweenSupportAndCenterline) != pierModel.SupportTransverseSpacing)
                        {
                            pierNums.Add(tempList[0].PierNum);
                        }

                        if (Math.Abs(tempList[2].DistanceBetweenSupportAndCenterline - tempList[3].DistanceBetweenSupportAndCenterline) != pierModel.SupportTransverseSpacing)
                        {
                            pierNums.Add(tempList[0].PierNum);
                        }

                        if (tempList[0].DistanceBetweenSupportAndCenterline != tempList[2].DistanceBetweenSupportAndCenterline)
                        {
                            pierNums.Add(tempList[0].PierNum);
                        }

                        if (tempList[1].DistanceBetweenSupportAndCenterline != tempList[3].DistanceBetweenSupportAndCenterline)
                        {
                            pierNums.Add(tempList[0].PierNum);
                        }
                        break;
                    case "品字形":
                        if (isTurn == "是")
                        {
                            if (Math.Abs(tempList[2].DistanceBetweenSupportAndCenterline - tempList[1].DistanceBetweenSupportAndCenterline) != 
                                pierModel.SupportTransverseSpacing)
                            {
                                pierNums.Add(tempList[0].PierNum);
                            }

                            if (2 * tempList[0].DistanceBetweenSupportAndCenterline != tempList[2].DistanceBetweenSupportAndCenterline
                                + tempList[1].DistanceBetweenSupportAndCenterline)
                            {
                                pierNums.Add(tempList[0].PierNum);
                            }
                        }
                        else if (isTurn == "否")
                        {
                            if (Math.Abs( tempList[0].DistanceBetweenSupportAndCenterline - tempList[1].DistanceBetweenSupportAndCenterline)
                                != pierModel.SupportTransverseSpacing)
                            {
                                pierNums.Add(tempList[0].PierNum);
                            }

                            if (2 * tempList[2].DistanceBetweenSupportAndCenterline != tempList[0].DistanceBetweenSupportAndCenterline 
                                + tempList[1].DistanceBetweenSupportAndCenterline)
                            {
                                pierNums.Add(tempList[0].PierNum);
                            }
                        }

                        break;
                    default:
                        break;
                }
                tempList.ForEach(aa => supportSModelsTemp.Remove(aa));
                if (supportSModelsTemp.Count > 0)
                {
                    isRight = true;
                }
                else
                {
                    isRight = false;
                }
            }
        }

        private void skinButton4_Click(object sender, EventArgs e)
        {
            List<string> pierNums = new List<string>();
            this.ExamineSupportsData(bridgeModelV.supportSModelVs, out pierNums);
            List<int> indexs = new List<int>();
            for (int i = 0; i < pierNums.Count; i++)
            {
                var tempList = bridgeModelV.supportSModelVs.Where(aa => aa.PierNum == pierNums[i]).ToList();
                tempList.ForEach(aa => indexs.Add(bridgeModelV.supportSModelVs.IndexOf(aa)));
            }

            for (int i = 0; i < skinDataGridView1.Rows.Count; i++)
            {
                DataGridViewRow row = skinDataGridView1.Rows[i];
                row.DefaultCellStyle.BackColor = Color.White;
            }


            for (int i = 0; i < indexs.Count; i++)
            {
                var xx = skinDataGridView1[Column12.Index, indexs[i]];
                skinDataGridView1[14, indexs[i]].Style.BackColor = Color.Red;
                //skinDataGridView1.Rows[indexs[i]].DefaultCellStyle.BackColor = Color.Red;
            }
        }


        private void skinDataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (skinDataGridView1.Columns[e.ColumnIndex].HeaderText == "支座与中心线距离")
            {
                var supportModel = supportSModelVs[e.RowIndex];
                string pierNum = supportModel.PierNum;
                string pierName = bridgeModelV.subSModelVs.Where(aa => aa.PierNum == pierNum).First().PierName;
                var pierModel = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == pierName).First();
                double hxjl = pierModel.SupportTransverseSpacing;
                skinDataGridView1[e.ColumnIndex, e.RowIndex].ToolTipText = $"支座横向距离为{hxjl}";
            }
        }

        private void skinDataGridView1_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (skinDataGridView1.Columns[e.ColumnIndex].HeaderText == "支座与中心线距离")
                {
                    var supportModel = supportSModelVs[e.RowIndex];
                    string pierNum = supportModel.PierNum;
                    string pierName = bridgeModelV.subSModelVs.Where(aa => aa.PierNum == pierNum).First().PierName;
                    var pierModel = GlobalData.sourceModelV.pierSourceModelVs.Where(aa => aa.Id == pierName).First();
                    double hxjl = pierModel.SupportTransverseSpacing;
                    skinDataGridView1[e.ColumnIndex, e.RowIndex].ToolTipText = $"支座横向距离为{hxjl}";
                }
            }
        }
    }

    class Index
    {
        public int startIndex;
        public int endIndex;
    }

}
