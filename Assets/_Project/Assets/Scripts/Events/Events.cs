using System;
using UnityEngine;

namespace Spotnose.Stardust
{
    public static class Events
    {
        // Game State Events
        public static readonly GameEvent<GameObject> OnGameStarted = new();
        public static readonly GameEvent<GameState> OnBeforeGameStateChanged = new();
        public static readonly GameEvent<GameState> OnAfterGameStateChanged = new();

        // Body Change Events
        public static readonly GameEvent<BodyDetailsSO, Mass> OnBodyChanged = new();

        // Mass, Metal and Health Events
        public static readonly GameEvent<Mass> OnMassChanged = new();
        public static readonly GameEvent<Mass> OnMassReachedMin = new();
        public static readonly GameEvent<Mass> OnMassReachedMax = new();
        public static readonly GameEvent<int> OnHealthChanged = new();
        public static readonly GameEvent<int> OnMetalCountChanged = new();

        // UI Events
        public static readonly GameEvent OnUpgradeMenuInputDown = new();
        public static readonly GameEvent OnUpgradeMenuOpened = new();
        public static readonly GameEvent OnUpgradeMenuClosed = new();

        // Upgrade Events
        public static readonly GameEvent<EngineDetailsSO> OnEnginePurchased = new();
    }
}
