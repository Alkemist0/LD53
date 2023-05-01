using UnityEngine;
using UnityEngine.Animations;

public class Bounce : MonoBehaviour
{
    Animator animator;
    AudioSource boingSound;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boingSound = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("Bounce");
            boingSound.Play();
        }
    }
}
