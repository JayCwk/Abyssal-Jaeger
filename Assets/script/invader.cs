using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    public Sprite[] animationSprites;
    public float animationTime = 1.0f;
    private SpriteRenderer _spriteRender;
    private int _animationFrame;

    public float speed = 2f;

    public System.Action killed;

    private void Awake()
    {
        _spriteRender = GetComponent<SpriteRenderer>();

    }

    private void Start()
    {
        InvokeRepeating(nameof(animateSprite), this.animationTime, this.animationTime);
    }

    private void animateSprite()
    {
        _animationFrame++;
        if(_animationFrame>=this.animationSprites.Length)
        {
            _animationFrame = 0;
        }

        _spriteRender.sprite = this.animationSprites[_animationFrame];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Bullet"))
        {
            this.killed.Invoke();
            this.gameObject.SetActive(false);
        }
    }
}
