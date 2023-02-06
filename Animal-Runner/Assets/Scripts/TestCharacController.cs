using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class TestCharacController : MonoBehaviour
{
    // Yarýn yapýlacaklar obje havuzu ve altýn havuzu yapýlacak
    [Header("Control")]
    //[SerializeField] private float movementSpeed=10f;
    [SerializeField] private float slideSpeed;
    private Vector3 clickedScreenPosition;
    private Vector3 clickedPlayerPosition;
    Vector3 position;
    bool canMove = false;
    bool isJumping = false;
    bool gameOver;
    bool revived = false;
    private float speed;

    [Header("Jump")]
    [SerializeField] private float jumpForce;
    Rigidbody rb;
    float hMovement;
    private Vector3 movementVector;

    //Rotation
    Quaternion rightTurningAngle = Quaternion.Euler(0, 20, 0);
    Quaternion leftTurningAngle = Quaternion.Euler(0, -20, 0);
    float currentAngle;
    private GameObject animal;

    [Header("Spawn")]
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private GameManager gameManager;

    //[Header("Animation")]
    //[SerializeField] private Animator playerAnimator;
    private Animator playerAnimator;

    int reward;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gameOver = false;
    }

    void Update()
    {
        if (canMove)
        {
            MoveForward();
            
            if (Input.touchCount > 0)
            {
                ManageMobileControl();

                ManageMobileJumpAndRotate();
            }

            else
            {
                MovementWithKeyboard();

                JumpWithKeyboard();
            }

        }

    }

    #region Movement
    private void MoveForward()
    {
        transform.position += Vector3.forward * speed * Time.deltaTime;
    }

    #region Keyboard
    private void MovementWithKeyboard()
    {
        hMovement = Input.GetAxisRaw("Horizontal") * (speed / 2);

        //vMovement = Mathf.Abs(Input.GetAxisRaw("Vertical") * movementSpeed);

        movementVector = new Vector3(hMovement, 0, 0);
        transform.Translate(movementVector * Time.deltaTime);

        if (hMovement > 0)
        {
            animal.transform.localRotation = Quaternion.Slerp(animal.transform.localRotation, rightTurningAngle, .1f);
        }

        else if (hMovement < 0)
        {
            animal.transform.localRotation = Quaternion.Slerp(animal.transform.localRotation, leftTurningAngle, .1f);
        }

        if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            ReturnDefaultAngle();
        }

        else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.RightArrow))
        {
            ReturnDefaultAngle();
        }

        CheckPlayersPosition();
    }
   
    private void JumpWithKeyboard()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isJumping == false)
            {
                rb.velocity = Vector3.zero;
                rb.velocity = Vector3.up * jumpForce;
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (isJumping)
            {
                rb.velocity = Vector3.zero;
                rb.velocity = Vector3.down * jumpForce;
            }
        }
        
    }

    #endregion

    #region Mobile
    private void ManageMobileControl()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedScreenPosition = Input.mousePosition;
            clickedPlayerPosition = transform.position;
        }
        else if (Input.GetMouseButton(0))
        {
            float xScreenDifference = Input.mousePosition.x - clickedScreenPosition.x;

            xScreenDifference /= Screen.width;
            xScreenDifference *= slideSpeed;

            position = transform.position;
            position.x = clickedPlayerPosition.x + xScreenDifference;
            transform.position = position;

           /* Debug.Log(xScreenDifference);
            if(xScreenDifference > 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, rightTurningAngle, .1f);
            }
            else if(xScreenDifference < 0)
            {
                transform.rotation = Quaternion.Slerp(transform.rotation, leftTurningAngle, .1f);
            }*/
            //transform.position = clickedPlayerPosition + Vector3.right * xScreenDifference;

            CheckPlayersPosition();
        }

        /*else if (Input.GetMouseButtonUp(0))
        {
            transform.rotation = defaultAngle;
        }*/
    }

    private void ManageMobileJumpAndRotate()
    {
        //Jump
        Touch touch = Input.GetTouch(0);
        /*if (touch.phase == TouchPhase.Moved)
        {
            
        }*/
        //Jump

        if(touch.deltaPosition.y > 40f)
        {
            if (isJumping == false)
            {
                rb.velocity = Vector3.zero;
                rb.velocity = Vector3.up * jumpForce;
            }
        }

        else if (touch.deltaPosition.y < -40f)
        {
            if (isJumping)
            {
                rb.velocity = Vector3.zero;
                rb.velocity = Vector3.down * jumpForce;
            }
        }
        //Rotate

        if (touch.deltaPosition.x > 0)
        {

            animal.transform.localRotation = Quaternion.Slerp(animal.transform.localRotation, rightTurningAngle, .1f);
        }
        else if(touch.deltaPosition.x < 0)
        {
            animal.transform.localRotation = Quaternion.Slerp(animal.transform.localRotation, leftTurningAngle, .1f);
        }

        if(touch.phase == TouchPhase.Ended)
        {
            ReturnDefaultAngle();
        }

    }

    #endregion

    private void ReturnDefaultAngle()
    {
        //transform.rotation = Quaternion.Slerp(transform.rotation, defaultAngle, 2f);
        currentAngle = animal.transform.localEulerAngles.y;
        
        if (currentAngle > 180)
        {
            currentAngle = 360 - currentAngle;
            animal.transform.DORotate(new Vector3(0,currentAngle,0), .2f, RotateMode.LocalAxisAdd);
        }
        else
        {
            animal.transform.DORotate(new Vector3(0, -currentAngle, 0), .2f, RotateMode.LocalAxisAdd);
        }
        
    }
   
    public void StartMovement(float speedValue)
    {
        animal = transform.GetChild(MarketController.current.ReturnActiveAnimalIndex()).gameObject;
        playerAnimator = animal.GetComponent<Animator>();
        canMove = true;
        speed = speedValue;
        playerAnimator.SetTrigger("isRunning");
        Debug.Log("Hareket hizi:" + speed);
    }
   
    public void StopMovement(bool dead=false)
    {
        if (dead)
        {
            canMove = false;
            playerAnimator.SetBool("isDead", true);
            if (revived)
            {
                playerAnimator.SetBool("revived", false);
            }
        }
        else
        {
            canMove = false;
            playerAnimator.SetBool("gameFinished", true);
            playerAnimator.ResetTrigger("isRunning");
        }
        
    }
   
    private void CheckPlayersPosition()
    {
        if (transform.position.x > 0.7f)
        {
            transform.position = new Vector3(0.7f, transform.position.y, transform.position.z);
        }
        else if (transform.position.x < -0.7f)
        {
            transform.position = new Vector3(-0.7f, transform.position.y, transform.position.z);
        }
    }
    #endregion

    #region Collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("SpawnTrigger"))
        {
            spawnManager.SpawnPointTriggered();
        }
        if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            gameManager.AddCoin();
            SoundManager.current.PlayCoinSound();
        }
        if (other.CompareTag("Finish"))
        {
            //reward = PlayerPrefs.GetInt("Coin") + 50;
            //PlayerPrefs.SetInt("Coin", reward);
            gameManager.WinTheGame();
            StopMovement();
        }

        if (other.CompareTag("Deathzone"))
        {
            if (!gameOver)
            {
                StopMovement(true);
                gameManager.LoseTheGame();
                gameOver = true;
            }
        }

        if (other.CompareTag("Zoo"))
        {
            CameraManager.current.ChangeCamera(CameraManager.current.cameras[2]);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Barrier"))
        {
            if (!gameOver)
            {
                StopMovement(true);
                gameManager.LoseTheGame();
                gameOver = true;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        isJumping = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        isJumping = true;

    }

    #endregion

    public void Revive()
    {
        gameOver = false;
        revived = true;
        playerAnimator.SetBool("revived", true);
        playerAnimator.SetBool("isDead", false);
    }

    public void RestartTheMovement()
    {
        canMove = true;
        playerAnimator.SetTrigger("isRunning");
    }
}
