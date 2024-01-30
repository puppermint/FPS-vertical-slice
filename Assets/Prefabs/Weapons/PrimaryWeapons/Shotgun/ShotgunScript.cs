using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunScript : MonoBehaviour, IWeapon
{
    public GunData data;
    public int pellots = 12;

    bool canShoot;
    float timeToNextShot;
    float timeBetweenShots;
    int bullets;
    bool isReloading;
    float reloadTimeLeft;
    int missingFromMag;

    void Start()
    {
        timeToNextShot = 1.0f / data.fireRate;
        timeBetweenShots = 1.0f / data.fireRate;
        canShoot = true;
        isReloading = false;
        reloadTimeLeft = 0f;
        bullets = data.magSize;
    }

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
        if (isReloading)
        {
            reloadTimeLeft -= Time.deltaTime;
            if (reloadTimeLeft <= 0)
            {
                bullets = data.magSize;
                if (missingFromMag < 0)
                    bullets += missingFromMag;
                reloadTimeLeft = 0;
                isReloading = false;
            }
        }
    }

    public int GetBullets()
    {
        return bullets;
    }

    public bool GetIsReloading()
    {
        return isReloading;
    }

    public int HandleReload(int reserveAmmo)
    {
        missingFromMag = data.magSize - bullets;
        reserveAmmo -= missingFromMag;
        if (reserveAmmo < 0)
        {
            missingFromMag = reserveAmmo;
            reserveAmmo -= reserveAmmo;
        }
        reloadTimeLeft = data.reloadSpeed;
        isReloading = true;
        return reserveAmmo;
    }

    public void HandleLeftClick()
    {
        if (canShoot && bullets > 0 && !isReloading)
        {
            for (int i = 0; i < pellots; i++)
            {
                GameObject clone = Instantiate(data.bulletPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
                
                clone.GetComponent<Rigidbody>().velocity = (clone.transform.forward * data.projectileSpeed) + (clone.transform.up * Random.Range(-1f * data.spread, data.spread)) + (clone.transform.right * Random.Range(-1f * data.spread, data.spread)) + clone.transform.up;
                Destroy(clone, 5.0f);
            }
            bullets--;
            canShoot = false;
        }
    }

    public void HandleRightClick()
    {
        throw new System.NotImplementedException();
    }

}
