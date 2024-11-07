using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovementShooter : MonoBehaviour
{
    [SerializeField] private List<OG_MovementByMouse> players; // Array of player references
    private OG_MovementByMouse closestPlayer;
    private Vector3 closestPlayerPos;
    private float moveTime;
    private EnemyBase enemyStats;
    [SerializeField] private GameObject bulletShot; // Bullet prefab to shoot
    [SerializeField] private float velocity; // Movement speed
    [SerializeField] private float range; // Shooting range
    [SerializeField] private float minDistance = 2f; // Minimum distance before moving back

    private bool isReloading = false; // To control the "reload" wait after shooting
    private bool hasShot = false; // To ensure only one shot per turn
    private bool lastIsMovingState = false; // Track the last state of GetIsMoving to detect state change

    void Start()
    {
        enemyStats = GetComponent<EnemyBase>();

        // Get all OG_MovementByMouse objects in the scene and add them to the players list
        OG_MovementByMouse[] playersArray = GameObject.FindObjectsByType<OG_MovementByMouse>(FindObjectsSortMode.None);
        foreach (OG_MovementByMouse player in playersArray)
        {
            players.Add(player);
        }
        FindClosestPlayer();
    }

    void Update()
    {
        moveTime = Time.deltaTime * velocity;

        if (enemyStats.isAlive && closestPlayer != null)
        {
            bool currentIsMoving = closestPlayer.GetIsMoving();

            // Only take action on a new turn (when GetIsMoving toggles from false to true)
            if (!lastIsMovingState && currentIsMoving && !isReloading)
            {
                TakeAction();
            }

            // Update the last state of GetIsMoving
            lastIsMovingState = currentIsMoving;
        }
    }

    // Decides whether to shoot or move based on distance
    private void TakeAction()
    {
        FindClosestPlayer();
        float distanceToPlayer = Vector3.Distance(transform.position, closestPlayerPos);

        if (distanceToPlayer > range)
        {
            // Move towards the player if out of range
            MoveTowardsPlayer();
        }
        else if (distanceToPlayer < minDistance)
        {
            // Move away from the player if too close
            MoveAwayFromPlayer();
        }
        else if (!hasShot) // Shoot only once per turn
        {
            // In range, shoot
            Shoot();
            StartCoroutine(Reload());
        }
    }

    // Finds the closest player in the players array
    private void FindClosestPlayer()
    {
        float closestDistance = 100;

        foreach (var player in players)
        {
            float distance = Vector3.Distance(transform.position, player.GetPosition());
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
                closestPlayerPos = player.GetPosition();
            }
        }
    }

    // Moves towards the closest player
    private void MoveTowardsPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, closestPlayerPos, moveTime);
    }

    // Moves away from the closest player
    private void MoveAwayFromPlayer()
    {
        Vector3 directionAway = (transform.position - closestPlayerPos).normalized;
        transform.position = Vector3.MoveTowards(transform.position, transform.position + directionAway, moveTime);
    }

    // Shoots a bullet towards the closest player
    private void Shoot()
    {
        Debug.Log("BANG");
        GameObject bullet = Instantiate(bulletShot, transform.position, Quaternion.identity);
        GunBullet bulletScript = bullet.GetComponent<GunBullet>();
        bulletScript.isFromPlayer = false;
        bulletScript.Shoot((closestPlayerPos - transform.position).normalized); // Set bullet direction
        hasShot = true; // Mark that it has shot this turn
    }

    // Reload coroutine to wait until the next GetIsMoving toggle after shooting
    private IEnumerator Reload()
    {
        isReloading = true;
        yield return new WaitUntil(() => closestPlayer.GetIsMoving() == false);
        yield return new WaitUntil(() => closestPlayer.GetIsMoving() == true);
        isReloading = false;
        hasShot = false; // Reset shooting state for the next turn
    }
}
