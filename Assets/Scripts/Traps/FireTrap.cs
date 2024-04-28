
using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;//idicates the time taken by the trap to activate the fire trap after stepping
    [SerializeField] private float activeTime;//time take to stay active after activation
    [Header("SFX")]
    [SerializeField] private AudioClip fireSound;
    private Animator anim;
    private SpriteRenderer spriteRend;

    private bool triggered;//when trap is triggered
    private bool active;//when trap is active to hurt

    private Health playerHealth;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(playerHealth != null && active)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        { 

            playerHealth = collision.GetComponent<Health>();


            if (!triggered)
            {
                //trigger the firetrap
                StartCoroutine(ActivateFiretrap());
            }
            if (active) 
            {
                collision.GetComponent<Health>().TakeDamage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFiretrap() 
    {
        //turn the sprite red to notify the player and trigger the trap
        triggered = true;
        spriteRend.color = Color.red;

        //wait for delay,activate trap,start animation,return color back to normal
        yield return new WaitForSeconds(activationDelay);// for a time delay in activating the trap
        SoundManager.instance.PlaySound(fireSound);
        spriteRend.color = Color.white;//turn the sprite normal 
        active = true;
        anim.SetBool("activated", true);

        //wait for some seconds,reset all variables and deactivate the trap and animator
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;//this is to deactivate after a certain time
        anim.SetBool("activated", false);
    }
}
