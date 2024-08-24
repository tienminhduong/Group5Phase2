using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Block playerBlock;

    // Update is called once per frame
    void Update()
    {
        if (!playerBlock.IsMoving) {
            if (Input.GetKeyDown(KeyCode.W))
                playerBlock.MoveTo(Vector3.up);
            else if (Input.GetKeyDown(KeyCode.S))
                playerBlock.MoveTo(Vector3.down);
            else if (Input.GetKeyDown(KeyCode.D))
                playerBlock.MoveTo(Vector3.right);
            else if (Input.GetKeyDown(KeyCode.A))
                playerBlock.MoveTo(Vector3.left);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // for test
            PlayerPrefs.SetInt("completedMap", PlayerPrefs.GetInt("completedMap") + 1);

            SceneManager.LoadScene(0);
        }    
            
    }
}
