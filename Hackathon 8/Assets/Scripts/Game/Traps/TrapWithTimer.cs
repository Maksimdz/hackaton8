using UnityEngine;

public class TrapWithTimer : LevelObjectBehaviour
{
    public Animator animator;

    protected override bool EnableCollidersOnActivate => false;

    public override void Activate()
    {
        base.Activate();

        animator.enabled = true;
    }

    private void OnStartActiveAnimation()
    {
        EnableColliders(true);
    }

    private void OnStartIdleAnimation()
    {
        EnableColliders(false);
    }
}
