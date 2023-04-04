using System;
using Core;
using UnityEngine;


public class InputReader : MonoBehaviour
{
    [SerializeField] private Hero _hero;

    private Vector2 _direction;
    
    private void Update()
    {
        var horizontal = Input.GetAxisRaw(InputConstants.Horizontal.ToString());
        var vertical = Input.GetAxisRaw(InputConstants.Vertical.ToString());

        if (Input.GetButtonDown(InputConstants.Jump.ToString()))
            _hero.Jump();

        _direction = new Vector2(horizontal, vertical);
    }

    private void FixedUpdate()
    {
        _hero.Move(_direction);
    }
}
