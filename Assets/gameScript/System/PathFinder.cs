using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    // Start is called before the first frame update
   // float x, y;
    [SerializeField] private GameObject targetObj;//probably the player
    private point startPoint;
    

    void Awake()
    {
    }

    // Update is called once per frame
    void Update()
    {
        startPoint = new point(GetComponent<Entity>().hitbox);
        FindPath();
    }

    public void FindPath()
    {
        DynList<point> visited = new DynList<point>(),
            toVisit = new DynList<point>(); //known as open and closed lists
        Up();
        Right();
        Down();
        Left();
    }





    private void Up()
    {
        //Debug.Log(startPoint.c);
        point up = new point(new Rect(startPoint.c.x - startPoint.area.width/2,
                                       startPoint.c.y + startPoint.area.height/2,
                                       startPoint.area.width, 
                                       startPoint.area.height    ));
        //Debug.Log(up.c);
        //Debug.Log("size= " + startPoint.area.width + ", " + startPoint.area.height);
 
        GameObject sc = up.scan();
        if (sc == null) Debug.Log("Up : clear");
        else Debug.Log("Up : WALL");
    }
    private void Right()
    {
        point right = new point(new Rect(startPoint.c.x + startPoint.area.width / 2,
                               startPoint.c.y - startPoint.area.height / 2,
                               startPoint.area.width,
                               startPoint.area.height));
        GameObject sc = right.scan();
        if (sc == null) Debug.Log("Right : clear");
        else Debug.Log("Right : WALL");
    }
    private void Down()
    {
        point Down = new point(new Rect(startPoint.c.x - startPoint.area.width/2,
                               startPoint.c.y - startPoint.area.height,
                               startPoint.area.width,
                               startPoint.area.height));
        GameObject sc = Down.scan();
        if (sc == null) Debug.Log("Down : clear");
        else Debug.Log("Down : WALL");
    }
    private void Left()
    {
        point Left = new point(new Rect(startPoint.c.x - startPoint.area.width,
                               startPoint.c.y - startPoint.area.height/2,
                               startPoint.area.width,
                               startPoint.area.height));
        GameObject sc = Left.scan();
        if (sc == null) Debug.Log("Left : clear");
        else Debug.Log("Left : WALL");
    }

 


        

}
class point
{
    public Rect area { get; private set; }
    public Vector2 c {  get ; private set; } //centre position of the point
    public point(Rect r)
    {
        area = r;
        c = new Vector2 (r.x+r.width/2, r.y+r.height/2);
        //Debug.Log(c);
    }
    public GameObject scan(GameObject target = null)
    {
        foreach(Entity e in GameObject.FindObjectsOfType<Entity>())
        {
            if (area.Overlaps(e.hitbox))
            {
                Debug.Log("has");
                if (e.gameObject.tag.Equals("Wall"))
                {
                    return e.gameObject;
                }
                if (target != null && e.gameObject.tag.Equals(target.tag))
                {
                    return e.gameObject;
                }

            }

        }
        return null;
    }
}


