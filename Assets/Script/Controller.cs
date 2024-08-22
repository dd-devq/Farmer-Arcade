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
    public Vector3 SpawnPosition;
    public Transform Scythe;
    public float ScytheSpeed;
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
        transform.position = SpawnPosition;
    }

    void Update()
    {
        var inputVelocity = Mathf.Sqrt(HUDController.Direction.y * HUDController.Direction.y + HUDController.Direction.x * HUDController.Direction.x);

        if (inputVelocity > MovingThreshold)
        {
            Scythe.GetComponent<Collider>().enabled = false;
            foreach (Transform child in Scythe.transform)
            {
                child.gameObject.SetActive(false);
            }
            // Move
            Vector3 movement = new Vector3(-HUDController.Direction.y, 0, HUDController.Direction.x);
            _controller.Move(movement * Time.deltaTime * MovementSpeed);

            // Rotate
            float angle = Mathf.Atan2(HUDController.Direction.y, HUDController.Direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, -angle, 0));

            // Animation
            _animator.SetFloat(_velocity, inputVelocity);
            AudioManager.Instance.PlaySound("run");
        }
        else
        {
            // Animation
            _animator.SetFloat(_velocity, 0);
            foreach (Transform child in Scythe.transform)
            {
                child.gameObject.SetActive(true);
            }
            Scythe.GetComponent<Collider>().enabled = true;

            Scythe.RotateAround(transform.position, Vector3.up, ScytheSpeed * Time.deltaTime);
        }

        //Gravity
        _fallVelocity.y += Gravity * Time.deltaTime;
        _controller.Move(_fallVelocity * Time.deltaTime);

        // Move Scythe
    }

    private void OnTriggerEnter(Collider other)
    {

        // Game Over
        if (other.gameObject.name == "Platform")
        {
            Debug.Log("Plane");
        }

        if (other.gameObject.name.Contains("Bridge"))
        {
            Debug.Log("Bridge");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.name == "Water")
        {
            Debug.Log("Water");
        }
    }
}
