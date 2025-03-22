using System.Collections;
using UnityEngine;
using System;

[RequireComponent(typeof(AudioSource), typeof(SpriteRenderer), typeof(Collider2D))]
public class Coin : MonoBehaviour, ITakeable
{
    private AudioSource _pickUpSound;
    private WaitForSeconds _delay;
    private SpriteRenderer _render;
    private Collider2D _collider;
    
    private void Awake()
    {
        _pickUpSound = GetComponent<AudioSource>();
        _render = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _delay = new WaitForSeconds(_pickUpSound.clip.length);
    }
    
    public void Dispose() => StartCoroutine(PlaySoundWithDelay());
    
    public void Accept(ItemInteractor itemInteractor) => itemInteractor.VisitCoin(this);

    private IEnumerator PlaySoundWithDelay()
    {
        _pickUpSound.Play();

        _render.enabled = false;
        _collider.enabled = false;

        yield return _delay;

        gameObject.SetActive(false);
    }
}