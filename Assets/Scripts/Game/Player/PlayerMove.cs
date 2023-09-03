
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{
    #region Variables
    //public variables
    [SerializeField] [Range (1f, 20f)] public float speed;
    [SerializeField] [Range (1f, 10f)] public float jumpAmount;
    [SerializeField] [Range (1f,10f)] public float bounceAmount;

    //private variables
    private Rigidbody2D rb;
    private PhotonView view;
    private Joystick joystick;
    private Vector2 input;
    private Button jumpBtn;
    private bool isRight;
    private bool isGrounded;
    private bool isBouncable;
    private Transform originalObject;
    #endregion

    #region Monobehaviour Callbacks
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        view = GetComponent<PhotonView>();
        transform.rotation = new Quaternion(0, 0, 180, 0);
        jumpBtn = GameObject.FindGameObjectWithTag("JumpButton").GetComponent<Button>();
        jumpBtn.onClick.AddListener(Jump);
    }
    void Update(){
        input = new Vector2(joystick.Horizontal, 0);
        if(view.IsMine){
            if(input != new Vector2(0,0)){
                Move();
            }
            
        }
            

        if(input.x > 0)
            isRight = true;

        if(Input.GetButtonDown("Jump"))
            Jump();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bouncable")){
            isBouncable = true;   
            originalObject = other.gameObject.transform;
        }
        else if (other.gameObject.CompareTag("Ground")){
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bouncable")){
            isBouncable = false;
            originalObject = null;
        }
        else if (other.gameObject.CompareTag("Ground")){
            isGrounded = false;
        }
    }
    #endregion

    #region Private Methods
    private void Move()
    {
        transform.position += (Vector3)input * speed * Time.deltaTime; 

        if(isRight)
            transform.rotation = Quaternion.Euler(0, 0, 90);
        else
            transform.rotation = Quaternion.Euler(0, 0, -90);

    }


    private void Jump(){
        if (view.IsMine)
        {
            if(isGrounded)
            {
                rb.AddForce(Vector2.up * jumpAmount, ForceMode2D.Impulse);
            }
            else if(isBouncable)
            {
                rb.AddForce(Vector3.Reflect(originalObject.position, Vector2.right + Vector2.up) * Time.deltaTime, ForceMode2D.Impulse);
                rb.AddForce(Vector3.Reflect(originalObject.position, Vector2.right + Vector2.up) * Mathf.Pow(bounceAmount, 2) * Time.deltaTime, ForceMode2D.Impulse);
            }
            
        }
    }
    #endregion
}
