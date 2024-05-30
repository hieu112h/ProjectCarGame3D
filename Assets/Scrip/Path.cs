using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    public Color lineColor;
    public List<Transform> nodes = new List<Transform>();
    private void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        Transform[] pathTranforms = GetComponentsInChildren<Transform>();
        nodes = new List<Transform>();

        for(int i = 0; i < pathTranforms.Length; i++)
        {
            if(pathTranforms[i] != transform)
            {
                nodes.Add(pathTranforms[i]);
            }
        }
        for(int i = 0; i < nodes.Count; i++)
        {
            Vector3 curentNode = nodes[i].position;
            Vector3 previousNode = Vector3.zero;
            if (i > 0)
            {
                previousNode = nodes[i - 1].position;
            }
            else if(i==0 && nodes.Count > 1)
            {
                previousNode = nodes[nodes.Count - 1].position;
            }
            Gizmos.DrawLine(previousNode, curentNode);
            Gizmos.DrawWireSphere(curentNode, 0.3f);
        }
    }
}
