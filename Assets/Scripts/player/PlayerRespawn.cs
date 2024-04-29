using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound; //sound when player gets new checkpoint
    private Transform currentCheckpoint; //hold current checkpoint
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void Respawn()
    {
        transform.position = currentCheckpoint.position; // move player to current position
        playerHealth.Respawn();// restore player health and reset animation

        Camera.main.GetComponent<CameraControl>().MoveToNewRoom(currentCheckpoint.parent);   //move camera to checkpoint room (**fro this to work the checkpoitns has to be placed as a child of the room object)
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.tag == "checkpoint")
        {
            currentCheckpoint = collision.transform; //store the checkpoint that we activated as current one
            SoundManager.instance.PlaySound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false; // deactivate checkpoint collider
            collision.GetComponent<Animator>().SetTrigger("appear"); //trigger checkpoint animation
        }
    }
}
