using UnityEngine;

public class WayPointMGR : MonoBehaviour
{
    public static Transform[] WayPointList = null;
    private void Awake()
    {
        WayPointList = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i ++)
        {
            WayPointList[i] = transform.GetChild(i);
        }
    }
}
