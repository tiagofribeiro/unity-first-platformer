using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerScripts
{
    public class PlayerLife : MonoBehaviour
    {
        private Rigidbody2D rb;
        private Animator animator;
        private PlayerMovement pm;
        private SpriteRenderer sr;

        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
            pm = GetComponent<PlayerMovement>();
            sr = GetComponent<SpriteRenderer>();
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Trap"))
            {
                pm.CanMove = false;
                sr.sortingLayerName = "Front";
                Die();
            }
        }

        private void Die()
        {
            rb.velocity = new Vector3(-5, 14f, 0);
            GetComponent<Collider2D>().enabled = false;
            animator.SetTrigger("death");
        }

        private void RestartLevel()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}