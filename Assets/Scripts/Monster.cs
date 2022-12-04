using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[SelectionBase]
public class Monster : MonoBehaviour
{
    [SerializeField] Sprite _deadSprite;
    [SerializeField] ParticleSystem _particleSystem;
    bool _hasDied;

    void OnCollisionEnter2D(Collision2D col)
    {
        if (ShouldDieFromCollision(col))
        {
            StartCoroutine(Die());

        }
    }
    IEnumerator Die()
    {
        _hasDied = true;
        GetComponent<SpriteRenderer>().sprite = _deadSprite;
        _particleSystem.Play();
        yield return new WaitForSeconds(1);
        gameObject.SetActive(false);
    }
    bool ShouldDieFromCollision(Collision2D col)
    {
        if (_hasDied) { return false; }
        Bird bird = col.gameObject.GetComponent<Bird>();
        if (bird != null)
            return true;

        if (col.contacts[0].normal.y < -0.5f)
            return true;


        return false;
    }
}