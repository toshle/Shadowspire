using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public CharacterController Controller;
    public float Speed;
    private Vector2 _move, _look;
    private float _gravity = -9.8f;
    private Vector3 _velocity, _rotationTarget;

    public void OnMove(InputAction.CallbackContext context)
    {
        _move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        _look = context.ReadValue<Vector2>();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _orientPlayer();

        _movePlayer();

        _applyGravity();
    }

    void _applyGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;

        if (Controller.isGrounded && _velocity.y < 0)
        {
            _velocity.y = -6f;
        }

        Controller.Move(_velocity * Time.deltaTime);
    }

    void _orientPlayer()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(_look);

        if (Physics.Raycast(ray, out hit))
        {
            _rotationTarget = hit.point;
        }

        var lookPosition = _rotationTarget - transform.position;
        lookPosition.y = 0;
        var rotation = Quaternion.LookRotation(lookPosition);

        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
    }

    void _movePlayer()
    {
        Vector3 movement = new Vector3(_move.x, 0f, _move.y);

        //transform.Translate(movement * Speed * Time.deltaTime, Space.World);
        if (movement != Vector3.zero)
        {
            Controller.Move(movement * Speed * Time.deltaTime);
        }
    }
}
