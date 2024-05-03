using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Health playerHealth;
    private UIManager uiManager;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
        uiManager = FindObjectOfType<UIManager>();
    }

    public void CheckRespawn()
    {
        if (playerHealth.currentHealth==0) 
        {
            uiManager.GameOver();
        }

    }
}