using LiftSystem.DTO;
using LiftSystem.interfaces;
using LiftSystem.Model;

namespace LiftSystem.controllers
{
    public class Floor2 : IFloor
    {
        public override int GetFloorNumber() => 2;
    }
}