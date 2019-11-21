using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    public AudioClip collectedClip;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        RubyController rubyController = collision.GetComponent<RubyController>();
        if (rubyController!=null) {
            if (rubyController.health < rubyController.maxHealth) {
                rubyController.ChangeHealth(1);
                rubyController.PlaySound(collectedClip);
                Destroy(gameObject);
            }
        }
        Debug.Log("collision occurs:"+collision.name);
    }
}
