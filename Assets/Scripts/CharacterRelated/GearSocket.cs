using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum animatorLayerDirectional
{
    attack_back = 1 << 1,
    attack_front = 1 << 2,
    attack_left = 1 << 3,
    attack_right = 1 << 4,
    idle_back = 1 << 5,
    idle_front = 1 << 6,
    idle_left = 1 << 7,
    idle_right = 1 << 8,
    walk_back = 1 << 9,
    walk_front = 1 << 10,
    walk_left = 1 << 11,
    walk_right = 1 << 12
}

public class GearSocket : MonoBehaviour
{
    public Animator MyAnimator { get; set; }

    private Animator parentAnimator;
    protected SpriteRenderer spriteRenderer;

    private AnimatorOverrideController animatorOverrideController;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        parentAnimator = GetComponentInParent<Animator>();
        MyAnimator = GetComponent<Animator>();

        animatorOverrideController = new AnimatorOverrideController(MyAnimator.runtimeAnimatorController);

        MyAnimator.runtimeAnimatorController = animatorOverrideController;
    }

    public virtual void SetXandY(float x, float y)
    {
        MyAnimator.SetFloat("x", x);
        MyAnimator.SetFloat("y", y);
    }

    public void ActivateLayer(string layerName)
    {
        for (int i = 0; i < MyAnimator.layerCount; i++)
        {
            MyAnimator.SetLayerWeight(i, 0);
        }

        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
    }
    public void Equip(AnimationClip[] animations)
    {
        spriteRenderer.color = Color.white;

        animatorOverrideController[animatorLayerDirectional.attack_back.ToString()] = animations[0];
        animatorOverrideController[animatorLayerDirectional.attack_front.ToString()] = animations[1];
        animatorOverrideController[animatorLayerDirectional.attack_left.ToString()] = animations[2];
        animatorOverrideController[animatorLayerDirectional.attack_right.ToString()] = animations[3];

        animatorOverrideController[animatorLayerDirectional.idle_back.ToString()] = animations[4];
        animatorOverrideController[animatorLayerDirectional.idle_front.ToString()] = animations[5];
        animatorOverrideController[animatorLayerDirectional.idle_left.ToString()] = animations[6];
        animatorOverrideController[animatorLayerDirectional.idle_right.ToString()] = animations[7];

        animatorOverrideController[animatorLayerDirectional.walk_back.ToString()] = animations[8];
        animatorOverrideController[animatorLayerDirectional.walk_front.ToString()] = animations[9];
        animatorOverrideController[animatorLayerDirectional.walk_left.ToString()] = animations[10];
        animatorOverrideController[animatorLayerDirectional.walk_right.ToString()] = animations[11];
        
    }

    public void Dequip()
    {
        animatorOverrideController[animatorLayerDirectional.attack_back.ToString()] = null;
        animatorOverrideController[animatorLayerDirectional.attack_front.ToString()] = null;
        animatorOverrideController[animatorLayerDirectional.attack_left.ToString()] = null;
        animatorOverrideController[animatorLayerDirectional.attack_right.ToString()] = null;

        animatorOverrideController[animatorLayerDirectional.idle_back.ToString()] = null;
        animatorOverrideController[animatorLayerDirectional.idle_front.ToString()] = null;
        animatorOverrideController[animatorLayerDirectional.idle_left.ToString()] = null;
        animatorOverrideController[animatorLayerDirectional.idle_right.ToString()] = null;

        animatorOverrideController[animatorLayerDirectional.walk_back.ToString()] = null;
        animatorOverrideController[animatorLayerDirectional.walk_front.ToString()] = null;
        animatorOverrideController[animatorLayerDirectional.walk_left.ToString()] = null;
        animatorOverrideController[animatorLayerDirectional.walk_right.ToString()] = null;

        Color c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;
    }
}
