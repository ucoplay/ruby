using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch(Vector2 direction,float force)
    {
        rigidbody2D.AddForce(direction*force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Projectile Collision with "+ collision.gameObject.name);
        Destroy(gameObject);
    }
}
