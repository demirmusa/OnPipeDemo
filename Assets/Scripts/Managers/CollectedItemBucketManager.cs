using UnityEngine;
using UnityEngine.UI;
using Utilities.PlayerInfoStore;

namespace Managers
{
    public class CollectedItemBucketManager : MonoBehaviour
    {
        private static readonly int Run = Animator.StringToHash("Run");
        
        [SerializeField] private int bucketItemLength = 10;

        [Header("UI")] [SerializeField] private Animator bucketFilledAnimator;
        [SerializeField] private Text percentText;
        [SerializeField] private Image percentImage;

        private int _currentCount = 0;

        private void SetUI()
        {
            if (_currentCount == bucketItemLength)
            {
                _currentCount = 0;
                bucketFilledAnimator.SetTrigger(Run);
            }

            var percent = (float) _currentCount / bucketItemLength;
            percentText.text = (int) (percent * 100) + "%";
            percentImage.fillAmount = percent;
        }

        private void OnEnable()
        {
            GlobalEvents.OnGameCollectedItemCountChanged += GlobalEventsOnOnGameCollectedItemCountChanged;
            GlobalEvents.OnGameStart += OnGameStart;
            GlobalEvents.OnGameEnd += OnGameEnd;
        }

        private void OnDisable()
        {
            GlobalEvents.OnGameCollectedItemCountChanged -= GlobalEventsOnOnGameCollectedItemCountChanged;
            GlobalEvents.OnGameStart -= OnGameStart;
            GlobalEvents.OnGameEnd -= OnGameEnd;
        }

        private void GlobalEventsOnOnGameCollectedItemCountChanged()
        {
            _currentCount++;
            SetUI();
        }

        private void OnGameStart()
        {
            _currentCount = CollectedItemBucketCountStore.Get();
            SetUI();
        }

        private void OnGameEnd()
        {
            CollectedItemBucketCountStore.Set(_currentCount);
        }
    }
}