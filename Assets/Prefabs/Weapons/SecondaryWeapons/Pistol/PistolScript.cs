using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistolScript : MonoBehaviour, IWeapon
{
    public GunData data;
    
    bool canShoot;
    float timeToNextShot;
    float timeBetweenShots;
    int bullets;
    bool isReloading;
    float reloadTimeLeft;

    private void Start()
    {
        timeToNextShot = 1.0f / data.fireRate;
        timeBetweenShots = 1.0f / data.fireRate;
        canShoot = true;
        isReloading = false;
        reloadTimeLeft = 0f;
        bullets = data.magSize;
    }

    private void Update()
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
        int missingFromMag = data.magSize - bullets;
        reloadTimeLeft = data.reloadSpeed;
        isReloading = true;
        return missingFromMag;
    }

    public void HandleLeftClick()
    {
        if (canShoot && bullets > 0 && !isReloading)
        {
            GameObject clone = Instantiate(data.bulletPrefab, Camera.main.transform.position, Camera.main.transform.rotation);
            bullets--;
            clone.GetComponent<Rigidbody>().velocity = clone.transform.forward * data.projectileSpeed + clone.transform.up; 
            Destroy(clone, 5.0f);

            canShoot = false;
        }
      
    }

    public void HandleRightClick()
    {
        throw new System.NotImplementedException();
    }

}
