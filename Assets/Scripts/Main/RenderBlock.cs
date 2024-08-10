using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderBlock : MonoBehaviour
{
    [SerializeField] Block block;

    [SerializeField] RenderBlock largerBlock, smallerBlock;
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
        if (movingBlock != block) return;
        transform.localPosition += distance / 9f;
    }

    public void MoveBackToCamera(Vector3 direction)
    {
        Transform largest = transform;
        while (largest.parent.CompareTag("RenderBlock"))
            largest = transform.parent;
        largest.position += direction;
    }
}
