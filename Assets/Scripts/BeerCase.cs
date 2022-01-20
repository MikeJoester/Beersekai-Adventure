using UnityEngine;
using static System.Math;

public class BeerCase : MonoBehaviour
{
    public float CaseSpd;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Update()
    {
        float input = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(input * CaseSpd, rb.velocity.y);
    }
}
