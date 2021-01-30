using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    Rigidbody2D rb2d;
    float speed;
    float h, v;
    Vector2 moveDirection;
    bool isMoving = false;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        speed = (int)Random.Range(1f, 10f);
        transform.localScale = new Vector3((int)Random.Range(1f, 30f), 3, 1);
        gameObject.GetComponent<SpriteRenderer>().color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
    }

    public void RandomMovement()
    {
        if (!isMoving)
        {
            h = RandomSelection();
            v = RandomSelection();
        }
        moveDirection = new Vector2(h, v).normalized;
    }

    float RandomSelection()
    {
        var r = Mathf.Round(Random.Range(-1f, 1f));
        while(r == 0)
        {
            r = Mathf.Round(Random.Range(-1f, 1f));
        } 

        return r;
    }

    void FixedUpdate()
    {
        rb2d.velocity = moveDirection * speed;
    }

    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 wallNormal = collision.contacts[0].normal;
        moveDirection = Vector2.Reflect(rb2d.velocity, wallNormal).normalized;

        rb2d.velocity = moveDirection * speed;
    }
    
}
