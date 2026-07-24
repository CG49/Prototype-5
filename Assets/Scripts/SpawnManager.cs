using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;

    private const float spawnRate = 1.0f;

    private Coroutine spawnCoroutine;

    void OnEnable()
    {
        Target.OnGameOver += StopSpawning;
    }

    void OnDisable()
    {
        Target.OnGameOver -= StopSpawning;
    }

    void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnTarget());
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

    private void StopSpawning()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }
}
