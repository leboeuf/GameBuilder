using OpenTK.Graphics;

namespace GameBuilder.Library.Gui
{
    public class RadioButton : Control
    {
        private bool _checked;

        public RadioButton(int x, int y, int z, State state) :
            base(x, y, z, 20, 20, state)
        {
            _checked = false;
            SetBorderRadius(10);
        }

        public override void OnTrigger()
        {
            base.OnTrigger();
            _checked = _checked ? false : true;
        }

        public override void RenderContent()
        {
            base.RenderContent();
            if (_checked)
            {
                Graphics.GraphicsManager.FillRoundedRect(0, 0, 0, _content.Width, _content.Height, _borderRadius, Color4.DarkSlateBlue);
            }
        }
    }
}
