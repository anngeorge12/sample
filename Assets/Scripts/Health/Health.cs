using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startinghealth;
    public float currentHealth { get; private set; }
    private Animator anim;

    private bool dead;

    private void Awake()
    {
        currentHealth = startinghealth;
        anim = GetComponent<Animator>();
    }
    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startinghealth);
        
        if(currentHealth > 0)
        {
            //player hurt
            anim.SetTrigger("hurt");
            //iframes
        }
        else{
            //player dead
            if(!dead)
            {
                anim.SetTrigger("die");
                GetComponent<playermovement>().enabled = false;
                dead = true;
            }
            
        }
    }
    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startinghealth);
    }
}