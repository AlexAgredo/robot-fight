using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void Walk(float speedw)
    {
        anim.SetFloat(AnimationTags.Walk, Mathf.Abs(speedw)  );
    }

    public void punchR()
    {
        anim.SetTrigger(AnimationTags.PRight_Trigger);
    }

    public void punchL()
    {
        anim.SetTrigger(AnimationTags.PRLeft_Trigger);
    }

    public void Hurt()
    {
        anim.SetTrigger(AnimationTags.Hurt_Trigger);
    }

}
