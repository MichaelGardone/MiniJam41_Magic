using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SimpleEnemyAnim : MonoBehaviour
{
    public float damageDelay = 1;
    [SerializeField] float VelocityThreshold;
    [SerializeField] string ControlIntName;
    private int _ControlIntName;
    private Animator animator;
    private NavMeshAgent navMeshAgent;

    private enum EnemyType {Walkadile, Crabbo };
    private EnemyType enemyType;
    private Walkadile walkadile;
    bool attackedPreviousFrame;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        _ControlIntName = Animator.StringToHash("AnimState");

        if (GetComponent<Walkadile>()) {
            enemyType = EnemyType.Walkadile;
            walkadile = GetComponent<Walkadile>();
            attackedPreviousFrame = walkadile.attacked;
        }
        //if (GetComponent<Walkadile>()) { enemyType = EnemyType.Walkadile; } crab when crab is done

        
    }

    public void SetAnimState(int state) //0 is idle, 1 is walking, 2 is attacking
    {
        if (state == 2)// if the attack anim is requested, reset to previous state after a short duration
        {
            StartCoroutine(ResetToPreviousState(animator.GetInteger(_ControlIntName)));
        }
        animator.SetInteger(_ControlIntName, state);
        //Debug.Log(ControlIntName);
        

    }

    private IEnumerator ResetToPreviousState(int previousState)
    {
        yield return new WaitForSeconds(.5f);
        animator.SetInteger(_ControlIntName, previousState);
    }

    // Update is called once per frame
    void Update()
    {
        
        if(navMeshAgent.velocity.magnitude > VelocityThreshold)
        {
            
            SetAnimState(1);
        }
        else
        {
            SetAnimState(0);
        }
        attackedPreviousFrame = walkadile.attacked;
    }

    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
}
