using Managers;
using UnityEngine;

namespace Players
{
    public class PlayerScaleManager : MonoBehaviour
    {
        [SerializeField] private GameObject arch;
        private Vector3 _startScale;
        private bool _checkScaleChanges;

        private void Start()
        {
            _startScale = arch.transform.localScale;
        }

        private bool isDown = false;

        private void Update()
        {
            if (GameManager.Instance.GameState != GameState.GAME)
            {
                return;
            }

            if (Input.GetMouseButton(0) && !isDown)
            {
                SetScale();
                isDown = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                ResetScale();
                isDown = false;
            }

            if (_checkScaleChanges)
            {
                _checkScaleChanges = false;
                //boru karakterden daha büyükse oyun biter
                if (arch.transform.localScale.x < GameManager.Instance.CurrentPipeScale)
                {
                    Debug.Log("_checkScaleChanges ile öldün");
                    GameManager.Instance.SetGameOver();
                }
                else if (Input.GetMouseButton(0)) //boru karakterden daha küçükse ve basmaya devam ediyorsa karakteri küçült
                {
                    SetScale();
                }
            }
        }

        public void ResetScale()
        {
            arch.transform.localScale = _startScale;
        }

        private void SetScale()
        {
            arch.transform.localScale = new Vector3(GameManager.Instance.CurrentPipeScale, GameManager.Instance.CurrentPipeScale, arch.transform.localScale.z);
        }

        private void OnEnable()
        {
            GlobalEvents.OnPipeScaleChanged += OnPipeScaleChanged;
        }

        private void OnDisable()
        {
            GlobalEvents.OnPipeScaleChanged -= OnPipeScaleChanged;
        }

        private void OnPipeScaleChanged()
        {
            _checkScaleChanges = true;
        }
    }
}