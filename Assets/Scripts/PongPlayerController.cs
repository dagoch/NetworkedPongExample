using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PongPlayerController : NetworkBehaviour {

    public GameObject playerPaddle;

	// Use this for initialization
	void Start () {
		
	}
	
    public override void OnStartLocalPlayer()
    {
        playerPaddle.GetComponent<Renderer>().material.color = Color.blue;
    }

	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f;
        if ( (x < 0 && playerPaddle.transform.position.x > -1.8f) ||
            (x > 0 && playerPaddle.transform.position.x < 1.8f) )
            playerPaddle.transform.Translate(x, 0, 0,Space.World);
         
            
	}
}
