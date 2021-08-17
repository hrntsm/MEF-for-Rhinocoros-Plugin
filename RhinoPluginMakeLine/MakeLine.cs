using RhinoPluginContractInterface;
using System.ComponentModel.Composition;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

namespace RhinoPluginMakeLine
{
    [Export(typeof(IPlugin))]
    public class CodeEjector : IPlugin
    {
        public Result Run(RhinoDoc doc, RunMode mode)
        {
            Point3d pt0;
            using (var getPointAction = new GetPoint())
            {
                getPointAction.SetCommandPrompt("Please select the start point for a line: 1");
                if (getPointAction.Get() != GetResult.Point)
                {
                    RhinoApp.WriteLine("No start point was selected.");
                    return getPointAction.CommandResult();
                }
                pt0 = getPointAction.Point();
            }

            Point3d pt1;
            using (var getPointAction = new GetPoint())
            {
                getPointAction.SetCommandPrompt("Please select the end point for a line: 2");
                getPointAction.SetBasePoint(pt0, true);
                getPointAction.DynamicDraw +=
                    (sender, e) => e.Display.DrawLine(pt0, e.CurrentPoint, System.Drawing.Color.DarkRed);
                if (getPointAction.Get() != GetResult.Point)
                {
                    RhinoApp.WriteLine("No end point was selected.");
                    return getPointAction.CommandResult();
                }
                pt1 = getPointAction.Point();
            }

            doc.Objects.AddLine(pt0, pt1);
            doc.Views.Redraw();

            return Result.Success;
        }
    }
}
