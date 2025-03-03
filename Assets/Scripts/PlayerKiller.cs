﻿using Managers;
using UnityEngine;

/// <summary>
/// PlayerKiller objeler PlayerKiller layerında ve bu layer da yalnızca player ile ile etkileşime geçebilir. Layer Collusion Matrixinden bu şekilde ayarlandı.
/// </summary>
public class PlayerKiller : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (GameManager.Instance.player.IsDead || !other.gameObject.CompareTag("Player"))
        {
            return;
        }

        Debug.Log("Killer ile öldün", gameObject);
        GameManager.Instance.SetGameOver();
    }
}