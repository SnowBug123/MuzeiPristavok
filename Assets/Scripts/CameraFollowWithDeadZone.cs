using UnityEngine;

public class CameraFollowWithDeadZone : MonoBehaviour
{
    [Header("Цель слежения")]
    public Transform target;

    [Header("Мёртвая зона")]
    public Vector2 deadZoneSize = new Vector2(2f, 1f);

    [Header("Смещение камеры")]
    public Vector2 cameraOffset = Vector2.zero;

    [Header("Плавность")]
    public float smoothTime = 0.35f;

    [Header("Границы камеры")]
    public BoxCollider2D cameraBounds;

    private Vector3 velocity = Vector3.zero;

    private void LateUpdate()
    {
        if (target == null || cameraBounds == null)
            return;

        Vector3 currentPos = transform.position;

        Vector3 desiredPos = new Vector3(
            target.position.x + cameraOffset.x,
            target.position.y + cameraOffset.y,
            currentPos.z
        );

        float deadZoneLeft = currentPos.x - deadZoneSize.x * 0.5f;
        float deadZoneRight = currentPos.x + deadZoneSize.x * 0.5f;
        float deadZoneBottom = currentPos.y - deadZoneSize.y * 0.5f;
        float deadZoneTop = currentPos.y + deadZoneSize.y * 0.5f;

        Vector3 targetPos = currentPos;

        if (desiredPos.x < deadZoneLeft || desiredPos.x > deadZoneRight)
        {
            targetPos.x = Mathf.Clamp(
                desiredPos.x,
                deadZoneLeft,
                deadZoneRight
            );
        }

        if (desiredPos.y < deadZoneBottom || desiredPos.y > deadZoneTop)
        {
            targetPos.y = Mathf.Clamp(
                desiredPos.y,
                deadZoneBottom,
                deadZoneTop
            );
        }

        Bounds bounds = cameraBounds.bounds;

        Camera cam = Camera.main;
        float camHeight = cam.orthographicSize;
        float camWidth = camHeight * cam.aspect;

        targetPos.x = Mathf.Clamp(
            targetPos.x,
            bounds.min.x + camWidth,
            bounds.max.x - camWidth
        );

        targetPos.y = Mathf.Clamp(
            targetPos.y,
            bounds.min.y + camHeight,
            bounds.max.y - camHeight
        );

        transform.position = Vector3.SmoothDamp(
            currentPos,
            targetPos,
            ref velocity,
            smoothTime
        );
    }
}