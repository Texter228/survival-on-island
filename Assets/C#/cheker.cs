using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cheker : MonoBehaviour
{
    public bool isground;  // Переменная для хранения состояния предмета
    public Vector2 boxSize = new Vector2(1f, 1f); // Размеры коробки для проверки на пересечение
    public LayerMask groundLayer; // Слой для проверки пересечения с землей

    void Start()
    {
        
    }

    public void CheckGround()
    {
        Collider2D[] hitColliders = Physics2D.OverlapBoxAll(transform.position, boxSize, 0f, groundLayer);
        isground = false;
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Ground"))
            {
                isground = true;
                break;
            }
        }
    }

    // Визуализация Box в редакторе для удобства настройки
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }
}
