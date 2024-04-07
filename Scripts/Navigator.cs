using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Navigator : MonoBehaviour
{
    public Transform road;
    private List<Transform> nodes = new List<Transform>();
    private int curNodeIdx;
    // Start is called before the first frame update
    void Start()
    {
        curNodeIdx = -1;

        Transform spline = road.Find ("Spline");
        for (int i = 0; i < spline.childCount; ++i)
        {
            nodes.Add (spline.GetChild (i));
        }
    }

    public Transform Next ()
    {
        curNodeIdx++;
        curNodeIdx %= nodes.Count;
        return nodes[curNodeIdx];
    }

    public Transform GetNode (int next)
    {
        int idx = curNodeIdx + next;
        idx %= nodes.Count;
        return nodes[idx];
    }
}
