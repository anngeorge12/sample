
using UnityEngine;

public class EnemyProjectile : EnemyDamage //will damage the player when in contact
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;// to deactivate the trap after some time
    private float lifetime;
    public void ActivateProjectile()
    {
        lifetime = 0;
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed,0,0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);//executes the logic from the enemy damage script for collision
        gameObject.SetActive(false);//deactivates the projectile if hits collider   
    }
}
