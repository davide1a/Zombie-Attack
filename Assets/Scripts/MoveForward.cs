using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    private float zBound = 12;
    private Rigidbody objectRb;
    private bool isDead = false;
    private Animator objectAnim;
    private GameManager gameManager;
    private AudioSource audioPlayer;
    [SerializeField] AudioClip zombieShot;
    [SerializeField] AudioClip[] zombieWalking;
    [SerializeField] AudioClip zombieAttack;

    int zombie1score = 10;
    int zombie2score = 25;
    int zombie3score = 50;

    // Start is called before the first frame update
    void Start()
    {
        objectRb = GetComponent<Rigidbody>();
        objectAnim = GetComponent<Animator>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        audioPlayer = GetComponent<AudioSource>();
        int randomIndex = Random.Range(0, zombieWalking.Length);
        audioPlayer.PlayOneShot(zombieWalking[randomIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the object forwards if it has not been shot
        if (!isDead)
        {
        objectRb.AddForce(Vector3.forward * -speed);
        }

        // Remove object from scene when going off screen
        if (transform.position.z < -zBound || transform.position.z > zBound)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If a zombie is hit by a bullet, destroy the bullet
        if (collision.gameObject.CompareTag("Projectile"))
        {
            isDead = true;
            objectRb.AddForce(Vector3.back * -speed, ForceMode.Impulse);
            audioPlayer.PlayOneShot(zombieShot);
            objectAnim.SetTrigger("Dead");
            objectAnim.SetFloat("Blend", -1);
            Destroy(collision.gameObject);
            Debug.Log("Zombie killed");
            
            // Add score for different zombies
            if (gameObject.name == "Zombie1(Clone)")
            {
                gameManager.UpdateScore(zombie1score);
            }
            else if (gameObject.name == "Zombie2(Clone)")
            {
                gameManager.UpdateScore(zombie2score);
            }
            else if (gameObject.name == "Zombie3(Clone)")
            {
                gameManager.UpdateScore(zombie3score);
            }

            Destroy(gameObject, 1);
        }

        // If a bullet hits a bride, kill them
        if (gameObject.CompareTag("Projectile") && collision.gameObject.CompareTag("Bride"))
        {
            Destroy(gameObject);
        }

        // If a zombie attacks the player
        if (collision.gameObject.CompareTag("Player"))
        {
            objectAnim.SetTrigger("Attack");
            objectAnim.SetFloat("Blend", 1);
            audioPlayer.PlayOneShot(zombieAttack);
            StartCoroutine(ResetAnim());
        }

        // If a zombie attacks a bride
        if (collision.gameObject.CompareTag("Bride"))
        {
            objectAnim.SetTrigger("Attack");
            objectAnim.SetFloat("Blend", 1);
            audioPlayer.PlayOneShot(zombieAttack);
            StartCoroutine(ResetAnim());
        }
    }

    IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(1);
        objectAnim.SetFloat("Blend", 0);
    }
}
