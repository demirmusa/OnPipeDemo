using UnityEngine;
using Utilities;

namespace UI
{
    public class CanvasGroupManager : MonoBehaviour
    {
        [Header(" Canvases ")] public CanvasGroup MENU;
        public CanvasGroup GAME;
        public CanvasGroup LEVELCOMPLETE;
        public CanvasGroup GAMEOVER;

        CanvasGroup[] canvases;

        private void Awake()
        {
            canvases = new CanvasGroup[] {MENU, GAME, LEVELCOMPLETE, GAMEOVER};
        }

        public void OpenMenuCanvas()
        {
            CanvasGroupHelper.HideAllCGsAndShowOne(canvases, MENU);
        }
        
        public void OpenGameCanvas()
        {
            CanvasGroupHelper.HideAllCGsAndShowOne(canvases, GAME);
        }
        
        public void OpenLevelCompleteCanvas()
        {
            CanvasGroupHelper.HideAllCGsAndShowOne(canvases, LEVELCOMPLETE);
        }
        
        public void OpenGameOverCanvas()
        {
            CanvasGroupHelper.HideAllCGsAndShowOne(canvases, GAMEOVER);
        }
    }
}