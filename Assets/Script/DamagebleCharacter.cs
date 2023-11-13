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

    public GameObject[] loots;

    public bool isinvincibilitiEnable = false;

    private float invincibleTimeElapsed = 0f;

    public Color DamageColor = Color.red;

    Color _defaultColor;

    Animator animator;

    Rigidbody2D rb;

    SpriteRenderer _spriteRend;

    Collider2D physicsCollider;

    public float _health;

    public float _maxhealth;

    bool _targetable = true;

    bool _invincible = false;

    private void Start()
    {
        _health = _maxhealth;
        _spriteRend = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _defaultColor = _spriteRend.color;
    }
    void generateLoot()
    {
        var range = Random.Range(0f, 101f);
        if (loots.Length == 0)
        {
            return;
        }
        else
        {
            if (30f > range)
            {
                GameObject Loot = Instantiate(loots[1], transform.position, Quaternion.identity);
            }
            else
            {
                if (80f > range)
                {
                    GameObject Loot = Instantiate(loots[0], transform.position, Quaternion.identity);
                }
            }
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
                if (loots.Length == 0)
                {
                    animator.SetBool("Defeated", true);
                    Targetable = false;
                }
                else
                {
                    generateLoot();
                    animator.SetBool("Defeated", true);
                    Targetable = false;
                }
                
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
    void RemoveEnemy()
    {
        Destroy(gameObject);
    }

    public void damageText(float damage)
    {
        GameObject textInstance = Instantiate(healthText);

        TextMeshProUGUI textDa = textInstance.GetComponent<TextMeshProUGUI>();

        RectTransform textTransform = textInstance.GetComponent<RectTransform>();

        textTransform.transform.position = UnityEngine.Camera.main.WorldToScreenPoint(gameObject.transform.position);

        Canvas canvas = GameObject.FindObjectOfType<Canvas>();

        textTransform.SetParent(canvas.transform);

        textDa.text = damage.ToString();
    }

    public void OnHit(float damage, Vector2 knockback, string tagEnemy)
    {
        if (tagEnemy == "Enemy")
        {
            if ((!isinvincibilitiEnable || !Invincible) && enemy.tag != "Enemy")
            {                
                Health -= damage;

                damageText(damage);

                AudioManager.instance.Play("Damage");

                rb.AddForce(knockback, ForceMode2D.Impulse);
            }
        }
        else if (tagEnemy == "Player")
        {
            if (!isinvincibilitiEnable || !Invincible)
            {
                AudioManager.instance.Play("Damage");

                Health -= damage;

                damageText(damage);

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