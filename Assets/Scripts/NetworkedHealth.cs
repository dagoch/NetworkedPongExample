using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections;

public class NetworkedHealth : NetworkBehaviour
{

    public const int maxHealth = 100;
    public bool destroyOnDeath;

    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;

    public Text healthText;
    private Vector3 spawnPosition;

    public override void OnStartClient()
    {
        healthText.text = currentHealth.ToString();
    }

    public override void OnStartLocalPlayer()
    {
        spawnPosition = transform.position;
    }


    public void TakeDamage(int amount)
    {
        if (!isServer)
        {
            return;
        }

        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
            else
            {
                currentHealth = maxHealth;

                // called on the Server, but invoked on the Clients
                RpcRespawn();
            }
        }

    }

    public void OnChangeHealth(int health)
    {
        Debug.Log("Got health change: " + health);
        healthText.text = health.ToString();
    }

    [ClientRpc]  // called on the Server, but invoked on the Clients
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // move back to zero location
            transform.position = spawnPosition;
        }
    }

}
