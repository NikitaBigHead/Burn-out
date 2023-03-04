using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomAttack : Attack
{
    [SerializeField]
    WaveSlashProjectile waveSlash;

    private void FixedUpdate()
    {
        if (isAttacked)
        {
            spriteRendererPan.color = Color.red;
            animator.Play(animation, layer);
            isCanAttack = false;
            collider.enabled = true;
            isAttacked = false;
            Invoke("disableCollider", timeAttack);
            Instantiate(waveSlash).Launch(this.transform.position, this.transform.rotation, 0.1f, 10f);
        }
    }
}
