using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MegamanXController : MonoBehaviour
{

    //public properties
    public float velocityX = 12f;
    public float jumpForce = 40f;

    public GameObject balaDerecha;
    public GameObject balaIzquierda;
    public GameObject bala2Derecha;
    public GameObject bala2Izquierda;
    public GameObject bala3Derecha;
    public GameObject bala3Izquierda;



    // Start is called before the first frame update

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    //private properties
    private bool isJumping = false;
    private bool isCharging = false;
    
    private float cargaDisparo = 0f;
    private float cargaDisparo1 = 3f;
    private float cargaDisparo2 = 5f;


    //Constants

    private const int ANIMATION_IDLE = 0;
    private const int ANIMATION_RUN = 1;
    private const int ANIMATION_JUMP = 2;
    private const int ANIMATION_RUN_SHOOT = 3;
    private const int ANIMATION_SHOOT = 4;
    
    //Tags

    private const string TAG_PISO = "Ground";
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //Quieto
        rb.velocity = new Vector2(0, rb.velocity.y);
        changeAnimation(ANIMATION_IDLE);

        //caminarDerecha
        if(Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocityX, rb.velocity.y);
            sr.flipX = false;
            changeAnimation(ANIMATION_RUN);

            //DispararDerecha
            if(Input.GetKeyUp(KeyCode.X) && !sr.flipX)
            {
                isCharging = true;
                comprobarCarga(isCharging);

                //Crear el objeto
                //1. GameObject que debemos crear
                //2. Position donde va a aparecer
                //3. Rotación
                changeAnimation(ANIMATION_RUN_SHOOT);

                /*var position = new Vector2(transform.position.x,transform.position.y);
                var rotation = balaDerecha.transform.rotation;
                Instantiate(balaDerecha,position,rotation);*/

                /*if(cargaDisparo >=3f && cargaDisparo <5f)
                {
                    Debug.Log("Bala2");
                    var position2 = new Vector2(transform.position.x,transform.position.y);
                    var rotation2 = bala2Derecha.transform.rotation;
                    Instantiate(bala2Derecha,position2,rotation2);
                }*/
            }

        }


        //caminarIzquierda
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocityX, rb.velocity.y);
            sr.flipX = true;
            changeAnimation(ANIMATION_RUN);
            
        }

        //Saltar
        if(Input.GetKey(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            changeAnimation(ANIMATION_JUMP);
            isJumping= true;
        }

        //DispararDerecha
        if(Input.GetKeyUp(KeyCode.X) && !sr.flipX)
        {
            isCharging = true;
            comprobarCarga(isCharging);
            
            //Crear el objeto
            //1. GameObject que debemos crear
            //2. Position donde va a aparecer
            //3. Rotación
            changeAnimation(ANIMATION_SHOOT);
            
            var position = new Vector2(transform.position.x,transform.position.y);
            var rotation = balaDerecha.transform.rotation;
            Instantiate(balaDerecha,position,rotation);

            if(cargaDisparo >=3f && cargaDisparo <5f)
            {
                Debug.Log("Bala2");
                var position2 = new Vector2(transform.position.x,transform.position.y);
                var rotation2 = bala2Derecha.transform.rotation;
                Instantiate(bala2Derecha,position2,rotation2);
            }
        }
        

        //DispararIzquierda
        if(Input.GetKeyUp(KeyCode.X) && sr.flipX)
        {
            //Crear el objeto
            //1. GameObject que debemos crear
            //2. Position donde va a aparecer
            //3. Rotación
            changeAnimation(ANIMATION_SHOOT);
            
            var position = new Vector2(transform.position.x,transform.position.y);
            var rotation = balaDerecha.transform.rotation;
            Instantiate(balaIzquierda,position,rotation);

        }
    }

    private void comprobarCarga(bool estaCargando)
    {
        if(estaCargando)
        {
            cargaDisparo += Time.deltaTime;
            Debug.Log(cargaDisparo);
        }
    }


    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.tag == TAG_PISO)
        {
            isJumping = false;
        }
    }

    private void changeAnimation(int animation)
    {
        animator.SetInteger("Estado", animation);
    }
}
