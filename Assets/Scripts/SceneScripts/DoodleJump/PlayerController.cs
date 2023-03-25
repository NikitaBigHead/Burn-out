using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 15;
    public float moveSpeed = 5f;
    public float cameraFollowSpeed = 5f;

    private Rigidbody2D rb;
    private Camera mainCamera;
    private float cameraTargetY;

    private Collider2D collider;
    public TextMeshProUGUI text;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        collider= rb.GetComponent<Collider2D>();
        mainCamera = Camera.main;
        GetComponentInChildren<AttackableEntity>().health = PlayerData.healthWhenEnterDoodle;
        PlayerData.playerCurrentHealth = PlayerData.healthWhenEnterDoodle;
    }

    void Update()
    {
        // Move the camera up to follow the player
        if (transform.position.y > mainCamera.transform.position.y)
        {
            cameraTargetY = transform.position.y;
        }
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position,
            new Vector3(0f, cameraTargetY, -10f), Time.deltaTime * cameraFollowSpeed);

        // Move the player left or right
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * moveSpeed, rb.velocity.y);
        if (rb.velocity.y <= 0)
        {
            collider.enabled = true;
        }


    }

    // Handle collisions with platforms
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Platform"))
        {
            animator.Play("Jump");
            
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            collider.enabled = false;
        }
        else if (collision.collider.name == "DeadZone")
        {
            SceneManager.LoadScene("Doodle Jump");
        }
    }
}
