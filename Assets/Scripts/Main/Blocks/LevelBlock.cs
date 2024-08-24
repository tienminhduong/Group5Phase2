using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBlock : Block
{
    [SerializeField] Level referenceLevel;
    public Level ReferenceLevel => referenceLevel;
    MeshRenderer meshRenderer;
    public Material material { get { return meshRenderer != null ? meshRenderer.material : null; } }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public Vector3 VectorToMapCenter
    {
        get
        {
            return transform.position - level.transform.position;
        }
    }
}
