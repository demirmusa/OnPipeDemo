using UnityEngine;
using UnityEngine.UI;
using Utilities.PlayerInfoStore;

namespace UI
{
    public class GameUIManager : MonoBehaviour
    {
        [SerializeField] private Text levelText;
        [SerializeField] private Text collectedItemText;
        [SerializeField] private CanvasGroup canvasGroup;

        private void OnGameStart()
        {
            collectedItemText.text = "High Score: " + PlayerCollectedItemStore.GetHighScore().ToString();
            levelText.text = "Level " + PlayerLevelStore.GetCurrentLevel().ToString();
        }

       

        private void OnEnable()
        {
            GlobalEvents.OnGameStart += OnGameStart;
        }

        private void OnDisable()
        {
            GlobalEvents.OnGameStart -= OnGameStart;
        }
    }
}