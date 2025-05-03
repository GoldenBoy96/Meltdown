using UnityEngine;

public class PlayerDustTrail : MonoBehaviour
{
    public ParticleSystem dustTrailEffect;
    public float moveThreshold = 0.1f; // Ngưỡng để xác định di chuyển
    private Vector3 lastPosition;
    private bool isMoving;

    void Start()
    {
        lastPosition = transform.position;
        if (dustTrailEffect == null)
            dustTrailEffect = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        float moveDistance = Vector3.Distance(transform.position, lastPosition);
        isMoving = moveDistance > moveThreshold * Time.deltaTime;

        // Điều chỉnh vị trí bụi theo hướng
        Vector3 moveDirection = (transform.position - lastPosition).normalized;
        if (isMoving)
        {
            dustTrailEffect.transform.localPosition = new Vector3(-moveDirection.x * 0.5f, 0, 0); // Bụi ở phía sau
            if (!dustTrailEffect.isPlaying)
                dustTrailEffect.Play();
        }
        else
        {
            if (dustTrailEffect.isPlaying)
                dustTrailEffect.Stop();
        }

        lastPosition = transform.position;
    }
}