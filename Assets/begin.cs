using UnityEngine.SceneManagement;
using UnityEngine;

public class begin : MonoBehaviour
{
    [Header("Begin")]
    [SerializeField] private GameObject Canvas;
    private void Awake()
    {
        Canvas.SetActive(false);
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
