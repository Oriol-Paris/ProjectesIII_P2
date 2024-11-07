using System.Collections.Generic;
using UnityEngine;

public class PlayerActionManager : MonoBehaviour
{
    PlayerBase player;
    public GameObject bulletToInstantiate;

    [SerializeField]
    public GameObject bulletPrefab;
    public GunBullet gunBullet;

    public bool isMoving = true;
    public bool isShooting = true;
    bool lastIsHit;

    private void Start()
    {
        player = GetComponent<PlayerBase>();
        //bulletPrefab = bulletToInstantiate;
        gunBullet = bulletPrefab.GetComponent<GunBullet>();
    }

    public void UpdateAction(Vector3 newPos, float t)
    {
        if (player.GetIsMoving() && (!player.GetComponent<OG_MovementByMouse>().GetIsMoving() || isMoving))
        {
            isMoving = true;
            UpdateLinearMovement(newPos);
        }
        else if (player.GetIsShoooting() && (!player.GetComponent<OG_MovementByMouse>().GetIsMoving() || isShooting))
        {
            if (bulletPrefab==null&&t<0.01f)
            {
                
                    Debug.Log("AAAAA");

                    bulletPrefab = Instantiate(bulletToInstantiate);
                
                    gunBullet = bulletPrefab.GetComponent<GunBullet>();
                



            }

            if(bulletPrefab!=null) { 
            isShooting = true;
            bulletPrefab.SetActive(true);
            Shoot(newPos);
            }




        }
        if (!isShooting)
        {

            

        }
        if (!player.GetComponent<OG_MovementByMouse>().GetIsMoving())
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
        

    }
}