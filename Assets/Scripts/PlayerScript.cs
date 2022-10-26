using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public TextMeshProUGUI score;
    private int scoreValue;
    public TextMeshProUGUI lives;
    private int livesValue = 3;
    public GameObject winObjects;
    public GameObject loseObjects;
   
    public AudioClip winMusic;
    public AudioSource musicSource2;

    
    
    // Start is called before the first frame update
    void Start()
    {
       rd2d = GetComponent<Rigidbody2D>(); 
       score.text = scoreValue.ToString();
     
       winObjects.SetActive(false);
       loseObjects.SetActive(false);
       lives.text = "Lives:" + livesValue.ToString();
       score.text = "Score:" + scoreValue.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
       float hozMovement = Input.GetAxis("Horizontal");
       float vertMovement = Input.GetAxis("Vertical");
       rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); 
            }
        }

        if (collision.collider.tag == "base")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse); 
            }
        }
    } 


   private void OnCollisionEnter2D(Collision2D collision)
   {
     if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score:" + scoreValue.ToString();
            if (scoreValue == 4 && SceneManager.GetActiveScene().name != "Level 2")
            {
                SceneManager.LoadScene("Level 2");
                livesValue = 3;
            } 
            else
            {
                if (scoreValue == 4)
                {
                    winObjects.SetActive(true);
                }
            }
     
            Destroy(collision.collider.gameObject);
        }
    
    if (collision.collider.tag == "enemy")
        {
            livesValue = livesValue - 1;
            lives.text = "Lives:" + livesValue.ToString();
            if (livesValue < 0 && SceneManager.GetActiveScene().name != "Level 2")
            {
                SceneManager.LoadScene("Level 2");
                livesValue = 3;
            } 
            else
            {
                if (livesValue == 0)
                {
                    loseObjects.SetActive(true);
                }
            }
            Destroy(collision.collider.gameObject);
        }
    }  
} 
