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

        // Body Change Events
        public static readonly Evt<BodyDetailsSO, Mass> OnBodyChanged = new();

        // Mass, Metal and Health Events
        public static readonly Evt<Mass> OnMassChanged = new();
        public static readonly Evt<Mass> OnMassReachedMin = new();
        public static readonly Evt<Mass> OnMassReachedMax = new();
        public static readonly Evt<int> OnHealthChanged = new();
        public static readonly Evt<int> OnMetalCountChanged = new();

        // UI Events
        public static readonly Evt OnUpgradeMenuInputDown = new();
        public static readonly Evt OnUpgradeMenuOpened = new();
        public static readonly Evt OnUpgradeMenuClosed = new();

        // Upgrade Events
        public static readonly Evt<EngineDetailsSO> OnEnginePurchased = new();
    }
}
