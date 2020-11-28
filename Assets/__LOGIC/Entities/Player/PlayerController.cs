using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    private CharacterMovement _characterMovement;
    private Vector2 movementAxis;

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
        movementAxis.x = Input.GetAxis("Horizontal");
        movementAxis.y = Input.GetAxis("Vertical");
        movementAxis.Normalize();

        _characterMovement.Move(movementAxis);
        _characterMovement.Drop = Input.GetButton("Drop");
        _characterMovement.Use = Input.GetButton("Use");
    }
}
