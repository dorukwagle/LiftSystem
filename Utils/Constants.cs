
using LiftSystem.controllers;
using LiftSystem.interfaces;

namespace LiftSystem
{
    public static class Constants
    {
        public const float LeftPanelWidthPercent = 0.3f;
        public const float RightPanelWidthPercent = 1 - LeftPanelWidthPercent;
        public const string LogsButtonShowText = "Show Logs >>";
        public const string LogsButtonHideText = "<< Hide Logs";
        public const string SqlConnectionString = "server=localhost;database=liftsystem;uid=root;pwd=doruk;";
        public const int DoorAnimationDuration = 1; //sec
        public static readonly IFloor[] Floors =
        {
            new Floor1(), new Floor2(), new Floor3(), new Floor4()
        };
    }
}