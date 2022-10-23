using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
public class ExplosionAnimation : MonoBehaviour
{
    private float _explosionModifier = 7.5f;

    public void Explode()
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 3f);
        foreach (Collider2D collider in hitColliders)
        {
            if (collider.gameObject.CompareTag("Player"))
            {
                Vector2 direction = collider.transform.position - transform.position;
                collider.gameObject.GetComponent<Rigidbody2D>().AddForce((direction + Vector2.up) * _explosionModifier, ForceMode2D.Impulse);
            }
        }
        SoundManager.soundManager.PlayExplosion();
    }

    public void RemoveSelf()
    {
        Destroy(this.gameObject);
    }
}
