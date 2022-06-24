using Autodesk.AutoCAD.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bssc.Control.DataControl
{
    public static class ResFoundationControl
    {

        public static void test()
        {
            
        }
        /// <summary>
        /// 获取桩布置点
        /// </summary>
        /// <param name="xNum"></param>
        /// <param name="yNum"></param>
        /// <param name="xDis"></param>
        /// <param name="yDis"></param>
        /// <returns></returns>
        public static string GetPositions(double xNum,double yNum,double xDis,double yDis)
        {
            double orginX = -(xNum - 1) / 2.0 * xDis;
            double orginY = -(yNum - 1) / 2.0 * yDis;

            List<Position> positions = new List<Position>();

            for (int i = 0; i < xNum; i++)
            {
                for (int j = 0; j < yNum; j++)
                {
                    positions.Add(new Position(orginX + i * xDis, orginY + j * yDis));
                }
            }
            return positions.GetPosition();
        }
        /// <summary>
        /// 获取规则承台的四个角点
        /// </summary>
        /// <param name="xlength"></param>
        /// <param name="ylength"></param>
        /// <returns></returns>
        public static string GetPositions(double xlength,double ylength)
        {
            List<Position> positions = new List<Position>();
            positions.Add(new Position(xlength / 2, ylength / 2));
            positions.Add(new Position(xlength / 2, -ylength / 2));
            positions.Add(new Position(-xlength / 2, -ylength / 2));
            positions.Add(new Position(-xlength / 2, ylength / 2));
            return positions.GetPosition();
        }

        /// <summary>
        /// 将字符串转换为list
        /// </summary>
        /// <param name="str"> 字符串格式2,2;3,3;4,4</param>
        /// <param name="dgv"></param>
        public static List<Position> GetPositions(this string str)
        {
            string[] strs = str.Split(';');
            List<Position> positions = new List<Position>();
            for (int i = 0; i < strs.Length; i++)
            {
                if (strs[i] != "")
                {
                    string[] strs1 = strs[i].Split(',');
                    positions.Add(new Position()
                    {
                        X = Convert.ToDouble(strs1[0]),
                        Y = Convert.ToDouble(strs1[1])
                    });
                }
            }
            return positions;
        }

        /// <summary>
        /// 将表格转list
        /// </summary>
        /// <param name="dgv"></param>
        /// <returns></returns>
        public static List<Position> GetPositions(this DataGridView dgv)
        {
            List<Position> positions = new List<Position>();
            for (int i = 0; i < dgv.RowCount; i++)
            {
                if (dgv[0, i].Value != null)
                {
                    positions.Add(new Position()
                    {
                        X = Convert.ToDouble(dgv[0, i].Value),
                        Y = Convert.ToDouble(dgv[1, i].Value),
                    });
                }
            }
            return positions;
        }

        /// <summary>
        /// 将list转字符串
        /// </summary>
        /// <param name="positions"></param>
        /// <returns></returns>
        public static string GetPosition(this List<Position> positions)
        {
            string str = "";
            for (int i = 0; i < positions.Count; i++)
            {
                if (i < positions.Count - 1)
                {
                    str = str + positions[i].X + "," + positions[i].Y + ";";
                }
                else
                {
                    str = str + positions[i].X + "," + positions[i].Y;
                }
                
            }
            return str;
        }

    }

    public class Position
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Position()
        {

        }

        public Position(double X,double Y)
        {
            this.X = X;
            this.Y = Y;
        }
    }
}
