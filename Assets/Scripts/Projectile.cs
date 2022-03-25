using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

[RequireComponent(typeof(NetworkRigidbody2D))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Projectile : NetworkBehaviour
{
    [SerializeField] private int projectileDamage = 10;
    [SerializeField] private float projectileSpeed = 1f;
    private float lifeTime = 4f;
    private ulong shooterPlayerID;

    public override void OnNetworkSpawn()
    {
        // Get the provided velocity and make sure its speed is correct
        Vector2 unitVelocity = GetComponent<Rigidbody2D>().velocity.normalized;
        GetComponent<Rigidbody2D>().velocity = unitVelocity * projectileSpeed;
        StartCoroutine(TimeToLive());
    }

    public int GetProjectileDamage()
    {
        return projectileDamage;
    }

    public void SetShooterPlayerID(ulong ownerID)
    {
        shooterPlayerID = ownerID;
    }

    public ulong GetShooterPlayerID()
    {
        return shooterPlayerID;
    }

    IEnumerator TimeToLive()
    {
        yield return new WaitForSeconds(lifeTime);
        DestroyServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    public void DestroyServerRpc()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        DestroyServerRpc();
    }
}