using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerNeeds : MonoBehaviour, IDamageable
{
    [SerializeField] private Need health;
    [SerializeField] private Need hunger;
    [SerializeField] private Need thirst;
    [SerializeField] private Need sleep;
    [SerializeField] private float noHungerHealthDecay;
    [SerializeField] private float noThirstHealthDecay;

    public UnityEvent onTakeDamage;

    //public Need Health { get => health; set => health = value; }
    //public Need Hunger { get => hunger; set => hunger = value; }
    //public Need Thirst { get => thirst; set => thirst = value; }
    //public Need Sleep { get => sleep; set => sleep = value; }

    private void Start()
    {
        //set start values
        health.curValue = health.startValue;
        hunger.curValue = hunger.startValue;
        sleep.curValue = sleep.startValue;
        thirst.curValue = thirst.startValue;
    }

    private void Update()
    {
        // decay needs over time
        hunger.Subtract(hunger.decayRate * Time.deltaTime);
        thirst.Subtract(thirst.decayRate * Time.deltaTime);
        sleep.Add(sleep.regenRate * Time.deltaTime);

        //decay health over time if no hunger or thirst
        if (hunger.curValue == 0.0f)
        {
            health.Subtract(noHungerHealthDecay * Time.deltaTime);
        }

        if (thirst.curValue == 0.0f)
        {
            health.Subtract(noThirstHealthDecay * Time.deltaTime);
        }

        //check if player is dead
        if(health.curValue == 0.0f)
        {
            Die();
        }

        //update ui bars
        health.uiBar.fillAmount = health.GetPercentage();
        hunger.uiBar.fillAmount = hunger.GetPercentage();
        thirst.uiBar.fillAmount = thirst.GetPercentage();
        sleep.uiBar.fillAmount = sleep.GetPercentage();

    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    public void Drink(float amount)
    {
        thirst.Add(amount);
    }

    public void Sleep(float amount)
    {
        sleep.Subtract(amount);
    }

    public void TakeDamage(int amount)
    {
        health.Subtract(amount);
        onTakeDamage?.Invoke();
    }

    public void Die()
    {
        Debug.Log("Player is dead");
    }
}

[System.Serializable]
public class Need
{
    [HideInInspector]
    public float curValue;
    public float maxValue;
    public float startValue;
    public float regenRate;
    public float decayRate;
    public Image uiBar;

    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }
}

public interface IDamageable
{
    void TakeDamage(int amount);
}
