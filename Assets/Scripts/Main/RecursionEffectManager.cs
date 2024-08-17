using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecursionEffectManager : MonoBehaviour
{
    static private RecursionEffectManager instance;
    static public RecursionEffectManager Instance => instance;

    void Awake()
    {
        instance = this;
    }

    [SerializeField] RenderBlock largeQuad, quad;
    [SerializeField] List<Material> materials;
    int activeMaterialIndex = 0;
    public bool IsPlayingAnimation { private set; get; }

    public IEnumerator ZoomOutEffect()
    {
        IsPlayingAnimation = true;
        float maxSize = largeQuad.transform.localScale.x / quad.transform.localScale.x * Camera.main.orthographicSize;
        float dSize = maxSize - Camera.main.orthographicSize;

        Vector3 distance = Camera.main.transform.position;
        distance.z = 0;

        while (Camera.main.orthographicSize < maxSize) {
            Camera.main.orthographicSize += dSize * Time.fixedDeltaTime;
            Camera.main.transform.position -= distance * Time.fixedDeltaTime;
            yield return null;
        }

        SwapLevel();
        Camera.main.orthographicSize = 5f;
        IsPlayingAnimation = false;
    }

    public IEnumerator ZoomInEffect()
    {
        IsPlayingAnimation = true;
        float minSize = Camera.main.orthographicSize / quad.block.level.Size;
        float dSize = Camera.main.orthographicSize - minSize;

        Vector3 distance = largeQuad.block.transform.position - largeQuad.block.level.transform.position;
        distance.z = 0;

        while (Camera.main.orthographicSize > minSize) {
            Camera.main.orthographicSize -= dSize * Time.fixedDeltaTime;
            Camera.main.transform.position -= distance * Time.fixedDeltaTime;
            yield return null;
        }

        SwapLevel();
        Camera.main.orthographicSize = 5f;
        IsPlayingAnimation = false;
    }

    void SwapLevel()
    {
        // Swap material
        largeQuad.meshRenderer.material = materials[activeMaterialIndex];

        activeMaterialIndex += 1;
        activeMaterialIndex %= materials.Count;

        quad.meshRenderer.material = materials[activeMaterialIndex];

        // Swap block
        LevelBlock lvBlock = largeQuad.block;
        largeQuad.block = quad.block;
        quad.block = lvBlock;

        largeQuad.transform.localScale = (9 * largeQuad.block.ReferenceLevel.Size) * Vector3.one;

        // Position
        quad.transform.position = (quad.block.transform.position - quad.block.level.transform.position) * 9 + Vector3.back;
    }
}
