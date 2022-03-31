using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Vector3 targetPosition;
    void Start()
    {
        
    }

    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);
    }   
}
