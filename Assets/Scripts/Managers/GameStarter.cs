using UnityEngine;

namespace Managers
{
    public class GameStarter : MonoBehaviour
    {
        public void Start()
        {
            if (GameManager.Instance.GameState != GameState.MENU)
            {
                return;
            }

            Debug.Log("Start Game");
            GameManager.Instance.SetGame();
        }
    }
}