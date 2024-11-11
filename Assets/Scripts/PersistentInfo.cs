using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentInfo : MonoBehaviour
{
    private int index = 0;

    private void Start()
    {
        DontDestroyOnLoad(this);

        PersistentInfo[] instancesInfo = FindObjectsByType<PersistentInfo>(FindObjectsSortMode.None);

        if (instancesInfo.Length > 1)
        {
            foreach (PersistentInfo info in instancesInfo)
            {
                if (info != this)
                {
                    Destroy(info.gameObject);
                }
            }
        }
    }

    public int GetIndex()
    {
        return index;
    }

    public void SetIndex(int value)
    {
        index = value;
    }
}
