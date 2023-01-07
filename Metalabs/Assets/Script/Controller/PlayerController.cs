using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    private Player pInput;

    [SerializeField] private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    
    private float gravityValue = -9.81f;

    [SerializeField] private Transform cameraMain;

    [Header("Player Setting")]
    [Range(0, 10)]

    public float playerSpeed = 2.0f;
    public float jumpHeight = 1.0f;
    public float runSpeed = 10.0f;

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Movement();
        Jump();
    }

    public void Movement()
    {
        Vector2 movement = InputManager.instance.PlayerInput.Character.Movement.ReadValue<Vector2>();
        Debug.Log(movement);

        Vector3 move = (cameraMain.forward * movement.y + cameraMain.right * movement.x);
        move.y = 0f;
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (InputManager.instance.PlayerInput.Character.Run.IsPressed() && groundedPlayer)
        {
            move = ((cameraMain.forward * movement.y + cameraMain.right * movement.x) * runSpeed);
            move.y = 0f;
            controller.Move(move * Time.deltaTime * playerSpeed);
        }

        if (move != Vector3.zero)
        {
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, Quaternion.LookRotation(move), 0.15f);
        }
    }

    public void Jump()
    {
        if (InputManager.instance.PlayerInput.Character.Jump.IsPressed() && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        Debug.Log("Jump");
    }
}
