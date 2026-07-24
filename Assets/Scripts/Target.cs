using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    [SerializeField] private int points;
    [SerializeField] ParticleSystem explosionParticle;

    private const float minSpeed = 13;
    private const float maxSpeed = 16.5f;
    private const float maxTorque = 10;
    private const float xRange = 4;
    private const float ySpawnPos = -5;

    private Rigidbody targetRb;

    public static event Action<int> OnTargetHit;

    void Awake()
    {
        float x = RandomTorque();
        float y = RandomTorque();
        float z = RandomTorque();

        targetRb = GetComponent<Rigidbody>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(x, y, z, ForceMode.Impulse);

        transform.position = RandomSpawnPos();
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    public void Hit()
    {
        OnTargetHit?.Invoke(points);

        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DestroyZone"))
            Destroy(gameObject);
    }
}
