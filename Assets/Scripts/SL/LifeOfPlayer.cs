using System;
using SL;
using UnityEngine;
using UnityEngine.UI;

public class LifeOfPlayer : MonoBehaviour, ILifeOfPlayer
{
    [SerializeField] private int totalLife;
    [SerializeField] private bool isAlive = true;
    [SerializeField] private Slider sliderLife;
    
    private float currentLife;
    void Start()
    {
        ServiceLocator.Instance.RegisterService<ILifeOfPlayer>(this);
        currentLife = totalLife;
        UpdateSlider();
    }

    private void OnDestroy()
    {
        ServiceLocator.Instance.RemoveService<ILifeOfPlayer>();
    }

    public void GetDamage(float damage)
    {
        currentLife -= damage;
        UpdateSlider();
        if (currentLife <= 0)
        {
            isAlive = false;
        }
    }

    private void UpdateSlider()
    {
        sliderLife.value = currentLife / totalLife;
    }

    public bool IsAlive()
    {
        return isAlive;
    }
}