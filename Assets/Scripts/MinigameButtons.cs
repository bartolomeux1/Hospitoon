using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameButtons : MonoBehaviour
{
    public Color objectColor;
    private Vector3 Position;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = objectColor;
        Position = transform.position;
        new Vector3(transform.position.x + 0.03f, transform.position.y, transform.position.z);
    }
    private void OnMouseEnter()
    {
        GetComponent<SpriteRenderer>().color = new Color(objectColor.r, objectColor.g, objectColor.b, 0.5f);
    }
    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = objectColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
