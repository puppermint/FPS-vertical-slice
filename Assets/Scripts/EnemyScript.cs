using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyScript : MonoBehaviour, IDamageable
{
    public TMP_Text damageNum;
    int health = 100;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DoDamage(int damage)
    {
        health -= damage;
        TMP_Text clone = Instantiate(damageNum, transform.position + transform.up * 1f, Quaternion.LookRotation(transform.position - Camera.main.transform.position, Vector3.up), transform);
        clone.SetText(damage.ToString());
        Destroy(clone.gameObject, 0.5f);
    }
}
