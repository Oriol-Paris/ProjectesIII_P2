using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    PlayerBase player;
    public GameObject bulletToInstantiate;

    [SerializeField]
    public GameObject bulletPrefab;
    public GunBullet gunBullet;

    bool isMoving = true;
    bool isShooting = true;

    private void Start()
    {
        player = GetComponent<PlayerBase>();
        bulletPrefab = GameObject.Find("GunBullet");
        gunBullet = bulletPrefab.GetComponent<GunBullet>();
    }

    public void UpdateAction(Vector3 newPos)
    {
        if (player.GetIsMoving() && (!player.GetComponent<OG_MovementByMouse>().GetIsMoving() || isMoving))
        {
            isMoving = true;
            UpdateLinearMovement(newPos);
        }
        else if (player.GetIsShoooting() && (!player.GetComponent<OG_MovementByMouse>().GetIsMoving() || isShooting))
        {
            if (gunBullet == null)
            {
                bulletPrefab = bulletToInstantiate;
                gunBullet = bulletPrefab.GetComponent<GunBullet>();

            }

            isShooting = true;
            Shoot(newPos);
        }

        if(!player.GetComponent<OG_MovementByMouse>().GetIsMoving())
        {
            isMoving = false;
            isShooting = false;
        }
    }

    private void UpdateLinearMovement(Vector3 newPos)
    {
        GetComponent<Transform>().position = newPos;
    }

    private void Shoot(Vector3 newPos)
    {
        gunBullet.transform.position = newPos;
        if (gunBullet.isHit)
        {
            Destroy(gunBullet);
            Destroy(gunBullet);
            Destroy(bulletPrefab);
            player.SetRange(player.GetOldRange());

        }

    }
}