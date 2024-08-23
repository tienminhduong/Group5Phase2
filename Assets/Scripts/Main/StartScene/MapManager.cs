using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public MapBlock[] map = new MapBlock[6];
    [SerializeField] int currentMap = 0;
    [SerializeField] bool tempCompleted = false;

    private void Start()
    {
        map[0].CanGetIn = true;
    }

    private void Update()
    {
        if (tempCompleted)
        {
            PrepareTheNext();
            tempCompleted = false;
        }
    }

    public void PrepareTheNext()
    {
        map[currentMap].CanGetIn = false;
        map[currentMap].ChangeSprite();
        map[currentMap + 1].CanGetIn = true;
    }
}
