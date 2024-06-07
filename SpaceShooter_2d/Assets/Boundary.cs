using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{
    public BoundaryType boundaryType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print(collision.gameObject.tag);
        switch (boundaryType)
        {
            case BoundaryType.enemyBoundary:
                if (collision.gameObject.CompareTag("Alien"))
                {
                    collision.gameObject.SetActive(false);
                }
                break;
            case BoundaryType.missileBoundary:
                break;
            default:
                break;
        }
    }

}
public enum BoundaryType
{
    enemyBoundary,
    missileBoundary
}
