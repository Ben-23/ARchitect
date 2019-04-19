using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class he : MonoBehaviour
{
    public GameObject obj;
    int lcount = 0,rcount=0;
    Vector3 v;
    public void Left()
    {
        lcount++;
    }
    public void right()
    {
        rcount++;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if(lcount>0)
        {
            v = Vector3.left*lcount;
            obj.transform.rotation= Quaternion.LookRotation(v);
        }
        if(rcount>0)
        {
            v = Vector3.right*rcount;
            obj.transform.rotation = Quaternion.LookRotation(v);
        }
    }
}
