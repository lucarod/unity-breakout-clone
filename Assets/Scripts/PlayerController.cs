using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float maxBounceAngle = 75f;

    Rigidbody2D rb;
    AudioSource audioSource;
    Vector2 moveDirection = Vector2.zero;
    bool canPlayerMove = true;

    void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    public void onMove(InputAction.CallbackContext context)
    {
        if (canPlayerMove)
        {
            moveDirection = context.ReadValue<Vector2>();
        }
    }

    public void onLaunchBall(InputAction.CallbackContext context)
    {
        Debug.Log("Launch Ball");
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        rb.velocity = moveDirection * moveSpeed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        if (collidedObject.tag.Equals("Ball"))
        {
            audioSource.Play();
            LaunchBall(collision);
        }
    }

    void LaunchBall(Collision2D collision)
    {
        Vector2 playerPosition = transform.position;
        Vector2 contactPoint = collision.GetContact(0).point;

        float offset = playerPosition.x - contactPoint.x;
        float playerWidth = collision.otherCollider.bounds.size.x / 2;

        float currentAngle = Vector2.SignedAngle(Vector2.up, collision.rigidbody.velocity);
        float bounceAngle = offset / playerWidth * maxBounceAngle;
        float newAngle = Mathf.Clamp(currentAngle + bounceAngle, -maxBounceAngle, maxBounceAngle);

        Quaternion rotation = Quaternion.AngleAxis(newAngle, Vector3.forward);
        collision.rigidbody.velocity = rotation * Vector2.up * collision.rigidbody.velocity.magnitude;
    }

    void HandleGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.GameOver)
        {
            moveDirection = Vector2.zero;
            canPlayerMove = false;
        }
    }
}
