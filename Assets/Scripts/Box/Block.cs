using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float gridSize = 1f;
    [SerializeField] protected LayerMask obstacleLayer;

    public Vector2 targetPosition;
    protected bool isMoving = false;
    virtual public void Move(Vector2 direction)
    {
        
    }
}
