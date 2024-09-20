using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private CharacterController character;
    private Vector3 inputX;

    public GameObject bisturi;
    public GameObject siringa;

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

            if (siringa.activeSelf)
            {
                siringa.SetActive(false);
            }
        }

        if (other.gameObject.tag == "Siringa")
        {
            siringa.SetActive(true);

            if (bisturi.activeSelf)
            {
                bisturi.SetActive(false);
            }
        }
    }
}
