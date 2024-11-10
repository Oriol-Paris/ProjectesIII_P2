using UnityEngine;

public class ShootAction : ActiveAction
{
    public GameObject bulletPrefab;
    public GameObject bulletToInstantiate;

    public override void Execute(PlayerBase player, Vector3 targetPosition)
    {
        if (bulletToInstantiate == null)
        {
            Debug.LogError("Bullet prefab is not set.");
            return;
        }

        // Instantiate the bullet at the player's position
        GameObject bulletInstance = Instantiate(bulletToInstantiate, player.transform.position, Quaternion.identity);

        // Get the GunBullet component and set the direction
        GunBullet gunBullet = bulletInstance.GetComponent<GunBullet>();
        if (gunBullet != null)
        {
            Vector3 direction = (targetPosition - player.transform.position).normalized;
            gunBullet.Shoot(direction);
        }
        else
        {
            Debug.LogError("GunBullet component not found on the instantiated bullet.");
        }
    }
}