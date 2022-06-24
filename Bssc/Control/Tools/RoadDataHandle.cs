using Bssc.Models.ModelsV.SourceModelsV;
using BSSC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.Tools
{
    class RoadDataHandle
    {

        /// <summary>
        /// 获取txt文档的数据
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns>字符串数组</returns>
        public static string[] getTxtData(string path)
        {

            string line = string.Empty;
            List<string> lines = new List<string>();

            FileStream file = new FileStream(path, FileMode.Open);
            using (StreamReader reader = new StreamReader(file))
            {
                line = reader.ReadLine();

                line = line.Trim();

                while (line != "" && line != null)
                {
                    lines.Add(line);
                    line = reader.ReadLine();
                }
            }

            return lines.ToArray();

        }




        #region 测试
        /// <summary>
        /// 我也不知道啥用，
        /// </summary>
        /// <param name="r1"></param>
        /// <param name="r2"></param>
        /// <param name="a1"></param>
        /// <returns></returns>
        public static double AtoL(double r1, double r2, double a1)
        {
            if (r1 == 0)
            {
                r1 = Math.Pow(10, 45);
            }
            if (r2 == 0)
            {
                r2 = Math.Pow(10, 45);
            }
            return Math.Abs(a1 * a1 / r1 - a1 * a1 / r2);
        }


        public static void xyfqx(List<PqxSourceModelV> pqxDatas)
        {
            int i = 0; int j = 1;

            while (pqxDatas[j - 1].EndMark != 0)
            {
                pqxDatas[j].StartMark = pqxDatas[j - 1].EndMark;
                pqxDatas[j].StartX = xyf_xy(pqxDatas[i].EndMark, 0, 0, 2, pqxDatas);
                pqxDatas[j].StartY = xyf_xy(pqxDatas[i].EndMark, 0, 0, 1, pqxDatas);
                pqxDatas[j].StartAngle = xyf_xy(pqxDatas[i].EndMark, 0, 0, 3, pqxDatas);

                if (pqxDatas[j].StartAngle < 0)
                {
                    pqxDatas[j].StartAngle = pqxDatas[j].StartAngle + Math.PI * 2;
                }

                j += 1;
                i += 1;
            }

        }

        /// <summary>
        /// 获取平面线指定桩号的信息
        /// </summary>
        /// <param name="dk">桩号</param>
        /// <param name="pj">偏距</param>
        /// <param name="pa">偏角</param>
        /// <param name="pd">1y2x3角度</param>
        /// <param name="dataGridView"></param>
        /// <returns>pd = 1 返回 x;pd =2 返回 y;pd =3 返回 角度</returns>
        public static double xyf_xy(double dk, double pj, double pa, double pd, List<PqxSourceModelV> pqxDatas)
        {
            double x; double y; double S;
            double l1;
            double l2;
            double l3;
            double l4;
            double t = dk;
            double o = 0; double zk = 0; double u = 0; double v = 0; double qf = 0; double p = 0; double rn = 0; double q = 0;
            int i;
            for (i = 0; i < pqxDatas.Count; i++)
            {
                if (pqxDatas[i].EndMark == 0)
                {
                    break;
                }
                if (t <= pqxDatas[i].EndMark)
                {
                    o = pqxDatas[i].StartMark;
                    zk = pqxDatas[i].EndMark;
                    u = pqxDatas[i].StartX;
                    v = pqxDatas[i].StartY;
                    qf = pqxDatas[i].StartAngle;
                    if (pqxDatas[i].StartRadius != 0)
                    {
                        p = pqxDatas[i].StartRadius;
                    }
                    else
                    {
                        p = 0;
                    }

                    if (pqxDatas[i].EndRadius != 0)
                    {
                        rn = pqxDatas[i].EndRadius;
                    }
                    else
                    {
                        rn = 0;
                    }

                    if (pqxDatas[i].UnitTurn != 0)
                    {
                        q = pqxDatas[i].UnitTurn;
                    }
                    else
                    {
                        q = 0;
                    }

                    if (p == 0 || p == null)
                    {
                        p = Math.Pow(10, 45);
                    }
                    if (rn == 0 || rn == null)
                    {
                        rn = Math.Pow(10, 45);
                    }
                    break;
                }
            }
            double x1 = t - o;
            double ii = pj;
            double jj = pa;
            double h = zk - o;
            double a = 0.1739274226;
            double b = 0.3260725774;
            double k = 0.0694318442;
            double l = 0.3300094782;
            double cc = 1 / p;
            double e = 1;
            double g = qf;
            double d = (p - rn) / (2 * h * p * rn);
            double f = 1 - l;
            double m = 1 - k;
            l1 = g + q * e * k * x1 * (cc + k * x1 * d);
            l2 = g + q * e * l * x1 * (cc + l * x1 * d);
            l3 = g + q * e * f * x1 * (cc + f * x1 * d);
            l4 = g + q * e * m * x1 * (cc + m * x1 * d);
            x = (u + x1 * (a * Math.Cos(l1) + b * Math.Cos(l2) + b * Math.Cos(l3) + a * Math.Cos(l4)));
            y = (v + x1 * (a * Math.Sin(l1) + b * Math.Sin(l2) + b * Math.Sin(l3) + a * Math.Sin(l4)));
            S = qf + q * x1 * (cc + x1 * d);

            if (S > 2 * Math.PI)
            {
                S = S - Math.PI * 2;
            }
            if (S < 0)
            {
                S = S + Math.PI * 2;
            }

            x = x + ii * Math.Cos(jj + S);
            y = y + ii * Math.Sin(jj + S);

            if (pd == 1)
            {
                return y;
            }
            else if (pd == 2)
            {
                return x;
            }
            else if (pd == 3)
            {
                return S;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 用于计算地面竖曲线标高
        /// </summary>
        /// <param name="dk">桩号</param>
        /// <param name="dataGridView">存储地面竖曲线数据的表格</param>
        /// <returns>地面竖曲线标高</returns>
        public static double dmsqx(double dk, List<DmxSourceModelV> dmxSourceModelVs)
        {
            int i; double k1 = 0, k2 = 0, h1 = 0, h2 = 0;
            for (i = 0; i < 88888; i++)
            {
                if (dk < Convert.ToDouble(dmxSourceModelVs[i].Mark))
                {
                    k1 = Convert.ToDouble(dmxSourceModelVs[i - 1].Mark);
                    k2 = Convert.ToDouble(dmxSourceModelVs[i].Mark);
                    h1 = Convert.ToDouble(dmxSourceModelVs[i - 1].Elevation);
                    h2 = Convert.ToDouble(dmxSourceModelVs[i - 1].Elevation);
                    break;
                }
            }
            return h1 + (h2 - h1) / (k2 - k1);
        }

        /// <summary>
        /// 用于计算地面竖曲线标高
        /// </summary>
        /// <param name="dk">桩号</param>
        /// <param name="dataGridView">存储地面竖曲线数据的表格</param>
        /// <returns>地面竖曲线标高</returns>
        public static double dmsqx(double dk, DataGridView dataGridView)
        {
            int i; double k1 = 0, k2 = 0, h1 = 0, h2 = 0;
            for (i = 0; i < 88888; i++)
            {
                if (Convert.ToString(dataGridView[0, i].Value) == "")
                {
                    break;
                }
                if (dk < Convert.ToDouble(dataGridView[0, i].Value))
                {
                    k1 = Convert.ToDouble(dataGridView[0, i - 1].Value);
                    k2 = Convert.ToDouble(dataGridView[0, i].Value);
                    h1 = Convert.ToDouble(dataGridView[1, i - 1].Value);
                    h2 = Convert.ToDouble(dataGridView[1, i].Value);
                    break;
                }
            }
            return h1 + (h2 - h1) / (k2 - k1);
        }

        /// <summary>
        /// 获取指定桩号的标高
        /// </summary>
        /// <param name="dk"></param>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static double jqsqx(double dk, DataGridView dataGridView)
        {

            int i;
            double k = 0, i1 = 0, i2 = 0, dh = 0, r = 0, l = 0, t1 = 0, e = 0, zy = 0, yz = 0, zz = 0, zz1 = 0, zz2 = 0;
            double a1 = 0, a2 = 0, w = 0, t = 0, d1 = 0, da = 0, h = 0, d = 0, h1 = 0, q = 0;

            for (i = 1; i < 8888; i++)
            {
                if (Convert.ToString(dataGridView[6, i].Value) == "")
                {
                    break;
                }

                if (dk <= Convert.ToDouble(dataGridView[10, i].Value))
                {
                    k = Convert.ToDouble(dataGridView[1, i].Value);
                    dh = Convert.ToDouble(dataGridView[2, i].Value);
                    r = Convert.ToDouble(dataGridView[3, i].Value);
                    i1 = Convert.ToDouble(dataGridView[4, i].Value);
                    i2 = Convert.ToDouble(dataGridView[5, i].Value);
                    i1 = i1 / 100;
                    i2 = i2 / 100;
                    break;
                }

            }

            zz = Convert.ToDouble(dataGridView[1, i - 1].Value);//变坡点桩号
            zz1 = Convert.ToDouble(dataGridView[2, i - 1].Value);//变坡点高程
            zz2 = Convert.ToDouble(dataGridView[5, i - 1].Value);//后段纵坡

            l = r * Math.Abs(i1 - i2); t1 = l / 2; e = Math.Pow(t1, 2) / 2 / r; zy = k - t1; yz = zy + l;
            a1 = Math.Atan(i1); a2 = Math.Atan(i2);
            w = a1 - a2;
            if (i1 > 0 && i2 > 0 && i1 < i2)
            {
                q = -1;
            }
            else if (i1 < 0 && i2 < 0 && i1 > i2)
            {
                q = -1;
            }
            else
            {
                q = 1;
            }

            t = r * Math.Tan(Math.Abs(w) / 2);
            d1 = t * Math.Cos(Math.Abs(a1));
            da = r * Math.Sin(Math.Abs(a1));
            d = dk - (k - d1);
            h = Math.Sqrt(r * r - (d - q * da) * (d - q * da));
            h1 = Math.Sqrt(r * r - da * da) - Math.Sign(w) * dh + Math.Sign(w) * d1 * i1;
            if (dk < zy)
            {
                return dh + (dk - k) * i1;
            }
            else if (dk > yz)
            {
                return zz1 + (dk - zz) * zz2 / 100;
            }
            else
            {
                return Math.Sign(w) * (h - h1);
            }
        }

        /// <summary>
        /// 获取指定桩号的标高
        /// </summary>
        /// <param name="dk"></param>
        /// <param name="dataGridView"></param>
        /// <returns></returns>
        public static double jqsqx(double dk, List<SqxSourceModelV> sqxSourceModelVs)
        {

            int i;
            double k = 0, i1 = 0, i2 = 0, dh = 0, r = 0, l = 0, t1 = 0, e = 0, zy = 0, yz = 0, zz = 0, zz1 = 0, zz2 = 0;
            double a1 = 0, a2 = 0, w = 0, t = 0, d1 = 0, da = 0, h = 0, d = 0, h1 = 0, q = 0;

            for (i = 1; i < 8888; i++)
            {
                if (sqxSourceModelVs[i].CurveLength == 0)
                {
                    break;
                }

                if (dk <= sqxSourceModelVs[i].CircleStraightMark)
                {
                    k = sqxSourceModelVs[i].GradientChangePointMark;
                    dh = sqxSourceModelVs[i].GradientChangePointElevation;
                    r = sqxSourceModelVs[i].DesignCurveRadius;
                    i1 = sqxSourceModelVs[i].LongitudinalSlopeI1;
                    i2 = sqxSourceModelVs[i].LongitudinalSlopeI2;
                    i1 = i1 / 100;
                    i2 = i2 / 100;
                    break;
                }

            }

            zz = sqxSourceModelVs[i - 1].GradientChangePointMark;//变坡点桩号
            zz1 = sqxSourceModelVs[i - 1].GradientChangePointElevation;//变坡点高程
            zz2 = sqxSourceModelVs[i - 1].LongitudinalSlopeI2;//后段纵坡

            l = r * Math.Abs(i1 - i2); t1 = l / 2; e = Math.Pow(t1, 2) / 2 / r; zy = k - t1; yz = zy + l;
            a1 = Math.Atan(i1); a2 = Math.Atan(i2);
            w = a1 - a2;
            if (i1 > 0 && i2 > 0 && i1 < i2)
            {
                q = -1;
            }
            else if (i1 < 0 && i2 < 0 && i1 > i2)
            {
                q = -1;
            }
            else
            {
                q = 1;
            }

            t = r * Math.Tan(Math.Abs(w) / 2);
            d1 = t * Math.Cos(Math.Abs(a1));
            da = r * Math.Sin(Math.Abs(a1));
            d = dk - (k - d1);
            h = Math.Sqrt(r * r - (d - q * da) * (d - q * da));
            h1 = Math.Sqrt(r * r - da * da) - Math.Sign(w) * dh + Math.Sign(w) * d1 * i1;
            if (dk < zy)
            {
                return dh + (dk - k) * i1;
            }
            else if (dk > yz)
            {
                return zz1 + (dk - zz) * zz2 / 100;
            }
            else
            {
                return Math.Sign(w) * (h - h1);
            }
        }

        public static double[] GetCgSlope(double dk, List<CgxSourceModelV> cgxSourceModelVs)
        {
            int index = 0;
            for (int i = 0; i < cgxSourceModelVs.Count - 1; i++)
            {
                if (dk > cgxSourceModelVs[i].St && dk < cgxSourceModelVs[i].St)
                {
                    index = i;
                }
            }

            double fst = cgxSourceModelVs[index + 1].St;
            double fcgl = cgxSourceModelVs[index + 1].Cgl;
            double fcgr = cgxSourceModelVs[index + 1].Cgr;

            double lst = cgxSourceModelVs[index].St;
            double lcgl = cgxSourceModelVs[index].Cgl;
            double lcgr = cgxSourceModelVs[index].Cgr;

            double[] results = new double[2];

            if (cgxSourceModelVs[0].Jd1 == 1)
            {
                results[0] = (fcgl - lcgl) / (fst - lst) * (dk - lst) + lcgl;
                results[1] = (fcgr - lcgr) / (fst - lst) * (dk - lst) + lcgr;
            }
            else if (cgxSourceModelVs[0].Jd1 == 3)
            {
                results[0] = lcgl + (fcgl - lcgl) * Math.Pow(dk - lst, 2) / Math.Pow(fst - lst, 2) * (3 - 2 * (dk - lst) / (fst - lst));
                results[1] = lcgr + (fcgr - lcgr) * Math.Pow(dk - lst, 2) / Math.Pow(fst - lst, 2) * (3 - 2 * (dk - lst) / (fst - lst));
            }

            results[0] = results[0];
            results[1] = -results[1];

            return results;
        }
        #endregion




    }
}
