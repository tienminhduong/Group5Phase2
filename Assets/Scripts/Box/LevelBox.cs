using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class LevelBox : Box
{
    public Level level;
    bool pushOppositeDirection = false;
    override public bool CanMove(Vector2 direction, GameObject pusher)
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
            bool consume = false;
            // Handle collision with other boxes
            Collider2D[] colliders = Physics2D.OverlapBoxAll(potentialPosition, new Vector2(checkSize, checkSize), 0, obstacleLayer);
            foreach (Collider2D col in colliders)
            {
                Box box = col.GetComponent<Box>();
                //null is wall
                if (box == null) goto skip;

                //the player go out from the level block
                if (pusher == null && box.CanMove(direction, gameObject)) 
                {
                    box.Move(direction);
                    return true;
                }
                if (!box.CanMove(direction, gameObject))
                {
                    //Check can it consume the next block
                    
                    Vector2 oppositeDirection = OppositeDirection(direction);
                    if (direction == Vector2.down && level.downBox.CanMove(oppositeDirection, null))
                    {
                        col.transform.localPosition = level.downBox.transform.position;
                        col.GetComponent<Block>().targetPosition = level.downBox.targetPosition;
                        consume = true;
                    }
                    else if (direction == Vector2.up && level.upBox.CanMove(oppositeDirection, null))
                    {
                        col.transform.localPosition = level.upBox.transform.position;
                        col.GetComponent<Block>().targetPosition = level.upBox.targetPosition;
                        consume = true;
                    }
                    else if (direction == Vector2.left && level.leftBox.CanMove(oppositeDirection, null))
                    {
                        col.transform.position = level.leftBox.transform.position;
                        col.GetComponent<Block>().targetPosition = level.leftBox.targetPosition;
                        consume = true;
                    }
                    else if (direction == Vector2.right && level.rightBox.CanMove(oppositeDirection, null))
                    {
                        col.transform.position = level.rightBox.transform.position;
                        col.GetComponent<Block>().targetPosition = level.rightBox.targetPosition;
                        consume = true;
                    }
                    if (consume)
                    {
                        pushOppositeDirection = true;
                        return true;
                    }
                    goto skip;
                }
            }
            return true;
        }
    skip:
        //Check can the previous block enter it
        if (direction == Vector2.down && level.upBox.CanMove(direction, null))
        {
            pusher.transform.position = level.upBox.transform.position;
            pusher.GetComponent<Block>().targetPosition = level.upBox.targetPosition;
        }else if (direction == Vector2.up && level.downBox.CanMove(direction, null))
        {
            pusher.transform.position = level.downBox.transform.position;
            pusher.GetComponent<Block>().targetPosition = level.downBox.targetPosition;
        }else if(direction == Vector2.left && level.rightBox.CanMove(direction, null))
        {
            pusher.transform.position = level.rightBox.transform.position;
            pusher.GetComponent<Block>().targetPosition = level.rightBox.targetPosition;
        }
        else if (direction == Vector2.right && level.leftBox.CanMove(direction, null))
        {
            pusher.transform.position = level.leftBox.transform.position;
            pusher.GetComponent<Block>().targetPosition = level.leftBox.targetPosition;
        }
        return false;
    }
    override public void Move(Vector2 direction)
    {
        Vector2 potentialPosition = targetPosition + gridSize * direction;
        float checkSize = gridSize * 0.45f;

        Collider2D[] colliders = Physics2D.OverlapBoxAll(potentialPosition, new Vector2(checkSize, checkSize), 0, obstacleLayer);
        foreach (Collider2D col in colliders)
        {
            Block box = col.GetComponent<Block>();
            if (box != null)
            {
                Vector2 dir = direction;
                if(pushOppositeDirection) dir = OppositeDirection(dir);
                box.Move(dir);
            }
        }
        pushOppositeDirection = false;
        targetPosition += gridSize * direction;
        isMoving = true;
    }
}
