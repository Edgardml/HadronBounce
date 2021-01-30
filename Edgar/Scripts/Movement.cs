using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public int speed = 5;
    float h, v;
    Rigidbody2D rb2d;
    LevelManager lm;
    private Vector2 moveDirection;

    Vector2 difference = Vector2.zero;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lm = GameObject.Find("Canvas").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!lm.isPaused)
        {
            h = Input.GetAxisRaw("Horizontal");
            v = Input.GetAxisRaw("Vertical");
        }
        else
        {
            h = 0;
            v = 0;
        }
        moveDirection = new Vector2(h, v).normalized;
    }

    private void FixedUpdate()
    {
        rb2d.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    private void OnMouseDown()
    {
        difference = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - (Vector2)transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - difference;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            lm.Shake();
            lm.GameOver();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Reward"))
        {
            lm.score += 125;
            Destroy(collision.gameObject);
        }
    }
}
