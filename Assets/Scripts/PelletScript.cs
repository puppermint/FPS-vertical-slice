using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PelletScript : MonoBehaviour
{
    public GunData data;
    int enemyLayerMask = 1 << 11;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.2f, enemyLayerMask);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            IDamageable tempScript = hitColliders[i].gameObject.GetComponent<IDamageable>();
            if (tempScript != null)
            {
                tempScript.DoDamage(data.damage);
                Destroy(gameObject);
                break;
            }
        }
    }
    /*
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 11)
        {
            IDamageable collideScript = other.gameObject.GetComponent<IDamageable>();
            if (collideScript != null)
            {
                collideScript.DoDamage(data.damage);
            }
            Destroy(gameObject);
        }
    }*/
}
