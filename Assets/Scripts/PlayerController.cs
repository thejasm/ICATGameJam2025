using System.Collections;
using System.Collections.Generic;
using TopDownCharacter2D.Controllers;
using UnityEngine;

public class PlayerController : TopDownCharacterController {
    //Player Stats
    public float movementSpeed;
    public float fireRate;
    public float accuracy;
    public float critChance;
    public float multiplier;
    public float projectileSpeed;
    public float range;
    public float maxHealth;
    public float armor;
    public float evasionChance;
    public float knockback;
    public float statusDuration;

    // Start is called before the first frame update
    void Start(){
    }

    void FixedUpdate() {
    }

}
