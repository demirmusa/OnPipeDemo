using UnityEngine;

namespace Utilities
{
    public class CanvasGroupHelper
    {
        public static void HideAllCGsAndShowOne(CanvasGroup[] cgs, CanvasGroup toShow = null)
        {
            for (int i = 0; i < cgs.Length; i++)
            {
                if(cgs[i] == toShow)
                    EnableCG(toShow);
                else
                    DisableCG(cgs[i]);
            }
        }

        private static void EnableCG(CanvasGroup cg)
        {
            cg.alpha = 1;
            cg.interactable = true;
            cg.blocksRaycasts = true;
        }

        private static void DisableCG(CanvasGroup cg)
        {
            cg.alpha = 0;
            cg.interactable = false;
            cg.blocksRaycasts = false;
        }
    }
}