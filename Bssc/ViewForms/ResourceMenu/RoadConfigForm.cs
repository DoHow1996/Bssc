using Autodesk.AutoCAD.Geometry;
using Bssc.Control.CadControl;
using Bssc.Control.DataControl;
using Bssc.Control.Tools;
using Bssc.Control.Tools.TreeviewContextMenu;
using Bssc.Models.ModelsV;
using Bssc.Models.ModelsV.SourceModelsV;
using Bssc.ViewForms.AffiliateForms;
using BSSC.Models;
using CCWin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.ViewForms.ResourceMenu
{
    public partial class RoadConfigForm : Skin_Mac
    {

        public int openStatus;

        public TreeNode node;

        public List<PqxSourceModelV> pqxSourceModelVs = new List<PqxSourceModelV>();
        public List<SqxSourceModelV> sqxSourceModelVs = new List<SqxSourceModelV>();
        public List<DmxSourceModelV> dmxSourceModelVs = new List<DmxSourceModelV>();
        public List<CgxSourceModelV> cgxSourceModelVs = new List<CgxSourceModelV>();

        public string roadId;
        public string pqxId;
        public string sqxId;
        public string dmxId;
        public string cgxId;

        public RoadConfigForm(int openStatus,TreeNode node)
        {
            InitializeComponent();
            this.openStatus = openStatus;
            this.node = node;
            GlobalInitialize();
        }

        private void GlobalInitialize()
        {

            var roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();

            if (roadSourceModelV == null)
            {
                roadSourceModelV = new RoadSourceModelV();
                GlobalData.sourceModelV.roadSourceModelVs.Add(roadSourceModelV);
            }


            pqxSourceModelVs = roadSourceModelV.pqxSourceModelVs;
            sqxSourceModelVs = roadSourceModelV.sqxSourceModelVs;
            dmxSourceModelVs = roadSourceModelV.dmxSourceModelVs;
            cgxSourceModelVs = roadSourceModelV.cgxSourceModelVs;

            if (openStatus == 0)
            {
                this.Text = "新增路线资源数据";

                roadId ="roadline" + UUIDUtil.Get32UUID();
                pqxId = "pqx" + UUIDUtil.Get32UUID();
                sqxId = "sqx" + UUIDUtil.Get32UUID();
                dmxId = "dmx" + UUIDUtil.Get32UUID();
                cgxId = "cgx" + UUIDUtil.Get32UUID();
            }
            else if (openStatus == 1)
            {
                this.Text = "修改路线数据";
                roadId = roadSourceModelV.Id;
                pqxId = roadSourceModelV.pqxSourceModelVs.Count > 0 ? roadSourceModelV.pqxSourceModelVs[0].Id : "pqx" + UUIDUtil.Get32UUID();
                sqxId = roadSourceModelV.sqxSourceModelVs.Count > 0 ? roadSourceModelV.sqxSourceModelVs[0].Id : "sqx" + UUIDUtil.Get32UUID();
                dmxId = roadSourceModelV.dmxSourceModelVs.Count > 0 ? roadSourceModelV.dmxSourceModelVs[0].Id : "dmx" + UUIDUtil.Get32UUID();
                cgxId = roadSourceModelV.cgxSourceModelVs.Count > 0 ? roadSourceModelV.cgxSourceModelVs[0].Id : "cgx" + UUIDUtil.Get32UUID();
                skinTextBoxName.Text = roadSourceModelV.Designation;
                skinTextBoxPqx.Text = roadSourceModelV.PqxFileName;
                skinTextBoxSqx.Text = roadSourceModelV.SqxFileName;
                skinTextBoxDmx.Text = roadSourceModelV.DmxFileName;
                skinTextBoxCgx.Text = roadSourceModelV.CgxFileName;
                skinTextBoxCrossSlpoe.Text = Convert.ToString(roadSourceModelV.CrossSlope);
            }
        }
        /// <summary>
        /// 导入平曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButtonPqx_Click(object sender, EventArgs e)
        {
            pqxSourceModelVs.Clear();
            //List<PqxSourceModelV> pqxSourceModelVs = new List<PqxSourceModelV>();

            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择辅道平曲线文件";
            dialog.Filter = "平曲线文件|*.icd";
            string  fileName = "未导入平曲线文件";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                fileName = dialog.FileName;
            }
            else
            {
                return ;
            }

            skinTextBoxPqx.Text = fileName;
            string[] lines = RoadDataHandle.getTxtData(fileName);
            string[] strs;
            int inrow = 1;

            for (int i = 0; i < lines.Length; i++)
            {
                strs = lines[i].Split(',');
                if (strs[0] != "0")
                {
                    PqxSourceModelV pqxModel = new PqxSourceModelV();
                    pqxModel.Id = pqxId;
                    pqxModel.CurveId = "pqxCurve" + Guid.NewGuid().ToString();
                    pqxModel.Serial = i;
                    pqxSourceModelVs.Add(pqxModel);

                    if (i == 0)
                    {
                        pqxModel = new PqxSourceModelV();
                        pqxModel.Id = pqxId;
                        pqxModel.CurveId = "pqxCurve" + Guid.NewGuid().ToString();
                        pqxModel.Serial = i + 1;
                        pqxSourceModelVs.Add(pqxModel);

                        pqxSourceModelVs[1].StartMark = Convert.ToDouble(strs[0]);
                        pqxSourceModelVs[0].EndMark = Convert.ToDouble(strs[0]);
                    }
                    if (i == 1)
                    {
                        pqxSourceModelVs[1].StartX = Convert.ToDouble(strs[0]);
                        pqxSourceModelVs[1].StartY = Convert.ToDouble(strs[1]);
                        pqxSourceModelVs[1].StartAngle = Convert.ToDouble(strs[2]);

                    }

                    if (i > 1)
                    {
                        switch (strs[0])
                        {
                            case "0":

                                pqxSourceModelVs[inrow].UnitType = Convert.ToInt16(strs[0]);

                                inrow = inrow + 1;
                                break;
                            case "1":

                                pqxSourceModelVs[inrow].UnitType = Convert.ToInt16(strs[0]);
                                pqxSourceModelVs[inrow].UnitLength = Convert.ToDouble(strs[1]);
                                pqxSourceModelVs[inrow].EndMark = pqxSourceModelVs[inrow - 1].EndMark + pqxSourceModelVs[inrow].UnitLength;

                                inrow = inrow + 1;
                                break;
                            case "2":

                                pqxSourceModelVs[inrow].UnitType = Convert.ToInt16(strs[0]);
                                pqxSourceModelVs[inrow].StartRadius = Convert.ToDouble(strs[1]);
                                pqxSourceModelVs[inrow].EndRadius = Convert.ToDouble(strs[1]);
                                pqxSourceModelVs[inrow].UnitTurn = Convert.ToInt16(strs[3]);
                                pqxSourceModelVs[inrow].UnitLength = Convert.ToDouble(strs[2]);
                                pqxSourceModelVs[inrow].EndMark = pqxSourceModelVs[inrow - 1].EndMark + pqxSourceModelVs[inrow].UnitLength;

                                inrow = inrow + 1;
                                break;
                            case "3":

                                pqxSourceModelVs[inrow].UnitType = Convert.ToInt16(strs[0]);
                                pqxSourceModelVs[inrow].StartRadius = 0;
                                pqxSourceModelVs[inrow].EndRadius = Convert.ToDouble(strs[2]);
                                pqxSourceModelVs[inrow].UnitTurn = Convert.ToInt16(strs[3]);
                                pqxSourceModelVs[inrow].UnitA = Convert.ToDouble(strs[1]);
                                pqxSourceModelVs[inrow].UnitLength = RoadDataHandle.AtoL(pqxSourceModelVs[inrow].StartRadius,
                                    pqxSourceModelVs[inrow].EndRadius, pqxSourceModelVs[inrow].UnitA);
                                pqxSourceModelVs[inrow].EndMark = pqxSourceModelVs[inrow - 1].EndMark + pqxSourceModelVs[inrow].UnitLength;

                                inrow = inrow + 1;
                                break;
                            case "4":

                                pqxSourceModelVs[inrow].UnitType = Convert.ToInt16(strs[0]);
                                pqxSourceModelVs[inrow].StartRadius = Convert.ToDouble(strs[2]);
                                pqxSourceModelVs[inrow].EndRadius = 0;
                                pqxSourceModelVs[inrow].UnitTurn = Convert.ToInt16(strs[3]);
                                pqxSourceModelVs[inrow].UnitA = Convert.ToDouble(strs[1]);
                                pqxSourceModelVs[inrow].UnitLength = RoadDataHandle.AtoL(pqxSourceModelVs[inrow].StartRadius,
                                    pqxSourceModelVs[inrow].EndRadius, pqxSourceModelVs[inrow].UnitA);
                                pqxSourceModelVs[inrow].EndMark = pqxSourceModelVs[inrow - 1].EndMark + pqxSourceModelVs[inrow].UnitLength;

                                inrow = inrow + 1;
                                break;
                            case "5":

                                pqxSourceModelVs[inrow].UnitType = Convert.ToInt16(strs[0]);
                                pqxSourceModelVs[inrow].StartRadius = Convert.ToDouble(strs[2]);
                                pqxSourceModelVs[inrow].EndRadius = Convert.ToDouble(strs[3]);
                                pqxSourceModelVs[inrow].UnitTurn = Convert.ToInt16(strs[4]);
                                pqxSourceModelVs[inrow].UnitA = Convert.ToDouble(strs[1]);

                                pqxSourceModelVs[inrow].UnitLength = RoadDataHandle.AtoL(pqxSourceModelVs[inrow].StartRadius,
                                    pqxSourceModelVs[inrow].EndRadius, pqxSourceModelVs[inrow].UnitA);
                                pqxSourceModelVs[inrow].EndMark = pqxSourceModelVs[inrow - 1].EndMark + pqxSourceModelVs[inrow].UnitLength;

                                inrow = inrow + 1;
                                break;
                            case "6":

                                pqxSourceModelVs[inrow].UnitType = Convert.ToInt16(strs[0]);
                                pqxSourceModelVs[inrow].StartRadius = Convert.ToDouble(strs[2]);
                                pqxSourceModelVs[inrow].EndRadius = Convert.ToDouble(strs[3]);
                                pqxSourceModelVs[inrow].UnitTurn = Convert.ToInt16(strs[4]);
                                pqxSourceModelVs[inrow].UnitA = Convert.ToDouble(strs[1]);

                                pqxSourceModelVs[inrow].UnitLength = RoadDataHandle.AtoL(pqxSourceModelVs[inrow].StartRadius,
                                    pqxSourceModelVs[inrow].EndRadius, pqxSourceModelVs[inrow].UnitA);
                                pqxSourceModelVs[inrow].EndMark = pqxSourceModelVs[inrow - 1].EndMark + pqxSourceModelVs[inrow].UnitLength;

                                inrow = inrow + 1;
                                break;
                        }
                    }
                }

            }

            pqxSourceModelVs.RemoveAt(0);
            RoadDataHandle.xyfqx(pqxSourceModelVs);

        }

        /// <summary>
        /// 导入竖曲线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButtonSqx_Click(object sender, EventArgs e)
        {
            sqxSourceModelVs.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择地道竖曲线文件";
            dialog.Filter = "地道竖曲线文件|*.sqx";
            string filename = "";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = dialog.FileName;
                this.skinButtonSqx.ForeColor = Color.Red;
            }
            else
            {
                return;
            }

            skinTextBoxSqx.Text = filename;

            string[] lines = RoadDataHandle.getTxtData(filename);
            string[] strs;

            string[,] data = new string[lines.Length, 3];

            for (int i = 0; i < lines.Length; i++)
            {
                strs = System.Text.RegularExpressions.Regex.Split(lines[i], @"\s{1,}");
                data[i, 0] = strs[0];
                data[i, 1] = strs[1];

                if (strs.Length == 3)
                {
                    data[i, 2] = strs[2];
                }

            }

            for (int i = 0; i < lines.Length; i++)
            {
                sqxSourceModelVs.Add(new SqxSourceModelV());
                strs = System.Text.RegularExpressions.Regex.Split(lines[i], @"\s{1,}");
                double zpi1; double zpi2; double quxc; double qexc; double wj; double zyzh; double yzzh; double r;
                double zh_p = 0; double zh_c = 0; double zh_n = 0;
                double gc_p = 0; double gc_c = 0; double gc_n = 0;
                double r_p = 0; double r_c = 0; double r_n = 0;
                for (int j = 0; j < strs.Length; j++)
                {
                    if (strs.Length == 2)
                    {
                        sqxSourceModelVs[i].Id = sqxId;
                        sqxSourceModelVs[i].CurveId= "sqxCurve" + UUIDUtil.Get32UUID();
                        sqxSourceModelVs[i].Serial = i + 1;
                        sqxSourceModelVs[i].GradientChangePointMark = Convert.ToDouble(data[i, 0]);
                        sqxSourceModelVs[i].GradientChangePointElevation = Convert.ToDouble(data[i, 1]);

                    }
                    else if (strs.Length == 3)
                    {

                        zh_p = Convert.ToDouble(data[i - 1, 0]); zh_c = Convert.ToDouble(data[i, 0]); zh_n = Convert.ToDouble(data[i + 1, 0]);
                        gc_p = Convert.ToDouble(data[i - 1, 1]); gc_c = Convert.ToDouble(data[i, 1]); gc_n = Convert.ToDouble(data[i + 1, 1]);
                        r_p = Convert.ToDouble(data[i - 1, 2]); r_c = Convert.ToDouble(data[i, 2]); r_n = Convert.ToDouble(data[i + 1, 2]);
                        if (r_c == 0)
                        {
                            r = 0.000000001;
                        }
                        else
                        {
                            r = r_c;
                        }

                        zpi1 = (gc_c - gc_p) / (zh_c - zh_p) * 100;
                        zpi2 = (gc_n - gc_c) / (zh_n - zh_c) * 100;
                        quxc = r * Math.Abs(zpi2 - zpi1) / 100;
                        qexc = quxc / 2;
                        wj = Math.Pow(qexc, 2) / 2 / r;
                        zyzh = zh_c - qexc;
                        yzzh = zyzh + quxc;

                        sqxSourceModelVs[i].Id = sqxId;
                        sqxSourceModelVs[i].CurveId = "sqxCurve" + UUIDUtil.Get32UUID();
                        sqxSourceModelVs[i].Serial = i + 1;
                        sqxSourceModelVs[i].GradientChangePointMark = Convert.ToDouble(data[i, 0]);
                        sqxSourceModelVs[i].GradientChangePointElevation = Convert.ToDouble(data[i, 1]);
                        sqxSourceModelVs[i].DesignCurveRadius = Convert.ToDouble(data[i, 2]);
                        sqxSourceModelVs[i].LongitudinalSlopeI1 = zpi1;
                        sqxSourceModelVs[i].LongitudinalSlopeI2 = zpi2;
                        sqxSourceModelVs[i].CurveLength = quxc;
                        sqxSourceModelVs[i].TangentLength = qexc;
                        sqxSourceModelVs[i].OuterDistance = wj;
                        sqxSourceModelVs[i].StraightCircleMark = zyzh;
                        sqxSourceModelVs[i].CircleStraightMark = yzzh;


                    }
                }
            }
        }
        /// <summary>
        /// 导入地面线
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void skinButtonDmx_Click(object sender, EventArgs e)
        {
            dmxSourceModelVs.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择DMX文件";
            dialog.Filter = "DMX文件|*.DMX";
            string filename = "";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = dialog.FileName;
                this.skinButtonDmx.ForeColor = Color.Red;
            }
            else
            {
                return;
            }

            skinTextBoxDmx.Text = filename;

            string[] lines = RoadDataHandle.getTxtData(filename);
            string[] strs;

            for (int i = 0; i < lines.Length; i++)
            {
                dmxSourceModelVs.Add(new DmxSourceModelV());
                strs = System.Text.RegularExpressions.Regex.Split(lines[i], @"\s{1,}");
                for (int j = 0; j < strs.Length; j++)
                {

                    dmxSourceModelVs[i].Id = dmxId;
                    dmxSourceModelVs[i].CurveId = "dmxCurve" + UUIDUtil.Get32UUID();
                    dmxSourceModelVs[i].Serial = i + 1;
                    dmxSourceModelVs[i].Mark = Convert.ToDouble(strs[0]);
                    dmxSourceModelVs[i].Elevation = Convert.ToDouble(strs[1]);
                    if (strs.Length == 3)
                    {
                        dmxSourceModelVs[i].Radius = Convert.ToDouble(strs[2]);
                    }

                }
            }

        }

        private void skinButton5_Click(object sender, EventArgs e)
        {
            if (skinTextBoxName.Text.Contains(" "))
            {
                MessageBox.Show("路线名称中含有空格，请清除空格!");
                return;
            }
            if (!node.hasName(skinTextBoxName.Text,openStatus))
            {
                AlertForm alertForm = new AlertForm();
                alertForm.skinTextBox1.Text = "路线数据名已存在";
                alertForm.Show();
                return;
            }

            if (openStatus == 0)
            {
                RoadSourceModelV roadSourceModelV = new RoadSourceModelV();
                roadSourceModelV.Id = roadId;
                roadSourceModelV.Designation = skinTextBoxName.Text;
                roadSourceModelV.PqxFileName = skinTextBoxPqx.Text;
                roadSourceModelV.SqxFileName = skinTextBoxSqx.Text;
                roadSourceModelV.DmxFileName = skinTextBoxDmx.Text;
                roadSourceModelV.CgxFileName = skinTextBoxCgx.Text;
                roadSourceModelV.CrossSlope = Convert.ToDouble(skinTextBoxCrossSlpoe.Text);
                roadSourceModelV.sqxSourceModelVs = sqxSourceModelVs;
                roadSourceModelV.pqxSourceModelVs = pqxSourceModelVs;
                roadSourceModelV.dmxSourceModelVs = dmxSourceModelVs;
                roadSourceModelV.cgxSourceModelVs = cgxSourceModelVs;
                GlobalData.sourceModelV.roadSourceModelVs.Add(roadSourceModelV);

                TreeNode childNode = new TreeNode();
                node.Nodes.Add(childNode);
                childNode.Name = roadId;
                childNode.Text = skinTextBoxName.Text;
                ContextMenuWrapForSingleSourceNode context = new ContextMenuWrapForSingleSourceNode(childNode);
            }
            else if (openStatus == 1)
            {
                var roadSourceModelV = GlobalData.sourceModelV.roadSourceModelVs.Where(aa => aa.Id == node.Name).FirstOrDefault();
                roadSourceModelV.Designation = skinTextBoxName.Text;
                roadSourceModelV.PqxFileName = skinTextBoxPqx.Text;
                roadSourceModelV.SqxFileName = skinTextBoxSqx.Text;
                roadSourceModelV.DmxFileName = skinTextBoxDmx.Text;
                roadSourceModelV.CgxFileName = skinTextBoxCgx.Text;
                roadSourceModelV.CrossSlope  = Convert.ToDouble(skinTextBoxCrossSlpoe.Text);
                roadSourceModelV.sqxSourceModelVs = sqxSourceModelVs;
                roadSourceModelV.pqxSourceModelVs = pqxSourceModelVs;
                roadSourceModelV.dmxSourceModelVs = dmxSourceModelVs;
                roadSourceModelV.cgxSourceModelVs = cgxSourceModelVs;

                var aaa = GlobalData.sourceModelV.roadSourceModelVs;

                node.Text = skinTextBoxName.Text;
            }
            this.Close();
        }

        private void skinButton6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void skinButton1_Click(object sender, EventArgs e)
        {
            GlobalData.sourceModelV.roadSourceModelVs[0].pqxSourceModelVs.CreatePqx();
        }

        private void skinLabel1_Click(object sender, EventArgs e)
        {
            showdatagridview showdDgv = new showdatagridview(GlobalData.sourceModelV.roadSourceModelVs[0].pqxSourceModelVs);
            showdDgv.Show();
        }

        private void skinButtonCgx_Click(object sender, EventArgs e)
        {
            cgxSourceModelVs.Clear();
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Title = "请选择超高线文件";
            dialog.Filter = "超高线文件|*.cg";
            string filename = "";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = dialog.FileName;
                this.skinButtonSqx.ForeColor = Color.Red;
            }
            else
            {
                return;
            }

            skinTextBoxCgx.Text = filename;


            string[] lines = RoadDataHandle.getTxtData(filename);

            List<string> strs = new List<string>();
            if (lines[0].Contains(' '))
            {
                strs = lines[0].Split(' ').ToList();
            }
            else if (lines[0].Contains('\t'))
            {
                strs = lines[0].Split('\t').ToList();
            }

            for (int i = 1; i < lines.Length; i++)
            {
                List<string> strs1 = new List<string>();;
                if (lines[i].Contains(' '))
                {
                    strs1 = lines[i].Split(' ').ToList();
                }
                else if (lines[i].Contains('\t'))
                {
                    strs1 = lines[i].Split('\t').ToList();
                }
                CgxSourceModelV cgxSourceModelV = new CgxSourceModelV()
                {
                    Id = cgxId,
                    CurveId = "cgxCurve" + UUIDUtil.Get32UUID(),
                    Symbol = Convert.ToInt16(strs[0]),
                    Dtl = Convert.ToDouble(strs[1]),
                    Dtr = Convert.ToDouble(strs[2]),
                    Is1 = Convert.ToDouble(strs[3]),
                    Jd1 = Convert.ToInt16(strs[4]),
                    Axi = Convert.ToDouble(strs[5]),
                    Jd2 = Convert.ToInt16(strs[6]),
                    Ismax1 = Convert.ToDouble(strs[7]),
                    Ismax2 = Convert.ToDouble(strs[8]),
                    St = Convert.ToDouble(strs1[0]),
                    Cgl = Convert.ToDouble(strs1[1]),
                    Cgr = Convert.ToDouble(strs1[2])
                };
                cgxSourceModelVs.Add(cgxSourceModelV);
            }


        }
    }
}
