using UnityEngine;

public class CameraControl : MonoBehaviour
{
    //room camera
    [SerializeField] private float speed;
    private float currentPosX;
    private Vector3 velocity = Vector3.zero;

    //this oart shows how the camera follows the player
    [SerializeField] private Transform player;
    [SerializeField] private float aheadDistance;
    //records the speed of the camera along with the player movents
    [SerializeField] private float cameraSpeed;
    private float lookAhead;


    private void Update()
    {
       
       // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(currentPosX, transform.position.y, transform.position.z), ref velocity, speed);

       //Follow player
       transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z);
        //setting the intial and final value of lookahead 
        //lerp method gradually changes the lookahead value from initial to final
        //the camera will move more to the left when facing the direction
        lookAhead = Mathf.Lerp(lookAhead, (aheadDistance * player.localScale.x),Time.deltaTime * cameraSpeed);
    }
   //change the destination of the camera
   public void MoveToNewRoom(Transform _newroom)
   {
        currentPosX = _newroom.position.x;

   }
}
