using System.Windows.Controls;
using ICSharpCode.AvalonEdit;

namespace GameBuilder.IDE.Modules.ShaderEditor.Views
{
    /// <summary>
    /// Interaction logic for ShaderEditorView.xaml
    /// </summary>
    public partial class ShaderEditorView : UserControl, IShaderEditorView
    {
        public TextEditor TextEditor
        {
            get
            {
                return CodeEditor;
            }
        }

        public ShaderEditorView()
        {
            InitializeComponent();
        }
    }
}
