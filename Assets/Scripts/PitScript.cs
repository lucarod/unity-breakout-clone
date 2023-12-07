using UnityEngine;

public class PitScript : MonoBehaviour
{    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            GameManager.Instance.UpdateGameState(GameState.GameOver);
        }
    }
}
