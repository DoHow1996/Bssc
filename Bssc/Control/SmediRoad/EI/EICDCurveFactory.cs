using Bssc.Models.ModelsV.SourceModelsV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EI
{
    /// <summary>
    /// EICD平曲线工厂，用于构建平曲线
    /// </summary>
    public class EICDCurveFactory : EICurveFactory
    {
        #region 全局常量
        public  readonly double DEF_A = 0.1739274226;
        public  readonly double DEF_B = 0.3260725774;
        public  readonly double DEF_K = 0.0694318442;
        public  readonly double DEF_L = 0.3300094782;
        public  readonly double PI = 3.1415926535897931;
        public  readonly double STEP = 1.0;
        public  readonly int EICDLine = 1;
        public  readonly int EICDARC = 2;
        public  readonly int EICDEaseCurveR = 3;
        public  readonly int EICDEaseCurveL = 4;
        public  readonly int EICDHalfEaseCurveDec = 5;
        public  readonly int EICDHalfEaseCurveInc = 6;
        #endregion

        private List<EICDUnit> ListUnit = new List<EICDUnit>();

        private List<List<EICDPoint>> basePoints = new List<List<EICDPoint>>();

        public override List<List<EICDPoint>> GetGeoEICurve()
        {
            return  basePoints;
        }

        public override List<EICDUnit> LoadData(List<PqxSourceModelV> pqxSourceModelVs)
        {
            int count = pqxSourceModelVs.Count;
            for (int i = 0; i < count; )
            {

                if (pqxSourceModelVs[i].UnitType == 0)
                {
                    pqxSourceModelVs.RemoveAt(i);
                }
                else
                {
                    i++;
                }

                count = pqxSourceModelVs.Count;

            }
            ListUnit.Clear();
            foreach (var item in pqxSourceModelVs)
            {
                EICDUnit unit = new EICDUnit()
                {
                    EndR = item.EndRadius,
                    StartAzimuth = item.StartAngle,
                    StartMark = item.StartMark,
                    StartR = item.StartRadius,
                    StartX = item.StartX,
                    StartY = item.StartY,
                    StartZ = 0,
                    UnitA = item.UnitA,
                    UnitLength = item.UnitLength,
                    UnitTurn = (int)item.UnitTurn,
                    UnitType = (int)item.UnitType,
                };
                ListUnit.Add(unit);
            }
            Build();
            return ListUnit;
        }

        /// <summary>
        /// 加载EICD数据文件
        /// </summary>
        /// <param name="filename">数据文件路径及文件名</param>
        public override List<EICDUnit> Load(string filename)
        {
            ListUnit.Clear();
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(filename, Encoding.Default);
                string line = null;
                int LineIndex = 0;
                EICDPoint StartPoint = new EICDPoint();
                double LastMark = 0.0;
                while ((line = reader.ReadLine().Replace(" ", "")) != null)
                {
                    LineIndex++;
                    if (LineIndex == 1)
                    { //第一行起点桩号
                        string[] Values = line.Split(',');
                        if (Values.Length != 1) throw new EIParseException("frist line format error");
                        StartPoint.Mark = double.Parse(Values[0]);
                        LastMark = StartPoint.Mark;  //起始桩号
                    }
                    else if (LineIndex == 2)
                    { //第二行起点坐标
                        string[] Values = line.Split(',');
                        if (Values.Length != 3) throw new EIParseException("second line format error");
                        double X = double.Parse(Values[0]);
                        double Y = double.Parse(Values[1]);
                        double Azimuth = double.Parse(Values[2]);
                        if (X == 0.0 && Y == 0.0 && Azimuth == 0.0)
                        { //判断是否为终止
                            throw new  EIParseException("end");
                        }
                        else
                        {
                            StartPoint.X = X;
                            StartPoint.Y = Y;
                            StartPoint.Azimuth = Azimuth;
                        }

                    }
                    else
                    {
                        string[] Values = line.Split(',');
                        if (Values.Length >= 1)
                        {
                            EICDUnit unit = new EICDUnit();
                            unit.StartX = StartPoint.X;
                            unit.StartY = StartPoint.Y;
                            unit.StartZ = StartPoint.Z;
                            unit.StartAzimuth = StartPoint.Azimuth;
                            unit.StartMark = StartPoint.Mark;
                            int ICDUnitType = int.Parse(Values[0]);
                            if (ICDUnitType == 0)
                            {  //终止标记
                                if (Values.Length != 3) continue;
                                if (ICDUnitType == 0 && int.Parse(Values[1]) == 0 && int.Parse(Values[2]) == 0)
                                {
                                    break;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else if (ICDUnitType == 1)
                            { //直线
                                if (Values.Length == 2)
                                {
                                    unit.StartMark = LastMark;
                                    unit.UnitType = int.Parse(Values[0]);
                                    unit.UnitLength = double.Parse(Values[1]);

                                }
                                else if (Values.Length == 3)
                                {
                                    unit.StartMark = LastMark;
                                    unit.UnitType = int.Parse(Values[0]);
                                    unit.UnitLength = double.Parse(Values[1]);
                                }
                                else continue;
                            }
                            else if (ICDUnitType == 2)
                            { //圆曲线
                                if (Values.Length == 4)
                                {
                                    unit.StartMark = LastMark;
                                    unit.UnitType = int.Parse(Values[0]);
                                    unit.StartR = double.Parse(Values[1]);
                                    unit.EndR = double.Parse(Values[1]);
                                    unit.UnitLength = double.Parse(Values[2]);
                                    unit.UnitTurn = int.Parse(Values[3]);
                                }
                                else continue;
                                ;
                            }
                            else if (ICDUnitType == 3)
                            { //完整缓和曲线（R∈（∞-Ro）时）标示符、回旋参数、终点半径、转向(1:右转;-1：左转)
                                if (Values.Length == 4)
                                {
                                    unit.StartMark = LastMark;
                                    unit.UnitType = int.Parse(Values[0]);
                                    unit.UnitA = double.Parse(Values[1]);
                                    unit.EndR = double.Parse(Values[2]);
                                    unit.UnitTurn = int.Parse(Values[3]);

                                }
                                else continue;
                            }
                            else if (ICDUnitType == 4)
                            { //完整缓和曲线（R∈（Ro-∞）时）标示符、回旋参数、起点半径、转向(1:右转;-1：左转)
                                if (Values.Length == 4)
                                {
                                    unit.StartMark = LastMark;
                                    unit.UnitType = int.Parse(Values[0]);
                                    unit.UnitA = double.Parse(Values[1]);
                                    unit.StartR = double.Parse(Values[2]);
                                    unit.UnitTurn = int.Parse(Values[3]);
                                }
                                else continue;
                            }
                            else if (ICDUnitType == 5)
                            { //不完整缓和曲线（R∈（R大-R小）时）标示符、回旋参数、起点半径、终点半径、转向(1:右转;-1：左转)
                                if (Values.Length == 5)
                                {
                                    unit.StartMark = LastMark;
                                    unit.UnitType = int.Parse(Values[0]);
                                    unit.UnitA = double.Parse(Values[1]);
                                    unit.StartR = double.Parse(Values[2]);
                                    unit.EndR = double.Parse(Values[3]);
                                    unit.UnitTurn = int.Parse(Values[4]);
                                }
                                else continue;
                            }
                            else if (ICDUnitType == 6)
                            { //不完整缓和曲线（R∈（R小-R大）时）标示符、回旋参数、起点半径、终点半径、转向(1:右转;-1：左转)
                                if (Values.Length == 5)
                                {
                                    unit.StartMark = LastMark;
                                    unit.UnitType = int.Parse(Values[0]);
                                    unit.UnitA = double.Parse(Values[1]);
                                    unit.StartR = double.Parse(Values[2]);
                                    unit.EndR = double.Parse(Values[3]);
                                    unit.UnitTurn = int.Parse(Values[4]);
                                }
                                else continue;
                            }
                            else continue;
                            ListUnit.Add(unit);
                        }
                        else
                        { //格式不满足，则过滤
                            continue;
                        }

                    }
                }
                Build();
            }
            catch(EIParseException e)
            {
                throw e;
            }
            finally
            {
                if (reader != null) reader.Close();
            }

            return ListUnit;
        }
        /// <summary>
        /// 构建点集数据
        /// </summary>
        protected override void Build()
        {
            basePoints.Clear();
            if (ListUnit.Count > 0)
            {
                EICDUnit eICDUnit = ListUnit[0];
                EICDPoint P = new EICDPoint();
                P.X = eICDUnit.StartX;
                P.Y = eICDUnit.StartY;
                P.Z = 0;
                P.Mark = eICDUnit.StartMark;
                P.Azimuth = eICDUnit.StartAzimuth;
                for (int i = 0; i < ListUnit.Count; i++)
                {
                    EICDUnit unit = ListUnit[i];
                    unit.StartX = P.X;
                    unit.StartY = P.Y;
                    unit.StartZ = P.Z;
                    unit.StartMark = P.Mark;
                    unit.StartAzimuth = P.Azimuth;
                    if (unit.UnitType == EICDLine)
                    {
                        List<EICDPoint> Points = CreateEICDLine(unit);
                        P = Points.Last();
                        basePoints.Add(Points);
                    }
                    else if (unit.UnitType == EICDARC)
                    {
                        List<EICDPoint> Points = CreateEICDArc(unit);
                        P = Points.Last();
                        basePoints.Add(Points);
                    }
                    else
                    {
                        List<EICDPoint> Points = CreateEICDEaseCurve(unit);
                        P = Points.Last();
                        basePoints.Add(Points);
                    }
                }
            }
        }


        #region 平曲线接口部分
        /// <summary>
        /// 
        /// </summary>
        /// <param name="A"></param>
        /// <param name="startR"></param>
        /// <param name="endR"></param>
        /// <returns></returns>
        public  double GetEaseCurveLength(double A, double startR = 0, double endR = 0)
        {
            if (startR == 0) startR = Math.Pow(10, 45);
            if (endR == 0) endR = Math.Pow(10, 45);
            return Math.Abs(A * A / startR - A * A / endR);
        }

        public  double GetEaseCurveEndMark(double A, double startR = 0, double endR = 0, double startMark = 0)
        {
            return startMark + GetEaseCurveLength(A, startR, endR);
        }

        public EICDPoint CALEICDLineCurve(EICDUnit unit, double dist, int targetFlag, double offset = 0, double driftAngle = 0)
        {
            EICDPoint point = new EICDPoint();
            point.X = CALEICDLineCurve(unit.UnitLength, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, 0, offset, driftAngle);
            point.Y = CALEICDLineCurve(unit.UnitLength, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, 1, offset, driftAngle);
            point.Z = 0.0;
            point.Azimuth = CALEICDLineCurve(unit.UnitLength, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, 2, offset, driftAngle);
            point.Bulge = CALEICDLineCurve(unit.UnitLength, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, 3, offset, driftAngle);
            point.Mark = CALEICDLineCurve(unit.UnitLength, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, 4, offset, driftAngle);
            return point;
        }

        public  double CALEICDLineCurve(double length, double startX, double startY, double startMark, double startAzimuth, double dist, int targetFlag, double offset = 0, double driftAngle = 0)
        {
            return CALEICDCurve(length, 0, 0, 0, startX, startY, startMark, startAzimuth, dist, dist + 1.5 * STEP, targetFlag, offset, driftAngle);
        }

        public EICDPoint CALEICDArcCurve(EICDUnit unit, double dist, double offset = 0, double driftAngle = 0)
        {
            EICDPoint point = new EICDPoint();
            point.X = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, 0, offset, driftAngle);
            point.Y = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, 1, offset, driftAngle);
            point.Z = 0.0;
            point.Azimuth = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, 2, offset, driftAngle);
            point.Bulge = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, 3, offset, driftAngle);
            point.Mark = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, 4, offset, driftAngle);
            return point;
        }

        public double CALEICDArcCurve(double R, double length, int turn, double startX, double startY, double startMark, double startAzimuth, double dist, int targetFlag, double offset = 0, double driftAngle = 0)
        {
            return CALEICDCurve(length, R, R, turn, startX, startY, startMark, startAzimuth, dist, dist + 1.5 * STEP, targetFlag, offset, driftAngle);
        }

        public EICDPoint CALEICDEaseCurve(EICDUnit unit, double dist, double step, double offset = 0, double driftAngle = 0)
        {
            EICDPoint point = new EICDPoint();
            point.X = CALEICDEaseCurve(unit.UnitA, unit.StartR, unit.EndR, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, step, 0, offset, driftAngle);
            point.Y = CALEICDEaseCurve(unit.UnitA, unit.StartR, unit.EndR, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, step, 1, offset, driftAngle);
            point.Z = 0;
            point.Azimuth = CALEICDEaseCurve(unit.UnitA, unit.StartR, unit.EndR, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, step, 2, offset, driftAngle);
            point.Bulge = CALEICDEaseCurve(unit.UnitA, unit.StartR, unit.EndR, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, step, 3, offset, driftAngle);
            point.Mark = CALEICDEaseCurve(unit.UnitA, unit.StartR, unit.EndR, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, step, 4, offset, driftAngle);
            return point;
        }

        public  double CALEICDEaseCurve(double A, double startR, double endR, int turn, double startX, double startY, double startMark, double startAzimuth, double dist, double step, int targetFlag, double offset = 0, double driftAngle = 0)
        {
            return CALEICDCurve(A, startR, endR, turn, startX, startY, startMark, startAzimuth, dist, step, targetFlag, offset, driftAngle);
        }

        /// <summary>
        /// 根据偏移距离计算EICD线元坐标及凸度等参数
        /// </summary>
        /// <param name="A">当startR=endR=0时A为直线长度，当startR=endR=R时A为圆弧长度，当startR!=endR时A为回旋曲率</param>
        /// <param name="startR">起点半径</param>S
        /// <param name="endR">终点半径</param>
        /// <param name="turn">转向 1:右转 -1：左转 0: 直线</param>
        /// <param name="startX">起点X坐标</param>
        /// <param name="startY">起点Y坐标</param>
        /// <param name="startMark">起点桩号</param>
        /// <param name="startAzimuth">起点弧度值方位角</param>
        /// <param name="dist">距离线元起点的偏移距离</param>
        /// <param name="step">下一个所求点的距次点的偏移距离，用于计算曲线凸度</param>
        /// <param name="targetFlag">所求参数类型0：X坐标；1：Y坐标;2:弧度制方位角；3：凸度；4：桩号</param>
        /// <param name="offset">偏移距离</param>
        /// <param name="driftAngle">偏移角度</param>
        /// <returns></returns>
        public  double CALEICDCurve(double A, double startR, double endR, int turn, double startX, double startY, double startMark, double startAzimuth, double dist, double step = 0.5, int targetFlag = 0, double offset = 0, double driftAngle = 0)
        {
            double X = 0.0; //X坐标
            double Y = 0.0; //Y坐标
            double s = 0.0; //角度
            double dgr = 0.0;//凸度

            if (startR == 0) startR = Math.Pow(10, 45);
            if (endR == 0) endR = Math.Pow(10, 45);

            double L1 = 0.0, L2 = 0.0, L3 = 0.0, L4 = 0.0;

            double u = startX; //起始X坐标
            double v = startY; //起始Y坐标
            double o = startMark; //起始桩号
            double zk = 0.0;
            if (startR == 0 && endR == 0) // 曲线为直线
            {
                zk = startMark + A;
            }
            else if (startR == endR && startR != 0)
            { //曲线为圆弧
                zk = startMark + A;
            }
            else
            { //曲线为缓和曲线
                zk = GetEaseCurveEndMark(A, startR, endR, startMark);//终止桩号
            }
            double qf = startAzimuth; //起始方位角
            double p = startR; //起始半径
            double rn = endR; //终止半径
            double q = turn; //转向

            double t = o + dist; //要求的桩号
            double hxcs = A; //回旋参数
            double dk = step; //距离所求桩号点下一点步长距离


            double x1 = dist; //求解位置桩号与起点桩号偏移
            #region 相同计算部分
            double ii = offset; //偏距
            double jj = driftAngle; //偏角
            double h = zk - o; //起点终点偏移距离
            double a = DEF_A;
            double b = DEF_B;
            double k = DEF_K;
            double l = DEF_L;
            double cc = 1 / p; //起始半径倒数
            double e = 1;//(180 / pi) / (180 / pi)
                         //g = qf / (180 / pi)    '输入为度数时的赋值
            double g = qf; //'输入为弧度时的赋值 
            double d = (p - rn) / (2 * h * p * rn);
            double f = 1 - l;
            double m = 1 - k;
            L1 = g + q * e * k * x1 * (cc + k * x1 * d);
            L2 = g + q * e * l * x1 * (cc + l * x1 * d);
            L3 = g + q * e * f * x1 * (cc + f * x1 * d);
            L4 = g + q * e * m * x1 * (cc + m * x1 * d);
            X = (u + x1 * (a * Math.Cos(L1) + b * Math.Cos(L2) + b * Math.Cos(L3) + a * Math.Cos(L4)));
            Y = (v + x1 * (a * Math.Sin(L1) + b * Math.Sin(L2) + b * Math.Sin(L3) + a * Math.Sin(L4)));
            s = qf + q * x1 * (cc + x1 * d);

            if (s > PI * 2) s = s - PI * 2;
            if (s < 0) s = s + PI * 2;
            #endregion

            if (startR == 0 && endR == 0) // 曲线为直线
            {
                dgr = 0;
                s = startAzimuth;
            }
            else if (startR == endR && startR != 0)
            { //曲线为圆弧
                double D = (p - rn) / (2 * A * p * rn);
                double da = turn * A * (cc + step * D);
                if (da < 0.0) da = -da;
                dgr = -1 * q * Math.Tan(da / 4.0);
            }
            else
            { //曲线为缓和曲线
                double qdl = 0.0;
                qdl = Math.Pow(hxcs, 2) / p;// '以起点处计算得出的缓和曲线长度
                if (p > rn)//'如果起点半径大于终点半径
                {
                    //dgr = Math.Tan(dk * (qdl - (i + 0.5) * dk) / p / qdl / 4); //凸度值
                    dgr = Math.Tan(dk * (qdl - (dist + 1.5 * dk)) / p / qdl / 4); //凸度值
                }
                else
                {
                    /* dgr = Math.Math.Tan(dk * (qdl + (i + 0.5) * dk) / p / qdl / 4);*/
                    dgr = Math.Tan(dk * (qdl + (dist + 1.5 * dk)) / p / qdl / 4);
                }
            }


            //dgr = s * 180 / 3.14159265358979
            //X = X + ii * mMath.Cos(dms(jj) + s)
            //Y = Y + ii * mMath.Sin(dms(jj) + s)
            X = X + ii * Math.Cos(jj + s);
            Y = Y + ii * Math.Cos(jj + s);
            /*    double angle = s - startAngle;
                if (angle < 0.0) angle = 2 * PI + angle;
                dgr = Math.Tan(angle / 4);*/


            if (targetFlag == 0) return X; //X坐标
            else if (targetFlag == 1) return Y; //Y坐标
            else if (targetFlag == 2) return s; //角度
            else if (targetFlag == 3) return dgr; //桩号
            else if (targetFlag == 4) return t;
            else return X;

        }
        #endregion

        #region 接口调用
        /// <summary>
        /// 创建EICD直线
        /// </summary>
        /// <param name="unit">线元</param>
        /// <returns>多段线类型直线</returns>
        public  List<EICDPoint> CreateEICDLine(EICDUnit unit)
        {
            List<EICDPoint> ListVertex = new List<EICDPoint>();
            if (unit.UnitType == EICDLine)
            {
                EICDPoint P1 = new EICDPoint { X = unit.StartX, Y = unit.StartY, Z = unit.StartZ, Mark = unit.StartMark, Azimuth = unit.StartAzimuth };
                EICDPoint P2 = new EICDPoint();
                P2.X = CALEICDLineCurve(unit.UnitLength, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, unit.UnitLength, 0);
                P2.Y = CALEICDLineCurve(unit.UnitLength, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, unit.UnitLength, 1);
                P2.Azimuth = CALEICDLineCurve(unit.UnitLength, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, unit.UnitLength, 2);
                P2.Bulge = CALEICDLineCurve(unit.UnitLength, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, unit.UnitLength, 3);
                P2.Mark = CALEICDLineCurve(unit.UnitLength, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, unit.UnitLength, 4);
                ListVertex.Add(P1);
                ListVertex.Add(P2);
            }
            return ListVertex;
        }
        /// <summary>
        /// 创建EICD圆曲线
        /// </summary>
        /// <param name="unit">线元</param>
        /// <returns>多段线类型圆曲线</returns>
        public  List<EICDPoint> CreateEICDArc(EICDUnit unit)
        {
            List<EICDPoint> ListVertex = new List<EICDPoint>();
            if (unit.UnitType == EICDARC)
            {



                EICDPoint P2 = new EICDPoint();
                P2.X = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, unit.UnitLength, 0);
                P2.Y = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, unit.UnitLength, 1);
                P2.Azimuth = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, unit.UnitLength, 2);
                P2.Bulge = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, unit.UnitLength, 3);
                P2.Mark = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, unit.UnitLength, 4);

                EICDPoint P1 = new EICDPoint();
                P1.X = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, 0, 0);
                P1.Y = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, 0, 1);
                P1.Azimuth = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, 0, 2);
                P1.Bulge = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, 0, 3);
                P1.Mark = CALEICDArcCurve(unit.StartR, unit.UnitLength, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, 0, 4);
                //P1.Bulge = P2.Bulge;
                ListVertex.Add(P1);
                ListVertex.Add(P2);
            }
            return ListVertex;
        }
        /// <summary>
        /// 创建EICD完缓和曲线
        /// </summary>
        /// <param name="unit">线元</param>
        /// <returns>多段线类型缓和曲线</returns>
        public  List<EICDPoint> CreateEICDEaseCurve(EICDUnit unit)
        {
            List<EICDPoint> ListVertex = new List<EICDPoint>();
            if (unit.UnitType == EICDEaseCurveL ||
                unit.UnitType == EICDEaseCurveR ||
                unit.UnitType == EICDHalfEaseCurveDec ||
                unit.UnitType == EICDHalfEaseCurveInc)
            {
                EICDPoint StartP = new EICDPoint { X = unit.StartX, Y = unit.StartY, Z = unit.StartZ, Mark = unit.StartMark, Azimuth = unit.StartAzimuth };
                double length = GetEaseCurveLength(unit.UnitA, unit.StartR, unit.EndR);
                double dist = 0;
                double len = 0;
                ListVertex.Add(StartP);
                while (len <= length)
                {
                    dist = len;
                    if (dist > length) dist = length;
                    EICDPoint P = new EICDPoint();
                    P.X = CALEICDEaseCurve(unit.UnitA, unit.StartR, unit.EndR, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, STEP, 0, 0, 0);
                    P.Y = CALEICDEaseCurve(unit.UnitA, unit.StartR, unit.EndR, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, STEP, 1, 0, 0);
                    P.Azimuth = CALEICDEaseCurve(unit.UnitA, unit.StartR, unit.EndR, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, STEP, 2, 0, 0);
                    P.Bulge = CALEICDEaseCurve(unit.UnitA, unit.StartR, unit.EndR, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, STEP, 3, 0, 0);
                    P.Mark = CALEICDEaseCurve(unit.UnitA, unit.StartR, unit.EndR, unit.UnitTurn, unit.StartX, unit.StartY, unit.StartMark, unit.StartAzimuth, dist, STEP, 4, 0, 0);
                    len = len + STEP;
                    ListVertex.Add(P);
                }
            }
            return ListVertex;
        }
        #endregion
    }


    /// <summary>
    /// EICD线元类
    /// </summary>
    public class EICDUnit
    {
        /// <summary>
        /// 线元类型 1-直线 ,2-圆曲线,3-起点半径无穷大完整缓和曲线，4-终点半径无穷大完整缓和曲线，5-半径递减不完整缓和曲线，6-半径递增不完整缓和曲线
        /// </summary>
        private int unitType;
        /// <summary>
        /// 线元回旋参数
        /// </summary>
        private double unitA;
        /// <summary>
        /// 线元长度
        /// </summary>
        private double unitLength;
        /// <summary>
        /// 起点半径
        /// </summary>
        private double startR;
        /// <summary>
        /// 终点半径
        /// </summary>
        private double endR;
        /// <summary>
        /// 线元转向
        /// </summary>
        private int unitTurn;
        /// <summary>
        /// 起点X坐标
        /// </summary>
        private double startX;
        /// <summary>
        /// 起点Y坐标
        /// </summary>
        private double startY;
        /// <summary>
        /// 起点Z坐标
        /// </summary>
        private double startZ;
        /// <summary>
        /// 起点桩号
        /// </summary>
        private double startMark;
        /// <summary>
        /// 起点弧度方位角
        /// </summary>
        private double startAzimuth;

        public double UnitA { get => unitA; set => unitA = value; }
        public double UnitLength { get => unitLength; set => unitLength = value; }
        public double StartR { get => startR; set => startR = value; }
        public double EndR { get => endR; set => endR = value; }
        public int UnitTurn { get => unitTurn; set => unitTurn = value; }
        public double StartX { get => startX; set => startX = value; }
        public double StartY { get => startY; set => startY = value; }
        public double StartZ { get => startZ; set => startZ = value; }
        public double StartMark { get => startMark; set => startMark = value; }
        public double StartAzimuth { get => startAzimuth; set => startAzimuth = value; }
        public int UnitType { get => unitType; set => unitType = value; }
    }
}
