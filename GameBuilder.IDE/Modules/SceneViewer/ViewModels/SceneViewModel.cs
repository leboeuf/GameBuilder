using System;
using System.ComponentModel.Composition;
using GameBuilder.IDE.Modules.SceneViewer.Views;
using Gemini.Framework;
using OpenTK.Graphics.OpenGL4;
using GameBuilder.IDE.Utils;
using OpenTK;
using GameBuilder.Library.OpenGL;
using Gemini.Modules.Inspector;
using GameBuilder.IDE.Modules.Inspector;
using Caliburn.Micro;

namespace GameBuilder.IDE.Modules.SceneViewer.ViewModels
{
    [Export(typeof(SceneViewModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class SceneViewModel : Document
    {
        private ISceneView _sceneView;

        public override bool ShouldReopenOnStart => true;

        public Vector3 CameraPosition
        {
            get { return _sceneView.OpenGLManager.CameraPosition; }
            set
            {
                _sceneView.OpenGLManager.CameraPosition = value;
                _sceneView.Invalidate();
            }
        }

        public Vector3 CameraRotation
        {
            get { return _sceneView.OpenGLManager.CameraRotation; }
            set
            {
                _sceneView.OpenGLManager.CameraRotation = value;
                _sceneView.Invalidate();
            }
        }

        public Vector3 ModelViewMatrixTranslationVector
        {
            get { return _sceneView.OpenGLManager.ModelViewMatrixTranslationVector; }
            set
            {
                _sceneView.OpenGLManager.ModelViewMatrixTranslationVector = value;
                _sceneView.Invalidate();
            }
        }

        public Vector3 ProjectionMatrixVector
        {
            get { return _sceneView.OpenGLManager.ProjectionMatrixVector; }
            set
            {
                _sceneView.OpenGLManager.ProjectionMatrixVector = value;
                _sceneView.Invalidate();
            }
        }

        public SceneViewModel()
        {
            DisplayName = "3D Scene";
        }

        protected override void OnViewLoaded(object view)
        {
            _sceneView = view as ISceneView;
            base.OnViewLoaded(view);

            InspectScene();
        }

        protected override void OnDeactivate(bool close)
        {
            if (close)
            {
                var view = GetView() as IDisposable;
                if (view != null)
                    view.Dispose();
            }

            base.OnDeactivate(close);
        }

        internal void ReplaceShader(ShaderType shaderType, string shaderCode)
        {
            _sceneView.ReplaceShader(shaderType, shaderCode);
        }

        private void InspectScene()
        {
            IoC.Get<IInspectorTool>().SelectedObject = new InspectableObjectBuilder()
                .WithVector3Editor(this, x => x.CameraPosition)
                .WithVector3Editor(this, x => x.ModelViewMatrixTranslationVector)
                .WithVector3Editor(this, x => x.ProjectionMatrixVector)
                .WithVector3Editor(this, x => x.CameraRotation)
                .ToInspectableObject();
        }
    }
}