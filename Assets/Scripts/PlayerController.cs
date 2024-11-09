using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    [SerializeField] private float speed = 15;
    private float xBound = 12;
    private float zBound = 7;
    public GameObject projectile;
    private Animator playerAnim; 
    private bool isDead = false;
    [SerializeField] private AudioSource playerAudio;
    [SerializeField] private AudioClip collectibleSound;
    [SerializeField] private AudioClip gunshot;
    [SerializeField] private AudioClip dying;
    [SerializeField] private AudioClip rescue;
    private GameManager gameManager;
    int brideSavedScore = 50;
    int bulletsCollected = 10;


    // Start is called before the first frame update
    void Start()
    {
        // Get the rigid body component of the Player
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // As long as player is alive
        if (!isDead)
        {
            // Call the move player function 
            MovePlayer();

            // Shoot a bullet when space bar is pressed
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (gameManager.bulletCount > 0)
                {
                    ShootProjectile();
                }
            }
        }
    }

    void MovePlayer()
    {
        // Get player input and move player
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(Vector3.forward * speed * verticalInput);
        playerRb.AddForce(Vector3.right * speed * horizontalInput);

        // Animate player when moving
        if (verticalInput != 0 || horizontalInput != 0)
        {
            playerAnim.SetFloat("Blend", 1);
        } else {
            playerAnim.SetFloat("Blend", 0);
        }

        // Constrain player within bounds
        if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        }
        else if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);
        }
        else if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If player gets hit by a zombie, they die
        if (collision.gameObject.CompareTag("Zombie"))
        {
            isDead = true;
            playerAnim.SetFloat("Blend", -1);
            playerAudio.PlayOneShot(dying);
            Debug.Log("Player eaten!");
            gameManager.GameOver();
        }
        // If player touches a bride, they save her
        if (collision.gameObject.CompareTag("Bride"))
        {
            Destroy(collision.gameObject);
            playerAudio.PlayOneShot(rescue);
            Debug.Log("Bride Saved!");
            gameManager.UpdateScore(brideSavedScore);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When player collects bullets
        if (other.gameObject.CompareTag("Bullet Collectible"))
        {
            playerAudio.PlayOneShot(collectibleSound);
            Destroy(other.transform.parent.gameObject);
            Debug.Log("Bullets Collected");
            gameManager.UpdateBullets(bulletsCollected);
        }
    }

    private void ShootProjectile()
    {
        playerAnim.SetFloat("Blend", 3);
        Vector3 spawnPos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z + 1f);
        Instantiate(projectile, spawnPos, projectile.transform.rotation);
        playerAudio.PlayOneShot(gunshot);
        gameManager.UpdateBullets(-1);
    }
}
