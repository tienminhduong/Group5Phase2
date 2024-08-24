using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPlayer : MonoBehaviour
{
    [SerializeField] float hitDistance = 1.6f;
    [SerializeField] Animator animator;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        if (PlayerPrefs.HasKey("xPos"))
        {
            float x = PlayerPrefs.GetFloat("xPos");
            float y = PlayerPrefs.GetFloat("yPos") + 1;
            transform.position = new Vector3(x, y, 0);
        }
    }
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
        // check collider with wall and map block
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direct, hitDistance);
        Debug.DrawRay(transform.position, direct, Color.red, hitDistance);

        if (hit)
        {
            Debug.Log("Hit something : " + hit.collider); 
            if (hit.collider.CompareTag("MapBlock") && hit.collider.GetComponent<MapBlock>().CanGetIn)
            {
                transform.position += direct/2;
            }    
        }
        else
        {
            Debug.Log("Hit nothing");
            transform.position += direct/2;
        }
    }


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Mathf.Approximately(transform.position.x, collision.transform.position.x) &&
            Mathf.Approximately(transform.position.y, collision.transform.position.y))
        {
            
                SceneManager.LoadScene(collision.GetComponent<MapBlock>().MapID);
                PlayerPrefs.SetFloat("xPos", transform.position.x);
                PlayerPrefs.SetFloat("yPos", transform.position.y);
            
        }
    }

}
