using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class MainCam : MonoBehaviour
{
    //[SerializeField]
    //private UnityEngine.Vector3 Position = new UnityEngine.Vector3();

    //private UnityEngine.Vector3[] ModelSpaceVertices;

    //public Mesh myMesh;

    //[SerializeField]
    //UnityEngine.Vector3 Rotation = new UnityEngine.Vector3();
    
    //[SerializeField]
    //UnityEngine.Vector3 Scale = new UnityEngine.Vector3(2.0f, 2.0f, 2.0f);
    //[SerializeField]
    //float rotationSpeed = 0.0f;

    [SerializeField]
    private UnityEngine.Vector3 LinearTarget = new UnityEngine.Vector3();
    [SerializeField]
    bool LinearInterpolate = true;
    [SerializeField]
    float LinearInterpolateSpeed = 1.0f;
    [SerializeField]
    private UnityEngine.Vector3 OriginTarget = new UnityEngine.Vector3(0, 100, 0);

    MyVector3 MyPosition;

    [SerializeField]
    int followingPlanet = -1;
    [SerializeField]
    GameObject[] Planets;

    UnityEngine.Vector2 lastMousePos = new UnityEngine.Vector2(0, 0);

    int selectedPlanet = 0;

    void Start()
    {
        LinearTarget = OriginTarget;
        //MyPosition = new MyVector3(Position.x, Position.y, Position.z);
        //LinearTarget = OriginTarget;
        // MeshFilter meshFilter = GetComponent<MeshFilter>();
        // meshFilter.sharedMesh = Instantiate(myMesh);
        // ModelSpaceVertices = meshFilter.sharedMesh.vertices;
    }

    void Update()
    {
        UnityEngine.Vector2 mousePos = Input.mousePosition;

        UnityEngine.Vector2 mouseDelta = new UnityEngine.Vector2(0, 0);

        mouseDelta = mousePos - lastMousePos;

        lastMousePos = mousePos;

        transform.eulerAngles = new UnityEngine.Vector3(transform.eulerAngles.x - mouseDelta.y, transform.eulerAngles.y + mouseDelta.x, 0);

        if (Input.GetKeyDown(KeyCode.Escape)) { followingPlanet = -1; LinearInterpolateSpeed = 3.0f; }
        if (Input.GetKeyDown(KeyCode.E))
        {
            followingPlanet = 1;
            selectedPlanet++;
            if (selectedPlanet > Planets.Length - 1)
            {
                selectedPlanet = 0;
            }
            LinearInterpolateSpeed = Planets[selectedPlanet].GetComponent<QuatMovement>().Period * 15;
            //LinearTarget = Planets[selectedPlanet].GetComponent<QuatMovement>().UTransform;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            followingPlanet = 2;
            selectedPlanet--;
            if (selectedPlanet < 0)
            {
                selectedPlanet = Planets.Length - 1;
            }
            LinearInterpolateSpeed = Planets[selectedPlanet].GetComponent<QuatMovement>().Period * 15;
            //LinearTarget = Planets[selectedPlanet].GetComponent<QuatMovement>().UTransform;
        }
        
            LinearTarget = Planets[selectedPlanet].GetComponent<QuatMovement>().UTransform;
        
        
        //switch (selectedPlanet)
        //{
        //    case (-1):
        //        LinearTarget = OriginTarget;
        //        break;
        //    case (1):
        //        LinearTarget = Planet1.GetComponent<QuatMovement>().UTransform;
        //        break;
        //    case (2):
        //        LinearTarget = Planet2.GetComponent<QuatMovement>().UTransform;

        //        break;
        //    case (3):
        //        LinearTarget = Planet3.GetComponent<QuatMovement>().UTransform;
        //        break;
        //    default:
        //        break;
        //}


        //Rotation.x += Time.deltaTime * rotationSpeed;
        //Rotation.y += Time.deltaTime * rotationSpeed;
        //Rotation.z += Time.deltaTime * rotationSpeed;



        //Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        ////ROTATION
        ////ROLL
        //Matrix4by4 rollMatrix = new Matrix4by4(
        //    new Vector3(Mathf.Cos(Rotation.z), Mathf.Sin(Rotation.z), 0),
        //    new Vector3(-Mathf.Sin(Rotation.z), Mathf.Cos(Rotation.z), 0),
        //    new Vector3(0, 0, 1),
        //    Vector3.zero
        //    );

        ////PITCH
        //Matrix4by4 pitchMatrix = new Matrix4by4(
        //    new Vector3(1, 0, 0),
        //    new Vector3(0, Mathf.Cos(Rotation.y), Mathf.Sin(Rotation.y)),
        //    new Vector3(0, -Mathf.Sin(Rotation.y), Mathf.Cos(Rotation.y)),
        //    Vector3.zero
        //    );

        ////YAW
        //Matrix4by4 yawMatrix = new Matrix4by4(
        //    new Vector3(Mathf.Cos(Rotation.x), 0, -Mathf.Sin(Rotation.x)),
        //    new Vector3(0, 1, 0),
        //    new Vector3(Mathf.Sin(Rotation.x), 0, Mathf.Cos(Rotation.x)),
        //    Vector3.zero
        //    );


        ////SCALE
        //Matrix4by4 scaleMatrix = new Matrix4by4(new Vector3(1, 0, 0) * Scale.x, new Vector3(0, 1, 0) * Scale.y, new Vector3(0, 0, 1) * Scale.z, Vector3.zero);

        ////TRANSLATION
        //Matrix4by4 transMatrix = new Matrix4by4(
        //    new Vector4(1, 0, 0, 0),
        //    new Vector4(0, 1, 0, 0),
        //    new Vector4(0, 0, 1, 0),
        //    new Vector4(Position.x, Position.y, Position.z, 1));


        //Matrix4by4 T = transMatrix;
        //Matrix4by4 R = yawMatrix * (pitchMatrix * rollMatrix);
        //Matrix4by4 S = scaleMatrix;

        //Matrix4by4 CombinedMatrix = T * (R * S);

        if (LinearInterpolate)
        {
            if (transform.position != LinearTarget)
            {
                transform.position = MathLib.LinInterpolate(transform.position, LinearTarget, Time.deltaTime * LinearInterpolateSpeed);
            }
            else
            {
                //LinearTarget = newLinLocation();
            }
            MyPosition = new MyVector3(transform.position.x, transform.position.y, transform.position.z);
        }
    //    else
    //    {
    //        for (int i = 0; i < TransformedVertices.Length; i++)
    //        {

    //            AABB TheBox = new AABB(new Vector3(0, 0, 0), new Vector3(3, 3, 3));

    //            Vector3 GlobalLineStart = new Vector3(-2, -2, -2);
    //            Vector3 GlobalLineEnd = new Vector3(3, 4, 5);

    //            Matrix4by4 InverseMatrix = S.ScaleInverse() * (R.RotationInverse() * T.TranslationInverse());

    //            Vector4 TranslationModelSpaceV = new Vector4(ModelSpaceVertices[i].x, ModelSpaceVertices[i].y, ModelSpaceVertices[i].z, 1);
    //            TransformedVertices[i] = CombinedMatrix * TranslationModelSpaceV;

    //        }

    //        MeshFilter meshFilter = GetComponent<MeshFilter>();

    //        meshFilter.sharedMesh.vertices = TransformedVertices;

    //        meshFilter.sharedMesh.RecalculateNormals();
    //        meshFilter.sharedMesh.RecalculateBounds();
    //    }
    }


    //Vector3 newLinLocation()
    //{
    //    Vector3 rv = new Vector3(Random.Range(-10, 10), Random.Range(0, 10), Random.Range(-10, 10));

    //    return rv;
    //}

}
