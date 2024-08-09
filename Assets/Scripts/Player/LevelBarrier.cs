using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarrier : MonoBehaviour
{
    [SerializeField] BarrierDirection barrierDirection;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Block block = collision.GetComponent<Block>();
        block?.MoveOutLevel(direction);
    }

    public Vector3 direction
    {
        get
        {
            return barrierDirection switch
            {
                BarrierDirection.Left => Vector3.left,
                BarrierDirection.Right => Vector3.right,
                BarrierDirection.Up => Vector3.up,
                BarrierDirection.Down => Vector3.down,
                _ => Vector3.zero,
            };
        }
    }
}

enum BarrierDirection
{
    Up, Down, Left, Right
}