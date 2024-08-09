using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] Block referenceBlock;
    public Block ReferenceBlock => referenceBlock;

    [SerializeField] bool enterLeft, enterRight, enterTop, enterBottom;
    public bool CanEnterFrom(Vector3 direction)
    {
        if (direction == Vector3.right) {
            return enterLeft;
        }
        else if (direction == Vector3.left) {
            return enterRight;
        }
        else if (direction == Vector3.down) {
            return enterTop;
        }
        else if (direction == Vector3.up) {
            return enterBottom;
        }
        else
            return false;
    }
    public Vector3 EnterPosition(Vector3 direction)
    {
        return transform.position - direction * 4f;
    }
}
