using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator animator;
    private int isWalking;
    private int isRunning;
    private int isCasting;
    private int isFalling;



    public bool IsWalking;
    public bool IsRunning;
    public bool IsCasting;
    public bool IsFalling;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalking = Animator.StringToHash("isWalking");
        isRunning = Animator.StringToHash("isRunning");
        isCasting = Animator.StringToHash("isCasting");
        isFalling = Animator.StringToHash("isFalling");
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool(isWalking, IsWalking);
        animator.SetBool(isRunning, IsRunning);
        animator.SetBool(isFalling, IsFalling);
        animator.SetBool(isCasting, IsCasting);
    }
}
