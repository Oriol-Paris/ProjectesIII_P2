using UnityEngine;

public class GunBullet : BulletPrefab
{
    public bool isHit;
    private Vector3 targetPosition; // Target position the bullet is moving towards
    private float speed = 10f; // Speed of the bullet (adjust as needed)
    private float lifetime = 5f; // Lifetime in seconds before auto-destruction

    void Start()
    {
        isHit = false;
    }

    void Update()
    {
        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Decrease lifetime over time
        lifetime -= Time.deltaTime;

        // Check if the bullet has reached its destination, hit something, or if its lifetime has expired
        if (isHit || Vector3.Distance(transform.position, targetPosition) < 0.1f || lifetime <= 0f)
        {
            DestroyBullet();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFromPlayer)
        {
            EnemyBase enemy = collision.gameObject.GetComponent<EnemyBase>();
            if (enemy != null && enemy.GetHealth() > 0)
            {
                enemy.Damage(damage);
                isHit = true;
            }
        }
        else
        {
            PlayerBase player = collision.gameObject.GetComponent<PlayerBase>();
            if (player != null)
            {
                player.Damage();
                isHit = true;
            }
        }
    }

    public void Shoot(Vector3 direction)
    {
        targetPosition = transform.position + direction * 20f; // Set target position in the direction, adjust range as needed
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
