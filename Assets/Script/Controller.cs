using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float Gravity = -9.81f;
    public float MovementSpeed;
    public Canvas HUDCanvas;
    public FloatingJoystick HUDController;
    public float MovingThreshold;

    private CharacterController _controller;
    private Vector3 _fallVelocity;
    private Animator _animator;
    private int _velocity;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _velocity = Animator.StringToHash("Velocity");
    }

    void Start()
    {
        HUDCanvas.gameObject.SetActive(true);
    }

    void Update()
    {
        var inputVelocity = Mathf.Sqrt(HUDController.Direction.y * HUDController.Direction.y + HUDController.Direction.x * HUDController.Direction.x);

        if (inputVelocity > MovingThreshold)
        {
            Vector3 movement = new Vector3(-HUDController.Direction.y, 0, HUDController.Direction.x);
            _controller.Move(movement * Time.deltaTime * MovementSpeed);
            float angle = Mathf.Atan2(HUDController.Direction.y, HUDController.Direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));
            _animator.SetFloat(_velocity, inputVelocity);
        }
        else
        {
            _animator.SetFloat(_velocity, 0);
        }
        _fallVelocity.y += Gravity * Time.deltaTime;
        _controller.Move(_fallVelocity * Time.deltaTime);
    }
}
