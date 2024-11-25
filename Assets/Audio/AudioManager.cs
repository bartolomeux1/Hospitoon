using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("AudioMngr");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        else
            DontDestroyOnLoad(this.gameObject);
    }
}
