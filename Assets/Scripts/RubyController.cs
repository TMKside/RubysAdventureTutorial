using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;

    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public GameObject projectilePrefab;

    public AudioClip Swanging;
    public AudioClip rubyHit;
    public AudioClip croak;

    int currentHealth;
    public int health { get { return currentHealth; } }
    public int score;
    public TextMeshProUGUI scoreText;

    public GameObject screenLose;
    public GameObject screenWin;
    bool playerLost;
    bool playerWon;

    bool isInvincible;
    float invincibleTimer;

    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    public Animator animator;
    public Vector2 lookDirection = new Vector2(1,0);
    public ParticleSystem hitEffect;
    public ParticleSystem healthEffect;

    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        audioSource= GetComponent<AudioSource>();

        playerLost = false;
        playerWon = false;
    }

    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector2 move = new Vector2(horizontal, vertical);

        if(!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }
        
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        if(Input.GetKeyDown(KeyCode.C))
        {
            Launch();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                if (character != null)
                {
                    character.DisplayDialog();
                    PlaySound(croak);
                }
            }
        }

        if (currentHealth == 0)
        {
            screenLose.SetActive(true);
            playerLost = true;
            speed = 0.0f;

        }

        if (Input.GetKey(KeyCode.R))
        {
            if (playerLost == true)
            {
                speed = 3.0f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reloads Scene
            }
        }

        if (score == 2)
        {
            screenWin.SetActive(true);
        }
    }

    void FixedUpdate()
    {
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;

        rigidbody2d.MovePosition(position);
    }

    public void ChangeHealth(int amount)
    {
        if (playerLost == true)
        {
            return;
        }
        
        if (amount < 0)
        {
            if (isInvincible)
                return;
            isInvincible = true;
            invincibleTimer = timeInvincible;
            hitEffect.Play();

            PlaySound(rubyHit);
        }

        if (amount > 0)
        {
            healthEffect.Play();
        }

        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);
    }

    public void changeScore(int amount)
    {
        score = score + amount;
        scoreText.text = score.ToString();
        Debug.Log("New Score: " + score);
    }

    void Launch()
    {
        animator.SetTrigger("Launch");
        PlaySound(Swanging);

        //For the old gear throw
        /*GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 500);

        animator.SetTrigger("Launch");

        PlaySound(gearThrow);*/
    }
}