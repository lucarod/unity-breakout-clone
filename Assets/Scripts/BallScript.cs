using UnityEngine;

public class BallScript : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;

    Rigidbody2D rb;
    Vector2 moveDirection;

    void OnEnable()
    {
        GameManager.OnGameStateChanged += HandleGameStateChanged;
    }

    void OnDisable()
    {
        GameManager.OnGameStateChanged -= HandleGameStateChanged;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        Invoke(nameof(StartGame), 3);
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude != moveSpeed)
        {
            rb.velocity = rb.velocity.normalized * moveSpeed;
        }
    }

    void StartGame()
    {
        moveDirection = new Vector2(Random.Range(-1f, 1f), -1);
        rb.velocity = moveDirection.normalized * moveSpeed;
    }

    void HandleGameStateChanged(GameState gameState)
    {
        if (gameState == GameState.GameOver)
        {
            Destroy(gameObject);
        }
    }
}
