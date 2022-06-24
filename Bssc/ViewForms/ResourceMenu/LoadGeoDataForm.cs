using Bssc.Control.Tools;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.SourceModelsV;
using Bssc.ViewForms.AffiliateForms;
using CCWin;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Geometry;
using BaseLibrary.ExtensionMethod;
using Autodesk.AutoCAD.EditorInput;

using BaseLibrary.ResultData;
using Application = Autodesk.AutoCAD.ApplicationServices.Application;
using Bssc.Control.CadControl;

namespace Bssc.ViewForms.ResourceMenu
{
    public partial class LoadGeoDataForm : Skin_Mac
    {
        /// <summary>
        /// 勘探数据总id
        /// </summary>
        string explorId;
        /// <summary>
        /// 勘探点主要数据id
        /// </summary>
        string explorationSourceId;
        /// <summary>
        /// 地层分布一览表id
        /// </summary>
        string soilLayerSourceId;
        /// <summary>
        /// 土层特性id
        /// </summary>
        string soilCharacterSourceId;
        /// <summary>
        /// 岩层特性id
        /// </summary>
        string rockCharacterSourceId;

        public LoadGeoDataForm()
        {
            InitializeComponent();
            explorId = "mainExplor" + UUIDUtil.Get32UUID();
            explorationSourceId = "explorationSource" + UUIDUtil.Get32UUID();
            soilLayerSourceId = "soilLayerSource" + UUIDUtil.Get32UUID();
            soilCharacterSourceId = "soilCharacterSource" + UUIDUtil.Get32UUID();
            rockCharacterSourceId = "rockCharacterSource" + UUIDUtil.Get32UUID();

            if (GlobalData.sourceModelV.explorSourceModelV.explorationSourceModelVs != null)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = GlobalData.sourceModelV.explorSourceModelV.explorationSourceModelVs;
                dataGridView1.DataSource = bs;
            }

            if (GlobalData.sourceModelV.explorSourceModelV.soiLayerSourceModelVs != null)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = GlobalData.sourceModelV.explorSourceModelV.soiLayerSourceModelVs;
                skinDataGridView1.DataSource = bs;
            }

            if (GlobalData.sourceModelV.explorSourceModelV.soilCharacterSourceModelVs != null)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = GlobalData.sourceModelV.explorSourceModelV.soilCharacterSourceModelVs;
                skinDataGridView2.DataSource = bs;
            }

            if (GlobalData.sourceModelV.explorSourceModelV.soilCharacterSourceModelVs != null)
            {
                BindingSource bs = new BindingSource();
                bs.DataSource = GlobalData.sourceModelV.explorSourceModelV.rockCharacterSourceModelVs;
                skinDataGridView3.DataSource = bs;
            }

        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            skinTabControl1.SelectedTab = skinTabPage1;
            try
            {
                ExplorSourceModelV explorsourcemodelv = GlobalData.sourceModelV.explorSourceModelV;
                
                System.Windows.Forms.OpenFileDialog dialog1 = new System.Windows.Forms.OpenFileDialog();
                dialog1.Title = "打开勘探孔数据文件";
                dialog1.Filter = "勘探孔数据文件|*.xlsx";
                string sourceFileName;
                if (dialog1.ShowDialog() == DialogResult.OK)
                {
                    sourceFileName = dialog1.FileName;
                    explorsourcemodelv.explorationSourceModelVs.Clear();
                }
                else
                {
                    return;
                }

                GlobalData.projectModelV.roadModelVs.ForEach(
                    aa => aa.bridgeModelVs.ForEach(
                        bb => bb.subSModelVs.ForEach(cc => cc.HoleNum = null)));

                using (ExcelPackage package = new ExcelPackage(new FileInfo(sourceFileName)))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[1];

                    for (int i = 7; i < 99999; i++)
                    {
                        if (Convert.ToString(sheet.Cells[i, 1].Value) != "")
                        {
                            ExplorationSourceModelV explorationSourceModelV = new ExplorationSourceModelV()
                            {
                                Id = explorationSourceId,
                                UnitId = "unitExplorationSource" + UUIDUtil.Get32UUID(),
                                Serial = Convert.ToInt16(sheet.Cells[i, 1].Value),
                                Num = Convert.ToString(sheet.Cells[i, 2].Value),
                                Type = Convert.ToString(sheet.Cells[i, 3].Value),
                                PointX = Convert.ToString(sheet.Cells[i, 4].Value),
                                PointY = Convert.ToString(sheet.Cells[i, 5].Value),
                                Elevation = Convert.ToString(sheet.Cells[i, 6].Value),
                                HoleDepth = Convert.ToString(sheet.Cells[i, 7].Value),
                                GroundwaterStableWaterLevelDepth = Convert.ToString(sheet.Cells[i, 8].Value),
                                GroundwaterStableWaterLevelElevation = Convert.ToString(sheet.Cells[i, 9].Value),
                                SoilCharacter = "…"
                            };

                            explorsourcemodelv.explorationSourceModelVs.Add(explorationSourceModelV);

                        }
                        else
                        {
                            break;
                        }
                    }
                }
                skinButton1.BackColor = Color.Red;
                dataGridView1.DataSource = explorsourcemodelv.explorationSourceModelVs;
            }
            catch (System.Exception)
            {
                AlertForm alertForm = new AlertForm();
                alertForm.skinTextBox1.Text = "请检查表格是否有误";
                alertForm.Show();
                return;
            }
        }

        private void skinButton2_Click(object sender, EventArgs e)
        {
            ExplorSourceModelV explorsourcemodelv = GlobalData.sourceModelV.explorSourceModelV;
            if (explorsourcemodelv.explorationSourceModelVs.Count <= 0)
            {
                AlertForm alertForm = new AlertForm();
                alertForm.skinTextBox1.Text = "请先导入勘探点数据";
                alertForm.Show();
                return;
            }
            skinTabControl1.SelectedTab = skinTabPage2;
            try
            {
                
                
                System.Windows.Forms.OpenFileDialog dialog1 = new System.Windows.Forms.OpenFileDialog();
                dialog1.Title = "打开地层分布数据文件";
                dialog1.Filter = "地层分布数据文件|*.xlsx";
                string sourceFileName;

                if (dialog1.ShowDialog() == DialogResult.OK)
                {
                    sourceFileName = dialog1.FileName;
                    explorsourcemodelv.soiLayerSourceModelVs.Clear();
                }
                else
                {
                    return;
                }

                using (ExcelPackage package = new ExcelPackage(new FileInfo(sourceFileName)))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                    for (int i = 4; i < 99999; i++)
                    {
                        if (Convert.ToString(sheet.Cells[i, 1].Value) != "")
                        {
                            SoiLayerSourceModelV soiLayerSourceModelV = new SoiLayerSourceModelV()
                            {
                                Id = soilLayerSourceId,
                                Serial = i,
                                UnitId = "unitSoilLayerCharacter" + UUIDUtil.Get32UUID(),
                                SoilLayerNum = Convert.ToString(sheet.Cells[i, 1].Value),
                                Designation = Convert.ToString(sheet.Cells[i, 2].Value),
                                HoleNum = Convert.ToString(sheet.Cells[i, 3].Value),
                                UnitExplorationUnitId = (explorsourcemodelv.explorationSourceModelVs.Where(aa => aa.Num == Convert.ToString(sheet.Cells[i, 3].Value))).FirstOrDefault().UnitId,
                                TopLayerDepth = Convert.ToString(sheet.Cells[i, 4].Value),
                                TopLayerElevation = Convert.ToString(sheet.Cells[i, 5].Value),
                                BottomLayerDepth = Convert.ToString(sheet.Cells[i, 6].Value),
                                BottomLayerElevation = Convert.ToString(sheet.Cells[i, 7].Value),
                                LayerThickness = Convert.ToString(sheet.Cells[i, 8].Value),
                                CoringRate = Convert.ToString(sheet.Cells[i, 9].Value),
                                Rqd = Convert.ToString(sheet.Cells[i, 10].Value)
                            };
                            explorsourcemodelv.soiLayerSourceModelVs.Add(soiLayerSourceModelV);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                skinButton2.BackColor = Color.Red;
                skinDataGridView1.DataSource = explorsourcemodelv.soiLayerSourceModelVs;

            }
            catch (System.Exception)
            {
                AlertForm alertForm = new AlertForm();
                alertForm.skinTextBox1.Text = "请检查表格是否有误";
                alertForm.Show();
                return;
            }
        }

        private void skinButton3_Click(object sender, EventArgs e)
        {
            skinTabControl1.SelectedTab = skinTabPage3;
            ExplorSourceModelV explorsourcemodelv = GlobalData.sourceModelV.explorSourceModelV;
            try
            {
                
                System.Windows.Forms.OpenFileDialog dialog1 = new System.Windows.Forms.OpenFileDialog();
                dialog1.Title = "打开土层特征分布数据文件";
                dialog1.Filter = "土层特征数据文件|*.xlsx";
                string sourceFileName;

                if (dialog1.ShowDialog() == DialogResult.OK)
                {
                    sourceFileName = dialog1.FileName;
                    explorsourcemodelv.soilCharacterSourceModelVs.Clear();
                    explorsourcemodelv.rockCharacterSourceModelVs.Clear();
                }
                else
                {
                    return;
                }

                using (ExcelPackage package = new ExcelPackage(new FileInfo(sourceFileName)))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                    for (int i = 2; i < 99999; i++)
                    {
                        if (Convert.ToString(sheet.Cells[i, 1].Value) != "")
                        {
                            SoilCharacterSourceModelV soilCharacterSourceModelV = new SoilCharacterSourceModelV()
                            {
                                Id = soilCharacterSourceId,
                                UnitId = "UnitsoilCharacterSourceId" + UUIDUtil.Get32UUID(),
                                Serial = Convert.ToString(sheet.Cells[i, 1].Value),
                                SoilLayerIndexName = Convert.ToString(sheet.Cells[i, 2].Value),
                                SoilCharacterDesc = Convert.ToString(sheet.Cells[i, 3].Value),
                                TopLayerElevation = Convert.ToString(sheet.Cells[i, 4].Value),
                                SoilNaturalWeight = Convert.ToString(sheet.Cells[i, 5].Value),
                                SoilSaturationWeight = Convert.ToString(sheet.Cells[i, 6].Value),
                                IsPermeable = Convert.ToString(sheet.Cells[i, 7].Value),
                                CompressionModulus = Convert.ToString(sheet.Cells[i, 8].Value),
                                LateraResistanceElasticProportionalityCoefficient = Convert.ToString(sheet.Cells[i, 9].Value),
                                VerticalResistanceElasticProportionalityCoefficient = Convert.ToString(sheet.Cells[i, 10].Value),
                                FrictionAngle = Convert.ToString(sheet.Cells[i, 11].Value),
                                StandardValueOfFrictionBetweenSoilLayerAndPileSide = Convert.ToString(sheet.Cells[i, 12].Value),
                                BasicAllowableValueOfBearingCapacity = Convert.ToString(sheet.Cells[i, 13].Value),
                                IsSoftSoil = Convert.ToString(sheet.Cells[i, 14].Value),
                                BaseWidthCorrectionFactor = Convert.ToString(sheet.Cells[i, 15].Value),
                                BaseDepthCorrectionFactor = Convert.ToString(sheet.Cells[i, 16].Value),
                                UpperLimitOfAllowableBearingCapacityInPileTipDiagram = Convert.ToString(sheet.Cells[i, 17].Value)
                            };
                            explorsourcemodelv.soilCharacterSourceModelVs.Add(soilCharacterSourceModelV);

                            RockCharacterSourceModelV rockCharacterSourceModelV = new RockCharacterSourceModelV()
                            {
                                Id = rockCharacterSourceId,
                                UnitId = "unitrockCharacterSourceId" + UUIDUtil.Get32UUID(),
                                Serial = Convert.ToString(sheet.Cells[i, 1].Value),
                                RockLayerIndexName = Convert.ToString(sheet.Cells[i, 2].Value),
                                RockCharacterDesc = Convert.ToString(sheet.Cells[i, 3].Value),
                                TopLayereElevation = Convert.ToString(sheet.Cells[i, 4].Value),
                                BasicAllowableValueOfBearingCapacity = Convert.ToString(sheet.Cells[i, 17].Value),
                                SaturatedUniaxialCompressiveStrengthStandardValue = Convert.ToString(sheet.Cells[i, 18].Value),
                                GroundResistanceCoefficient = Convert.ToString(sheet.Cells[i, 19].Value),
                                BottomResistanceOperateCoefficient = Convert.ToString(sheet.Cells[i, 20].Value),
                                LateralResistanceOperateCoefficient = Convert.ToString(sheet.Cells[i, 21].Value)
                            };
                            explorsourcemodelv.rockCharacterSourceModelVs.Add(rockCharacterSourceModelV);

                        }
                        else
                        {
                            break;
                        }
                    }
                }
                skinDataGridView2.DataSource = explorsourcemodelv.soilCharacterSourceModelVs;
                skinButton3.BackColor = Color.Red;
            }
            catch (System.Exception)
            {

                AlertForm alertForm = new AlertForm();
                alertForm.skinTextBox1.Text = "请检查表格是否有误";
                alertForm.Show();
                return;
            }
        }

        private void skinButton4_Click(object sender, EventArgs e)
        {
            skinTabControl1.SelectedTab = skinTabPage4;
            ExplorSourceModelV explorsourcemodelv = GlobalData.sourceModelV.explorSourceModelV;
            try
            {
                
                System.Windows.Forms.OpenFileDialog dialog1 = new System.Windows.Forms.OpenFileDialog();
                dialog1.Title = "打开岩层特征分布数据文件";
                dialog1.Filter = "岩层特征数据文件|*.xlsx";
                string sourceFileName;

                if (dialog1.ShowDialog() == DialogResult.OK)
                {
                    sourceFileName = dialog1.FileName;
                    explorsourcemodelv.rockCharacterSourceModelVs.Clear();
                }
                else
                {
                    return;
                }


                using (ExcelPackage package = new ExcelPackage(new FileInfo(sourceFileName)))
                {
                    ExcelWorksheet sheet = package.Workbook.Worksheets[1];
                    for (int i = 2; i < 99999; i++)
                    {
                        if (Convert.ToString(sheet.Cells[i, 1].Value) != "")
                        {
                            RockCharacterSourceModelV rockCharacterSourceModelV = new RockCharacterSourceModelV()
                            {
                                Id = rockCharacterSourceId,
                                UnitId = "unitrockCharacterSourceId" + UUIDUtil.Get32UUID(),
                                Serial = Convert.ToString(sheet.Cells[i, 1].Value),
                                RockLayerIndexName = Convert.ToString(sheet.Cells[i, 2].Value),
                                RockCharacterDesc = Convert.ToString(sheet.Cells[i, 3].Value),
                                TopLayereElevation = Convert.ToString(sheet.Cells[i, 4].Value),
                                BasicAllowableValueOfBearingCapacity = Convert.ToString(sheet.Cells[i, 5].Value),
                                SaturatedUniaxialCompressiveStrengthStandardValue = Convert.ToString(sheet.Cells[i, 6].Value),
                                GroundResistanceCoefficient = Convert.ToString(sheet.Cells[i, 7].Value),
                                BottomResistanceOperateCoefficient = Convert.ToString(sheet.Cells[i, 8].Value),
                                LateralResistanceOperateCoefficient = Convert.ToString(sheet.Cells[i, 9].Value)
                            };
                            explorsourcemodelv.rockCharacterSourceModelVs.Add(rockCharacterSourceModelV);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                skinDataGridView3.DataSource = explorsourcemodelv.rockCharacterSourceModelVs;
            }
            catch (System.Exception)
            {

                AlertForm alertForm = new AlertForm();
                alertForm.skinTextBox1.Text = "请检查表格是否有误";
                alertForm.Show();
                return;
            }
        }

        private void skinButton5_Click(object sender, EventArgs e)
        {
            ExplorSourceModelV explorsourcemodelv = GlobalData.sourceModelV.explorSourceModelV;

            Application.DocumentManager.MdiActiveDocument.PlotExplorationHole(explorsourcemodelv.explorationSourceModelVs);
        }
    }
}
