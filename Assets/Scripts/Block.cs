using UnityEngine;

public class Block : MonoBehaviour
{
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
