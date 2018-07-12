using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class NPCController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    [SyncVar]
    public Vector3 lastPosition;
    [SyncVar]
    public Vector3 nextPosition;

    public float speed;
    bool walking = false;

    public override void OnStartServer() {
        InvokeRepeating("CmdFire", 2.0f, 1.0f);
    } 

    void Update()
    {
        if (!isServer) {
            return;
        }

        if (!walking)
        {
            walking = true;
            Vector2 rand = Random.insideUnitCircle.normalized;
            nextPosition = this.transform.position + new Vector3(rand.x, 0, rand.y);
            lastPosition = this.transform.position;
            StartCoroutine(Walk());
        }
    }

    IEnumerator Walk()
    {

        float count = 0;
        while (count < speed)
        {
            count += Time.deltaTime;
            this.transform.position = Vector3.Lerp(lastPosition, nextPosition, count / speed);
            yield return null;
        }
        walking = false;


    }



    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var bullet = (GameObject)Instantiate(
            bulletPrefab,
            bulletSpawn.position,
            bulletSpawn.rotation);

        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6;

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);

        // Destroy the bullet after 2 seconds
        Destroy(bullet, 2.0f);
    }
}
