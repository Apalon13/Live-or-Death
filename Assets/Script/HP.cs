using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class HP : MonoBehaviour, IDamageable
{
    [SerializeField] private bool isDead = false;
    public UnityEvent<GameObject> OnHitWithReference, OnDealWithReference;
    public bool disableSimulation = false;
    public GameObject healthText;
    public float invincibilitiTime = 0.25f;
    public bool isinvincibilitiEnable = false;
    private float invincibleTimeElapsed = 0f;
    public Color DamageColor = Color.red;
    Color _defaultColor;
    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer _spriteRend;
    Collider2D physicsCollider;
    public MainStats st;
    public float _health;
    public float _maxhealth;
    bool _targetable = true;
    bool _invincible = false;
    private void Start()
    {
        if(st != null)
        {
            _maxhealth = st.hp;
        }
        _health = _maxhealth;
        _spriteRend = GetComponent<SpriteRenderer>();
        physicsCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _defaultColor = _spriteRend.color;
    }
    public float Health
    {
        set
        {
            _health = value;

            if (_health <= 0)
            {
                Targetable = false;
                isDead = true;
                OnDealWithReference?.Invoke(gameObject);
                Destroy(gameObject);
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
    public void OnHit(float damage, GameObject sender)
    {
        if (isDead)
        {
            return;
        }
        if (sender.layer == gameObject.layer)
        {
            return;
        }
        if (!isinvincibilitiEnable || !Invincible)
        {   
            OnHitWithReference?.Invoke(sender);
            Health -= damage;
            damageText(damage);
            AudioManager.instance.Play("Damage");
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
