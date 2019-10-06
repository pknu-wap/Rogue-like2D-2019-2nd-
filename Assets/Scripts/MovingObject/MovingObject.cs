using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{
    public float moveTime = .1f;
    public LayerMask blockingLayer;

    private BoxCollider2D boxCollider;
    private Rigidbody2D rb2D;
    private float inverseMoveTime;

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        rb2D = GetComponent<Rigidbody2D>();
        inverseMoveTime = 1f / moveTime;
    }

    protected bool Move (int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);
        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, blockingLayer);

        boxCollider.enabled = true;

        if(hit.transform == null)
        {
            StartCoroutine(ObjectMove(end));

            return true;
        }

        return false;
    }

    protected IEnumerator ObjectMove(Vector3 toward)
    {
        float remaingDistance = (transform.position - toward).sqrMagnitude;

        while(remaingDistance > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, toward, inverseMoveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            remaingDistance = (transform.position - toward).sqrMagnitude;
            yield return null;
        }
    }
    
}
