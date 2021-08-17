using System.Reflection;
using System;
using System.Collections.Generic;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;

using RhinoPluginContractInterface;

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace RhinoPluginLoader
{
    public class LoadPlugin : Command
    {
        [Import(typeof(IPlugin))]
        public IPlugin Plugin { get; set; }

        public LoadPlugin()
        {
            // Rhino only creates one instance of each command class defined in a
            // plug-in, so it is safe to store a refence in a static property.
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static LoadPlugin Instance { get; private set; }

        ///<returns>The command name as it appears on the Rhino command line.</returns>
        public override string EnglishName => "LoadPlugin";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // Prompt the user to enter a plugin name
            var gs = new GetString();
            gs.SetCommandPrompt("File name of plugin to run");
            gs.AcceptNothing(true);
            gs.Get();
            if (gs.CommandResult() != Result.Success)
            {
                return gs.CommandResult();
            }

            // Was a plugin name entered?
            string pluginName = gs.StringResult().Trim();
            if (string.IsNullOrEmpty(pluginName))
            {
                RhinoApp.WriteLine("Plugin name cannot be blank.");
                return Result.Cancel;
            }

            // Check if the plugin exists?
            if (!System.IO.File.Exists(pluginName))
            {
                RhinoApp.WriteLine("File not exist !!!");
                return Result.Cancel;
            }

            //--------------------------------------
            // MEF - Managed Extensibility Framework
            //--------------------------------------
            var catalog = new AggregateCatalog(
                new AssemblyCatalog(Assembly.Load(System.IO.File.ReadAllBytes(pluginName)))
            );

            // Create a compsition container
            var container = new CompositionContainer(catalog);

            try
            {
                container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                RhinoApp.WriteLine(compositionException.ToString());
                return Result.Cancel;
            }

            //Excute the Run method of plugin interface
            Result result = Plugin.Run(doc, mode);

            catalog.Dispose();
            container.Dispose();
            return result;
        }
    }
}
