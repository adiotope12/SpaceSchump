using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eWeaponType
{
    none, // The default / no weapon
    blaster, // A simple blaster
    spread, // Two shots simultaneously
    phaser, // Shots that move in waves
    missile, // Homing missiles
    laser, // Damage over time
    shield // Raise the player's defense
}

[System.Serializable]
public class WeaponDefinition
{
    public eWeaponType type = eWeaponType.none;
    public string letter; // The letter to show on the power-up
    public Color powerUpColor = Color.white; // The color of the power-up & letter
    
    public GameObject weaponModelPrefab; // The prefab for the weapon model
    public GameObject projectilePrefab; // The prefab for this weapon's projectiles
    public Color projectileColor = Color.white; // The color of the projectiles
    public float damageOnHit = 0; // The amount of damage caused
    public float damagePerSec = 0; // Damage per second (laser)
    public float delayBetweenShots = 0;
    public float velocity = 50; // The speed of the projectile
}
public class Weapon : MonoBehaviour
{

}
