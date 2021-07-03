using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalEnemieController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    Animator animator;

    public float EnemieSpeed = 5f;
    public bool isFighting =false;

    int direction = 0;
    bool ischangedDirection = false;

    public float selfDestructionTime = 4f;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetInteger("Direction", direction);

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
        float horizontal = 0;
        float vertical;

        if (isFighting)
        {
            horizontal = 0;
            vertical = 0;
        }
        else
        {
            horizontal = direction;
            vertical = -1;
        }

        Vector2 position = rigidbody2D.position;

        if(ischangedDirection)
            position.y = position.y + EnemieSpeed * vertical * Time.deltaTime;

        else
            position.x = position.x + EnemieSpeed * horizontal * Time.deltaTime;

        rigidbody2D.MovePosition(position);
    }

    public void SetDirection(int dir)
    {
        direction = dir;
    }

    private void OnCollisionEnter2D(Collision2D entity)
    {

        if (entity.gameObject.tag == "Allies" && isFighting == false)
        {
            isFighting = true;
            entity.gameObject.GetComponent<AllieController>().isFighting = true;
            animator.SetBool("IsFighting", isFighting);
        }

        if (entity.gameObject.tag == "Enemies")
        {
            ischangedDirection = true;
            animator.SetBool("GoingDown", ischangedDirection);

        }

        if (entity.gameObject.tag == "Player")
        {
            if (entity.gameObject.GetComponent<PlayerController>().CHEAT_INMORTAL == false)
            {
                entity.gameObject.GetComponent<PlayerController>().isAlive = false;
                animator.SetBool("IsFighting", isFighting);
            }
               
        }
    }
}
