using Managers;
using UnityEngine;

/// <summary>
/// Dedektör Dedector layerında ve bu layer da yalnızca bir boru ile ile etkileşime geçebilir. Layer Collusion Matrixinden bu şekilde ayarlandı.
/// </summary>
public class Detector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.CurrentPipeScale = other.transform.localScale.x;
    }
}