using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    [Header("Game Over")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private AudioClip gameOverSound;

    [Header("Pause")]
    [SerializeField] private GameObject pauseScreen;

    private void Awake()
   {
        gameOverScreen.SetActive(false);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            //if pause active unpause and vice versa
            if(pauseScreen.activeInHierarchy)
                PauseGame(false);
            else
                PauseGame(true);
        }
    }

    #region Game Over
    //helps to just structure the code
    //activating the gameover screen
    public void GameOver()
   {
    gameOverScreen.SetActive(true);
    SoundManager.instance.PlaySound(gameOverSound);
   }

   //gameover functions
   public void Restart()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
   public void MainMenu()
   {
      SceneManager.LoadScene(0);
   }

   public void Quit()
   {
        Application.Quit();// to quit on build
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;//exits the game(when inside the editor)
        #endif
    }
    #endregion

    #region Pause
    public void PauseGame(bool status)
    {
        //if status is true the game is paused else not
        pauseScreen.SetActive(status);
        if(status)
            Time.timeScale = 0;//to pause the game
        else
            Time.timeScale = 1;//to revert back the pause statement
    }
    #endregion
}
