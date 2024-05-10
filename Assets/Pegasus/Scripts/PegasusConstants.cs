﻿namespace Pegasus
{
    /// <summary>
    /// Flythrough constants
    /// </summary>
    public static class PegasusConstants
    {
        /// <summary>
        /// Version information
        /// </summary>
        public static string MajorVersion = "2";
        public static string MinorVersion = "5.2";

        /// <summary>
        /// The type of flythrough
        /// </summary>
        public enum FlythroughType { SingleShot, Looped }

        /// <summary>
        /// What to do when the thing is finished - onlt relevent when it is a one shot flythrough
        /// </summary>
        public enum FlythroughEndAction { StopFlythrough, QuitApplication, PlayNextPegasus }

        /// <summary>
        /// The various states the flythrough manager can be in
        /// </summary>
        public enum FlythroughState { Stopped, Initialising, Started, Paused }

        /// <summary>
        /// The mechanism the system uses to check for minimum heights
        /// </summary>
        public enum HeightCheckType { Collision, Terrain, None }

        /// <summary>
        /// The mechanism the POI uses to check for minimum heights
        /// </summary>
        public enum PoiHeightCheckType { ManagerSettings, Collision, Terrain, None }

        /// <summary>
        /// Type of POI
        /// </summary>
        public enum PoiType { Manual, AutoGenerated }

        /// <summary>
        /// Default low offset
        /// </summary>
        public const float FlybyOffsetDefaultHeight = 1.8f;

        /// <summary>
        /// Default low offset
        /// </summary>
        public const float FlybyOffsetLow = 5f;

        /// <summary>
        /// Default high offset
        /// </summary>
        public const float FlybyOffsetHigh = 40f;

        /// <summary>
        /// Lookat type
        /// </summary>
        public enum LookatType { Path, Target }

        /// <summary>
        /// Default values for speed
        /// </summary>
        public enum SpeedType { ReallySlow, Slow, Medium, Fast, ReallyFast, Stratospheric, Custom }

        /// <summary>
        /// Really slow speed - walking speed - 0.1ms
        /// </summary>
        public const float SpeedReallySlow = 0.01f;

        /// <summary>
        /// Slow speed - walking speed - 1.4ms
        /// </summary>
        public const float SpeedSlow = 1.4f;

        /// <summary>
        /// Fast walking speed
        /// </summary>
        public const float SpeedMedium = 8f;

        /// <summary>
        /// Fast speed
        /// </summary>
        public const float SpeedFast = 25.0f;

        /// <summary>
        /// Fast speed
        /// </summary>
        public const float SpeedReallyFast = 70.0f;

        /// <summary>
        /// Stratpspheric speed
        /// </summary>
        public const float SpeedStratospheric = 250.0f;

        /// <summary>
        /// The type of easing to apply - none == linear
        /// </summary>
        public enum EasingType { Linear, EaseIn, EaseOut, EaseInOut }

        /// <summary>
        /// The target framerate
        /// </summary>
        public enum TargetFrameRate { NineFps, FifteenFps, TwentyFourFps, TwentyFiveFps, ThirtyFps, SixtyFps, NinetyFps, MaxFps, LeaveAlone };

        #region Pegasus animation constants

        public enum PegasusAnimationState { Idle, Walking, Running };
        
        #endregion


        #region Pegasus trigger constants

        /// <summary>
        /// The type of POI trigger
        /// </summary>
        public enum PoiPegasusTriggerAction { PlayPegasus, PausePegasus, ResumePegasus, StopPegasus, DoNothing }

        /// <summary>
        /// The type of POI trigger
        /// </summary>
        public enum PoiHeliosTriggerAction { FadeIn, FadeOut, DoNothing }

        /// <summary>
        /// The type of POI trigger
        /// </summary>
        public enum PoiRotateTowardsTriggerAction { Rotate, DoNothing }

        /// <summary>
        /// The type of POI trigger
        /// </summary>
        public enum PoiAnimationTriggerAction { PlayAnimation, StopAnimation, DoNothing }

        #endregion
    }
}