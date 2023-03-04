using UnityEngine.UI;
using UnityEngine;

public class Player : MonoBehaviour{

    Rigidbody2D rb;
    Animator an;

    public Score score;
    public Transform RespawnPlayerPos;
    public Transform SpawnPlayerPos;
    public int PlayerMaxHealth = 10;
    public float Ypos;
    public float JumpForce = 10f;
    private float movement;
    private bool IsGround = true;
    private bool FacingRight = true;
    public float Speed = 6f;
    private int CurrentGems=0;
    public float Acceleretion;
    [HideInInspector] public bool GameActive = true;
    public GameObject Cam;
    public float DistanceBetween;
    // Start is called before the first frame update
    void Start() {
        rb = gameObject.GetComponent<Rigidbody2D>();
        an = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {

        Vector2 CurrentPlayerPos = new Vector2(rb.position.x, Cam.transform.position.y);
        if (Vector2.Distance(CurrentPlayerPos, Cam.transform.position) >=DistanceBetween) {
            GameActive = false;
        }

        if (PlayerMaxHealth <= 0){
            GameActive = false;
        }

        if (!GameActive)
            return;

        transform.Translate(Vector2.right * Speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.Space) && IsGround) { //jump handlings - tbc to joystick or button
            an.SetTrigger("jump");
            Jump();
            IsGround = false;
        } 

        if (transform.position.y <= -Ypos) { // player goes out of screen
            GameOverScene();
            RespawnPlayer();
        }        
    }

    void RespawnPlayer() {
        Debug.Log("Position Reset!");
        transform.position = RespawnPlayerPos.position;
    }

    private void FixedUpdate(){ //contanst mption
        Speed += Acceleretion;
    }

    void PlayerAnimation() { 
    //run, damage, die,jump
        
        if (Mathf.Abs(movement) < .1f) { 
            an.SetFloat("run", 0f);
        }
        else if (Mathf.Abs(movement)>.1f){
            an.SetFloat("run", 1f);
        }
    }

    void Flip() { 

        if (movement<0f && FacingRight) {
            GetComponent<SpriteRenderer>().flipX = false; // or false to flip it back
            FacingRight = false;
        }
        else if (movement>0f && !FacingRight) {
            GetComponent<SpriteRenderer>().flipX = true; 
            FacingRight = true;
        }
    }

    void Jump() {
        rb.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision){
        
        if (collision.tag == "Gem") {
            CurrentGems++;
            score.CurrentGems++;
        }

        if (collision.tag == "win") { //for levels -
            //SceneManager.LoadScene("Victory");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision){

        if (collision.collider.tag == "Fox") {
        
            RespawnPlayer();
            PlayerMaxHealth -= 1;
            score.CurrentHp -= 1;
            //an.SetTrigger("hurt");
  

        }

        if (collision.collider.tag == "Ground") {
            IsGround = true;
        }
    }
    void GameOverScene() {
        //SceneManager.LoadScene("Over");
    }
}
