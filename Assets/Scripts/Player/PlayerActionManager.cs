using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    PlayerBase player;
    GameObject bullet;

    [SerializeField]
    public GameObject bulletPrefab;

    private void Start()
    {
        player = GetComponent<PlayerBase>();
    }

    public void UpdateAction(Vector3 newPos)
    {
        if (player.GetIsMoving())
            UpdateLinearMovement(newPos);
        else if (player.GetIsShoooting())
        {
            if (bullet == null)
            {
                bullet = Instantiate(bulletPrefab);
                bullet.GetComponent<GunBullet>().SetFromPlayer(true);
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
        bullet.transform.position = newPos;
    }
}