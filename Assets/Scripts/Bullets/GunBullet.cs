using UnityEngine;

public class GunBullet : BulletPrefab
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public override void Shoot(Vector3 origin, Vector3 target){

        transform.position = Vector3.Lerp(origin, target, speed);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isFromPlayer)
        {
            if (!collision.gameObject.CompareTag("Player"))
            {
                //Pupa
                Destroy(gameObject);
            }
        } else
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                //Pupa a enmy
                Destroy(gameObject);
            }
        }
    }
}
