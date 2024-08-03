using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : Block
{
    [SerializeField] private float moveCooldown = 0.1f;

    private Vector2 input;
    private float moveCooldownTimer = 0f;

    private void Start()
    {
        targetPosition = transform.position;
    }

    private void Update()
    {
        GetInput();
        MovePlayer();
        UpdateCooldown();
    }

    private void GetInput()
    {
        if (!isMoving && moveCooldownTimer <= 0f)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            if (input.x > 0)
            {
                AttemptMove(Vector2.right);
            }
            else if (input.x < 0)
            {
                AttemptMove(Vector2.left);
            }
            else if (input.y > 0)
            {
                AttemptMove(Vector2.up);
            }
            else if (input.y < 0)
            {
                AttemptMove(Vector2.down);
            }
        }
    }

    private void MovePlayer()
    {
        if (isMoving)
        {
            if (Vector2.Distance(transform.position, targetPosition) > 0.1f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            }
            else
            {
                transform.position = targetPosition;
                isMoving = false;
                moveCooldownTimer = moveCooldown;
            }
        }
    }

    private void UpdateCooldown()
    {
        if (moveCooldownTimer > 0f)
        {
            moveCooldownTimer -= Time.deltaTime;
        }
    }

    private void AttemptMove(Vector2 direction)
    {
        Vector2 potentialPosition = targetPosition + gridSize * direction;
        float checkSize = gridSize * 0.45f;

        // Check if the move is possible and if not, handle box movement
        if (!Physics2D.OverlapBox(potentialPosition, new Vector2(checkSize, checkSize), 0, obstacleLayer))
        {
            Move(direction);
        }
        else
        {
            // Handle box movement
            Collider2D[] colliders = Physics2D.OverlapBoxAll(potentialPosition, new Vector2(checkSize, checkSize), 0, obstacleLayer);
            foreach (Collider2D col in colliders)
            {
                Box box = col.GetComponent<Box>();
                if (box != null && box.CanMove(direction,gameObject))
                {
                    box.Move(direction);
                    Move(direction);
                    return;
                }
            }
        }
    }

    override public void Move(Vector2 direction)
    {
        targetPosition += gridSize * direction;
        isMoving = true;
    }

    private void OnDrawGizmos()
    {
        float debugSize = gridSize * 0.9f;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(targetPosition, new Vector2(debugSize, debugSize));
    }
}
