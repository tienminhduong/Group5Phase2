using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarrier : Box
{
    public Level level;

    public override bool CanMove(Vector2 direction, GameObject pusher)
    {
        if (level.levelBox.CanMove(direction, null))
        {
            pusher.transform.position = level.levelBox.transform.position;
            pusher.GetComponent<Block>().targetPosition = level.levelBox.targetPosition;
            return true;
        }
        return false;
    }
    public override void Move(Vector2 direction)
    {
        return;
    }
}
