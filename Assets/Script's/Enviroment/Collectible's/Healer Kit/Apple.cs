using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource), typeof(SpriteRenderer), typeof(Collider2D))]
public class Apple : MonoBehaviour
{
    private AudioSource _pickUpSound;
    private WaitForSeconds _delay;
    private SpriteRenderer _render;
    private Collider2D _collider;

    public int AmountHealing { get; private set; } = 1;

    private void Awake()
    {
        _pickUpSound = GetComponent<AudioSource>();
        _render = GetComponent<SpriteRenderer>();
        _collider = GetComponent<Collider2D>();
        _delay = new WaitForSeconds(_pickUpSound.clip.length);
    }

    public void Dispose()
    {
        StartCoroutine(PlaySoundWithDelay());
    }

    private IEnumerator PlaySoundWithDelay()
    {
        _pickUpSound.Play();

        _render.enabled = false;
        _collider.enabled = false;

        yield return _delay;

        gameObject.SetActive(false);
    }
}