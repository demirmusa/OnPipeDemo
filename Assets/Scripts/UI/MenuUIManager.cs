using UnityEngine;
using UnityEngine.UI;
using Utilities.PlayerInfoStore;

namespace UI
{
    public class MenuUIManager : MonoBehaviour
    {
        [SerializeField] private Text levelText;
        [SerializeField] private Text highScoreText;

        private void OnSetMenu()
        {
            highScoreText.text = "High Score: " + PlayerCollectedItemStore.GetHighScore().ToString();
            levelText.text = "Level " + PlayerLevelStore.GetCurrentLevel().ToString();
        }       

        private void OnEnable()
        {
            GlobalEvents.OnSetMenu += OnSetMenu;
        }

        private void OnDisable()
        {
            GlobalEvents.OnSetMenu -= OnSetMenu;
        }
    }
}