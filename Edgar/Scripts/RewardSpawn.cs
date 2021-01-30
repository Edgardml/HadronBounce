using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardSpawn : MonoBehaviour
{
    [SerializeField]
    GameObject rewardPrefab;
    GameObject reward;

    bool canSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        var delay = Random.Range(5f, 30f);
        InvokeRepeating("SpawnReward", 2f, delay);
    }

    // Update is called once per frame
    void SpawnReward()
    {
        reward = Instantiate(rewardPrefab, RandomPosition(), Quaternion.identity);
        if (!canSpawn)
        {
            CancelInvoke("SpawnReward");
        }
    }

    Vector2 RandomPosition()
    {
        float x = Random.Range(-20, 20);
        float y = Random.Range(-11, 11);
        Vector2 randomPosition = new Vector2(x ,y);

        return randomPosition;
    }
}
