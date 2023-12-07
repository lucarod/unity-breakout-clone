using UnityEngine;

public class WallScript : MonoBehaviour
{
    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            audioSource.Play();
        }
    }
}
