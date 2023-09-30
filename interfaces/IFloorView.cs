using System.Windows.Controls;

namespace LiftSystem.interfaces
{
    public interface IFloorView
    {
        void OpenLeftDoor();
        void OpenRightDoor();
        void CloseLeftDoor();
        void CloseRightDoor();
        Canvas GetView();
    }
}