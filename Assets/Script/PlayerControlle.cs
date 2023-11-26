using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlle : MonoBehaviour
{
    [SerializeField] Rigidbody2D _rb;
    [SerializeField] Animator _anim;
    Vector2 _move;
    [SerializeField] float _speed;
    [SerializeField] float _jumpF;
    bool _checkGround, _isFacingRight, _checkIni;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_checkIni == true)
        {
            Move();
            Anim();
        }
       
    }

    void Move()
    {
            
        _rb.velocity = new Vector2(_move.x * _speed, _rb.velocity.y);
        
        if(_move.x > 0 && _isFacingRight==true) {
            Flip();
        }
        else if(_move.x < 0 && _isFacingRight == false)
        {
            Flip();
        }
    }

    void Jump()
    {
        if (_checkGround==true)
        {
            _rb.velocity = new Vector2(_rb.velocity.x,0);
            _rb.AddForce(new Vector2(0, 2* _jumpF),ForceMode2D.Impulse);
           // Debug.Log("Jump");
        }

    }

    void Anim()
    {
        float moveX = math.abs(_rb.velocity.x);
        _anim.SetFloat("Correndo", moveX);
        _anim.SetBool("CheckGround", _checkGround);
        _anim.SetFloat("VelocidadePulo", _rb.velocity.y);
    }
    void Flip()
    {
        _isFacingRight = !_isFacingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        // Flip collider over the x-axis
    }

    public void SetMove(InputAction.CallbackContext value)
    {
      //  _move.x = Input.GetAxisRaw("Horizontal");
        _move.x = value.ReadValue<Vector2>().x;
    }
    public void SetJump(InputAction.CallbackContext value)
    {
        Jump();
    }

    public void Iniciar()
    {
        _checkIni = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _checkGround = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _checkGround = false;
        }
    }


}
