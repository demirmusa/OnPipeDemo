using UnityEngine;
using UnityEngine.UI;
using Utilities.PlayerInfoStore;

namespace Collectables
{
    [RequireComponent(typeof(Button))]
    public class CollectableSelect : MonoBehaviour
    {
        public EnumCollectableType CollectableType = EnumCollectableType.Corn;

        private void Start()
        {
            var btn = GetComponent<Button>();
            btn.onClick.AddListener(ChangeCollectable);
        }

        private void ChangeCollectable()
        {
            var currentType = PlayerCollectableTypeInfoStore.Get();
            if (currentType == CollectableType)
            {
                return;
            }

            PlayerCollectableTypeInfoStore.Set(CollectableType);
            GlobalEvents.InvokeOnCollectableTypeChange();
        }
    }
}