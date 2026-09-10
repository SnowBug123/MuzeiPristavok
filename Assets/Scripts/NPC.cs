using UnityEngine;
using System.Collections;

public class NPC : MonoBehaviour
{
    public Transform[] points;

    public float moveSpeed = 2f;
    public float maxstopTime = 5f;
    public float minstopTime = 1f;

    private int currentPoint;

    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool waiting;

    private float lastMoveX;
    private float lastMoveY;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (waiting || points.Length == 0)
            return;

        Vector2 direction =
            (points[currentPoint].position - transform.position);

        float distance = direction.magnitude;

        direction.Normalize();

        transform.position +=
            (Vector3)(direction * moveSpeed * Time.deltaTime);

        if (direction != Vector2.zero)
        {
            lastMoveX = direction.x;
            lastMoveY = direction.y;
        }

        animator.SetFloat("MoveX", lastMoveX);
        animator.SetFloat("MoveY", lastMoveY);
        animator.SetFloat("Speed", 1f);


        if (distance < 0.1f)
            StartCoroutine(WaitAtPoint());
    }

    private IEnumerator WaitAtPoint()
    {
        waiting = true;

        animator.SetFloat("Speed", 0f);

        yield return new WaitForSeconds(Random.Range(minstopTime, maxstopTime));

        currentPoint++;

        if (currentPoint >= points.Length)
            currentPoint = 0;

        waiting = false;
    }
}
