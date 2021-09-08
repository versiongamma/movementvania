using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMath : MonoBehaviour {
     public static Vector2 CircleVectorLerp(Vector2 position, Vector2 target, float x) {
        return new Vector2(x, target.y - Mathf.Sqrt((target.y*target.y) - (2 * position.y * target.y) + (position.x * position.x) + (position.y * position.y) + (2 * x * target.x) - (2 * position.x * target.x) - x * x));
    }
}
