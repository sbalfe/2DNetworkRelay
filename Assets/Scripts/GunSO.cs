using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewGun", menuName = "ScriptableObjects/GunScriptableObject", order = 1)]
public class GunSO : ScriptableObject
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Sprite sprite;

    public GameObject GetProjectile()
    {
        return projectile;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }
}
