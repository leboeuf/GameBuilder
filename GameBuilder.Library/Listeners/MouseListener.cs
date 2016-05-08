using OpenTK.Input;

namespace GameBuilder.Library.Listeners
{
    public interface MouseListener
    {
        void OnMouseDown(object sender, MouseButtonEventArgs args);
        void OnMouseUp(object sender, MouseButtonEventArgs args);
        void OnMouseMove(object sender, MouseMoveEventArgs args);
        void OnMouseWheel(object sender, MouseWheelEventArgs args);
    }
}
