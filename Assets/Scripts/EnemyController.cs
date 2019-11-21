using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

    public float speed = 1.0f;
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    public AudioClip fixedClip;
    AudioSource audioSource;

    Rigidbody2D rigBody2D;
    float timer;
    int direction = 1;
    Animator animator;

    bool broken = true;

    // Start is called before the first frame update
    void Start()
    {
        rigBody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!broken) return;
        timer -= Time.deltaTime;
        if (timer < 0) {
            direction = -direction;
            timer = changeTime;
        }
        Vector2 vector = rigBody2D.position;
        if (vertical){
            vector.y += speed * Time.deltaTime*direction;
            animator.SetFloat("Move X",0);
            animator.SetFloat("Move Y", direction);
        }else {
            vector.x -= speed * Time.deltaTime * direction;
            animator.SetFloat("Move X",-direction);
            animator.SetFloat("Move Y",0);
        }
        
        rigBody2D.MovePosition(vector);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        RubyController rubyController = collision.gameObject.GetComponent<RubyController>();
        if (rubyController!=null) {
            rubyController.ChangeHealth(-1);
        }
    }

    public void Fix()
    {
        broken = false;
        audioSource.PlayOneShot(fixedClip);
        rigBody2D.simulated = false;
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
    }
}
