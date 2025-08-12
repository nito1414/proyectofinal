using UnityEngine;

public class Enemigo : MonoBehaviour
{
    public float velocidad = 3f;
    public float radioDesaparicion = 15f;
    public AudioClip sonidoMuerte; // Sonido al morir

    private Transform jugador;
    private AudioSource audioSource;
    private bool muriendo = false;

    private void Start()
    {
        // Encuentra al jugador por su tag
        GameObject objJugador = GameObject.FindGameObjectWithTag("Player");
        if (objJugador != null)
        {
            jugador = objJugador.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró al jugador. Asegúrate de que tenga el tag 'Player'.");
        }

        // Obtener el AudioSource del enemigo o agregar uno
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    private void Update()
    {
        if (jugador == null || muriendo) return;

        // Mover hacia el jugador
        Vector2 direccion = (jugador.position - transform.position).normalized;
        transform.position += (Vector3)direccion * velocidad * Time.deltaTime;

        // Verificar distancia para desaparecer
        float distancia = Vector2.Distance(transform.position, jugador.position);
        if (distancia > radioDesaparicion)
        {
            Matar();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AreaAtaque"))
        {
            Matar();
        }
    }

    private void Matar()
    {
        if (muriendo) return; // Evitar que se ejecute más de una vez
        muriendo = true;

        if (sonidoMuerte != null)
        {
            audioSource.PlayOneShot(sonidoMuerte);
            Destroy(gameObject, sonidoMuerte.length); // Destruir después de que termine el sonido
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
