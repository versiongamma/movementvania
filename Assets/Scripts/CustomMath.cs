using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Holds custom math functions that might be required in the engine
public class CustomMath : MonoBehaviour {

    /* 
    Returns the corresponding Vector2, calculating the y value for a given [x] coordinate, of the function that generates
    a circle with center [target] and that intercets [position].

    if position is (a,b) and target is (c,d), then this function returns the negative (x,y) values 
    (as there are 2 values for (x,y) for any given point) from the equation:

    (x - c)^2 + (x - d)^2 = (a - c)^2 + (b - d)^2
    
    i.e, a circle with center (c, d) that has a radius of the distance from (c,d) to (a,b)

    [ | | |B| | | ]
    [ | | | | | | ]
    [ |A| | | | | ]
    [ | | | | | | ]

    Say A is our [position], and B is our [target], the result of CircleVectorLerp will be the circular arc between those 2 
    points, given as a point in space for a given [x].

    [ | | |B| | | ]
    [X| | | | | |X]
    [ |X| | | |X| ]
    [ | |X|X|X| | ]

    The X's would be the Vectors returned for any given [x] position that is on that circle. 
    Any [x] positions that are not on that circle, will return a vector of (x, NaN)

    Is this too much information? Maybe, but I'm proud of it goddammit, and if I had to suffer through relearning functional algebra, so do you
    */
     public static Vector2 CircleVectorLerp(Vector2 position, Vector2 target, float x) {
        return new Vector2(x, target.y - Mathf.Sqrt((target.y*target.y) - (2 * position.y * target.y) + (position.x * position.x) + (position.y * position.y) + (2 * x * target.x) - (2 * position.x * target.x) - x * x));
    }
}
