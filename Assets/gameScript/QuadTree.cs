using System.Collections;
using System.Collections.Generic;
using UnityEngine;



class block
{
    public Vector2 pos { get; private set; }
    public float width {  get; private set; }
    public block(float x, float y, float w, float h)
    {

    }
}
public class QuadTree : MonoBehaviour
{
    private block centreBlock;
    public QuadTree(float w, float h, int capacity)
    {
        centreBlock = new block(0, 0, w, h);
    }

}
