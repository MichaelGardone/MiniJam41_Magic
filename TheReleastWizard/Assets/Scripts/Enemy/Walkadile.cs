using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Walkadile : Entity
{

    [SerializeField] Image healthImg;
    [SerializeField] Canvas canvas;

    [SerializeField] List<GameObject> targets;

    public float timeBetweenHits = 2.0f;

    public NavMeshAgent agent;

    bool playerInRange = false;
    public bool attacked = false;

    PlayerController target;

    SimpleEnemyAnim anim;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        health = maxHealth;

        targets.Add(FindObjectOfType<PlayerController>().gameObject);

        canvas.enabled = false;

        anim = GetComponent<SimpleEnemyAnim>();
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
            if (!attacked)
                StartCoroutine(Headbutt(target));
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

    IEnumerator Headbutt(PlayerController pc)
    {
        attacked = true;
        anim.Attack(); //aniamtion
        yield return new WaitForSeconds(anim.damageDelay);  
        if(playerInRange)
            pc.ModifyHealth(-hitPower);

        if (timeBetweenHits - anim.damageDelay > 0)
        {
            yield return new WaitForSeconds(timeBetweenHits - anim.damageDelay);
        }
        attacked = false;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerInRange = true;
            target = other.GetComponent<PlayerController>();
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
