using UnityEngine;

public class personaje : MonoBehaviour
{
    public float velocidad = 5f;
    public float velocidadCorrer = 8f; // velocidad al correr
    public GameObject areaAtaque;

    private Rigidbody2D rig;
    private Animator anim;

    private Vector2 input;
    private Vector2 ultimaDireccion;
    private bool atacando = false;

    private void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        areaAtaque.SetActive(false);
    }

    private void Update()
    {
        if (!atacando)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            input = new Vector2(horizontal, vertical).normalized;

            if (input != Vector2.zero)
            {
                ultimaDireccion = input;
            }

            
            float velocidadActual = velocidad;
            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                velocidadActual = velocidadCorrer;
            }

            rig.linearVelocity = input * velocidadActual;

            anim.SetFloat("Horizontal", input.x);
            anim.SetFloat("Vertical", input.y);
            anim.SetFloat("Speed", input.magnitude);
            anim.SetFloat("UltimoX", ultimaDireccion.x);
            anim.SetFloat("UltimoY", ultimaDireccion.y);
        }
        else
        {
            rig.linearVelocity = Vector2.zero;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !atacando)
        {
            atacando = true;
            anim.SetTrigger("Atacar");
            areaAtaque.SetActive(true);
            Invoke("FinAtaque", 0.4f); // Ajusta al tiempo de tu animación
        }
    }

    private void FinAtaque()
    {
        atacando = false;
        areaAtaque.SetActive(false);
    }
}
