using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovePlayer : MonoBehaviour
{
    public static MovePlayer instance;

    [SerializeField] private string draggingTag;
    [SerializeField] private Camera cam;

    private Vector3 dis;
    private float posY;

    private bool touched = false;
    private bool dragging = false;

    private Transform toDrag;
    private Rigidbody toDragRigidbody;
    private Vector3 previousPosition;

    [SerializeField] private GameObject GameOVER;
    [SerializeField] private GameObject VFX;
    private int score;
    [SerializeField] private Text scoreText;

    [SerializeField] private bool NoMinusScore;
    [SerializeField] private bool TouchScreen;

    private GameObject[] Steps;
    StepPlacer scriptStepPlacer;

    private void Awake()
    {
        instance = this;
        SCORE = PlayerPrefs.GetInt("SCORE");
        StartCoroutine(MinusScore());
    }
    private void Start()
    {
        scriptStepPlacer = GetComponent<StepPlacer>();
    }
    public int SCORE
    {
        get
        {
            return score;
        }
        set
        {
            if (value >= 0)
                score = value;
        }
    }
    void FixedUpdate()
    {
        scoreText.text = Convert.ToString(score);
        if (Input.touchCount != 1 || score == 0)
        {
            dragging = false;
            touched = false;
            if (toDragRigidbody)
            {
                SetFreeProperties(toDragRigidbody);
            }
            Steps = GameObject.FindGameObjectsWithTag("Step");

            for (int i = 0; i < Steps.Length; ++i)
                Steps[i].gameObject.SetActive(false);
            return;
        }

        Touch touch = Input.touches[0];
        if (touch.phase == TouchPhase.Began)
        {
            StartTouch();
        }

        if (touched && touch.phase == TouchPhase.Moved)
        {
            TouchMoved();
        }

        if (dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            TouchEnd();
        }
    }
    private void StartTouch()
    {
            toDrag = this.gameObject.transform;
            previousPosition = toDrag.position;
            toDragRigidbody = toDrag.GetComponent<Rigidbody>();

            dis = cam.WorldToScreenPoint(previousPosition);
            posY = Input.GetTouch(0).position.y - dis.y;

            SetDraggingProperties(toDragRigidbody);

            touched = true;   
    }
    private void TouchMoved()
    {
        dragging = true;
        float posYNow = Input.GetTouch(0).position.y - posY;
        Vector3 curPos = new Vector3(dis.x, posYNow, dis.z);

        Vector3 worldPos = cam.ScreenToWorldPoint(curPos) - previousPosition;
        worldPos = new Vector3(worldPos.x, worldPos.y, 0.0f);

        toDragRigidbody.velocity = worldPos / (Time.deltaTime * 10);

        previousPosition = toDrag.position;

    }
    private void TouchEnd()
    {
        dragging = false;
        touched = false;
        previousPosition = new Vector3(0.0f, 0.0f, 0.0f);
        SetFreeProperties(toDragRigidbody);
    }
    private void SetDraggingProperties(Rigidbody rb)
    {
        rb.useGravity = false;
        rb.drag = 20;
        VFX.gameObject.SetActive(true);
        TouchScreen = true;
    }

    private void SetFreeProperties(Rigidbody rb)
    {
        rb.useGravity = true;
        rb.drag = 5;
        TouchScreen = false;
        scriptStepPlacer.enabled = false;
        VFX.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Player") && other.CompareTag("Enemy"))
        {
            GameOVER.SetActive(true);
            Destroy(gameObject);
            Time.timeScale = 0f;
        }
        if (CompareTag("Player") && other.CompareTag("Bonus"))
        {
            SCORE += Score.instance.IndexScore;
            scriptStepPlacer.enabled = true;
        }
        if (CompareTag("Player") && other.CompareTag("Finish"))
        {
            PlayerPrefs.SetInt("SCORE", SCORE);
        }
        if (CompareTag("Player") && other.CompareTag("Ground"))
        {
            NoMinusScore = false;
        }
    }
    public IEnumerator MinusScore()
    {
        while(true)
        {
            if (NoMinusScore == false && TouchScreen == true)
            {
                SCORE--;
                scriptStepPlacer.enabled = true;
                Steps = GameObject.FindGameObjectsWithTag("Step");
                for (int i = 0; i < Steps.Length; ++i)
                    Steps[i].gameObject.SetActive(true);
            }
            yield return new WaitForSecondsRealtime(0.1f);
        }
    }
}

