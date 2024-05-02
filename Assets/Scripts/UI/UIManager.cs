using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
   [SerializeField] private GameObject gameOverScreen;
   [SerializeField] private AudioClip gameOverSound;

   private void Awake()
   {
      gameOverScreen.SetActive(false);
   }

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
      Application.Quit();
   }
}
