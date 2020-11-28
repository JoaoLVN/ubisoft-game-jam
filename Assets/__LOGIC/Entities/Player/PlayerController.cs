﻿using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    public bool Drop { get { return Input.GetButton("Drop"); } }
    public bool Use { get { return Input.GetButton("Use"); } }
    public Vector2 Forward { get { return _forward; } }
    public Vector2 Right { get { return Quaternion.Euler(0, 0, 90) * _forward; } }

    private CharacterMovement _characterMovement;
    private Vector2 _forward;
    private Vector2 _movementAxis;

    private void Start()
    {
        _characterMovement = GetComponent<CharacterMovement>();
    }

    private void Update()
    {
        ProcessInputs();
    }

    private void ProcessInputs()
    {
        _movementAxis.x = Input.GetAxis("Horizontal");
        _movementAxis.y = Input.GetAxis("Vertical");
        _movementAxis.Normalize();

        _forward = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        _forward.Normalize();
        _characterMovement.Move(_movementAxis);
    }
}
