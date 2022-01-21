using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Minion : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    [SerializeField] private NavMeshAgent nav;
    [SerializeField] private ParticleSystem explosionFX;

    private int currentHealth;
    public bool IsDead;

    private LolTower closestTower;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        var allTowers = TowersManager.AllTowers;
        closestTower = allTowers.OrderBy(o => (transform.position - o.transform.position).sqrMagnitude).First();
        nav.SetDestination(closestTower.transform.position);

    }

    // Update is called once per frame
    void Update()
    {
        //nav.Move(closestTower.transform.position);
        //transform.position += nav.velocity * Time.deltaTime;
    }

    public bool GetHit(int damage = 1)
    {
        IsDead = false;
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            IsDead = true;
            FindObjectOfType<LolTower>().LaserFX();
            ExplosionFX();
            Die();
        }

        return IsDead;
    }

    private void ExplosionFX()
    {
        Instantiate(explosionFX, transform.position, Quaternion.identity);
        Destroy(explosionFX, 3f);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
