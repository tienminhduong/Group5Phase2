using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tblock : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float gridSize = 1f;
    [SerializeField] protected LayerMask obstacleLayer;

    public Vector2 targetPosition;
    protected bool isMoving = false;
    virtual public void Move(Vector2 direction)
    {
        
    }

    public Vector2 OppositeDirection(Vector2 direction)
    {
        if (direction == Vector2.up)
        {
            return Vector2.down;
        }
        else if (direction == Vector2.down)
        {
            return Vector2.up;
        }else if (direction == Vector2.left)
        {
            return Vector2.right;
        }else if(direction == Vector2.right)
        {
            return Vector2.left;
        }
        return Vector2.zero;
    }
}
