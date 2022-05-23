using System;

namespace Spotnose.Stardust
{
    public static class Events
    {
        // Game State Events
        public static readonly Evt OnGameStart = new();
        
        public static readonly Evt<Mass> OnMassChanged = new();
        public static readonly Evt<int> OnHealthChanged = new();
        
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
