using System.Collections.Generic;
using UnityEngine;

public class ShootAction : ActiveAction
{
    public GameObject spawnedBullet;
    public GameObject bulletPrefab;
    public Vector3[] offsets = new Vector3[]
    {
        new Vector3(-0.5f, 0, 0), // Left offset
        Vector3.zero,             // Center
        new Vector3(0.5f, 0, 0)   // Right offset
    };

    public override void Execute(PlayerBase player, Vector3 targetPosition)
    {
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet prefab is not set.");
            return;
        }

        OG_MovementByMouse movementScript = player.GetComponent<OG_MovementByMouse>();

        // Instantiate the bullet at the player's position
        GameObject bulletInstance = Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);

        // Determine the type of bullet and call the appropriate Shoot method
        BulletPrefab bullet = bulletInstance.GetComponent<BulletPrefab>();
        if (bullet != null)
        {
            Vector3 direction = (targetPosition - player.transform.position).normalized;

            if (bullet is Shotgun)
            {
                // Loop through each offset and instantiate a bullet for Shotgun
                foreach (var offset in offsets)
                {
                    GameObject shotgunBulletInstance = Instantiate(bulletPrefab, player.transform.position, Quaternion.identity);
                    Shotgun shotgunBullet = shotgunBulletInstance.GetComponent<Shotgun>();
                    if (shotgunBullet != null)
                    {
                        shotgunBullet.Shoot(direction, offset);
                        movementScript.RegisterBullet(shotgunBullet);
                    }
                    else
                    {
                        Debug.LogError("Shotgun component not found on the instantiated bullet.");
                    }
                }
            }
            else if (bullet is GunBullet)
            {
                bullet.Shoot(direction);
                movementScript.RegisterBullet(bullet);
            }
            else if (bullet is LaserBullet)
            {
                bullet.Shoot(direction);
                movementScript.RegisterBullet(bullet);
            }
            else
            {
                Debug.LogError("Unknown bullet type.");
            }
        }
        else
        {
            Debug.LogError("BulletPrefab component not found on the instantiated bullet.");
        }
    }
}