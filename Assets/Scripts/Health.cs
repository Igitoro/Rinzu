using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{    
    [SerializeField] GameObject Loose;

    [SerializeField] Slider HPbar;
    [SerializeField] Text HPTxt;

    [SerializeField] public int maxHealth; 

    public static Health Instance;
    public int health;

    private SpriteRenderer spriteRenderer;
    private Color defaultColor;

    public Color DamageColor = Color.red;
    public float DamageTimeSec = 1f;

    private void Start()
    {
        HPbar = GameObject.FindGameObjectWithTag("UI").GetComponent<ObjectFinder1>().HpSlider;
        HPTxt = GameObject.FindGameObjectWithTag("UI").GetComponent<ObjectFinder1>().HPText;
        health = maxHealth;
        HPbar.maxValue = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
        Loose = GameObject.FindGameObjectWithTag("Character").GetComponent<LooseActivator>().Loose;
        
    }

    private void Update()
    {
        HPTxt.text = health + "/" + maxHealth;
        HPbar.value = health;
        if(health <= 0)
        { 
            Loose.SetActive(true);
            gameObject.SetActive(false);
        }
    }
    
    void OnTriggerEnter2D(Collider2D enemy)
    {
        if (enemy.gameObject.tag == "Enemy" || enemy.gameObject.tag == "EnemyBoss")
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
            health -= damage;
            HPbar.value = health;
            StartCoroutine(DamageEffectCoroutine());
            StopCoroutine(DamageEffectCoroutine());
    }

    public void AddHpOnLvlUp()
    {
        maxHealth += 1; 
        health = maxHealth;
        HPbar.maxValue = maxHealth;
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

