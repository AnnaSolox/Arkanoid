using UnityEngine;

public class Block : MonoBehaviour
{
    // Método que se ejecuta cuando un objeto colisiona con el bloque
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            ScoreManager.Instance.AddPoints();
            BlockPool.Instance.ReturnBlock(gameObject);
            GameManager.Instance.BlockHidden();
            ActivateExplosion();
        }
    }

    // Método para activar el efecto de partículas de explosión
    void ActivateExplosion()
    {
        // Obtener el prefab de partículas basado en el tag del bloque
        ParticleSystem explosionEffect = BlockPool.Instance.GetExplosionPrefab(tag);

        if (explosionEffect != null)
        {
            // Instanciamos el sistema de partículas en la posición del bloque
            ParticleSystem explosion = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            explosion.Play();  // Reproducimos la explosión
        }
    }
}
