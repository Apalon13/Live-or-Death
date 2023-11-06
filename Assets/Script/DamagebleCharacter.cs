using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamagebleCharacter : MonoBehaviour, IDamageable
{
    public bool disableSimulation = false;

    public GameObject healthText;

    public float invincibilitiTime = 0.25f;

    public GameObject enemy;

    string tagEnemy1;

    public bool isinvincibilitiEnable = false;

    private float invincibleTimeElapsed = 0f;

    public Color DamageColor = Color.red;

    Color _defaultColor;

    Animator animator;

    Rigidbody2D rb;

    SpriteRenderer _spriteRend;

    Collider2D physicsCollider;

    private void Start()
    {
        _spriteRend = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _defaultColor = _spriteRend.color;
        if (enemy.tag == "Enemy")
        {
            tagEnemy1 = "Enemy";
            print(tagEnemy1);
        }
        else if (enemy.tag == "Player")
        {
            tagEnemy1 = "Player";
            print(tagEnemy1);
        }
    }
    public float Health
    {
        set
        {
            if (value < _health)
            {
                animator.SetTrigger("Hit");
            }

            _health = value;

            if (_health <= 0)
            {
                animator.SetBool("Defeated", true);
                Targetable = false;
            }
        }
        get
        {
            return _health;
        }
    }

    public bool Targetable
    {
        get { return _targetable; }
        set
        {
            _targetable = value;

            if (disableSimulation)
            {
                rb.simulated = false;
            }
           
            physicsCollider.enabled = value;
        }

    }

    public bool Invincible
    {
        get
        {
            return _invincible;
        }
        set
        {
            _invincible = value;

            if(Invincible == true)
            {
                invincibleTimeElapsed = 0f;
            }
        }
    }

    public float _health = 3;

    bool _targetable = true;

    bool _invincible = false;

    void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void OnHit(float damage, Vector2 knockback, string tagEnemy)
    {
        if (tagEnemy == "Enemy")
        {
            if ((!isinvincibilitiEnable || !Invincible) && enemy.tag != "Enemy")
            {
                GameObject textInstance = Instantiate(healthText);
                TextMeshProUGUI textDa = textInstance.GetComponent<TextMeshProUGUI>();
                RectTransform textTransform = textInstance.GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
                AudioManager.instance.Play("Damage");
                textDa.text = damage.ToString();
                Health -= damage;
                rb.AddForce(knockback, ForceMode2D.Impulse);
            }
        }
        else if (tagEnemy == "Player")
        {
            if (!isinvincibilitiEnable || !Invincible)
            {
                GameObject textInstance = Instantiate(healthText);
                TextMeshProUGUI textDa = textInstance.GetComponent<TextMeshProUGUI>();
                RectTransform textTransform = textInstance.GetComponent<RectTransform>();
                textTransform.transform.position = Camera.main.WorldToScreenPoint(gameObject.transform.position);
                Canvas canvas = GameObject.FindObjectOfType<Canvas>();
                textTransform.SetParent(canvas.transform);
                AudioManager.instance.Play("Damage");
                textDa.text = damage.ToString();
                Health -= damage;
                rb.AddForce(knockback, ForceMode2D.Impulse);
            }
        }

        if (isinvincibilitiEnable && !Invincible)
        {
            Invincible = true;
        }
    }
    public void OnObjectDestroy()
    {
        GameObject.Destroy(gameObject);
    }

    void Update()
    {
        if (Invincible)
        {
            invincibleTimeElapsed += Time.deltaTime;
            if (invincibleTimeElapsed > invincibilitiTime)  
            {
                Invincible = false;
                _spriteRend.color = Color.Lerp(_defaultColor, DamageColor, Time.deltaTime);
            }
            else
            {
                _spriteRend.color = Color.Lerp(DamageColor, _defaultColor, Time.deltaTime);
            }
        }
    }
}