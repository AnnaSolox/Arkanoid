using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Movement Speed
    public float initialSpeed = 150.0f;
    private float speedIncrement = 5.0f;  // Incremento de velocidad
    private float incrementInterval = 2.5f;
    public float maxSpeed = 400.0f;

    private Rigidbody2D rb;
    public Racket racket;  // Referencia al script Racket
    private bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        
        // No lanzamos la bola inmediatamente
        rb.linearVelocity = Vector2.zero;
        
        // Si no tenemos la referencia al racket, la buscamos
        if (racket == null)
        {
            racket = FindFirstObjectByType<Racket>();
        }
    }

    void Update()
    {
        if (!isMoving && racket != null)
        {
            // Seguir al racket mientras no se haya lanzado
            transform.position = new Vector3(
                racket.transform.position.x,
                racket.transform.position.y + 8f,
                0
            );

            // Revisar si se debe lanzar la bola
            if (Input.GetKey(KeyCode.UpArrow))
            {
                isMoving = true;
                rb.linearVelocity = Vector2.up * initialSpeed;
                StartCoroutine(IncreaseSpeedOverTime());
            }
        }
    }

    IEnumerator IncreaseSpeedOverTime()
    {
        while (initialSpeed < maxSpeed)
        {
            yield return new WaitForSeconds(incrementInterval);
            initialSpeed += speedIncrement;
            rb.linearVelocity = rb.linearVelocity.normalized * initialSpeed;
        }
    }

    float hitFactor(Vector2 ballPos, Vector2 racketPos, float racketWidth)
    {
        //
        // 1  -0.5  0  0.5   1  <- x value
        // ===================  <- racket
        //
        return (ballPos.x - racketPos.x) / racketWidth;
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        // Hit the Racket?
        if (col.gameObject.name == "racket")
        {
            // Calculate hit Factor, direction of the ball
            float x = hitFactor(transform.position, col.transform.position, col.collider.bounds.size.x);

            // Calculate direction, set length to 1
            Vector2 dir = new Vector2(x, 1).normalized;

            // Set Velocity with dir * speed
            GetComponent<Rigidbody2D>().linearVelocity = dir * initialSpeed;
        }
    }
}
