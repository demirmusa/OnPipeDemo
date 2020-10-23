using Players;
using UI;
using UnityEngine;
using Utilities.PlayerInfoStore;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public Player player;
        public CanvasGroupManager canvasGroupManager;
        public static GameManager Instance;

        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Multiple Game Manager " + gameObject.name);
                Destroy(this);
            }

            Instance = this;

            SetMenu();
        }

        private float _currentPipeScale = 1;

        public float CurrentPipeScale
        {
            get => _currentPipeScale;
            set
            {
                if (_currentPipeScale == value)
                {
                    return;
                }

                _currentPipeScale = value;
                GlobalEvents.InvokeOnPipeScaleChanged();
            }
        }

        public GameState GameState { get; private set; }

        public void SetMenu()
        {
            GameState = GameState.MENU;
            canvasGroupManager.OpenMenuCanvas();
        }

        public void SetGame()
        {
            GameState = GameState.GAME;
            canvasGroupManager.OpenGameCanvas();
            
            CurrentCollectedItemCount = PlayerCollectedItemStore.GetCurrent();
            GlobalEvents.InvokeOnGameStart();
        }

        public void SetLevelComplete()
        {
            if (GameState != GameState.GAME)
            {
                return;
            }

            GameState = GameState.LEVELCOMPLETE;
            canvasGroupManager.OpenLevelCompleteCanvas();

            PlayerCollectedItemStore.StoreCurrent(CurrentCollectedItemCount);
            PlayerLevelStore.AddLevel();

            Debug.Log("Level Completed");
        }

        public void SetGameOver()
        {
            if (GameState != GameState.GAME)
            {
                return;
            }

            GameState = GameState.GAMEOVER;
            canvasGroupManager.OpenGameOverCanvas();

            PlayerCollectedItemStore.StoreCurrent(0);
            player.Die();

            Debug.Log("Game Over");
        }

        private int _currentCollectedItemCount = 1;

        public int CurrentCollectedItemCount
        {
            get => _currentCollectedItemCount;
            set
            {
                if (_currentCollectedItemCount == value)
                {
                    return;
                }

                _currentCollectedItemCount = value;
                GlobalEvents.InvokeOnGameCollectedItemCountChanged();
            }
        }
    }
}