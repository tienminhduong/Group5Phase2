using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderBlock : MonoBehaviour
{
    public LevelBlock block;

    public MeshRenderer meshRenderer { private set; get; }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    private void OnEnable()
    {
        Block.onMoveBlock += MoveRenderBlock;
    }
    private void OnDisable()
    {
        Block.onMoveBlock -= MoveRenderBlock;
    }

    void MoveRenderBlock(Block movingBlock, Vector3 distance)
    {
        if (movingBlock != block || name == "LargeQuad") return;
        transform.position += distance * 9;
    }
}
