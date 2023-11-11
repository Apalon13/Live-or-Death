using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeartSystem : MonoBehaviour
{
    public Slider slider;

    public DamagebleCharacter health;

    private void Start()
    {
        SetMaxHealth(health._maxhealth);
    }
    private void Update()
    {
        SetHealth(health._health);
    }
    public void SetMaxHealth(float health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(float health)
    {
        slider.value = health;
    }
}
