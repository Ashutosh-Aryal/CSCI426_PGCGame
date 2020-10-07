using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private const string HIGH_SCORE_TEXT = "High Score: ";
    private const string CURRENT_HEIGHT_TEXT = "Current Height: ";
    private const KeyCode RESTART_KEY = KeyCode.R;
    private const float X_LIMIT = 6.4f;

    [SerializeField] private GameObject firstGemObject;

    [SerializeField] private Text currentHeightText;

    [SerializeField] private Text highScoreText;
    private float currentHighScore = 0.0f;

    [SerializeField] private LayerMask layerMask;
    private Rigidbody2D rb2d;
    private BoxCollider2D box2d;
    private Vector3 mySpawnLocation;
    [SerializeField] private float moveSpeed; 
    [SerializeField] private float jumpVelocity;
    private bool canDoubleJump;

    [SerializeField] private SoundManager soundManager;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        box2d = GetComponent<BoxCollider2D>();
    }

    private void Start()
    {
        mySpawnLocation = gameObject.transform.position;
    }

    private void RestartGame()
    {
        gameObject.transform.position = mySpawnLocation;

        foreach (GameObject gem in RefresherRandomizer.s_SpawnedObjects)
        {
            Destroy(gem);
        }

        RefresherRandomizer.s_SpawnedObjects.Clear();

        firstGemObject.GetComponent<JumpRefresh>().ResetShouldSpawn();
    }

    void Update()
    {
        if(Input.GetKeyDown(RESTART_KEY))
        {
            RestartGame();
        }

        WrapXPosition();

        if (IsGrounded())
        {
            canDoubleJump = true;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (IsGrounded())
            {
                rb2d.velocity = Vector2.up * jumpVelocity;
                soundManager.PlaySound(SoundManager.Sound.jump);
            }
            else
            {
                if (canDoubleJump)
                {
                    rb2d.velocity = Vector2.up * jumpVelocity;
                    soundManager.PlaySound(SoundManager.Sound.jump);
                    canDoubleJump = false;
                }
            }
        }

        SetTexts();

        // Movement
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(x, y);
        Movement(direction);
    }

    private void SetTexts()
    {

        float currentHeight = gameObject.transform.position.y - mySpawnLocation.y;
        currentHeightText.text = CURRENT_HEIGHT_TEXT + Mathf.RoundToInt(currentHeight) + "m";
        if(currentHeight > currentHighScore)
        {
            currentHighScore = currentHeight;
        }

        highScoreText.text = HIGH_SCORE_TEXT + Mathf.RoundToInt(currentHighScore) + "m";
    }

    private void WrapXPosition()
    {
        Vector3 currentPosition = gameObject.transform.position;
        if (Mathf.Abs(currentPosition.x) >= Mathf.Abs(X_LIMIT))
        {
            currentPosition.x = (currentPosition.x >= X_LIMIT) ? -X_LIMIT : X_LIMIT;
        }

        gameObject.transform.position = currentPosition;
    }

    private bool IsGrounded()
    {
        RaycastHit2D ray = Physics2D.BoxCast(box2d.bounds.center, box2d.bounds.size, 0f, Vector2.down, 0.1f, layerMask);
        return ray.collider != null; 
    }

    private void Movement(Vector2 direction)
    {
        rb2d.velocity = new Vector2(direction.x * moveSpeed, rb2d.velocity.y);
    }

    public void JumpRefresh()
    {
        canDoubleJump = true;
        soundManager.PlaySound(SoundManager.Sound.pickup);
    }
}
