using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_4 : Enemy
{
    [Header("Enemy_4 Inscribed Fields")]
    public float duration = 4f;

    private EnemyShield[] allShields;
    private EnemyShield  thisShield;

    // Start is called before the first frame update
    void Start()
    {
        allShieldsd = GetComponentsInChildren<EnemyShield>();
        thisShield = GetComponent<EnemyShield>();
        
    }

    public override void Move()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        GameObject otherGO = coll.gameObject;
        ProjectileHero p = otherGO.GetComponent<ProjectileHero>();
        if (bndCheck.isOnScreen)
        {
            GameObject hitGO = coll.contacts[0].thisCollider.gameObject;
            if (hitGO == otherGO) hitGO = coll.contacts[0].otherCollider.gameObject;
            float dmg = main.GET_WEAPON_DEFINITION(p.type).damageOnHit;

            bool shieldFound = false;
            foreach (EnemyShield es in allShields)
            {
                if (es.gameObject == hitGO)
                {
                    es.TakeDamage(dmg);
                    shieldFound = true;
                }
            }

            if (!shieldFound) thisShield.TakeDamage(dmg);
            if(thisShield.isActive) return;
            if(!calledShipDestroyed) 
            {
                main.SHIP_DESTROYED(this);
                calledShipDestroyed = true;
            }

            Destroy(gameObject);
         } else
        {
            Debug.Log("Enemy hit by non-ProjectileHero: " + otherGO.name);
         }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
