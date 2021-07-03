using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigEnemieController : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    //public GameObject AtackVFX;


    public float EnemieSpeed = 1f;
    public bool isFighting;

    public float selfDestructionTime = 10f;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

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
        if (entity.gameObject.tag == "Allies" && isFighting == false)
        {
            isFighting = true;
            entity.gameObject.GetComponent<AllieController>().isFighting = true;
        }

        if (entity.gameObject.tag == "Player")
        {
            if(entity.gameObject.GetComponent<PlayerController>().CHEAT_INMORTAL==false)
                entity.gameObject.GetComponent<PlayerController>().isAlive = false;
        }
    }
}
