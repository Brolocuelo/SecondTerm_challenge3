using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;

    public float floatForce;
    private float gravityModifier = 1f;
    private Rigidbody playerRb;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;

    private float counter;


    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();

        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 5, ForceMode.Impulse);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !gameOver)
        {
            playerRb.AddForce(Vector3.up * floatForce , ForceMode.Impulse);
        }
    }

    private void GameOver()
    {
        gameOver = true;
        explosionParticle.Play();
        playerAudio.PlayOneShot(explodeSound, 1);
        Debug.Log("Game Over!");

    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Bomb")|| other.gameObject.CompareTag("Ground"))
        {
            Destroy(other.gameObject);
            GameOver();
            if (!gameOver)
            {
                explosionParticle.Stop();
            }
            if (other.gameObject.CompareTag("Bomb") && other.gameObject.CompareTag("Ground"))
            {
               
            }
        }

        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1);
            Destroy(other.gameObject);
            counter ++;
            Debug.Log(string.Format("You have {0} coins",counter));
        }
    }

}
