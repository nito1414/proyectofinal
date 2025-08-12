using UnityEngine;

public class GeneradorEnemigos : MonoBehaviour
{
    [Header("Configuración de Spawn")]
    public GameObject prefabEnemigo;              // Prefab del enemigo
    public int cantidadMaxima = 5;                // Máximo de enemigos en escena
    public float radioAparicion = 5f;             // Distancia desde el jugador
    public float tiempoEntreSpawns = 3f;          // Tiempo entre apariciones

    private float tiempoProximoSpawn = 0f;

    void Update()
    {
        // Solo genera si ya pasó el tiempo y no hay demasiados enemigos
        if (Time.time >= tiempoProximoSpawn && EnemigosActivos() < cantidadMaxima)
        {
            SpawnEnemigo();
            tiempoProximoSpawn = Time.time + tiempoEntreSpawns;
        }
    }

    void SpawnEnemigo()
    {
        // Genera una posición aleatoria alrededor del jugador
        Vector2 direccionAleatoria = Random.insideUnitCircle.normalized * radioAparicion;
        Vector2 posicionSpawn = (Vector2)transform.position + direccionAleatoria;

        // Instancia el enemigo
        Instantiate(prefabEnemigo, posicionSpawn, Quaternion.identity);
    }

    int EnemigosActivos()
    {
        // Cuenta enemigos con tag "Enemigo"
        return GameObject.FindGameObjectsWithTag("Enemigo").Length;
    }
}
