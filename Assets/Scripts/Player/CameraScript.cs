using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public RenderBlock playerInBlock;
    private void Update()
    {
        //if (((Vector2)(transform.position - playerInBlock.transform.position)).magnitude > 0.01f) {
            //Vector3 newPos = playerInBlock.transform.position;
            //newPos.z = -10;
            //transform.position = newPos;
        //}

        if (playerInBlock != null) {
            Vector3 direction = transform.position - playerInBlock.transform.position;
            direction.z = 0f;
            playerInBlock.MoveBackToCamera(direction);
        }
    }
}
