
using UnityEngine;

public class FadeTrigger : MonoBehaviour
{
    public Animator animator;

    //This function runs when the player ends the level (by colliding with the door)

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("HumanPlayer"))
        {
            animator.updateMode = AnimatorUpdateMode.UnscaledTime; //So that the animation does not freeze when pausing the game
            Time.timeScale = 0; //Freeze the game
            animator.SetTrigger("fade"); //Fade in
        }

    }
}
