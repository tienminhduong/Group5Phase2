using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPlayer : MonoBehaviour
{
    [SerializeField] float hitDistance = 1.6f;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            Move(Vector3.up);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            Move(Vector3.down);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            Move(Vector3.right);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            Move(Vector3.left);
    }

    private void Move(Vector3 direct)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direct, hitDistance);
        Debug.DrawRay(transform.position, direct, Color.red, hitDistance);
        if (hit)
        {
            Debug.Log("Hit something : " + hit.collider);
            if (hit.collider.CompareTag("MapBlock") && hit.collider.GetComponent<MapBlock>().CanGetIn)
            {
                transform.position += direct;  // or play animation zoom
                SceneManager.LoadScene(hit.collider.GetComponent<MapBlock>().MapID);
            }    
        }
        else
        {
            Debug.Log("Hit nothing");
            transform.position += direct;
        }
    }
}
