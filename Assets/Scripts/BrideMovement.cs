using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrideMovement : MonoBehaviour
{
    public Rigidbody brideRb;
    private GameObject player;
    private Animator brideAnim;
    [SerializeField] private float speed = 15;
    private float xBound = 12;
    private float zBound = 7;
    private bool isDead = false;
    private GameManager gameManager;
    int brideShotScore = -150;
    int brideKilledScore = -100;
    private AudioSource brideAudio;
    [SerializeField] AudioClip brideDying;

    // Start is called before the first frame update
    void Start()
    {
        brideRb = GetComponent<Rigidbody>();
        brideAnim = GetComponent<Animator>();
        player = GameObject.Find("Player");
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        brideAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            MoveBride();
        }
    }

    void MoveBride()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        brideRb.AddForce(lookDirection * speed);
        brideAnim.SetFloat("Blend", 1);

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
        if (collision.gameObject.CompareTag("Zombie"))
        {
            isDead = true;
            brideAnim.SetFloat("Blend", -1);
            brideAudio.PlayOneShot(brideDying);
            Debug.Log("Bride Eaten!");
            gameManager.UpdateScore(brideKilledScore);
            Destroy(gameObject, 1);
        }
        else if (collision.gameObject.CompareTag("Projectile"))
        {
            isDead = true;
            brideAnim.SetFloat("Blend", -1);
            brideAudio.PlayOneShot(brideDying);
            Debug.Log("Bride Shot!");
            gameManager.UpdateScore(brideShotScore);
            Destroy(gameObject, 1);
        }
    }
}
