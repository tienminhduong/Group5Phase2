using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : Tblock
{

    protected void Start()
    {
        targetPosition = transform.position;
    }

    protected void Update()
    {
        if (isMoving)
        {
            MoveToTarget();
        }
    }

    virtual public bool CanMove(Vector2 direction, GameObject pusher)
    {
        Vector2 potentialPosition = targetPosition + gridSize * direction;
        float checkSize = gridSize * 0.45f;

        // Check if the box can move to the potential position
        if (!Physics2D.OverlapBox(potentialPosition, new Vector2(checkSize, checkSize), 0, obstacleLayer))
        {
            return true;
        }
        else
        {
            // Handle collision with other boxes
            Collider2D[] colliders = Physics2D.OverlapBoxAll(potentialPosition, new Vector2(checkSize, checkSize), 0, obstacleLayer);
            foreach (Collider2D col in colliders)
            {
                Box box = col.GetComponent<Box>();
                if (box == null || !box.CanMove(direction, gameObject))
                {
                    return false;
                }
            }return true;
        }
    }

    override public void Move(Vector2 direction)
    {
        Vector2 potentialPosition = targetPosition + gridSize * direction;
        float checkSize = gridSize * 0.45f;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(potentialPosition, new Vector2(checkSize, checkSize), 0, obstacleLayer);
        foreach (Collider2D col in colliders)
        {
            Tblock box = col.GetComponent<Tblock>();
            if (box != null)
            {
                box.Move(direction);
            }
        }

        targetPosition += gridSize * direction;
        isMoving = true;
    }

    protected void MoveToTarget()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        if (Vector2.Distance(transform.position, targetPosition) <= 0.01f)
        {
            transform.position = targetPosition;
            isMoving = false;
        }
    }

    protected void OnDrawGizmos()
    {
        float debugSize = gridSize * 0.9f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(targetPosition, new Vector2(debugSize, debugSize));
    }
}
