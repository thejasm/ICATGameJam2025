using System.Collections;
using System.Collections.Generic;
using TopDownCharacter2D.Attacks;
using TopDownCharacter2D.Attacks.Range;
using TopDownCharacter2D.Controllers;
using TopDownCharacter2D.Stats;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;


public class statController: MonoBehaviour {
    //Player Stats
    public GameObject player;
    public RangedAttackConfig attackConfig;
    public float movementSpeed;
    public float fireRate;
    public float projectileSize;
    public float accuracy;
    public float damage;
    public float critChance;
    public float critMultiplier;
    public float projectileSpeed;
    public float range;
    public int maxHealth;
    public float armor;
    public float evasionChance;

    //enemy Stats
    public float knockback;
    public float statusDuration;
    public float enemyDamage;



    private void Start()
    {
        StatUpdate();
    }

    private void OnValidate()
    {
        StatUpdate();
    }
    void StatUpdate()
    {
        if (player == null)
        {
            Debug.LogError("Player GameObject is not assigned!");
            return;
        }

        if (attackConfig == null)
        {
            Debug.LogError("AttackConfig is not assigned!");
            return;
        }

        CharacterStats newStats = new CharacterStats();
        newStats.maxHealth = maxHealth;
        newStats.speed = movementSpeed;

        var characterStatsHandler = player.GetComponent<CharacterStatsHandler>();
        if (characterStatsHandler != null)
        {
            characterStatsHandler.UpdateStats((a, b) => b, newStats);
            Debug.Log("Updated character stats: " + newStats);
        }
        else
        {
            Debug.LogError("CharacterStatsHandler component is missing from the player GameObject!");
        }

        attackConfig.spread = accuracy;
        attackConfig.delay = fireRate;
        attackConfig.size = projectileSize;
        attackConfig.power = damage;
        attackConfig.speed = projectileSpeed;

        Debug.Log("Updated attack config: " + attackConfig);
    }


public void ApplyCard(Card card) {
        movementSpeed = Mathf.Max(1, movementSpeed + card.movementSpeedModifier);
        fireRate = Mathf.Max(0.05f, fireRate + card.fireRateModifier);
        projectileSize = Mathf.Max(0.1f, projectileSize + card.projectileSizeModifier);
        accuracy = Mathf.Max(0, accuracy + card.accuracyModifier);
        damage = Mathf.Max(1, damage + card.damageModifier);
        critChance = Mathf.Max(0, critChance + card.critChanceModifier);
        critMultiplier = Mathf.Max(1, critMultiplier + card.critMultiplierModifier);
        projectileSpeed = Mathf.Max(1, projectileSpeed + card.projectileSpeedModifier);
        range = Mathf.Max(1, range + card.rangeModifier);
        maxHealth = Mathf.Max(1, maxHealth + card.maxHealthModifier);
        armor = Mathf.Max(0, armor + card.armorModifier);
        evasionChance = Mathf.Max(0, evasionChance + card.evasionChanceModifier);

        StatUpdate();
    }

    public void RemoveCard(Card card) {
        movementSpeed -= card.movementSpeedModifier;
        fireRate /= card.fireRateModifier;
        projectileSize -= card.projectileSizeModifier;
        accuracy -= card.accuracyModifier;
        damage -= card.damageModifier;
        critChance -= card.critChanceModifier;
        critMultiplier -= card.critMultiplierModifier;
        projectileSpeed -= card.projectileSpeedModifier;
        range -= card.rangeModifier;
        maxHealth -= card.maxHealthModifier;
        armor -= card.armorModifier;
        evasionChance -= card.evasionChanceModifier;

        StatUpdate();
    }

}

