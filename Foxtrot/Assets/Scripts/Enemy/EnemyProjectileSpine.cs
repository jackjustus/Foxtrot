using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class EnemyProjectileSpine : MonoBehaviour
{
    [SerializeField] private float baseSpeed;
    [SerializeField] private float resetTime;
    [SerializeField] private UnityEngine.Transform enemy;
    private int damage = 1;
    Vector2 enemyToPlayer;

    private float speed;

    private float lifetime;
    private UnityEngine.Transform player;
    private BoxCollider2D coll;

    private bool hit;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;


        Vector3 pos = enemy.position;

        // Creating a unit vector from the player to the enemy
        enemyToPlayer = (pos - player.position);



        speed = baseSpeed;

        if (enemyToPlayer.x > 0)
        {
            speed = -speed;
        }
        
    }

    


    private void Update()
    {

        

        
        


        if (hit) return;
        //float movementSpeed = speed * Time.deltaTime;

        float movementSpeed = speed * Time.deltaTime;

        transform.Translate(movementSpeed, 0, 0);


        
        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.GetComponent<PlayerHealth>() != null)
        {
        
            PlayerHealth health = collision.GetComponent<PlayerHealth>();
            health.Damage(damage);


        }

        hit = true;
        //transform.localScale = enemy.localScale;
        //base.OnTriggerEnter2D(collision); //Execute logic from parent script first
        coll.enabled = false;

        
            gameObject.SetActive(false); //When this hits any object deactivate arrow
    }
    private void Deactivate()
    {
        gameObject.SetActive(false);
    }


}