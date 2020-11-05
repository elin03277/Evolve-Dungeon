using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Player reference with access speed, health, damage taken (speed, damageTaken, damageHealed)
// Enemy reference with access to enemy health (enemySetHealth at instantiation)
// Sword Projectile reference with access to sword damage (swordSetDamage at instantiation)
// Spawner reference with access to amount spawned
// Enemy rgb values based on strength
// Level size?

//Player(Health, Attack) Player: 100, Shooting: 20
//Heal(Amount healed) Player: 20
//Buffs(Spawn) Enemy: 40% / 0.4
//Level length(amount to kill) n/a
//Enemy(Health, Attack, Spawn) Enemy: 100, Player: 20, 40% / 0.4


public class ParameterManager : MonoBehaviour
{
    public Player player;
    public Shooting swordShot;
    public HealthBar healthBar;
    public LevelGenerator levelGenerator;
    public GameManager gameManager;
    public SaveManager saveManager;

    public float setDamage;
    public float setHealth;
    public float setHeal;
    public float setPotionSpawn;
    public int setRoomLength;
    public float setEnemyHealth;
    public float setEnemyDamage;
    public float setEnemySpawner;
    // public float setAttackPotionLength;
    // public float setSpeedPotionLength;

    // Start is called before the first frame update
    void Start()
    {
        //Initialize();

        // setAttackPotionLength = 100f;
        // setSpeedPotionLength = 100f;
    }

    public void Initialize()
    {
        setHealth = 100f;
        setDamage = 25f;
        setHeal = 20f;
        setPotionSpawn = 0.4f;
        setRoomLength = 3;
        setEnemyHealth = 100f;
        setEnemyDamage = 20f;
        setEnemySpawner = 0.4f;

        saveManager.LoadSave();
        gameManager.enemiesNeeded = setRoomLength;
    }

    public void SetGame() { 
        player = FindObjectOfType<Player>();
        swordShot = FindObjectOfType<Shooting>();
        player.maxHealth = setHealth;
        healthBar.SetMaxHealth(setHealth);
        //healthBar.SetHealth(setHealth);
        swordShot.swordSetDamage = setDamage;
        player.damageHealed = setHeal;
        levelGenerator.enemySetPotionChance = setPotionSpawn;
        levelGenerator.enemySetHealth = setEnemyHealth;            
        player.damageTaken = setEnemyDamage;
        levelGenerator.enemySpawnerChance = setEnemySpawner;
        gameManager.enemiesNeeded = setRoomLength;

        // player.attackPotionSetTimer = setAttackPotionLength;
        // player.speedPotionSetTimer = setSpeedPotionLength;
        // enemy.GetComponent<Enemy>().currentEnemyHealth = enemyHealth;

        // FindObjectOfType<AudioManager>().ChangeMusic("Theme");
    }
}
