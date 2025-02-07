using UnityEngine;

public class Block : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            BlockPool.Instance.ReturnBlock(gameObject);
            GameManager.Instance.BlockHidden();
        }
    }
}
