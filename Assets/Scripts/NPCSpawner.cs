using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NPCSpawner : NetworkBehaviour
{

    public GameObject npcPrefab;
    public int numberOfNPCs;
    public float maxDist = 7.0f;

    public override void OnStartServer()
    {
        for (int i = 0; i < numberOfNPCs; i++)
        {
            var spawnPosition = new Vector3(
                Random.Range(-maxDist, maxDist),
                0.0f,
                Random.Range(-maxDist, maxDist));

            var spawnRotation = Quaternion.Euler(
                0.0f,
                Random.Range(0, 180),
                0.0f);

            var enemy = (GameObject)Instantiate(npcPrefab, spawnPosition, spawnRotation);
            NetworkServer.Spawn(enemy);
        }
    }
}
