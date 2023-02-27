using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using UnityEngine.UI;


public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float runSpeed = 4.0f;
    [SerializeField] float jumpSpeed = 6.0f;
    Vector2 moveInput;
    Rigidbody2D rgbd2D;
    [SerializeField] PhysicsMaterial2D BounceState, NormalState;
    Animator myAnimator;
    CapsuleCollider2D myCapsuleCollider;
    AudioSource jumpSound;
    float startTime;
    float gravityScaleAtStart;
    public CompleteScreen complete;
    public TimerText endText;
    bool ended = false;


    // Start is called before the first frame update
    void Start()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCapsuleCollider = GetComponent<CapsuleCollider2D>();
        gravityScaleAtStart = rgbd2D.gravityScale;
        jumpSound = GetComponent<AudioSource>();
        jumpSound.time = GetComponent<AudioSource>().clip.length * 0.6f;
    }

    void Update()
    {
        if (ended) { return; }
        Run();
        FlipSprite();
        BirdTouch();
        GoalTouch();
    }
    void FixedUpdate()
    {
        if (rgbd2D.velocity.y < 0)
        {
            rgbd2D.sharedMaterial = NormalState;
            if (rgbd2D.gravityScale > 4) { Debug.Log("Max"); }
            else { rgbd2D.gravityScale = rgbd2D.gravityScale + 0.1f; }
        }
        isGrounded();
    }
    void GoalTouch()
    {
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Goal")))
        {
            rgbd2D.velocity = new Vector2(0,0);
            complete.Setup(endText.current());
            ended = true;
        }
    }

    void BirdTouch()
    {
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Enemies")))
        {
            rgbd2D.velocity = new Vector2(0, 2);
            myCapsuleCollider.enabled = false;
        }
    }

    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue ia)
    {
        if (rgbd2D.velocity.magnitude != 0) { startTime = Time.time; return; }
        myAnimator.SetBool("IsCharging", true);
        rgbd2D.sharedMaterial = BounceState;
        float val = ia.Get<float>();
        runSpeed = 0;
        if (val >= InputSystem.settings.defaultButtonPressPoint)
        {
            startTime = Time.time;
            
        }
        else
        {
            if (val <= InputSystem.settings.defaultButtonPressPoint)
            {
                if ((Time.time - startTime) >= 3.0f) {
                    jumpSound.Play();
                    float boost = 3.0f;
                    Debug.Log((boost).ToString("00:00.00"));
                    Vector2 playerVelo = new Vector2(moveInput.x * runSpeed + boost, jumpSpeed * 1 + 2*(boost));
                    rgbd2D.velocity = playerVelo;
                    runSpeed = 4.0f;
                    myAnimator.SetBool("IsCharging", false);
                } 
                else {
                    jumpSound.Play();
                    float boost = Time.time - startTime;
                    Debug.Log((boost).ToString("00:00.00"));
                    Vector2 playerVelo= new Vector2(moveInput.x * runSpeed + boost, jumpSpeed * 1 + 2*(boost));
                    rgbd2D.velocity = playerVelo;
                    runSpeed = 4.0f;
                    myAnimator.SetBool("IsCharging", false);
                }
            } 
        }
        
    }
    void Run()
    {
        if (!myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))) { return; }
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, rgbd2D.velocity.y);
        rgbd2D.velocity = playerVelocity;
    }

    void FlipSprite()
    {
        bool playHasHorizontalSpeed = Mathf.Abs(rgbd2D.velocity.x) > Mathf.Epsilon;
        if (playHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(rgbd2D.velocity.x), 1f);
        }
    }

    void isGrounded()
    {
        if (rgbd2D.gravityScale > 4) {myCapsuleCollider.enabled = true;}
        if (myCapsuleCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rgbd2D.gravityScale = gravityScaleAtStart;
        }
    }
}
