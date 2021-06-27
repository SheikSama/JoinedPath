using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public AudioSource audioSource;

    public AudioClip marchingSound;
    public AudioClip Death;

    public float playerSpeed = 10f;


    Rigidbody2D rigidbody2D;

    public Canvas deadCanvas;
    public Canvas CDCanvas;
    public Canvas CinematicCanvas;
    
    public GameObject CinematicPoint;
    public GameObject FireIndicator;


    public bool isAlive = true;
    public bool endGame = false;
    public bool ZeroUntilMidnight = false;


    public int alliesAlive { 
        get
        {
            int count = 0;

            foreach (var allie in allies)
            {
                AllieController allieController = allie.gameObject.GetComponent<AllieController>();
                if (!allieController.isFighting)
                    count++;
            }

            return count;
        }
    }

    public List<GameObject> allies;
    public List<GameObject> PosForm1;
    public List<GameObject> PosForm2;
    public List<GameObject> RighForm;
    public List<GameObject> LeftForm;
    public List<bool> PosAudit;


    public bool changingFormation = false;
    public float timeToChangeFormation = 1f;


    //skills
    public FormationTypes currentFormation = FormationTypes.LineFormation;
    public float LineFormDuration = 3f;
    public float UltimateCD = 25f;
    private float currentUltimateCD = 25f;

    bool isUltimateUsed = false;


    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ZeroUntilMidnight)
        {
            if (!isAlive)
                ZeroMinutesTillMidnight();                

            if (isUltimateUsed)
            {
                currentUltimateCD -= Time.deltaTime;

                if (currentUltimateCD <= 0.0f)
                {
                    currentUltimateCD = UltimateCD;
                    isUltimateUsed = false;
                    FireIndicator.SetActive(true);
                }
            }

            if (changingFormation == false)
            {

                if (alliesAlive > 0)
                {
                    if (Input.GetKeyDown(KeyCode.Joystick1Button3) || Input.GetKeyDown(KeyCode.Space))
                    {
                        if (currentFormation != FormationTypes.ColumnFormation)
                        {
                            audioSource.PlayOneShot(audioSource.clip);
                            MoveFormation2();

                        }

                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick1Button2) || Input.GetKeyDown(KeyCode.E))
                    {
                        if (currentFormation != FormationTypes.RightFormation && currentFormation != FormationTypes.ColumnFormation)
                        {
                            audioSource.PlayOneShot(audioSource.clip);
                            MoveRightFormation();

                        }
                    }
                    else if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Q))
                    {
                        if (currentFormation != FormationTypes.LeftFormation || currentFormation != FormationTypes.ColumnFormation)
                        {
                            audioSource.PlayOneShot(audioSource.clip);
                            MoveLeftFormation();
                        }
                    }

                    if (Input.GetKeyDown(KeyCode.Joystick1Button5) || Input.GetKeyDown(KeyCode.R))
                    {
                        if (!isUltimateUsed && currentFormation != FormationTypes.ColumnFormation)
                            ShootUlt();
                    }
                }
            }
        }
        else
        {
            EndingScene();
        }
        
    }

    private void FixedUpdate()
    {
        if (!changingFormation && !ZeroUntilMidnight)
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = 1;


            Vector2 position = rigidbody2D.position;

            position.x = position.x + playerSpeed * horizontal * Time.deltaTime;
            position.y = position.y + playerSpeed * vertical * Time.deltaTime;

            rigidbody2D.MovePosition(position);
        }

    }


    private void MoveFormation1()
    {
        audioSource.PlayOneShot(audioSource.clip);

        currentFormation = FormationTypes.LineFormation;
        if (PosAudit.Count > 0)
            PosAudit.Clear();

        foreach (var item in PosForm1)
            PosAudit.Add(false);


        changingFormation = true;
        allies.ForEach(allie =>
        {
            GameObject closestPoint = GetClosestPoint(PosForm1);
            if(closestPoint)
                allie.GetComponent<AllieController>().ChangeFormation(closestPoint, timeToChangeFormation);
            
        });

        
        
    }


    private void MoveFormation2()
    {

        currentFormation = FormationTypes.ColumnFormation;
        if (PosAudit.Count > 0)
            PosAudit.Clear();

        foreach (var item in PosForm2)
            PosAudit.Add(false);

        changingFormation = true;
        allies.ForEach(allie =>
        {
            GameObject closestPoint = GetClosestPoint(PosForm2);
            if (closestPoint)
                allie.GetComponent<AllieController>().ChangeFormation(closestPoint, timeToChangeFormation);

        });
        StartCoroutine(BackForm1());
    }

    private void MoveRightFormation()
    {


        currentFormation = FormationTypes.RightFormation;
        if (PosAudit.Count > 0)
            PosAudit.Clear();

        foreach (var item in RighForm)
            PosAudit.Add(false);

        changingFormation = true;
        allies.ForEach(allie =>
        {
            GameObject closestPoint = GetClosestPoint(RighForm);
            if (closestPoint)
                allie.GetComponent<AllieController>().ChangeFormation(closestPoint, timeToChangeFormation);

        });
    }
    private void ShootUlt()
    {
        isUltimateUsed = true;
        this.FireIndicator.SetActive(false);
        allies.ForEach(allie =>
        {

            AllieController allieController = allie.GetComponent<AllieController>();
            if(!allieController.isFighting)
                allieController.ShootBullet();

        });
    }

    private void MoveLeftFormation()
    {

        audioSource.PlayOneShot(audioSource.clip);

        currentFormation = FormationTypes.LeftFormation;
        if (PosAudit.Count > 0)
            PosAudit.Clear();

        foreach (var item in LeftForm)
            PosAudit.Add(false);

        changingFormation = true;
        allies.ForEach(allie =>
        {
            GameObject closestPoint = GetClosestPoint(LeftForm);
            if (closestPoint)
                allie.GetComponent<AllieController>().ChangeFormation(closestPoint, timeToChangeFormation);

        });
    }



    GameObject GetClosestPoint(List<GameObject> points)
    {
       

        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject point in points)
        {
            if (PosAudit[points.IndexOf(point)]== false) 
            {
                float dist = Vector3.Distance(point.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = point;
                    minDist = dist;

                    
                }
            }
        }
        PosAudit[points.IndexOf(tMin)] = true;
        return tMin;
    }

    public void ZeroMinutesTillMidnight()
    {
        if (!endGame)
        {
            audioSource.PlayOneShot(Death);
            endGame = true;
            Time.timeScale = 0;
            deadCanvas.gameObject.SetActive(true);
        }
       
    }

    public void EndingScene()
    {
        StartCoroutine(LerpToPoint(CinematicPoint.transform.position,3f));
        GameObject mainCamera = GameObject.Find("Main Camera");
        if (mainCamera)
        {
            mainCamera.GetComponent<SpawnerManager>().stopSpawns = true;
        }
        MoveFormation1();
    }

    private IEnumerator LerpToPoint(Vector2 destination, float lerpRate)
    {

            var timeStep = 0.0f;
            var startPoint = transform.position;
            while (timeStep < 1.0f)
            {
                timeStep += Time.deltaTime / lerpRate;
                transform.position = Vector2.Lerp(startPoint, destination, timeStep);
                yield return null;
            }

        ShowCinematicCanvas();
    }

    public void ShowCinematicCanvas()
    {
        CinematicCanvas.gameObject.SetActive(true);
    }

    private IEnumerator BackForm1 ()
    {
        var timeStep = LineFormDuration;
        CDCanvas.GetComponent<ProgressBarUIController>().decreasSliderProgress(LineFormDuration);

        while (timeStep > 0f)
            {
                timeStep -= Time.deltaTime;
                var scaledValue = timeStep / LineFormDuration;

                yield return null;
            }
        MoveFormation1();

    }

}
