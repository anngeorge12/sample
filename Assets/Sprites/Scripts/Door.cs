using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform previousRoom;
    [SerializeField] private Transform nextRoom;
    [SerializeField] private CameraControl cam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //to move the camera to the right room or the next room
            if(collision.transform.position.x < transform.position.x)
                cam.MoveToNewRoom(nextRoom);
            //to move the camera to the left room or the previous room
            else
                cam.MoveToNewRoom(previousRoom);
        }
    }
    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraControl>();
    }
}
