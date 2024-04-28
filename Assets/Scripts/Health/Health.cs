using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header ("Health")]
    [SerializeField] private float startinghealth;
    public float currentHealth { get; private set; }
    private Animator anim;

    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;
    
    [Header("Components")]
    [SerializeField] private Behaviour[] components;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioClip hurtSound;

    private bool invulnerable;
    private void Awake()
    {
        currentHealth = startinghealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void TakeDamage(float _damage)
    {
        if(invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startinghealth);
        
        if(currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            //iframes
            StartCoroutine(Invulnerabilty());
            SoundManager.instance.PlaySound(hurtSound);
        }
        else{
            //player dead
            if(!dead)
            {
                anim.SetTrigger("die");
                //player
                if(GetComponent<playermovement>() != null)
                    GetComponent<playermovement>().enabled = false;
                //enemy
                anim.SetTrigger("death");
                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;


                if(GetComponent<Wizard>() != null)    
                    GetComponent<Wizard>().enabled = false;



                dead = true;
                SoundManager.instance.PlaySound(deathSound);
            }
            
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startinghealth);
    }
    private IEnumerator Invulnerabilty()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(8, 9, true);
        //invulnerabilty time duration
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f); //the last floating point is for flashes
            yield return new WaitForSeconds(iFramesDuration/(numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(8, 9, false);
        invulnerable = false;
    }
}