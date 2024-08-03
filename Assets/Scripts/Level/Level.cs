using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameObject UpLeft, DownRight;
    public GameObject upBox, downBox, leftBox, rightBox;
    public Box upScript, downScript, leftScript, rightScript;
    public Level parent = null;

    private void Start()
    {
        upScript = upBox.GetComponent<Box>();
        downScript = downBox.GetComponent<Box>();
        leftScript = leftBox.GetComponent<Box>();
        rightScript = rightBox.GetComponent<Box>();
    }

}
