using UnityEngine;

namespace Managers
{
    public class GameStarter : MonoBehaviour
    {
        private void Update()
        {
            if (GameManager.Instance.GameState != GameState.MENU)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Start Game");
                GameManager.Instance.SetGame();
            }
        }
    }
}