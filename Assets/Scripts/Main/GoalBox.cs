using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalBox : MonoBehaviour
{
    [SerializeField] bool needPlayer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!needPlayer || (needPlayer && collision.gameObject.CompareTag("Player")))
            GameManager.Instance.EnterCheckPoint();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!needPlayer || (needPlayer && collision.gameObject.CompareTag("Player")))
            GameManager.Instance.ExitCheckPoint();
    }
}
