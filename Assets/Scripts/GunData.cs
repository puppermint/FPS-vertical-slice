using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GunData : ScriptableObject
{

    public int damage = 5;
    public float projectileSpeed = 8.0f;
    public GameObject bulletPrefab;
    public int magSize = 10;
    public float reloadSpeed = 3.0f;
    public float fireRate = 0.5f;
    public float spread = 0.2f;

}
