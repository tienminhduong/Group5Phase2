using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    //public LevelBlock playerInBlock;
    [SerializeField] RenderBlock onScreenBlock;
    private void Update()
    {
        if (!RecursionEffectManager.Instance || !RecursionEffectManager.Instance.IsPlayingAnimation)
            transform.position = onScreenBlock.transform.position + Vector3.back * 10;
    }
}
