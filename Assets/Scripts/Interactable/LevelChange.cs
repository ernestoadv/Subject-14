using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChange : MonoBehaviour
{
    public Animator animator;
    public int levelToLoad;

    //Loads new level

    public void OnFadeComplete()
    {
        SceneManager.LoadScene(levelToLoad); //Load new level
        Time.timeScale = 1; //Unfreeze the game
    }
}
