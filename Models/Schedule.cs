using System;
using System.Collections.Generic;
using LiftSystem.interfaces;

namespace LiftSystem.Model
{
    public static class Schedule
    {
        private static List<IFloor> schedule = new List<IFloor>();
        
        public static void AddSchedule(IFloor floor)
        {
            if (!schedule.Contains(floor))
                schedule.Add(floor);
        }

        public static bool Dequeue(IFloor floor)
        {
            if (!schedule.Contains(floor)) return false;
            schedule.Remove(floor);
            return true;
        }

        public static bool HasHigherFloors(IFloor floor) => 
            schedule.FindIndex(floor1 => floor1.GetFloorNumber() > floor.GetFloorNumber()) != -1;

        public static bool HasLowerFloors(IFloor floor) =>
            schedule.FindIndex(floor1 => floor1.GetFloorNumber() < floor.GetFloorNumber()) != -1;

        public static bool IsEmpty => schedule.Count == 0;
    }
}