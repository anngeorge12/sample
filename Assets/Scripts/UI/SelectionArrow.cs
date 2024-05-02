using UnityEngine;
using UnityEngine.UI;
public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private RectTransform[] options;
    [SerializeField] private AudioClip changeSound;//sound played when the arrow is moved
    [SerializeField] private AudioClip interactSound;//sound played when you select any option
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        //selecting the options 
        if(Input.GetKeyDown(KeyCode.UpArrow))
            ChangePosition(-1);
        if(Input.GetKeyDown(KeyCode.DownArrow))
            ChangePosition(1);

        //interacting with the options
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
            Interact();
    }



    private void ChangePosition(int _change)
    {
        currentPosition += _change;

        if(_change != 0)
            SoundManager.instance.PlaySound(changeSound);



        if(currentPosition < 0)
            currentPosition = options.Length - 1;
        else if(currentPosition > options.Length - 1)
            currentPosition = 0;
        //Assign the current Y position of the selection arrow 
        //Basically tells us the coordinates of the option that we are selecting
        //Used to move the options arow up and down to select a particular option
        rect.position = new Vector3(rect.position.x, options[currentPosition].position.y, 0);
    }

    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        //access the button component on each object
        options[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
