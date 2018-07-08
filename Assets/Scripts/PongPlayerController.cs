using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PongPlayerController : NetworkBehaviour {

    public GameObject playerPaddle;
    public float cameraHeight = 3.0f;
    public float cameraDistance = 7.0f;
    public float lookToDistanceDivisor = 3.0f;
    //public Camera playerCamera;
    public float dirFromCtr;

	// Use this for initialization
	void Start () {
		
	}
	
    public override void OnStartLocalPlayer()
    {
        if (playerPaddle == null) playerPaddle = gameObject;
        playerPaddle.GetComponent<Renderer>().material.color = Color.blue;

        float lookToDistance = cameraDistance / lookToDistanceDivisor;

        //playerCamera.gameObject.SetActive(true);
        Vector3 camPosition = transform.position;
        dirFromCtr = Mathf.Sign(camPosition.z);

        camPosition.z = dirFromCtr * cameraDistance;
        camPosition.x = 0;
        camPosition.y = cameraHeight;
        Quaternion camRotation = transform.rotation;
        camRotation.SetLookRotation(new Vector3(0,0,dirFromCtr*lookToDistance) - camPosition);
        Camera.main.transform.SetPositionAndRotation(camPosition, camRotation);


    }

	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 10.0f * -dirFromCtr;
        if ( (x < 0 && playerPaddle.transform.position.x > -1.8f) ||
            (x > 0 && playerPaddle.transform.position.x < 1.8f) )
            playerPaddle.transform.Translate(x, 0, 0,Space.World);
         
            
	}
}
