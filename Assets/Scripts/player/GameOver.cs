using UnityEngine;

public class GameOver : MonoBehaviour
{
   private UIManager uiManager;
   private Health playerHealth;

   private void Awake()
   {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
   }

   private void Screen()
   {
        uiManager.GameOver();
        return;
   }
}
