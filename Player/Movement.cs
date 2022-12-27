using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    private CharacterController cc;
    private Animator animator;
    [System.Serializable]
    public class AnimationString
    {
        public string forward = "forward";
        public string strafe = "strafe";
        public string sprint = "sprint";
        public string aim = "aim";
        public string pull = "pullString";
        public string fire = "fire";

    }

    [SerializeField]
    private AnimationString animationString;

    void Start()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
    }

    public void AnimatorCharacter(float forward, float strafe)
    {
        animator.SetFloat(animationString.forward, forward);
        animator.SetFloat(animationString.strafe, strafe);
    }

    public void SprintCharacter(bool isSprinting)
    {
        animator.SetBool(animationString.sprint, isSprinting);
    }

    public void CharacterAim(bool isAiming)
    {
        animator.SetBool(animationString.aim, isAiming);
    }
    public void CharacterPullString(bool pull)
    {
        animator.SetBool(animationString.pull, pull);
    }
    public void CharacterFireArrow()
    {
        animator.SetTrigger(animationString.fire);
    }
 
}
