using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Level parent;
    public static Action<Block, Vector3> onMoveBlock;

    public bool MoveTo(Vector3 direction)
    {
        RaycastHit2D check = CheckNext(direction);
        if (!check || check.transform.CompareTag("LevelBarrier")) {
            MoveBlock(direction);
            return true;
        }
        else {
            if (check.transform.CompareTag("Wall"))
                return false;

            Block nextBlock = check.transform.GetComponent<Block>();
            if (nextBlock.MoveTo(direction)) {
                MoveBlock(direction);
                return true;
            }
            else {

            }
        }

        return false;
    }

    RaycastHit2D CheckNext(Vector3 direction)
    {
        return Physics2D.Raycast(transform.position + direction * 0.51f, direction, 0.5f);
    }

    virtual protected void MoveBlock(Vector3 direction)
    {
        transform.position += direction;
        onMoveBlock?.Invoke(this, direction);
    }

    public void MoveOutBlock(Vector3 direction)
    {
        //Vector3 prevPos = transform.position;
        transform.position = parent.ReferenceBlock.transform.position + direction;
        parent = parent.ReferenceBlock.parent;
    }
}
