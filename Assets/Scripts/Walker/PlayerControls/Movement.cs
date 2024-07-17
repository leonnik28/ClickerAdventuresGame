using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;

    private CharacterController _characterController;

    private readonly float _initialYPosition = 1f;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }

    public void Move(Vector2 inputVector)
    {
        Vector3 moveDirection = transform.forward * inputVector.y + transform.right * inputVector.x;
        moveDirection.y = 0f;

        _characterController.Move(_moveSpeed * Time.deltaTime * moveDirection);

        Vector3 characterControllerPosition = _characterController.transform.position;
        characterControllerPosition.y = _initialYPosition;
        _characterController.transform.position = characterControllerPosition;
    }
}
