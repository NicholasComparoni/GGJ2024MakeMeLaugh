using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TargetType {
    CHARACTER,
    PICKUP, // key
    TABLE,
    TELEPORT
}

public abstract class Target : MonoBehaviour
{
    public TargetType type;
}
