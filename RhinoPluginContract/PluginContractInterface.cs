using Rhino;
using Rhino.Commands;

namespace RhinoPluginContractInterface
{
    public interface IPlugin
    {
        Result Run(RhinoDoc doc, RunMode mode);
    }
}
