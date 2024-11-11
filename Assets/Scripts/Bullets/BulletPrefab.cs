using UnityEngine;

public abstract class BulletPrefab : MonoBehaviour
{
    [SerializeField] public int range;
    [SerializeField] public float speed;
    [SerializeField] public int damage;
    [SerializeField] public bool isFromPlayer;
    public Vector3 velocity;

    private void OnBecameInvisible()
    {
        //Destroy(this);
    }

    public void SetFromPlayer(bool val) { isFromPlayer = val; }
    public abstract void Shoot(Vector3 direction);
    public void SetVelocity(float newSpeed)
    {
        speed = newSpeed;
    }
    public int GetRange() { return range; }
}
