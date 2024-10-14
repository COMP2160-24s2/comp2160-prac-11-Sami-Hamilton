using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform target;

    void LateUpdate()
    {
        this.transform.position = target.position;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, 0.5f);
    }
}