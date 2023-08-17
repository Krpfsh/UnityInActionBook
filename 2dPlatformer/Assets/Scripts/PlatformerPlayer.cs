using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerPlayer : MonoBehaviour
{
    public float speed = 250.0f;
    private Rigidbody2D _body;

    private Animator _anim;

    public float jumpForce = 12.0f;

    private BoxCollider2D _box;

    private void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _box = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        float deltaX = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        Vector2 movement = new Vector2(deltaX, _body.velocity.y);
        _body.velocity = movement;

        Vector3 max = _box.bounds.max;
        Vector3 min = _box.bounds.min;
        Vector2 corner1 = new Vector2(max.x, min.y - .1f);
        Vector2 corner2 = new Vector2(min.x, min.y - .2f);
        Collider2D hit = Physics2D.OverlapArea(corner1, corner2);

        bool grounded = false;
        if(hit != null)
        {
            grounded = true;
        }

        MovingPlatform platform = null;
        if(hit != null)
        {
            platform = hit.GetComponent<MovingPlatform>();
        }
        if (platform !=null)
        {
            transform.parent = platform.transform;
        }
        else
        {
            transform.parent = null;
        }



        _anim.SetFloat("speed", Mathf.Abs(deltaX));

        Vector3 pScale = Vector3.one;
        if(platform != null)
        {
            pScale = platform.transform.localScale;
        }
        if(deltaX != 0)
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.y, 1/ 1);
        }

        if(!Mathf.Approximately(deltaX, 0))
        {
            transform.localScale = new Vector3(Mathf.Sign(deltaX) / pScale.x, 1 / pScale.y, 1);
        }
        _body.gravityScale = grounded && deltaX == 0 ? 0 : 1; 
        if (grounded && Input.GetKeyDown(KeyCode.Space))
        {
            _body.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
}
