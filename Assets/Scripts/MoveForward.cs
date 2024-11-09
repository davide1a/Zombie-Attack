using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    [SerializeField] private float speed = 5;
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
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If a zombie is hit by a bullet, destroy the bullet
        if (collision.gameObject.CompareTag("Projectile"))
        {
            isDead = true;
            objectRb.AddForce(Vector3.back * -speed, ForceMode.Impulse);
            audioPlayer.PlayOneShot(zombieShot);
            collision.gameObject.SetActive(false);
            Debug.Log("Zombie killed");
            
            // Add score for different zombies and play their animation
            if (gameObject.name == "Zombie1(Clone)")
            {
                objectAnim.SetTrigger("Dead");
                gameManager.UpdateScore(zombie1score);
            }
            else if (gameObject.name == "Zombie2(Clone)")
            {
                objectAnim.SetFloat("Blend", -1);
                gameManager.UpdateScore(zombie2score);
            }
            else if (gameObject.name == "Zombie3(Clone)")
            {
                objectAnim.SetFloat("Blend", -1);
                gameManager.UpdateScore(zombie3score);
            }

            Destroy(gameObject, 1);
        }

        // If a zombie attacks the player
        if (collision.gameObject.CompareTag("Player"))
        {
            audioPlayer.PlayOneShot(zombieAttack);
            if (gameObject.name == "Zombie1(Clone)")
            {
                objectAnim.SetTrigger("Attack");
            }
            else if (gameObject.name == "Zombie2(Clone)")
            {
                objectAnim.SetFloat("Blend", 1);
                StartCoroutine(ResetAnim());
            }
        }

        // If a zombie attacks a bride
        if (collision.gameObject.CompareTag("Bride"))
        {
            audioPlayer.PlayOneShot(zombieAttack);
            if (gameObject.name == "Zombie1(Clone)")
            {
                objectAnim.SetTrigger("Attack");
            }
            else if (gameObject.name == "Zombie2(Clone)")
            {
                objectAnim.SetFloat("Blend", 1);
                StartCoroutine(ResetAnim());
            }
        }
    }

    IEnumerator ResetAnim()
    {
        yield return new WaitForSeconds(1);
        objectAnim.SetFloat("Blend", 0);
    }
}
