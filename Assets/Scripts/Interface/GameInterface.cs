using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO.IsolatedStorage;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameInterface : MonoBehaviour
{
    private static Health health;
    [SerializeField]private int maxHealth = 100;
    [SerializeField] private int currentHealth = 100;
    [SerializeField] private int currentMana = 100;
    [SerializeField] private int maxMana = 100;
    [SerializeField] private int currentShield = 100;
    [SerializeField] private int maxShield = 100;

    [SerializeField] private Text healthValue;
    [SerializeField] private Text manaValue;
    [SerializeField] private Text shieldValue;

    [SerializeField] private Image HealthBar;
    [SerializeField] private Image ManaBar;
    [SerializeField] private Image ShieldBar;

    private static GameInterface instance;
    public static void InitializeHealth(Health hp)
    {
        health = hp;
        health.onHealthChangeEvent += instance.OnHealthValueChanged;
        health.onMaxHealthChangeEvent += instance.OnMaxHealthValueChanged;
        health.onManaChangeEvent += instance.OnManaValueChanged;
        health.onMaxManaChangeEvent += instance.OnMaxManaValueChanged;
        health.onShieldChangeEvent += instance.OnShieldValueChanged;
        health.onMaxShieldChangeEvent += instance.OnMaxShieldValueChanged;
    }
    IEnumerator SlowChange(int oldValue, int newValue, int maxValue, Image bar, float width)
    {
        if(oldValue < newValue)
            for(int i = oldValue; i < newValue; i++)
            {
                yield return new WaitForSeconds(0.01f);
                bar.fillAmount = (float)i / (float)maxValue;
                bar.transform.position = new Vector3(bar.transform.position.x + width / (float)maxValue, bar.transform.position.y, 0);
            }
        else
            for (int i = oldValue; i > newValue; i--)
            {
                yield return new WaitForSeconds(0.01f);
                bar.fillAmount = (float)i / (float)maxValue;
                bar.transform.position = new Vector3(bar.transform.position.x - width / (float)maxValue, bar.transform.position.y, 0);
            }
    }
    IEnumerator SlowMaxChange(int oldMaxValue, int newMaxValue, int currentValue, Image bar, float width)
    {
        if (oldMaxValue < newMaxValue)
            for (int i = oldMaxValue + 1; i < newMaxValue + 2; i++)
            {
                yield return new WaitForSeconds(0.01f);
                float temp = bar.fillAmount;
                bar.fillAmount = (float)currentValue / (float)i;
                bar.transform.position = new Vector3(bar.transform.position.x - width * (temp - bar.fillAmount), bar.transform.position.y, 0);
            }
        else if (oldMaxValue > newMaxValue)
            for (int i = oldMaxValue + 1; i > newMaxValue; i--)
            {
                yield return new WaitForSeconds(0.01f);
                float temp = bar.fillAmount;
                bar.fillAmount = (float)currentValue / (float)i;
                bar.transform.position = new Vector3(bar.transform.position.x - width * (temp - bar.fillAmount), bar.transform.position.y, 0);
            }

    }
    private void OnHealthValueChanged(int maxHealth, int newHealth)
    {
        StartCoroutine(SlowChange(currentHealth, newHealth, maxHealth, HealthBar, 310f));
        currentHealth = newHealth;
        healthValue.text = currentHealth + "/" + maxHealth;
    }
    private void OnMaxHealthValueChanged(int currentHp, int newMaxHealth)
    {
        currentHealth = currentHp;
        if (currentHp <= newMaxHealth)
            StartCoroutine(SlowMaxChange(maxHealth, newMaxHealth, currentHealth, HealthBar, 310f));
        maxHealth = newMaxHealth;
        healthValue.text = currentHealth + "/" + maxHealth;
    }
    private void OnManaValueChanged(int maxMana, int newMana)
    {
        StartCoroutine(SlowChange(currentMana, newMana, maxMana, ManaBar, 240f));
        currentMana = newMana;
        manaValue.text = currentMana + "/" + maxMana;
    }
    private void OnMaxManaValueChanged(int currentMp, int newMaxMana)
    {
        currentMana = currentMp;
        if (currentMana <= newMaxMana)
            StartCoroutine(SlowMaxChange(maxMana, newMaxMana, currentMp, ManaBar, 240f));
        maxMana = newMaxMana;
        manaValue.text = currentMana + "/" + maxMana;
    }
    private void OnShieldValueChanged(int maxShield, int newShield)
    {
        StartCoroutine(SlowChange(currentShield, newShield, maxShield, ShieldBar, -237f));
        currentShield = newShield;
        shieldValue.text = currentShield + "/" + maxShield;
    }
    private void OnMaxShieldValueChanged(int currentSd, int newMaxShield)
    {
        currentShield = currentSd;
        if (currentShield <= newMaxShield)
            StartCoroutine(SlowMaxChange(maxShield, newMaxShield, currentShield, ShieldBar, -237f));
        maxShield = newMaxShield;
        shieldValue.text = currentShield + "/" + maxShield;
    }
    private void OnDestroy()
    {
        health.onHealthChangeEvent -= instance.OnHealthValueChanged;
        health.onMaxHealthChangeEvent -= instance.OnMaxHealthValueChanged;
        health.onManaChangeEvent -= instance.OnManaValueChanged;
        health.onMaxManaChangeEvent -= instance.OnMaxManaValueChanged;
        health.onShieldChangeEvent -= instance.OnShieldValueChanged;
        health.onMaxShieldChangeEvent -= instance.OnMaxShieldValueChanged;
    }
    private void Awake()
    {
        instance = this;
    }
}
