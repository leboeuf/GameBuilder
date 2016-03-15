﻿using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using GameBuilder.IDE.Modules.SceneViewer.ViewModels;
using Gemini.Framework;
using Gemini.Modules.Inspector;

namespace GameBuilder.IDE.Modules.SceneViewer
{
	[Export(typeof(IModule))]
	public class Module : ModuleBase
	{
	    private readonly IInspectorTool _inspectorTool;

	    public override IEnumerable<IDocument> DefaultDocuments
	    {
            get { yield return new SceneViewModel(); }
	    }

	    [ImportingConstructor]
	    public Module(IInspectorTool inspectorTool)
        {
            _inspectorTool = inspectorTool;
        }

	    public override void PostInitialize()
	    {
            var sceneViewModel = Shell.Documents.OfType<SceneViewModel>().FirstOrDefault();
	        if (sceneViewModel == null) return;

	        _inspectorTool.SelectedObject = new InspectableObjectBuilder().WithObjectProperties(Shell.ActiveItem, pd => true).ToInspectableObject();

	        //_inspectorTool.SelectedObject = new InspectableObjectBuilder()
	        //    .WithVector3Editor(sceneViewModel, x => x.Position)
	        //    .ToInspectableObject();
	    }
	}
}