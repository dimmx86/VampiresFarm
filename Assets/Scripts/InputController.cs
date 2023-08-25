using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InputController : MonoBehaviour
{
    [SerializeField] private Joystick joystick;

    public UnityEvent<Vector2> OnInput;
    public UnityEvent OnEndInput;

    private bool isInput = false;
    private bool isReady = false;
    private Vector2 direction = Vector2.zero;

    public bool IsReady => isReady;

   
    private void OnEnable()
    {
        joystick.OnBegin.AddListener(OnBegin);
        joystick.OnEnd.AddListener(OnEnd);
        isReady = true;
    }

    private void OnBegin()
    {
        isInput = true;
    }

    private void OnEnd()
    {
        isInput = false;
        OnEndInput?.Invoke();
        direction = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if (isInput)
        {
            direction.x = joystick.Horizontal;
            direction.y = joystick.Vertical;
            OnInput?.Invoke(direction);
            print(direction);
        }
    }

    private void OnDisable()
    {
        joystick.OnBegin.RemoveListener(OnBegin);
        joystick.OnEnd.RemoveListener(OnEnd);
        isReady = false;
    }
}
