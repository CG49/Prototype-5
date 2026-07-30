using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> targets;

    private bool isSpawning;
    public float spawnRate = 1.0f;

    private Coroutine spawnCoroutine;

    void OnEnable()
    {
        GameEvents.OnGameEvent += StopSpawning;
    }

    void OnDisable()
    {
        GameEvents.OnGameEvent -= StopSpawning;
    }

    public void StartSpawning()
    {
        if (spawnCoroutine != null || isSpawning)
            return;

        isSpawning = true;
        spawnCoroutine = StartCoroutine(SpawnTarget());
    }

    IEnumerator SpawnTarget()
    {
        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnRate);

            if (targets.Count == 0)
            {
                isSpawning = false;
                spawnCoroutine = null;

                Debug.LogError("No target prefabs assigned!");
                yield break;
            }

            int randomIndex = Random.Range(0, targets.Count);

            Instantiate(targets[randomIndex], transform);
        }
    }

    private void StopSpawning(GameEvent gameEvent)
    {
        if (gameEvent.Type != GameEventType.GameOver)
            return;

        if (!isSpawning || spawnCoroutine == null)
            return;

        isSpawning = false;
        StopCoroutine(spawnCoroutine);
        spawnCoroutine = null;
    }
}
