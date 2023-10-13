using System;
using System.Windows.Controls;
using LiftSystem.Enums;
using LiftSystem.interfaces;
using LiftSystem.Model;

namespace LiftSystem.controllers
{
    public class Floor1 : IFloor
    {
        public override int GetFloorNumber() => 1;
    }
}