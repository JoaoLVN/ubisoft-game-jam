using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    public bool Drop { get { return Input.GetButton("Drop"); } }
    public bool Use { get { return Input.GetButton("Use"); } }
    public Vector2 Forward { get { return _characterMovement.Forward; } }
    public Vector2 Right { get { return Quaternion.Euler(0, 0, 90) * _characterMovement.Forward; } }

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
    }
}
