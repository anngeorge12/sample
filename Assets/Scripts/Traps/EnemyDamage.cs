
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float damage;

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")//checking if the enemy has touched the player
        {
            collision.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
