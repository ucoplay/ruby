﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    Rigidbody2D rdbody2D;

    AudioSource audioSource;
    public GameObject projectilePrefab;
    public float speed = 3.0f;
    public int maxHealth = 5;
    int currentHealth;
    public int health {get {return currentHealth;}}
    public AudioClip hitClip;
    public AudioClip cogClip;
    public AudioClip talkClip;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    Vector2 lookDirection = new Vector2(1,0);
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rdbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal,vertical);
        if (!Mathf.Approximately(move.x,0.0f)|| !Mathf.Approximately(move.y, 0.0f)) {
            lookDirection.Set(move.x,move.y);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X",lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed",move.magnitude);
        //Debug.Log(horizontal);
        Vector2 vector = rdbody2D.position;
        //vector.x += speed * horizontal*Time.deltaTime;
        //vector.y += speed * vertical*Time.deltaTime;
        vector += move * speed * Time.deltaTime;
        //transform.position = vector;
        rdbody2D.MovePosition(vector);
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer<0) {
                isInvincible = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.C)) {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            RaycastHit2D hit = Physics2D.Raycast(rdbody2D.position+Vector2.up*0.2f,lookDirection,1.5f,LayerMask.GetMask("NPC"));
            if (hit.collider!=null) {
               NonPlayerCharacter npc = hit.collider.GetComponent<NonPlayerCharacter>();
                if (npc!=null) {
                    npc.DisplayDialog();
                    audioSource.PlayOneShot(talkClip);
                }
            }
        }
        
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0) {
            if (isInvincible) {
                return;
            }
            animator.SetTrigger("Hit");
            isInvincible = true;
            invincibleTimer = timeInvincible;
            audioSource.PlayOneShot(hitClip);
        }
        currentHealth = Mathf.Clamp(currentHealth+amount,0,maxHealth);
        UIHealthBar.instance.SetValue(currentHealth/(float)maxHealth);
    }

    void Launch() {
        GameObject projectileObject = Instantiate(projectilePrefab,rdbody2D.position+Vector2.up*0.5f,Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection,300);
        animator.SetTrigger("Launch");
        audioSource.PlayOneShot(cogClip);
    }

    public void PlaySound(AudioClip audioClip) {
        audioSource.PlayOneShot(audioClip);
    }
}
