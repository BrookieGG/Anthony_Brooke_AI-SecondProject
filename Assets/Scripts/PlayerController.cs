using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5;
    public float gravity = 9.8f;
    public float rotationSpeed = 12;
    public float crouchSpeed = 2.5f;
    public Transform cameraTransform;
    public Vector3 lastMoveDirection;
    
    private CharacterController characterController;
    private Vector2 moveInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    // Update is called once per frame
    void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        HandleMovement();
        CacheVelocity();
    }
    public void HandleMovement()
    {
        Vector3 moveDirection = Vector3.zero;
        if (moveInput.sqrMagnitude > 0.001f)
        {
            moveDirection = new Vector3(moveInput.x, 0f, moveInput.y).normalized;
            lastMoveDirection = moveDirection;

            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
        float currentSpeed = moveSpeed;
        Vector3 horizontalMove = moveDirection * currentSpeed;
        characterController.Move(horizontalMove * Time.deltaTime);
    }
    public void CacheVelocity()
    {

    }
}
