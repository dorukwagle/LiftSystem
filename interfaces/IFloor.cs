namespace LiftSystem.interfaces
{
    public abstract class IFloor
    {
        public void OpenDoor() {}
        
        public void CloseDoor() {}
        
        public void CreateFloor() {}

        public abstract void LogRequest();
        public abstract void LogArrival();
        public abstract void LogDelivery();
        public abstract void UpdateDisplay();
    }
}