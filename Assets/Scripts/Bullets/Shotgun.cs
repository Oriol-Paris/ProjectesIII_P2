using UnityEngine;

public class Shotgun : BulletPrefab
{
    private Vector3 targetPosition; // Target position the bullet is moving towards
    //private float lifetime = 5f; // Lifetime in seconds before auto-destruction
    private Vector3 offset; // Offset for the bullet

    void Start()
    {
        isHit = false;
        speed = 10f;
        for (int i = 0; i < playerData.availableActions.Count; i++)
        {
            if (playerData.availableActions[i].style.prefab == playerData.shotgun.prefab)
            {
                damage = playerData.availableActions[i].style.damage; break;
            }
        }
    }

    void Update()
    {
        if (IsPaused()) return;

        // Move towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Decrease lifetime over time
        //lifetime -= Time.deltaTime;

        // Check if the bullet has reached its destination, hit something, or if its lifetime has expired
        if (isHit || Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            DestroyBullet();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Walls"))
        {
            DestroyBullet();
        }
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

    public override void Shoot(Vector3 direction)
    {
        // Store the original direction and target position
        originalDirection = direction;
        targetPosition = transform.position + direction.normalized * 20f;
    }

    public void Shoot(Vector3 direction, Vector3 offset)
    {
        this.offset = offset;
        // Store the original direction and target position
        originalDirection = direction + offset;
        targetPosition = transform.position + (direction + offset).normalized * 20f;
    }
}
