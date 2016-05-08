using OpenTK;
using OpenTK.Input;

namespace GameBuilder.Library.Listeners
{
    public interface KeyListener
    {
        void OnKeyDown(object sender, KeyboardKeyEventArgs args);

        void OnKeyUp(object sender, KeyboardKeyEventArgs args);

        void OnKeyPress(object sender, KeyPressEventArgs args);
    }
}
