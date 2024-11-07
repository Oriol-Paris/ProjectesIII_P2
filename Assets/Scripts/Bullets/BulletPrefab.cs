using UnityEngine;

public abstract class BulletPrefab : MonoBehaviour
{
    [SerializeField] public int range;
    [SerializeField] public float speed;
    [SerializeField] public float damage;
    [SerializeField] public bool isFromPlayer;

    private void OnBecameInvisible()
    {
        //Destroy(this);
    }

    public void SetFromPlayer(bool val) { isFromPlayer = val; }
}
