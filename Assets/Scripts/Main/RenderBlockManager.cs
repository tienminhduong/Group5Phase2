using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderBlockManager : MonoBehaviour
{
    #region Singleton
    static private RenderBlockManager instance;
    static public RenderBlockManager Instance => instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    List<RenderBlock> renderBlocks;
    [SerializeField] float zoomSpeed;
    [SerializeField] Block player;
    public bool IsPlayingAnimation { private set; get; }

    private void Start()
    {
        renderBlocks = new List<RenderBlock>();
        for (int i = 0; i < transform.childCount; i++) {
            renderBlocks.Add(transform.GetChild(i).GetComponent<RenderBlock>());
        }
        if (transform.childCount != 3) {
            Debug.LogError("Not enough number of render blocks!!!");
        }
    }

    public IEnumerator ZoomOutEffect()
    {
        IsPlayingAnimation = true;
        float maxSize = renderBlocks[1].transform.localScale.x / renderBlocks[2].transform.localScale.x * Camera.main.orthographicSize;
        float dSize = maxSize - Camera.main.orthographicSize;
        Vector3 oldPos = renderBlocks[2].transform.position;

        Vector3 distance = renderBlocks[2].transform.position - renderBlocks[1].transform.position;
        distance.z = 0;

        while (Camera.main.orthographicSize < maxSize) {

            Camera.main.orthographicSize += dSize / zoomSpeed;
            Camera.main.transform.position -= distance / zoomSpeed;

            if (Mathf.Abs(Vector3.Distance(oldPos, renderBlocks[2].transform.position)) > 0.01f) {
                Camera.main.transform.position += renderBlocks[2].transform.position - oldPos;
                oldPos = renderBlocks[2].transform.position;
            }

            yield return null;
        }

        SwapLevel();
        Camera.main.orthographicSize = 5f;
        IsPlayingAnimation = false;
    }

    public IEnumerator ZoomInEffect(LevelBlock inLevel)
    {
        Debug.Log("Zoom in effect is called!");
        IsPlayingAnimation = true;
        float minSize = Camera.main.orthographicSize / inLevel.level.Size;
        Debug.Log("Minsize: " + minSize);
        float dSize = Camera.main.orthographicSize - minSize;

        Vector3 distance = inLevel.VectorToMapCenter * 9 / renderBlocks[2].block.ReferenceLevel.Size;
        Debug.Log("Distance: " + distance);
        distance.z = 0;
        Vector3 oldPos = renderBlocks[2].transform.position;

        while (Camera.main.orthographicSize > minSize) {
            Camera.main.orthographicSize -= dSize / zoomSpeed;
            Camera.main.transform.position += distance / zoomSpeed;

            if (Mathf.Abs(Vector3.Distance(oldPos, renderBlocks[2].transform.position)) > 0.01f) {
                Camera.main.transform.position += renderBlocks[2].transform.position - oldPos;
                oldPos = renderBlocks[2].transform.position;
            }

            yield return null;
        }

        SwapLevel();
        Camera.main.orthographicSize = 5f;
        IsPlayingAnimation = false;
    }

    public void SwapLevel()
    {
        // Reload material
        LevelBlock currentInLevel = player.level.ReferenceBlock;
        for (int i = 2; i >= 0; i--) {
            renderBlocks[i].meshRenderer.material = currentInLevel.material;
            renderBlocks[i].block = currentInLevel;
            currentInLevel = currentInLevel.level.ReferenceBlock;
        }

        for (int i = renderBlocks.Count - 2; i >= 0; i--) {
            renderBlocks[i].transform.localScale = renderBlocks[i + 1].transform.localScale * renderBlocks[i].block.ReferenceLevel.Size;
        }

        MoveRenderBlock();
    }

    public void MoveRenderBlock()
    {
        for (int i = 1; i < renderBlocks.Count; i++) {
            renderBlocks[i].transform.position = renderBlocks[i - 1].transform.position
                + renderBlocks[i].block.VectorToMapCenter * renderBlocks[i].transform.localScale.x + Vector3.back * i;
        }
    }
}
