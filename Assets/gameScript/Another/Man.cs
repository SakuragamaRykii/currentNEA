using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Man : MonoBehaviour
{
    public static otherQT qt;
    // Start is called before the first frame update
    void Awake()
    {
        qt = new otherQT(new Rect(-5, -5, 10, 10));
        qt.split();
    }

    // Update is called once per frame

}
