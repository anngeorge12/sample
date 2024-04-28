
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("SpikeHead Attributes")]
    [SerializeField] private float speed;
    [SerializeField] private float range;// to see how far the spike head can see or go
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    [Header("SFX")]
    [SerializeField] private AudioClip spikeSound;
    private float checkTimer;
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;//to store the players position when detected
    private bool attacking;//to see when the spike head hits the player

    private void OnEnable()//gets called every time the object gets activated
    {
        Stop();
    }

    private void Update()
    {
        //to move to final destination only if attacking
        if (attacking)
            transform.Translate(destination * Time.deltaTime * speed);
        else
        {
            checkTimer += Time.deltaTime;
            if(checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();
        //check if spikehead sees the player on all 4 sides
        for(int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i],Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i],range,playerLayer);

            if( hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }
    private void CalculateDirections()//to check if the spike head sees the player among all directions
    {
        directions[0] = transform.right * range;//moves right
        directions[1] = -transform.right * range;//moves left
        directions[2] = transform.up * range;//moves up
        directions[3] = -transform.up * range;//moves down
    }
    private void Stop()
    {
        destination = transform.position;//set as current position so it doesnt move
        attacking = false;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.PlaySound(spikeSound);
        base.OnTriggerEnter2D(collision);//to link with the parent script
        Stop();//spikehead stops after collision on something
    }
}
