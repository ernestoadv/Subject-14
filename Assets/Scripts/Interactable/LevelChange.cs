using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public Animator animator;

    //Loads new level

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); //Load new level
        Time.timeScale = 1; //Unfreeze the game
    }
}
