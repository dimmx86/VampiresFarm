using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private Rigidbody playerRB;
    [SerializeField] private InputController input;

    [SerializeField] private float speed = 1f;

    private Vector3 directionMove = Vector3.zero;
    private bool isReady = false;

    private void Start()
    {
        StartCoroutine(WaitInput());
    }

    private IEnumerator WaitInput()
    {
        yield return new WaitUntil(() => input.IsReady);
        input.OnInput.AddListener(Move);
        input.OnEndInput.AddListener(StopMove);
        isReady = true;
    }

    private void Move(Vector2 direction)
    {
        directionMove.x = direction.x;
        directionMove.z = direction.y;

        playerRB.velocity = directionMove * speed;
    }

    private void StopMove()
    {
        playerRB.velocity = Vector3.zero;
    }

    private void OnDestroy()
    {
        if (isReady)
        {
            input.OnInput.RemoveListener(Move);
            input.OnEndInput.RemoveListener(StopMove);
        }
    }
}
