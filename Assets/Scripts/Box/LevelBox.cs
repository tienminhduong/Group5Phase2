using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBox : Box
{
    public Level level;

    override public bool CanMove(Vector2 direction, GameObject pusher)
    {
        bool canMove = base.CanMove(direction, pusher);
        if (canMove) return true;
        if (direction == Vector2.down && level.upScript.CanMove(direction, null))
        {
            pusher.transform.localPosition = level.upBox.transform.localPosition;
            pusher.GetComponent<Block>().targetPosition = level.upScript.targetPosition;
            canMove = true;
        }else if (direction == Vector2.up && level.downScript.CanMove(direction, null))
        {
            pusher.transform.localPosition = level.downBox.transform.localPosition;
            pusher.GetComponent<Block>().targetPosition = level.downScript.targetPosition;
            canMove = true;
        }else if(direction == Vector2.left && level.rightScript.CanMove(direction, null))
        {
            pusher.transform.position = level.rightBox.transform.position;
            pusher.GetComponent<Block>().targetPosition = level.rightScript.targetPosition;
            canMove = true;
        }
        else if (direction == Vector2.right && level.leftScript.CanMove(direction, null))
        {
            pusher.transform.localPosition = level.leftBox.transform.localPosition;
            pusher.GetComponent<Block>().targetPosition = level.leftScript.targetPosition;
            canMove = true;
        }
        return false;
    }
}
