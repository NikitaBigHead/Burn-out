using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpersTarget : MonoBehaviour
{
#if UNITY_EDITOR 
    private void OnDrawGizmos() {
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawSphere(transform.position, 1);
        Gizmos.DrawIcon(transform.position, "Assets/Sprites/OtherSprites/JumperTargetIcon.png", true); 
    }
#endif
}
