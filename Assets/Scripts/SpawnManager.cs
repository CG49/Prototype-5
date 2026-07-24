using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;

    private const float spawnRate = 1.0f;

    void Start()
    {
        StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnRate);

            int randomIndex = Random.Range(0, targets.Count);

            Instantiate(targets[randomIndex], transform);
        }
    }
}
