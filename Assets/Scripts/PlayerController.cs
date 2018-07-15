using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour
{
    public GameObject bulletPrefab;
    public Transform bulletSpawn;
    public Transform cameraTransform;
    public Transform visorTransform;

    public override void OnStartLocalPlayer()
    {
        GetComponent<Renderer>().material.color = Color.blue;
        cameraTransform = Camera.main.transform;
        //Camera.main.transform.parent = transform;
        visorTransform = transform.Find("Visor");
        Camera.main.transform.position = visorTransform.position;
        Debug.Log("start local player: visor position = " + visorTransform.position + " camera posn = " + Camera.main.transform.position);
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }


        // rotate the player to match the camera's rotation (controlled by GoogleVR)
        transform.rotation = cameraTransform.rotation;


        // move the player according to input
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;
        transform.Translate(0, 0, z); 

        // move the camera to match the player's position
        cameraTransform.position = visorTransform.position;

        if (Input.GetKeyDown(KeyCode.Space)  || Input.GetMouseButtonUp(0)) // || Input.GetMouseButton(0)
        {
            CmdFire();
        }
    }

    // This [Command] code is called on the Client …
    // … but it is run on the Server!
    [Command]
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
