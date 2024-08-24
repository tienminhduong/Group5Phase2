using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapBlock : MonoBehaviour
{
    [SerializeField] int mapID;
    public int MapID => mapID;

    [SerializeField] bool canGetIn = false;
    public bool CanGetIn { get => canGetIn; set { canGetIn = value; } }


    [SerializeField] Sprite finishedSprite;
    [SerializeField] SpriteRenderer spriteRenderer;


    public void ChangeSprite()
    {
        spriteRenderer.sprite = finishedSprite;
        canGetIn = false;
    }    

    public void Prepare()
    {
        // is ready
        // spriteRenderer.sprite.
    }
    
}
