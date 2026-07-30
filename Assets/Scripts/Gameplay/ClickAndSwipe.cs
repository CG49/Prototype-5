using UnityEngine;

[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{
    private TrailRenderer trail;
    private BoxCollider boxCollider;

    void Awake()
    {
        trail = GetComponent<TrailRenderer>();
        boxCollider = GetComponent<BoxCollider>();

        trail.enabled = false;
        boxCollider.enabled = false;
    }

    public void BeginSwipe()
    {
        trail.enabled = true;
        boxCollider.enabled = true;
    }

    public void EndSwipe()
    {
        trail.enabled = false;
        boxCollider.enabled = false;
    }

    public void UpdateSwipe(Vector3 position)
    {
        transform.position = position;
    }
}
