using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    static private GameManager instance;
    static public GameManager Instance => instance;

    void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] int checkpointEntered;
    [SerializeField] int numberCheckpointMax;

    public void EnterCheckPoint()
    {
        checkpointEntered++;
        StartCoroutine(CheckWin());
    }

    public void ExitCheckPoint()
    {
        checkpointEntered--;
        StartCoroutine(CheckWin());
    }

    IEnumerator CheckWin()
    {
        if (checkpointEntered == numberCheckpointMax) {
            // Win and load first scene
            Debug.Log("You win");

            yield return new WaitForSeconds(2f);
            if (checkpointEntered == numberCheckpointMax)
                SceneManager.LoadScene(0);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            RestartGame();
    }

    void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
