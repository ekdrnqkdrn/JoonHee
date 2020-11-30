using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodesListMGR : MonoBehaviour
{
    public static int nNodesCount = 0;
    private void Awake()
    {
        nNodesCount = transform.childCount;

        //수정할거(삭제)
        for (int i = 0; i < nNodesCount; i++)
        {
            transform.GetChild(i).GetComponent<NodeMGR>().m_index = i;
        }
    }
}
