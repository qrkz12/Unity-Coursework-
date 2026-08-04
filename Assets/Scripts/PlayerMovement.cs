using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 6f;
    public float gravity = -20f;

    private CharacterController controller;
    private float verticalVelocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(x, 0f, z);
        moveDirection = Vector3.ClampMagnitude(moveDirection, 1f);

        // if (moveDirection != Vector3.zero)
        // {
        //     transform.forward = moveDirection;
        // }

        if (controller.isGrounded && verticalVelocity < 0f)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 horizontalMovement = moveDirection * speed;
        Vector3 verticalMovement = Vector3.up * verticalVelocity;

        controller.Move(
            (horizontalMovement + verticalMovement) * Time.deltaTime
        );
    }
}