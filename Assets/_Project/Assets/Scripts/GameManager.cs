using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        [SerializeField] private GameObject playerGameObject;
        
        [SerializeField] private BodyDetailsSO startingBody;

        public GameState CurrentState { get; private set; }
        public Player Player { get; private set; }

        private void Start()
        {
            Player = playerGameObject.GetComponent<Player>();
            ChangeState(GameState.Starting);
        }

        public void ChangeState(GameState newState)
        {
            Events.OnBeforeGameStateChanged.Invoke(CurrentState);
            
            CurrentState = newState;
            switch (newState)
            {
                case GameState.MainMenu:
                    HandleMainMenu();
                    break;
                case GameState.Starting:
                    HandleStarting();
                    break;
                case GameState.Gameplay:
                    HandleGameplay();
                    break;
                case GameState.UpgradeMenu:
                    HandleUpgradeMenu();
                    break;
                case GameState.Paused:
                    HandlePaused();
                    break;
                case GameState.GameOver:
                    HandleGameOver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(newState), newState, null);
            }
            
            Events.OnAfterGameStateChanged.Invoke(newState);
        }

        private void HandleMainMenu()
        {
            throw new NotImplementedException();
        }

        private void HandleStarting()
        {
            Events.OnGameStarted.Invoke(playerGameObject);
            Player.SetBodyDetails(startingBody);
            ChangeState(GameState.Gameplay);
        }

        private void HandleGameplay()
        {
            print("Changing state to gameplay...");
        }

        private void HandleUpgradeMenu()
        {
            throw new NotImplementedException();
        }

        private void HandlePaused()
        {
            throw new NotImplementedException();
        }

        private void HandleGameOver()
        {
            throw new NotImplementedException();
        }
    }
    
    [Serializable]
    public enum GameState 
    {
        MainMenu,
        Starting,
        Gameplay,
        UpgradeMenu,
        Paused,
        GameOver
    }
}
