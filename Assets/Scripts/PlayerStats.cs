using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerStats : NetworkBehaviour
{
    private const int StartingHealth = 100;
    private NetworkVariable<int> _playerHealth = new NetworkVariable<int>(StartingHealth);

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            Debug.Log("Adding CameraController");
            gameObject.AddComponent<CameraController>();
        }
    }

    [ClientRpc]
    private void DieClientRpc()
    {
        // Player died
        GetComponent<SpriteRenderer>().color = Color.red;
        Debug.Log("Dead");
    }

    [ServerRpc(RequireOwnership = false)]
    private void DealDamageServerRpc(int damage, ulong shooterPlayerID)
    {
        // Deal the damage
        _playerHealth.Value -= damage;
        Debug.Log("Hit! Player health: " + _playerHealth);
        if (_playerHealth.Value <= 0)
        {
            Debug.Log("Player killed by ID " + shooterPlayerID);
            // Player died
            DieClientRpc();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Projectile"))
        {
            var projectileDamage = other.gameObject.GetComponent<Projectile>().GetProjectileDamage();
            var projectileShooterID = other.gameObject.GetComponent<Projectile>().GetShooterPlayerID();
            Debug.Log("Hit by " + projectileShooterID + " for " + projectileDamage + " HP!");
            DealDamageServerRpc(projectileDamage, projectileShooterID);
        }
    }
}