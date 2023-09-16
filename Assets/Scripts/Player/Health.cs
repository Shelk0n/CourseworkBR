using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Unity.VisualScripting;

public class Health : MonoBehaviour
{
    public delegate void OnHealthChange(int maxHealth, int newHealth);
    public event OnHealthChange onHealthChangeEvent;
    public delegate void OnMaxHealthChange(int oldHealth, int newMaxHealth);
    public event OnMaxHealthChange onMaxHealthChangeEvent;
    public delegate void OnManaChange(int maxMana, int newMana);
    public event OnManaChange onManaChangeEvent;
    public delegate void OnMaxManaChange(int oldMana, int newMaxMana);
    public event OnMaxManaChange onMaxManaChangeEvent;
    public delegate void OnShieldChange(int maxShield, int newShield);
    public event OnManaChange onShieldChangeEvent;
    public delegate void OnMaxShieldChange(int oldShield, int newMaxShield);
    public event OnMaxShieldChange onMaxShieldChangeEvent;

    PhotonView view;
    Inventory inventory = Inventory.instance;

    private void Start()
    {
        inventory.OnHealthChanged(this);
        view = GetComponent<PhotonView>();
        if (!view.IsMine)
            Destroy(this); 
        else
            GameInterface.InitializeHealth(this);
    }

    public int hp = 100;
    public int maxHealth = 100;
    public int mana = 100;
    public int maxMana = 100;
    public int shield = 100;
    public int maxShield = 100;

    public void ChangeHealth(int amount)
    {
        this.hp += amount;
        this.onHealthChangeEvent?.Invoke( maxHealth, hp);
        inventory.OnHealthChanged(this);
    }
    public void ChangeMaxHealth(int amount)
    {
        if (maxHealth > maxHealth + amount && -amount > maxHealth - hp)
            hp = (maxHealth + amount);
        this.maxHealth += amount;
        this.onMaxHealthChangeEvent?.Invoke(hp, maxHealth);
        inventory.OnHealthChanged(this);
    }
    public void ChangeMana(int amount)
    {
        this.mana += amount;
        this.onManaChangeEvent?.Invoke(maxMana, mana);
        inventory.OnHealthChanged(this);
    }
    public void ChangeMaxMana(int amount)
    {
        if (maxMana > maxMana + amount && -amount > maxMana - mana)
            mana = (maxMana + amount);
        this.maxMana += amount;
        this.onMaxManaChangeEvent?.Invoke(mana, maxMana);
        inventory.OnHealthChanged(this);
    }
    public void ChangeShield(int amount)
    {
        this.shield += amount;
        this.onShieldChangeEvent?.Invoke(maxShield, shield);
        inventory.OnHealthChanged(this);
    }
    public void ChangeMaxShield(int amount)
    {
        if (maxShield > maxShield + amount && -amount > maxShield - shield)
            shield = (maxShield + amount);
        this.maxShield += amount;
        this.onMaxShieldChangeEvent?.Invoke(shield, maxShield);
        inventory.OnHealthChanged(this);
    }
    public bool IsEnoughHealth(int amount)
    {
        return hp > amount;
    }
    public void SetMaxHealth(int healthAmount, int manaAmount, int shieldAmount)
    {
        ChangeMaxHealth(healthAmount - maxHealth);
        ChangeMaxMana(manaAmount - maxMana);
        ChangeMaxShield(shieldAmount - maxShield);
        maxHealth = healthAmount;
        maxMana = manaAmount;
        maxShield = shieldAmount;
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Escape))
            ChangeMaxShield(20);
        if (Input.GetKeyDown(KeyCode.I))
            ChangeMaxShield(-20);
        if (Input.GetKeyDown(KeyCode.P))
            ChangeShield(-10);
        if (Input.GetKeyDown(KeyCode.O))
            ChangeShield(10);
    }
}
