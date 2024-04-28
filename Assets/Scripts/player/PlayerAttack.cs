
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    //the below one is to tell from which position the bullets will be fired.
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireballs;
    [SerializeField] private AudioClip fireballSound;
    //this above variable allows us to decide how much time should be considered when one shoot
    // to until the next shoot.
    private Animator anim;
    private playermovement playerMovement;
    private float coolDownTimer=Mathf.Infinity;
    //this variable is used to make sure that the cool down time is not 0 in the first stage
    //therefore the player can start attacking in the intial stanz
    //this variable is to check if enough time has passed after shooting is done.

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerMovement = GetComponent<playermovement>();
    }
    private void Update()
    {
        if (Input.GetMouseButton(0) && coolDownTimer > attackCooldown && playerMovement.canAttack())
            Attack();
        coolDownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        SoundManager.instance.PlaySound(fireballSound);
        anim.SetTrigger("attack");
        coolDownTimer = 0;

        fireballs[FindFireball()].transform.position = firePoint.position;
        fireballs[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }
    private int FindFireball()
    {
        for(int i = 0;i<fireballs.Length;i++)
        {
            if (!fireballs[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
}
