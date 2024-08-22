using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    [SerializeField] int mapID;
    public int MapID => mapID;

    bool canGetIn = true;
    public bool CanGetIn => canGetIn;

    [SerializeField] bool isReady = false;
    public bool IsReady => isReady;

    [SerializeField] Sprite finishedSprite;
    [SerializeField] SpriteRenderer spriteRenderer;


    public void ChangeSprite()
    {
        spriteRenderer.sprite = finishedSprite;
        canGetIn = false;
    }    
    
}
