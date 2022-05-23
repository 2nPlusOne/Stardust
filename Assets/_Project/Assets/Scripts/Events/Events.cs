using System;
using UnityEngine;

namespace Spotnose.Stardust
{
    public static class Events
    {
        // Game State Events
        public static readonly Evt<GameObject> OnGameStarted = new();
        public static readonly Evt<GameState> OnBeforeGameStateChanged = new();
        public static readonly Evt<GameState> OnAfterGameStateChanged = new();
        
        // Mass and Health Events
        public static readonly Evt<Mass> OnMassChanged = new();
        public static readonly Evt<int> OnHealthChanged = new();
        
        // Resources Events
        public static readonly Evt<int> OnMetalChanged = new();
        
        // Events with parameters, using Evt<T>
        public static readonly Evt<ChangeEngineArgs> OnChangeEngine = new();
    }

    public class ChangeEngineArgs : EventArgs
    {
        public EngineDetailsSO engine;
        
        public ChangeEngineArgs(EngineDetailsSO engine)
        {
            this.engine = engine;
        }
    }
}
