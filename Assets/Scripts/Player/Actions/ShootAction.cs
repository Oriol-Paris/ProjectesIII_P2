using UnityEngine;

public class ShootAction : ActiveAction
{
    public GameObject bulletPrefab;
    public GameObject bulletToInstantiate;

    public override void Execute(PlayerBase player, Vector3 targetPosition)
    {
        if (bulletPrefab == null)
        {
            bulletPrefab = Instantiate(bulletToInstantiate);
        }

        GunBullet gunBullet = bulletPrefab.GetComponent<GunBullet>();
        gunBullet.transform.position = targetPosition;

        if (bulletPrefab.transform.position == player.GetComponent<OG_MovementByMouse>().GetPositionDesired())
        {
            Destroy(bulletPrefab);
            bulletPrefab = null;
        }
    }
}