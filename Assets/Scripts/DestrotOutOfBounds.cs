using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrotOutOfBounds : MonoBehaviour
{
    private float zBound = 12;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Remove object from scene when going off screen
        if (transform.position.z < -zBound || transform.position.z > zBound)
        {
            // Deactivate game object
            if (gameObject.CompareTag("Projectile"))
            {
                gameObject.SetActive(false);
            }
            else if (gameObject.CompareTag("Zombie"))
            {
                Destroy(gameObject);
            }
        }
    }
}
