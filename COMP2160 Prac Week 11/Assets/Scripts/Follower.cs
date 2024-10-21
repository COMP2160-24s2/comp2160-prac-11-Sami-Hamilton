using UnityEngine;

public class Follower : MonoBehaviour
{
    [SerializeField] private Transform target1;
    [SerializeField] private Transform target2;

    void LateUpdate()
    {
        Vector3 target1Pos = new Vector3(target1.position.x, this.transform.position.y, target1.position.z);
        Vector3 target2Pos = new Vector3(target2.position.x, this.transform.position.y, target2.position.z);
        Vector3 newPos = Vector3.Lerp(target1Pos, target2Pos, 0.5f);
        this.transform.position = newPos;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(this.transform.position, 0.5f);
    }
}