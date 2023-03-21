using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSpawnAttribute : PropertyAttribute
{
    public bool enumMode = true;

    public Vector3 position = Vector3.zero;
    public SceneLoader.Position enumPosition = SceneLoader.Position.Center;
}
