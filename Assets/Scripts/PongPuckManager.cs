using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PongPuckManager : NetworkBehaviour {

    public GameObject ball;
    public float initSpeed = 5.0f;

    void Start () {
		
	}
	
	void Update () {
        if (!isLocalPlayer) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonUp(0)) // || Input.GetMouseButton(0)
        {
            CmdFire();
        }
	}

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var pongBall = Instantiate(ball);
//        pongBall.transform.position = new Vector3(0, 5, 0);
//        pongBall.GetComponent<Rigidbody>().useGravity = true;

        float z = initSpeed;
        if (Random.value > 0.5f)
            z = z * -1;
        float x = (Random.value - 0.5f) * (initSpeed/2);

        pongBall.GetComponent<Rigidbody>().AddForce(x, 0, z, ForceMode.Impulse);

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(pongBall);

    }
}
