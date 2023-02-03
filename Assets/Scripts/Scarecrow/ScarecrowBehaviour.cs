using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScarecrowBehaviour : MonoBehaviour
{
    [SerializeField]
    private GameObject area;

    private List<Vector2> areaLocations = new List<Vector2>();
    // Start is called before the first frame update
    void Start()
    {
        FillScarecrowAreaLocation();
    }

    void FillScarecrowAreaLocation()
    {
        areaLocations.Add(new Vector2(transform.position.x + 1, transform.position.y));
        areaLocations.Add(new Vector2(transform.position.x - 1, transform.position.y));
        areaLocations.Add(new Vector2(transform.position.x, transform.position.y+1));
        areaLocations.Add(new Vector2(transform.position.x, transform.position.y - 1));
        areaLocations.Add(new Vector2(transform.position.x + 1, transform.position.y + 1));
        areaLocations.Add(new Vector2(transform.position.x - 1, transform.position.y - 1));
        areaLocations.Add(new Vector2(transform.position.x + 1, transform.position.y - 1));
        areaLocations.Add(new Vector2(transform.position.x - 1, transform.position.y + 1));
        areaLocations.Add(new Vector2(transform.position.x, transform.position.y));
    }

    public List<Vector2> GetAreaLocations()
    {
        return areaLocations;
    }

    public void DestroyScarecrow()
    {
        Destroy(this);
    }
}
