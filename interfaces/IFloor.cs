using LiftSystem.views;

namespace LiftSystem.interfaces
{
    public abstract class IFloor
    {
        private FloorView view;

        public void OpenDoor()
        {
            view.OpenLeftDoor();
            view.OpenRightDoor();
        }

        public void CloseDoor()
        {
            view.CloseLeftDoor();
            view.OpenRightDoor();
        }

        public void SetView(FloorView view)
        {
            this.view = view;
        }
        
        public abstract void LogRequest();
        public abstract void LogArrival();
        public abstract void LogDelivery();
        public abstract void UpdateDisplay();
    }
}