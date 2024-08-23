using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartPlayer : MonoBehaviour
{
    [SerializeField] float hitDistance = 1.6f;
    [SerializeField] Animator animator;

    private void Start()
    {
        if (PlayerPrefs.HasKey("xPos"))
        {
            float x = PlayerPrefs.GetFloat("xPos");
            float y = PlayerPrefs.GetFloat("yPos");
            transform.position = new Vector3(x, y, 0);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            Move(Vector3.up /2);
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            Move(Vector3.down /2);
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            Move(Vector3.right / 2);
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            Move(Vector3.left /2);
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
                //StartCoroutine(PlayZoomAnim(direct));
                //transform.position += direct;
                PlayerPrefs.SetFloat("xPos", transform.position.x + direct.x * 3);
                PlayerPrefs.SetFloat("yPos", transform.position.y + direct.y * 3);
  
                SceneManager.LoadScene(hit.collider.GetComponent<MapBlock>().MapID);
            }    
        }
        else
        {
            Debug.Log("Hit nothing");
            transform.position += direct;
        }
    }

    public IEnumerator PlayZoomAnim(Vector3 direct)
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Zoom", true);
        
        yield return new WaitForSeconds(10f);
    }

}
