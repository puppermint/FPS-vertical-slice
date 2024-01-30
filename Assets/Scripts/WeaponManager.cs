using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponManager : MonoBehaviour
{

    public GameObject primaryWeapon;
    public GameObject secondaryWeapon;
    public Transform handHardPoint;

    public int maxPrimaryAmmo = 250;
    int primaryAmmo = 100;

    int activeItem = 0;
    int weaponLayerMask = 1 << 10;
    int ammoBoxLayerMask = 1 << 12;

    IWeapon primaryScript;
    IWeapon secondaryScript;
    IWeapon activeScript;

    public TMP_Text reloadingText;
    public TMP_Text primaryAmmoText;
    public TMP_Text secondaryAmmoText;
    public TMP_Text grabText;

    // Start is called before the first frame update
    void Start()
    {
        
        if (primaryWeapon != null && secondaryWeapon != null)
        {
            primaryWeapon = Instantiate(primaryWeapon, handHardPoint.position, handHardPoint.rotation, handHardPoint);
            primaryScript = primaryWeapon.GetComponent<IWeapon>();
            activeItem = 1;
            activeScript = primaryScript;

            secondaryWeapon = Instantiate(secondaryWeapon, handHardPoint.position, handHardPoint.rotation, handHardPoint);
            secondaryScript = secondaryWeapon.GetComponent<IWeapon>();
            secondaryWeapon.SetActive(false);
        } 
        else if (primaryWeapon != null)
        {
            primaryWeapon = Instantiate(primaryWeapon, handHardPoint.position, handHardPoint.rotation, handHardPoint);
            primaryScript = primaryWeapon.GetComponent<IWeapon>();
            activeItem = 1;
            activeScript = primaryScript;
        }
        else if (secondaryWeapon != null)
        {
            secondaryWeapon = Instantiate(secondaryWeapon, handHardPoint.position, handHardPoint.rotation, handHardPoint);
            secondaryScript = secondaryWeapon.GetComponent<IWeapon>();
            activeItem = 2;
            activeScript = secondaryScript;
        }
        else
        {
            Debug.Log("No weapons equipped");
        }
        
        

    }

    // Update is called once per frame
    void Update()
    {
        if (grabText.gameObject.activeSelf)
            grabText.gameObject.SetActive(false);

        if (primaryScript != null)
            primaryAmmoText.SetText(primaryScript.GetBullets() + "/" + primaryAmmo);
        if (secondaryScript != null)
            secondaryAmmoText.SetText(secondaryScript.GetBullets() + "/ inf.");

        if (activeScript.GetIsReloading())
        {
            reloadingText.gameObject.SetActive(true);
        } else
        {
            reloadingText.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown("r") && !activeScript.GetIsReloading())
        {
            if (activeItem == 1 && primaryAmmo > 0)
            {
                primaryAmmo = activeScript.HandleReload(primaryAmmo);
            }
            else if (activeItem == 2)
            {
                activeScript.HandleReload(-1);
            }
        }

        if (Input.GetKeyDown("1") && activeItem != 1 && primaryWeapon != null && !activeScript.GetIsReloading())
        {
            foreach(Transform child in handHardPoint)
            {
                child.gameObject.SetActive(false);
            }
            primaryWeapon.SetActive(true);
            activeScript = primaryScript;
            activeItem = 1;
        }

        if (Input.GetKeyDown("2") && activeItem != 2 && secondaryWeapon != null && !activeScript.GetIsReloading())
        {
            foreach (Transform child in handHardPoint)
            {
                child.gameObject.SetActive(false);
            }
            secondaryWeapon.SetActive(true);
            activeScript = secondaryScript;
            activeItem = 2;
        }

        if (Input.GetMouseButton(0))
        {
            activeScript.HandleLeftClick();
        }

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f, weaponLayerMask))
        {
            grabText.gameObject.SetActive(true);
            if (Input.GetKeyDown("e")) 
            { 
                if (hit.transform.gameObject.GetComponent<WeaponTags>().tags.Contains("Primary"))
                {
                    if (primaryWeapon != null)
                    {
                        primaryWeapon.SetActive(true);
                        primaryWeapon.transform.parent = null;
                    }
                        

                    primaryWeapon = hit.transform.gameObject;
                    primaryScript = hit.transform.gameObject.GetComponent<IWeapon>();
                    primaryWeapon.transform.SetParent(handHardPoint);
                    primaryWeapon.transform.position = handHardPoint.position;
                    primaryWeapon.transform.rotation = handHardPoint.rotation;

                    foreach (Transform child in handHardPoint)
                    {
                        child.gameObject.SetActive(false);
                    }
                    primaryWeapon.SetActive(true);
                    activeScript = primaryScript;
                    activeItem = 1;
                }
                else if (hit.transform.gameObject.GetComponent<WeaponTags>().tags.Contains("Secondary"))
                {
                    if (secondaryWeapon != null)
                    {
                        secondaryWeapon.SetActive(true);
                        secondaryWeapon.transform.parent = null;
                    }
                        

                    secondaryWeapon = hit.transform.gameObject;
                    secondaryScript = hit.transform.gameObject.GetComponent<IWeapon>();
                    secondaryWeapon.transform.SetParent(handHardPoint);
                    secondaryWeapon.transform.position = handHardPoint.position;
                    secondaryWeapon.transform.rotation = handHardPoint.rotation;

                    foreach (Transform child in handHardPoint)
                    {
                        child.gameObject.SetActive(false);
                    }
                    secondaryWeapon.SetActive(true);
                    activeScript = secondaryScript;
                    activeItem = 2;
                }
            }

        }

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f, ammoBoxLayerMask))
        {
            grabText.gameObject.SetActive(true);
            if (Input.GetKeyDown("e"))
            {
                primaryAmmo = maxPrimaryAmmo;
            }
        }

    }
}
