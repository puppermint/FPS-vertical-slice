using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeScript : MonoBehaviour, IWeapon
{
    public GunData data;
    public GameObject axeSwing;

    bool canShoot;
    float timeToNextShot;
    float timeBetweenShots;
    float swingTime;
    bool showSwing;

    int enemyLayerMask = 1 << 11;

    // Start is called before the first frame update
    void Start()
    {
        timeToNextShot = 1.0f / data.fireRate;
        timeBetweenShots = 1.0f / data.fireRate;
        swingTime = 0.3f;
        showSwing = true;
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (!canShoot)
        {
            timeToNextShot -= Time.deltaTime;
            if (timeToNextShot <= 0)
            {
                timeToNextShot = timeBetweenShots;
                canShoot = true;
            }
        }
        if (showSwing)
        {
            swingTime -= Time.deltaTime;
            if (swingTime <= 0)
            {
                axeSwing.SetActive(false);
                swingTime = 0.3f;
                showSwing = false;
            }
        }
    }
    
    public int GetBullets()
    {
        return 0;
    }

    public bool GetIsReloading()
    {
        return !canShoot;
    }
    
    public int HandleReload(int reserveAmmo)
    {
        return 0;
    }

    public void HandleLeftClick()
    {
        if (canShoot)
        {
            Collider[] hitColliders = Physics.OverlapBox(Camera.main.transform.position + (Camera.main.transform.forward * 1.8f), new Vector3(1.2f, 0.2f, 0.9f), Camera.main.transform.rotation, enemyLayerMask);
            for (int i = 0; i < hitColliders.Length; i++)
            {
                hitColliders[i].gameObject.GetComponent<IDamageable>().DoDamage(data.damage);
            }
            canShoot = false;
            showSwing = true;
            axeSwing.SetActive(true);
        }
    }

    public void HandleRightClick()
    {
        throw new System.NotImplementedException();
    }
}
