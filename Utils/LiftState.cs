using System.Runtime.CompilerServices;
using System.Windows.Controls;
using LiftSystem.Enums;
using LiftSystem.interfaces;

namespace LiftSystem
{
    public static class LiftState
    {
        private static IFloor currentFloor;
        private static LiftDirection direction;
        private static LiftStatus status;

        public static IFloor CurrentFloor
        {
            set => currentFloor = value;
            get => currentFloor;
        }
 
        public static LiftDirection Direction
        {
            set => direction = value;
            get => direction;
        }

        public static LiftStatus Status
        {
            set => status = value;
            get => status;
        }
    }
}  