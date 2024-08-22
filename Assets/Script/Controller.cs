using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool _isDead;

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
        _isDead = false;
    }

    void Update()
    {
        _fallVelocity.y += Gravity * Time.deltaTime;
        _controller.Move(_fallVelocity * Time.deltaTime);

        if (_isDead) return;
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
            UIManager.Instance.ShowUI(UIIndex.WinUI, new UIParam { Data = "GAME OVER" });
            _isDead = true;
            _animator.SetFloat(_velocity, 0);
            transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
            Scythe.gameObject.SetActive(false);
        }


        Debug.Log(other.gameObject.name);
        if (other.gameObject.name == "Water")
        {
            InvokeRepeating(nameof(CollectWater), 0.1f, .1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.name == "Water")
        {
            CancelInvoke(nameof(CollectWater));
        }
    }
    private void CollectWater()
    {
        ResourceManager.Instance.AddWater(1);
    }
}
