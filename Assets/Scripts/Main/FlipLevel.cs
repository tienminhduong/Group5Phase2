using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipLevel : Level
{
    public override bool CanEnterFrom(Vector3 direction)
    {
        Vector3 flipYDirection = direction;
        flipYDirection.x = -flipYDirection.x;
        return base.CanEnterFrom(flipYDirection);
    }
    public override Vector3 EnterPosition(Vector3 direction)
    {
        Vector3 flipXDirection = direction;
        flipXDirection.x = -flipXDirection.x;
        return base.EnterPosition(flipXDirection);
    }
}
