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

namespace Bssc.Control.CadControl
{
    class BaseGlyph : Glyph
    {
        public override void SetLocation(Point3d point)
        {

        }

        protected override void SubViewportDraw(ViewportDraw vd)
        {

        }
    }
}
