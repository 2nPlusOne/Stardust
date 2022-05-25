using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spotnose.Stardust
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        [SerializeField] private GameObject playerPrefab;
        
        [SerializeField] private BodyDetailsSO startingBody;

        public GameState CurrentState { get; private set; }
        public Player Player { get; private set; }

        private void Start()
        {
            ChangeState(GameState.Starting);
        }

        public void ChangeState(GameState newState)
        {
            Events.OnBeforeGameStateChanged.Invoke(newState);
            
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
        }

        private void HandleMainMenu()
        {
            throw new NotImplementedException();
        }

        private void HandleStarting()
        {
            Player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
            Player.SetBodyDetails(startingBody);
            Events.OnGameStarted.Invoke(Player.gameObject);
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
