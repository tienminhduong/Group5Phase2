using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] MapBlock[] map = new MapBlock[7];
    [SerializeField] int currentMap;


    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("completedMap"))
        {
            int cMap = PlayerPrefs.GetInt("completedMap");
            Debug.Log(cMap);
            map[cMap].CanGetIn = false;
            currentMap = cMap + 1;
        }
        else
        {
            currentMap = 0;
        }
        Prepare();
    }
    private void Update()
    {
        
    }

    public void Prepare()
    {
        map[currentMap].CanGetIn = true;
    }
}
