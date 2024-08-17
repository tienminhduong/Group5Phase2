using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Level level;
    public static Action<Block, Vector3> onMoveBlock;

    public bool MoveTo(Vector3 direction)
    {
        RaycastHit2D check = CheckNextTo(transform.position, direction);
        if (CheckAndMove(check, direction))
            return true;


        RaycastHit2D checkOpposite = CheckNextTo(transform.position, -direction);
        if (!checkOpposite || !checkOpposite.transform.CompareTag("LevelBlock")) return false;
        LevelBlock oppositeDirectionBlock = checkOpposite.transform.GetComponent<LevelBlock>();
        if (oppositeDirectionBlock != null) {
            if (!oppositeDirectionBlock.ReferenceLevel.CanEnterFrom(-direction)) {
                return false;
            }
            else {
                return MoveInLevel(oppositeDirectionBlock.ReferenceLevel, -direction);
            }
        }
        return false;
        
    }

    bool CheckAndMove(RaycastHit2D raycastHit2D, Vector3 direction)
    {
        // If there are no objects, move the current block
        if (!raycastHit2D) {
            MoveBlock(direction);
            return true;
        }
        // Stop moving if it is wall
        else if (raycastHit2D.transform.CompareTag("Wall")) {
            return false;
        }
        // If it's out the level, check if it can enter parent level
        else if (raycastHit2D.transform.CompareTag("LevelBarrier")) {
            Vector3 outLevelDirection = raycastHit2D.transform.GetComponent<LevelBarrier>().direction;
            RaycastHit2D checkOutLevel = CheckNextTo(level.ReferenceBlock.transform.position, outLevelDirection);
            return CheckAndMove(checkOutLevel, direction);
        }
        else {
            // If there is a block next to it, try moving it
            Block nextBlock = raycastHit2D.transform.GetComponent<Block>();
            // If success, move the current block
            if (nextBlock.MoveTo(direction)) {
                MoveBlock(direction);
                return true;
            }
            // If not, check if it's a level block or not
            else if (nextBlock.transform.CompareTag("LevelBlock")) {
                LevelBlock nextLvBlock = (LevelBlock)nextBlock;
                // If it is, check if the level is open from this direction
                if (!nextLvBlock.ReferenceLevel.CanEnterFrom(direction)) {
                    return false;
                }
                else {
                    return MoveInLevel(nextLvBlock.ReferenceLevel, direction);
                }
            }
        }

        return false;
    }

    RaycastHit2D CheckNextTo(Vector3 position, Vector3 direction)
    {
        return Physics2D.Raycast(position + direction * 0.51f, direction, 0.5f);
    }

    virtual protected void MoveBlock(Vector3 direction)
    {
        StartCoroutine(MoveAnimation(direction));
        onMoveBlock?.Invoke(this, direction);
    }

    public void MoveOutLevel(Vector3 direction)
    {
        breakMoving = true;
        transform.position = level.ReferenceBlock.transform.position + direction;
        level = level.ReferenceBlock.level;

        StartCoroutine(ZoomOutAnimation(direction));
    }

    public bool MoveInLevel(Level level, Vector3 direction)
    {
        RaycastHit2D check = CheckNextTo(level.EnterPosition(direction) - direction, direction);
        if (!check) {
            StartCoroutine(ZoomInAnimation(level, direction));
            return true;
        }
        Block block = check.transform.GetComponent<Block>();
        if (block.MoveTo(direction)) {
            StartCoroutine(ZoomInAnimation(level, direction));
            return true;
        }
        return false;
    }

    void SetBlockInLevel(Level level, Vector3 direction)
    {
        transform.position = level.EnterPosition(direction);
        this.level = level;
    }

    IEnumerator ZoomOutAnimation(Vector3 direction)
    {
        Debug.Log("Zoom Animation played");

        Vector3 dScale = Vector3.one / level.Size;
        Vector3 dDirection = direction / level.Size;

        transform.localScale = Vector3.zero;
        transform.position -= direction;

        while (transform.localScale.x < 1) {
            transform.localScale += dScale;
            transform.position += dDirection;

            yield return null;
        }
    }

    IEnumerator ZoomInAnimation(Level level, Vector3 direction)
    {
        Debug.Log("Zoom in animation played");
        Vector3 dScale = Vector3.one / level.Size;
        Vector3 dDirection = direction / level.Size / level.Size;

        while (transform.localScale.x > 0) {
            transform.localScale -= dScale;
            transform.position += dDirection;

            yield return null;
        }

        transform.localScale = Vector3.one;
        SetBlockInLevel(level, direction);
    }

    bool breakMoving = false;
    IEnumerator MoveAnimation(Vector3 direction)
    {
        Vector3 d = direction / level.Size;
        while (direction.magnitude > 0) {
            if (breakMoving) {
                breakMoving = false;
                break;
            }
            if (direction.magnitude < d.magnitude)
                d = direction;
            transform.position += d;
            direction -= d;
            yield return null;

        }
    }
}
