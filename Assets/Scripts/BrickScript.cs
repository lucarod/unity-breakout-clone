using UnityEngine;

public class BrickScript : MonoBehaviour
{
    [SerializeField] float animationDelay = 0f;

    Animator animator;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider2d;

    const string SHINING = "Shining";

    void Awake()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        Invoke(nameof(PlayShiningAnimation), animationDelay);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            spriteRenderer.enabled = false;
            boxCollider2d.enabled = false;
            audioSource.Play();
            Destroy(gameObject, audioSource.clip.length);
        }
    }

    void PlayShiningAnimation()
    {
        if (IsShiningAnimationPlaying(animator)) return;

        animator.Play(SHINING, -1, 0f);
    }

    bool IsShiningAnimationPlaying(Animator animator)
    {
        AnimatorStateInfo currentAnimatorState = animator.GetCurrentAnimatorStateInfo(0);

        return 
            currentAnimatorState.IsName(SHINING) && 
            currentAnimatorState.normalizedTime < 1.0f;
    }
}
