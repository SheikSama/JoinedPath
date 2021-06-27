using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControler : MonoBehaviour
{

    AudioSource audioSource;

    public AudioClip explosionClip;

    Rigidbody2D rigidbody2D;
    Animator animator;
    public float bulletSpeed = 25f;

    public float selfDestructionTime = 8f;

    bool hittedEnemie = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }

    void Update()
    {
        selfDestructionTime -= Time.deltaTime;

        if (selfDestructionTime <= 0.0f)
        {
            Destroy(this.gameObject);
        }
    }


    private void FixedUpdate()
    {
        if (!hittedEnemie)
        {
            float vertical = 1;
            Vector2 position = rigidbody2D.position;

            position.y = position.y + bulletSpeed * vertical * Time.deltaTime;

            rigidbody2D.MovePosition(position);
        }
            
    }

    private void OnCollisionEnter2D(Collision2D entity)
    {

        if (entity.gameObject.tag == "Allies" || entity.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }

        hittedEnemie = true;
        animator.SetBool("HittedEnemie", hittedEnemie);
        audioSource.PlayOneShot(explosionClip);
        rigidbody2D.bodyType = RigidbodyType2D.Static;

        if (entity.gameObject.tag == "Enemies")
        {
            Destroy(entity.gameObject);
            Destroy(this.gameObject);
        }

      //  Destroy(gameObject, this.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
    }

}
