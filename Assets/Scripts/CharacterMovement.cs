using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController character;
    private Vector3 inputX;

    public GameObject bisturi;

    public float velocity = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        character = GetComponent<CharacterController>();    
    }

    // Update is called once per frame
    void Update()
    {
        inputX = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        inputX = Vector3.ClampMagnitude(inputX, 1);
        character.Move(inputX * velocity * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bisturi")
        {
            bisturi.SetActive(true);
            Destroy(other.gameObject);
        }
    }
}
