using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PongBallManager : NetworkBehaviour {

    public GameObject ball;

    void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
	}

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        var pongBall = Instantiate(ball);

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(pongBall);

    }
}
