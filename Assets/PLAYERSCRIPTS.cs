using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PLAYERSCRIPTS : MonoBehaviour
{
    public Slider barraVida;
    private float x;
    private float y;
    private float speed = 10f;
    public float MinSpeed = 10f;
    public float MaxSpeed = 15f;

    public float JumpForce = 8f;

    public float Sensibility = 2f;
    public float LimitX = 45;
    public Transform cam;

    private float rotationX;

    public bool IsGrounded;

    private Rigidbody rb;
    private Animation anim;

    public int punchDamage = 5;

    [Header("Ground Check Settings")]
    public Transform groundCheck;
    public float groundRadius = 0.35f;
    public LayerMask groundMask;

    [Header("Wall Check Settings")]
    public Transform wallCheck;        // Punto desde el que saldrá el raycast
    public float wallCheckDistance = 0.5f;
    public LayerMask wallMask;

    // --- VIDA DEL JUGADOR ---
    public int vida = 100;
    public int vidaMax = 100;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        barraVida.maxValue = vidaMax;
        barraVida.value = vida;

        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.freezeRotation = true;

        anim = GetComponent<Animation>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        anim["Parado"].layer = 0;
        anim["Caminar"].layer = 0;
        anim["Correr"].layer = 0;
        anim["Puñetazo"].layer = 1;

        anim["Parado"].wrapMode = WrapMode.Loop;
        anim["Caminar"].wrapMode = WrapMode.Loop;
        anim["Correr"].wrapMode = WrapMode.Loop;
        anim["Puñetazo"].wrapMode = WrapMode.Once;
    }

    void Update()
    {
        barraVida.value = vida;
        // Chequear suelo
        Collider[] hits = Physics.OverlapSphere(groundCheck.position, groundRadius, groundMask);
        IsGrounded = hits.Length > 0;

        x = Input.GetAxisRaw("Horizontal");
        y = Input.GetAxisRaw("Vertical");

        bool isMoving = x != 0 || y != 0;
        bool isRunning = isMoving && Input.GetKey(KeyCode.LeftShift);

        speed = isRunning ? MaxSpeed : MinSpeed;

        Vector3 move = (transform.right * x + transform.forward * y).normalized;
        if (move.sqrMagnitude > 0.001f) // Solo si nos estamos moviendo 
        { // Raycast en la dirección real de movimiento
            bool wallInFront = Physics.Raycast(wallCheck.position, move, wallCheckDistance, wallMask);
            if (!wallInFront)
            {
                rb.MovePosition(rb.position + move * speed * Time.fixedDeltaTime);
            }
        }
            


        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        rotationX += -Input.GetAxis("Mouse Y") * Sensibility;
        rotationX = Mathf.Clamp(rotationX, -LimitX, LimitX);
        cam.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * Sensibility, 0);

        if (!Input.GetMouseButton(0))
        {
            if (isRunning)
                anim.CrossFade("Correr");
            else if (isMoving)
                anim.CrossFade("Caminar");
            else
                anim.CrossFade("Parado");
        }
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
        }

        if (wallCheck != null)
        {
            Gizmos.color = Color.red;
            Vector3 moveDir = (transform.right * x + transform.forward * y).normalized;
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + moveDir * wallCheckDistance);
        }
    }
    
}