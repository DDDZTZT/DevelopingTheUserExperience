
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private bool isPaused = false;
    public TextMeshProUGUI pauseTextObject;

    public AudioManager audioManager;

    private int count;
    public TextMeshProUGUI countText;
    // Rigidbody of the player.
    private Rigidbody rb;

    // Movement along X and Y axes.
    private float movementX;
    private float movementY;

    // Speed at which the player moves.
    public float speed = 0;

    // Start is called before the first frame update.
    void Start()
    {
        // Get and store the Rigidbody component attached to the player.
        rb = GetComponent<Rigidbody>();
        ResumeGame();
    }

    // This function is called when a move input is detected.
    void OnMove(InputValue movementValue)
    {
        // Convert the input value into a Vector2 for movement.
        Vector2 movementVector = movementValue.Get<Vector2>();

        // Store the X and Y components of the movement.
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    // FixedUpdate is called once per fixed frame-rate frame.
    void FixedUpdate()
    {
        // Create a 3D movement vector using the X and Y inputs.
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        // Apply force to the Rigidbody to move the player.
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the object the player collided with has the "PickUp" tag.
        if (other.gameObject.CompareTag("PickUp"))
        {
            audioManager.PlayHitSound();

            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
            count = count + 1;
            countText.text = "Count: " + count.ToString();
        }
        if (other.gameObject.CompareTag("Bonus"))
        {
            // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
            count = count + 5;
            countText.text = "Count: " + count.ToString();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
        pauseTextObject.gameObject.SetActive(true);
    }
    void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
        pauseTextObject.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused == true)
                ResumeGame();
            else
                PauseGame();
        }
    }
}