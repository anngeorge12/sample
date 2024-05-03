using UnityEngine.UI;
using UnityEngine;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string textIntro; // Sound: or music
    private Text txt;

    public void Awake()
    {
        txt = GetComponent<Text>();
    }

    public void Update()
    {
        UpdateVolume();
    } 

    public void UpdateVolume()
    {
        float volumeValue = PlayerPrefs.GetFloat(volumeName)*100;
        txt.text = textIntro + volumeValue.ToString();
    }
}

