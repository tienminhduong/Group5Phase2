using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipLevelBlock : LevelBlock
{
    protected override void MoveBlock(Vector3 direction)
    {
        StartCoroutine(MoveAnimation(direction));
        direction.x = -direction.x;
        //onMoveBlock?.Invoke(this, direction);
    }
}
