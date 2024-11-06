using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    PlayerBase player;
    public GameObject bulletToInstantiate;

    [SerializeField]
    public GameObject bulletPrefab;
    public GunBullet gunBullet;

    private void Start()
    {
        player = GetComponent<PlayerBase>();
        bulletPrefab = GameObject.Find("GunBullet");
        gunBullet = bulletPrefab.GetComponent<GunBullet>();
    }

    public void UpdateAction(Vector3 newPos)
    {
        if (player.GetIsMoving())
            UpdateLinearMovement(newPos);
        else if (player.GetIsShoooting())
        {
            if (gunBullet == null)
            {

                bulletPrefab = bulletToInstantiate;
                gunBullet = bulletPrefab.GetComponent<GunBullet>();

            }


            Shoot(newPos);
            
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