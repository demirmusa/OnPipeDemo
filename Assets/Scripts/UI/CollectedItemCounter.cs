using Managers;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class CollectedItemCounter : MonoBehaviour
    {
        [SerializeField] private Text collectedItemText;

        private void GlobalEventsOnOnGameCollectedItemCountChanged()
        {
            collectedItemText.text = GameManager.Instance.CurrentCollectedItemCount.ToString();
        }

        private void OnEnable()
        {
            GlobalEvents.OnGameCollectedItemCountChanged += GlobalEventsOnOnGameCollectedItemCountChanged;
        }

        private void OnDisable()
        {
            GlobalEvents.OnGameCollectedItemCountChanged -= GlobalEventsOnOnGameCollectedItemCountChanged;
        }
    }
}