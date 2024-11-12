using System.Collections.Generic;
using UnityEngine;

public class ShootAction : ActiveAction
{
    public GameObject spawnedBullet;
    public GameObject bulletPrefab;

    public override void Execute(PlayerBase player, Vector3 targetPosition)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is not set.");
            return;
        }

        // Instantiate the bullet at the player's position
        GameObject bulletInstance = Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);

        // Get the GunBullet component and set the direction
        BulletPrefab gunBullet = bulletInstance.GetComponent<BulletPrefab>();
        if (gunBullet != null)
        {
            Vector3 direction = (targetPosition - player.transform.position).normalized*gunBullet.GetRange();
            gunBullet.Shoot(direction);
        }
        else
        {
            Debug.LogError("GunBullet component not found on the instantiated bullet.");
        }
    }
}