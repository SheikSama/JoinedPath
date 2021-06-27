using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllieController : MonoBehaviour
{

    public GameObject EntityToFollow;
    public Vector3 offstet;
    Rigidbody2D rigidbody2D;
    Animator animator;

    public AudioSource AudioSource;
    public AudioClip boing;

    public GameObject bulletOrigin;
    public GameObject Bullet;

    bool isChangingPosition = false;
    public string TargetName = "";

    public bool isFighting = false;
    public bool IsFigthAnimation = false;

    GameObject FormationPosition { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        offstet = this.transform.transform.position - EntityToFollow.transform.position;
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {


    }

    private void FixedUpdate()
    {
        if (!isFighting)
        {
            if (!isChangingPosition)
                rigidbody2D.MovePosition(EntityToFollow.transform.position + offstet);
        }
        else
        {
            if (IsFigthAnimation)
            {
                animator.SetBool("IsFighting", isFighting);
                this.rigidbody2D.velocity = Vector2.zero;
            }
            
        }
    }

    public void ChangeFormation(GameObject targt,float TimeToLerp)
    {
        StartCoroutine(LerpToPoint(targt.transform.position, TimeToLerp));
    }

    private IEnumerator LerpToPoint(Vector2 destination, float lerpRate)
    {
        if (!isFighting)
        {
            var timeStep = 0.0f;
            var startPoint = transform.position;
            while (timeStep < 1.0f)
            {
                isChangingPosition = true;
                timeStep += Time.deltaTime / lerpRate;
                transform.position = Vector2.Lerp(startPoint, destination, timeStep);
                yield return null;
            }
            isChangingPosition = false;
            EntityToFollow.GetComponent<PlayerController>().changingFormation = false;
            offstet = this.transform.transform.position - EntityToFollow.transform.position;

        }

    }

    public void ShootBullet()
    {
        Vector3 pos = bulletOrigin.transform.position;
        Quaternion rot = bulletOrigin.transform.rotation;
        Instantiate(Bullet, pos, rot);
    }

    private void OnCollisionEnter2D(Collision2D entity)
    {

        if (entity.gameObject.tag == "Enemies")
        {
            AudioSource.PlayOneShot(AudioSource.clip);
            //isFighting = true;
        }else if (entity.gameObject.tag == "Obstacles")
        {
           // AudioSource.volume = ;
            AudioSource.PlayOneShot(boing);
            isFighting = true;
            animator.SetBool("HitWall", true);
        }
    }
}
