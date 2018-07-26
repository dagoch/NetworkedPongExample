using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PongBallManager : NetworkBehaviour {

    public GameObject ball;
    GameObject pongBall;
    public string score;
    TextMesh text;

    void Start () {
		
	}
	
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CmdFire();
        }
        if (pongBall != null) ;
            score = "Score/nPlayer 1: " + pongBall.GetComponent<PongBallController>().scoreA +
                    "Player 2: " + pongBall.GetComponent<PongBallController>().scoreB;
	}

    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        pongBall = Instantiate(ball);

        // Spawn the bullet on the Clients
        NetworkServer.Spawn(pongBall);

    }
}
