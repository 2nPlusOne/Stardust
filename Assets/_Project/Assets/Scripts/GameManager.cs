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
        
        private void OnEnable()
        {
            Events.OnPauseMenuInputDown.AddListener(OnPauseMenuInputDown);
            Events.OnUpgradeMenuInputDown.AddListener(OnUpgradeMenuInputDown);
        }
        
        private void OnDisable()
        {
            Events.OnPauseMenuInputDown.RemoveListener(OnPauseMenuInputDown);
            Events.OnUpgradeMenuInputDown.RemoveListener(OnUpgradeMenuInputDown);
        }

        private void Start()
        {
            Player = playerGameObject.GetComponent<Player>();
            ChangeState(GameState.Starting);
        }
        
        private void OnPauseMenuInputDown()
        {
            if (CurrentState == GameState.Paused)
            {
                ChangeState(GameState.Playing);
                Events.OnPauseMenuClosed.Invoke();
            }
            else
            {
                ChangeState(GameState.Paused);
                Events.OnPauseMenuOpened.Invoke();
            }
        }
        
        private void OnUpgradeMenuInputDown()
        {
            if (CurrentState == GameState.UpgradeMenu)
            {
                ChangeState(GameState.Playing);
                Events.OnUpgradeMenuClosed.Invoke();
            }
            else
            {
                ChangeState(GameState.UpgradeMenu);
                Events.OnUpgradeMenuOpened.Invoke();
            }
        }

        public void ChangeState(GameState newState)
        {
            Events.OnBeforeGameStateChanged.Invoke(CurrentState, newState);
            
            var oldState = CurrentState;
            CurrentState = newState;
            switch (newState)
            {
                case GameState.MainMenu:
                    HandleMainMenu();
                    break;
                case GameState.Starting:
                    HandleStarting();
                    break;
                case GameState.Playing:
                    HandlePlaying();
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
            
            Events.OnAfterGameStateChanged.Invoke(oldState, newState);
        }

        private void HandleMainMenu()
        {
            print("Changing state to main menu...");
        }

        private void HandleStarting()
        {
            print("Changing state to starting...");
            Events.OnGameStarted.Invoke(playerGameObject);
            Player.SetBodyDetails(startingBody);
            ChangeState(GameState.Playing);
        }

        private void HandlePlaying()
        {
            print("Changing state to playing...");
            Time.timeScale = 1f;
        }

        private void HandleUpgradeMenu()
        {
            print("Changing state to upgrade menu...");
            Time.timeScale = 0f;
        }

        private void HandlePaused()
        {
            print("Changing state to paused...");
            Time.timeScale = 0f;
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
        Playing,
        UpgradeMenu,
        Paused,
        GameOver
    }
}
