using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float damageRate;

    private List<IDamageable> thingsToDamage = new List<IDamageable>();


    private void Start()
    {
        StartCoroutine(DealDamageCo());
    }

    IEnumerator DealDamageCo()
    {
        while(true)
        {
            foreach(IDamageable damageable in thingsToDamage)
            {
                damageable.TakeDamage(damage);
            }

            yield return new WaitForSeconds(damageRate);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.GetComponent<IDamageable>() != null)
        {
            thingsToDamage.Add(collision.gameObject.GetComponent<IDamageable>());
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.GetComponent<IDamageable>() != null)
        {
            thingsToDamage.Remove(collision.gameObject.GetComponent<IDamageable>());
        }
    }
}
