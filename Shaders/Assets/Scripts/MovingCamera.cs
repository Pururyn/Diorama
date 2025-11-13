using UnityEngine;
using UnityEngine.InputSystem;

public class MovingCamera : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float lookSensitivity = 0.1f;
    [SerializeField] private Rigidbody rigidBody = null;

    private Vector3 input = Vector3.zero;
    private Vector2 lookInput = Vector2.zero;
    private float rotationX = 0f;
    private float rotationY = 0f;

    private void Reset()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        // Verrouiller et masquer le curseur
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void Player_OnMove(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>();
        input.z = input.y;
        input.y = 0;
    }

    public void Player_OnLook(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        // Rotation de la caméra
        rotationY += lookInput.x * lookSensitivity;
        rotationX -= lookInput.y * lookSensitivity;

        // Limiter la rotation verticale pour éviter le retournement
        rotationX = Mathf.Clamp(rotationX, -90f, 90f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        // Libérer le curseur avec Échap
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void FixedUpdate()
    {
        // Déplacement relatif à la direction de la caméra
        Vector3 moveDirection = transform.right * input.x + transform.forward * input.z;
        rigidBody.linearVelocity = speed * moveDirection.normalized;
    }
}
