using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;  
using UnityEngine;
using System.Dynamic;

public class main : MonoBehaviour
{
    static private main S;
    static private Dictionary<eWeaponType, WeaponDefinition> WEAP_DICT;
    [Header("Inscribed")]
    public bool spawnEnemies = true;
    public GameObject[] prefabEnemy;
    public float enemySpawnPerSecond = 0.5f;
    public float enemyInsetDefault = 1.5f;
    public float gameRestartDelay = 2f;
    public GameObject prefabPowerUp;
    public WeaponDefinition[] weaponDefinitions;
    public eWeaponType[] powerUpFrequency = new eWeaponType[]
    {
        eWeaponType.blaster, eWeaponType.blaster,
        eWeaponType.spread, eWeaponType.shield
    };

    static public WeaponDefinition GET_WEAPON_DEFINITION(eWeaponType wt)
    {
        foreach (WeaponDefinition def in S.weaponDefinitions)
        {
            if (def.type == wt)
            {
                return def;
            }
        }
        return new WeaponDefinition();
    }



    private BoundsCheck bndCheck;
    void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

        WEAP_DICT = new Dictionary<eWeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions)        {
            WEAP_DICT[def.type] = def;
        }
    }

    public void SpawnEnemy()
    {
        if (!spawnEnemies)
        {
            Invoke (nameof(SpawnEnemy), 1f / enemySpawnPerSecond);
            return;
        }
        int ndx = Random.Range(0, prefabEnemy.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemy[ndx]);

        float enemyInset = enemyInsetDefault;
        if (go.GetComponent<BoundsCheck>() != null)
        {
            enemyInset = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }
        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyInset;
        float xMax = bndCheck.camWidth - enemyInset;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyInset;
        go.transform.position = pos;
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    void DelayedRestart()
    {
        Invoke(nameof(Restart), gameRestartDelay);
    }

    void Restart()
    {
        SceneManager.LoadScene("__Scene_0");
    }

    static public void HERO_DIED()
    {
        S.DelayedRestart();
    }
    
    static public void SHIP_DESTROYED(Enemy e)
    {
        if (Random.value <= e.powerUpDropChance)
        {
            int ndx = Random.Range(0, S.powerUpFrequency.Length);
            eWeaponType pUpType = S.powerUpFrequency[ndx];

            GameObject go = Instantiate(S.prefabPowerUp);
            PowerUp pUp = go.GetComponent<PowerUp>();
            pUp.SetType(pUpType);
            pUp.transform.position = e.transform.position;
        }
    }
    // Start is called before the first frame update
}
