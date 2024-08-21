using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlock : Block
{
    [SerializeField] Level referenceLevel;
    public Level ReferenceLevel => referenceLevel;

    public Vector3 VectorToMapCenter
    {
        get
        {
            return transform.position - level.transform.position;
        }
    }
}
