using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 11f;
    public float desaceleracao = 0.87f;
    public int macas = 0;
    private bool keyF = false;

    private Rigidbody rb;
    private Vector2 movementVector = Vector2.zero;
    private Vector3 movement = Vector3.zero;
    public bool moveu = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(InputAction.CallbackContext movimento){
        movementVector = movimento.ReadValue<Vector2>();
        moveu = movimento.performed;
    }

    void FixedUpdate()
    {
        movement = new Vector3(movementVector.x, 0.0f, movementVector.y);
        if(rb.velocity.magnitude > 8f){
            //Se a bola estiver muito rápida, facilita na hora de fazer curvas ou dar meia volta
            rb.AddForce(1.2f * speed * movement);
        } else{
            rb.AddForce(movement * speed);
        }

        //Limita a aceleração da bola.
        if(rb.velocity.magnitude > 15f){
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, 15f);
        }

        if(keyF == true){
            Freio();
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp")){
            other.gameObject.SetActive(false);
            macas++;
        }
    }

    public float GetVelocidadeAtual()
    {
        return rb.velocity.magnitude;
    }

    public void Skill(InputAction.CallbackContext skill){
        keyF = skill.control.IsPressed();
    }

    void Freio(){
        //Desacelera a bola lentamente
        rb.velocity = new Vector3(rb.velocity.x * desaceleracao, rb.velocity.y, rb.velocity.z * desaceleracao);
        if(rb.velocity.magnitude < 0.3f){
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
    }
}
