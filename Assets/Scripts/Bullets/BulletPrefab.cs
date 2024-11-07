using UnityEngine;

public abstract class BulletPrefab : MonoBehaviour
{
    [SerializeField] public int range;
    [SerializeField] public float speed;
    [SerializeField] public int damage;
    [SerializeField] public bool isFromPlayer;
    public Vector3 velocity;
    

    public void Update()
    {
        this.transform.position = velocity * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        //Destroy(this);
    }

    public void SetFromPlayer(bool val) { isFromPlayer = val; }

    public void SetVelocity(float newSpeed)
    {
        speed = newSpeed;
    }
}
