using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planets : MonoBehaviour
{
    [SerializeField]
    private GameObject GameCamera;

    [SerializeField]
    private GameObject[] PlanetList;
    //[SerializeField]
    //public float SimulationSpeed = 1.0f;
    //[SerializeField]
    //private bool IncreaseTimeWarp = false;
    //[SerializeField]
    //private bool DecreaseTimeWarp = true;
    
    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float SimulationSpeed;

    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if (IncreaseTimeWarp)
        //{
        //    IncreaseTimeWarp = false;
        //    Increase();
        //}
        //if (DecreaseTimeWarp)
        //{
        //    DecreaseTimeWarp = false;
        //    Decrease();
        //}

        foreach (GameObject planet in PlanetList)
        {
            planet.GetComponent<QuatMovement>().Period = planet.GetComponent<QuatMovement>().PeriodNoTimeWarp * SimulationSpeed;
            planet.GetComponent<QuatMovement>().RotationPeriod = planet.GetComponent<QuatMovement>().RotationPeriodNoTimeWarp * SimulationSpeed;
        }
        GameCamera.GetComponent<MainCam>().LinearIntSpeedMultiplier = GameCamera.GetComponent<MainCam>().LinearIntSpeedMultiplierNoTimeWarp * SimulationSpeed;
    }

    void Decrease()
    {
        foreach (GameObject planet in PlanetList)
        {
            planet.GetComponent<QuatMovement>().Period *= 0.8f;
        }
    }

    void Increase()
    {
        foreach (GameObject planet in PlanetList)
        {
            planet.GetComponent<QuatMovement>().Period *= 1.2f;
        }
    }
}
