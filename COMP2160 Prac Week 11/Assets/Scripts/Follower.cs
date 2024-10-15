using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform target;

    void LateUpdate()
    {
        Vector3 newPos = new Vector3(target.position.x, this.transform.position.y, target.position.z);
        this.transform.position = newPos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, 0.5f);
    }
}