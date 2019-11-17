using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabAnim : MonoBehaviour
{
    // Start is called before the first frame update
    // Start is called before the first frame update
    private Animator animator;
    private int State; //0 = idle, 1= walking, 2= attacking

    [Range(0, 2)]
    public int PublicState;
    void Start()
    {
        animator = GetComponent<Animator>();
        State = Animator.StringToHash("AnimState");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetInteger(State, PublicState);
    }
}
