using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>();
        if (health == null) 
            
        if (health != null)
        {
            health.TakeDamage(10);
        } else {
            var nhealth = hit.GetComponent<NetworkedHealth>();
            if (nhealth != null)
                {
                    nhealth.TakeDamage(10);
                }
        }

        Destroy(gameObject);
    }
}

