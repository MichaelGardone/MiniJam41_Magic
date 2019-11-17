using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Walkadile : Entity
{

    [SerializeField] Image healthImg;
    [SerializeField] Canvas canvas;

    [SerializeField] List<GameObject> targets;

    public NavMeshAgent agent;

    bool playerInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        health = maxHealth;

        targets.Add(FindObjectOfType<PlayerController>().gameObject);

        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(targets[0].transform.position);

        if (GetHealthAsPercent() != 1f)
            canvas.enabled = true;

        healthImg.fillAmount = GetHealthAsPercent();

        if (playerInRange)
        {
            agent.isStopped = true;
        }
        else
        {
            agent.isStopped = false;
        }

        // Do this last or crash it all
        if (health <= 0)
        {
            offender.GetComponent<PlayerController>().xp += 5;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRange = false;
        }
    }

}
