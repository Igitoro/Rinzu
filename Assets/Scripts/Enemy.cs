using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public event Action<Enemy> OnDie;

    public int health;

    public int exp;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor;

    public Color DamageColor = Color.red;
    public float DamageTimeSec = 1f;
 
    public void Start()
    {
        Ai_manager.Instance.Register(this);

        exp = Random.Range(1, 3);
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }

    private void Update()
    {
        if (health <= 0)
        {
            OnDie?.Invoke(this);
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(DamageEffectCoroutine());
        StopCoroutine(DamageEffectCoroutine());
    }

    public void Die()
    {
        ExpManager.Instance.AddExp(exp);
        Destroy(gameObject);
    }

    private IEnumerator DamageEffectCoroutine()
    {
        float time = 0;
        float step = 1f / DamageTimeSec;
 
        while (time < DamageTimeSec)
        {
            time += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(DamageColor, defaultColor, step * time);
 
            yield return null;
        }
    }
}
