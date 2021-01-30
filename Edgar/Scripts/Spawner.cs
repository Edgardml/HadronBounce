using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject blockPrefab;
    GameObject block;

    bool canSpawn = true;
    float delay;

    // Start is called before the first frame update
    void Start()
    {
        delay = Random.Range(5f, 20f);
        InvokeRepeating("SpawnBlock", delay, delay);
    }

    void SpawnBlock()
    {
        block = Instantiate(blockPrefab, transform.position, RandomRotation());
        block.GetComponent<Block>().RandomMovement();
        if (!canSpawn)
        {
            CancelInvoke("SpawnBlock");
        }
    }

    Quaternion RandomRotation()
    {
        Quaternion rotation;
        var z = Mathf.Round(Random.Range(0, 180));
        Quaternion randomz = new Quaternion(0, 0, z * Time.fixedDeltaTime, 1);

        rotation = transform.rotation * randomz;

        return rotation;
    }
}

