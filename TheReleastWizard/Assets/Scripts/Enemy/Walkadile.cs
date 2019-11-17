using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Walkadile : Entity
{
    [SerializeField] List<GameObject> targets;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targets[0].transform.position);

        if (health <= 0)
            Destroy(gameObject);
    }
}
