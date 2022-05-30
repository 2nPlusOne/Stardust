using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Spotnose.Stardust
{
    public static class Events
    {
        // Game State Events
        public static readonly GameEvent<GameObject> OnGameStarted = new();
        public static readonly GameEvent<GameState, GameState> OnBeforeGameStateChanged = new();
        public static readonly GameEvent<GameState, GameState> OnAfterGameStateChanged = new();

        // Body Change Events
        public static readonly GameEvent<BodyDetailsSO, Mass> OnBodyChanged = new();

        // Mass, Metal and Health Events
        public static readonly GameEvent<Mass> OnMassChanged = new();
        public static readonly GameEvent<Mass> OnMassReachedMin = new();
        public static readonly GameEvent<Mass> OnMassReachedMax = new();
        public static readonly GameEvent<Mass> OnMassReachedZero = new();
        public static readonly GameEvent<int> OnHealthChanged = new();
        public static readonly GameEvent<int> OnMetalCountChanged = new();
        
        // Player Control Events
        public static readonly GameEvent OnThrustInputDown = new();
        public static readonly GameEvent OnThrustInputUp = new();

        // UI Events
        public static readonly GameEvent OnUpgradeMenuInputDown = new();
        public static readonly GameEvent OnUpgradeMenuOpened = new();
        public static readonly GameEvent OnUpgradeMenuClosed = new();
        public static readonly GameEvent OnPauseMenuInputDown = new();
        public static readonly GameEvent OnPauseMenuOpened = new();
        public static readonly GameEvent OnPauseMenuClosed = new();
        public static readonly GameEvent OnMainMenuInputDown = new();
        public static readonly GameEvent OnIntroScrollFinished = new();

        // Upgrade Events
        public static readonly GameEvent<EngineDetailsSO> OnEngineUpgradePurchased = new();
        public static readonly GameEvent<EngineDetailsSO> OnEngineChanged = new();
    }
}
