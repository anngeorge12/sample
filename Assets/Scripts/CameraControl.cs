using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    [SerializeField] private Transform player;


    private void update()
    {
       
       // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

       //Follow player
       transform.position = new Vector3(player.position.x, transform.position.y, transform.position.z);
    }
   //change the destination of the camera
   public void MoveToNewRoom(Transform _newroom)
   {
        currentPosX = _newroom.position.x;

   }
}
