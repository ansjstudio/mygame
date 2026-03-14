using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 5f;
    private float speed = 0f;
    [SerializeField]
    [Range(0, 100)]
    private float maxSpeed = 100f;
    [SerializeField] private Material[] ballMaterials;
    private MeshRenderer ballRenderer;

    private bool movingRight = true;
    private static PlayerController instance;
    private Rigidbody rb;

    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<PlayerController>();
                if (instance == null) Debug.LogError("PlayerController not found"); ;
            }
            return instance;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        ballRenderer = GetComponent<MeshRenderer>();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        UpdateBallAppearance();
    }

    public void UpdateBallAppearance()
    {
        int selectedID = PlayerPrefs.GetInt("SelectedBall", 0);

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(i == selectedID);
        }

        ballRenderer = GetComponentInChildren<MeshRenderer>();

        if (ballRenderer != null && selectedID < ballMaterials.Length)
        {
            ballRenderer.material = ballMaterials[selectedID];
        }
    }
    void Update()
    {
        if (GameManager.Instance.isGameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ChangeDirection();
            }
            if(transform.position.y < -0.2)
            {
                GameManager.Instance.EndGame();
            }
        }

    }

    void FixedUpdate()
    {
        if (GameManager.Instance.isGameStarted)
        {
            MoveBall();
        }
    }

    void ChangeDirection()
    {
        movingRight = !movingRight;
    }
    void MoveBall()
    {

        if (speed < maxSpeed)
        {
            speed = baseSpeed + (GameManager.Instance.Score / 10) * 0.5f;
        }

        float currentY = rb.linearVelocity.y;

        if (currentY > 0) currentY = -1f;

        if (movingRight)
        {
            rb.linearVelocity= new Vector3(speed,currentY,0);
        }
        else
        {
            rb.linearVelocity = new Vector3(0, currentY, speed);
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Diamond")
        {
            GameManager.Instance.AddDiamond();
            Destroy(other.gameObject);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Platform")
        {
            GameManager.Instance.AddScore();
           
            Spawner.Instance.SpawnPlatform();
        }
    }
}