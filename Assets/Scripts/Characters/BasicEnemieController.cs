using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemieController : MonoBehaviour
{
  
    Rigidbody2D rigidbody2D;
    Animator animator;

    public float selfDestructionTime = 5f;


    public float EnemieSpeed = 5f;
    public bool isFighting;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        

    }

    private void Awake()
    {
    }

    // Update is called once per frame
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
        float vertical = 0;

        if (isFighting)
            vertical = 0;
        else
            vertical = -1;

        Vector2 position = rigidbody2D.position;

        // position.x = position.x + EnemieSpeed * horizontal * Time.deltaTime;
        position.y = position.y + EnemieSpeed * vertical * Time.deltaTime;

        rigidbody2D.MovePosition(position);
    }

    private void OnCollisionEnter2D(Collision2D entity)
    {

        if (entity.gameObject.tag == "Allies" && isFighting ==false)
        {
            isFighting = true;
            entity.gameObject.GetComponent<AllieController>().isFighting = true;
            animator.SetBool("IsFighting", isFighting);
        }

        if (entity.gameObject.tag == "Player")
        {
            entity.gameObject.GetComponent<PlayerController>().isAlive = false;
        }
    }
}
