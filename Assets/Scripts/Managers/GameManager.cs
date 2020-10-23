using Players;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public Player player;
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
        }

        public void SetGame()
        {
            GameState = GameState.GAME;
        }

        public void SetLevelComplete()
        {
            GameState = GameState.LEVELCOMPLETE;
        }

        public void SetGameOver()
        {
            Debug.Log("Died Bro");
            GameState = GameState.GAMEOVER;
            player.Die();
        }
    }
}