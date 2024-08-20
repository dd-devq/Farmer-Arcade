using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float Gravity = -9.81f;
    public Vector3 FallVelocity;
    public float MovementSpeed;
    public float RotationSpeed;

    public Canvas HUDCanvas;
    public VariableJoystick HUDController;

    private CharacterController _controller;

    private bool isJoystick;
    private void Awake()
    {
        _controller = transform.GetComponent<CharacterController>();
    }

    void Start()
    {
        HUDCanvas.gameObject.SetActive(true);
        isJoystick = true;
    }

    void Update()
    {
        if (isJoystick)
        {
            Vector3 movement = new Vector3(HUDController.Direction.x, 0, HUDController.Direction.y);
            _controller.Move(movement * Time.deltaTime * MovementSpeed);
            var targetDirection = Vector3.RotateTowards(_controller.transform.forward, movement, RotationSpeed * Time.deltaTime, 0.0f);
            _controller.transform.rotation = Quaternion.LookRotation(targetDirection);
        }
        FallVelocity.y += Gravity * Time.deltaTime;
        _controller.Move(FallVelocity * Time.deltaTime);
    }
}
