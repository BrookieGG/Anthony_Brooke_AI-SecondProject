using UnityEngine;

public class CollisionCheckDoor : MonoBehaviour
{
    public GameObject target;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {

    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider collision)
    {
        GameObject other = collision.gameObject;
        if (target != null && other == target)
        {
            other.GetComponent<PlayerDeath>().Win();
        }

    }
}
