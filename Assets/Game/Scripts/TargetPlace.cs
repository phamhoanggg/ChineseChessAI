using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlace : MonoBehaviour
{
    public Vector2 BoardPosition;
    public LayerMask tileLayer;
    private void OnEnable()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 10, Vector3.down, out RaycastHit myhit, 100, tileLayer))
        {
            BoardPosition = myhit.collider.GetComponent<Tile>().BoardPosition;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI"))
        {
            GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            Debug.Log(other.gameObject.name);
        }
    }
}
