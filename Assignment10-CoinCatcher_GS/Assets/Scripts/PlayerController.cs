using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 3;
    public float jumpForce = 20;
    public GameObject player;
    public GameObject respawnPoint;
    Animator characterAnimation;
    public SpriteRenderer characterSprite;


    // Start is called before the first frame update
    void Start()
    {
        characterAnimation = GetComponentInChildren<Animator>();
        //characterSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");

        characterAnimation.SetFloat("speed", Mathf.Abs(h));

        characterSprite.flipX = h < 0;

        //transform.Translate(Vector3.right * h * Time.deltaTime * speed);

        Rigidbody2D rb = GetComponent<Rigidbody2D>();

        Vector3 vel = new Vector3();

        vel.x = h * speed;                      //Sets the velocity of the vector with the direction of x times the speed
        vel.y = rb.velocity.y;                  //Sets the velovity of y to the rigidbody's velocity of y
        vel.z = 0;                              //Sets velocity of z to zero 


        if (Input.GetButtonDown("Jump"))
        {
            //rb.AddForce(Vector2.up * jumpForce);
            vel.y = jumpForce;
        }

        rb.velocity = vel;                      //rigidbody velocity set to velocity we assigned
       
        if(GameManager.instance.lives <= 0)
        {
            characterSprite.forceRenderingOff = true;                               //I wasn't sure how to destroy the player and start the coroutine at once for the next scene          
            StartCoroutine("WaitTwoSeconds");
            
        
        }   
        if(GameManager.instance.coinsCtr == GameManager.instance.maxNumOfCoins)         //Once the player has collected all the coins, the "Victory" scene pops up
        {
            SceneManager.LoadScene("Victory");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Destroy")                   //If collision is has "Destroy" tag, the coin object will destroy and add to the coin counter
        {
            SoundManager.instance.PlaySound(0);
            Destroy(collision.gameObject);
            GameManager.instance.coinsCtr++;
        }
        
        if(collision.gameObject.tag == "Killzone")                  //If the collision tag is "Killzone", the player loses a life and respawns to respawn point
        {
            SoundManager.instance.PlaySound(1);                         //Plays death sound
                GameManager.instance.lives--;
                player.transform.position = respawnPoint.transform.position;                                 
        }
       
    }
    IEnumerator WaitTwoSeconds()
    {
        
        yield return new WaitForSecondsRealtime(1);
        SceneManager.LoadScene("GameOver");
        
    }
    
}
