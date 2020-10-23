using Managers;
using UnityEngine;

/// <summary>
/// GameFinishLine objeler GameFinishLine layerında ve bu layer da yalnızca player ile ile etkileşime geçebilir. Layer Collusion Matrixinden bu şekilde ayarlandı.
/// </summary>
public class GameFinishLine : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.SetLevelComplete();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }
}