using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class GameStarter : MonoBehaviour, IPointerDownHandler 
    {
        public void OnPointerDown(PointerEventData eventData)
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