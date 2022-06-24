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

//CAD开发基础库
using BaseLibrary.Runtime;
using BaseLibrary.ResultData;
using BaseLibrary.ExtensionMethod;
using BaseLibrary.Filter;
using Autodesk.AutoCAD.GraphicsInterface;

namespace Bssc.Control.CadControl
{
    public class SnapEntityJigForArrangePier : DrawJig
    {

        public BlockReference br1;
        public BlockReference br2;

        public Point3d p;
        public double angle;

        public Point3d m_position;
        public double rotation;

        public Line line;

        public SnapEntityJigForArrangePier(BlockReference br1, BlockReference br2, Point3d p, double angle)
        {
            this.br1 = br1;
            this.br2 = br2;
            this.p = p;
            this.angle = angle;

            Vector3d vector = new Vector3d(Math.Cos(angle), Math.Sin(angle), 0);

            line = new Line(new Point3d(p.X + 10 * vector.X, p.Y + 10 * vector.Y, 0),
                new Point3d(p.X - 10 * vector.X, p.Y - 10 * vector.Y, 0));
        }

        protected override SamplerStatus Sampler(JigPrompts prompts)
        {

            JigPromptPointOptions optJig = new JigPromptPointOptions("请选择插入");

            optJig.UserInputControls =
              (UserInputControls.Accept3dCoordinates |
              UserInputControls.NullResponseAccepted |
              UserInputControls.NoNegativeResponseAccepted);

            // 用AcquirePoint函数得到用户输入的点.
            PromptPointResult resJigDis = prompts.AcquirePoint(optJig);
            Point3d curPt = new Point3d();

            curPt = resJigDis.Value; //获取满足输入选项的输入值

            if (resJigDis.Status == PromptStatus.Cancel)
            {
                return SamplerStatus.Cancel;
            }

            m_position = line.GetClosestPointTo(curPt, true);

            return SamplerStatus.NoChange;

        }

        protected override bool WorldDraw(WorldDraw draw)
        {
            br1.Position = m_position;
            br1.Rotation = angle;

            br2.Position = m_position;
            br2.Rotation = angle;

            draw.Geometry.Draw(br1);
            draw.Geometry.Draw(br2);

            return true;
        }

    }
}
