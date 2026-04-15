using NodeCanvas.Tasks.Conditions;
using UnityEditor.Build;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{
    public string targetTag = "Player";
    public GameObject target;

    public bool hasCollided;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Update()
    {
      
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (target != null && other == target)
        {
            hasCollided = true;
            other.GetComponent<PlayerDeath>().Die();
            return;
        }
   
    }
}
