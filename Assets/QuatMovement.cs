using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuatMovement : MonoBehaviour
{
    [SerializeField]
    public Vector3 UTransform = new Vector3(0, 0, 0);
    [SerializeField]
    public Vector3 URotation = new Vector3(0, 0, 0);
    [SerializeField]
    public Vector3 UScale = new Vector3(0, 0, 0);

    [SerializeField]
    Vector3 OriginRotationPoint = new Vector3();

    [SerializeField]
    public float orbitSize = 1.0f;

    [SerializeField]
    public float axis = 1.0f;

    [SerializeField]
    float angle = 0f;

    [SerializeField]
    float RotationAngle = 0f;

    [SerializeField]
    float RotationPeriod = 1.0f;

    [SerializeField]
    bool isMoon = false;

    [SerializeField]
    GameObject ParentPlanet;

    [SerializeField]
    float PlanetScale = 2.0f;

    private Vector3[] ModelSpaceVertices;

    public Mesh myMesh;

    [SerializeField]
    float axialTiltAngle = 10.0f;

    [SerializeField]
    public float Period = 1.0f;

    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = Instantiate(myMesh);
        ModelSpaceVertices = meshFilter.sharedMesh.vertices;
    }

    void Update()
    {
        if (isMoon && ParentPlanet)
        {
            OriginRotationPoint = ParentPlanet.transform.position;
        }

        //Quat Movement Rotations
        //Local Rotation Period (Axial Tilt)
        RotationAngle += Time.deltaTime * RotationPeriod;

        Quat Orbit = new Quat(RotationAngle, new MyVector3(0, 1, 0));

        Quat AxialTilt = new Quat(axialTiltAngle, new MyVector3(1, 0, 0));
        Quat CombinedQuat = AxialTilt * Orbit;
        Matrix4by4 RotationMatrix = CombinedQuat.QuatToMatrix();


        //World Rotation Period (Orbit)

        angle += Time.deltaTime * Period;
        Quat q = new Quat(angle, new MyVector3(0, 1, 0));
        
        MyVector3 p = new MyVector3(orbitSize, 0, 0);

        Quat K = new Quat(p);

        Quat newK = q * K * q.Inverse();
        
        MyVector3 newP = newK.GetAxis();

        Matrix4by4 Transform = Matrix4by4.CreateTranslationMatrix(newP.ToUnityVector() + ParentPlanet.transform.position); //Make all the transforms my own transforms instead of using unitys built in ones.

        //if(ParentPlanet)
        //{ transform.position = newP.ToUnityVector() + ParentPlanet.transform.position; }
        //else
        //{ transform.position = newP.ToUnityVector(); }

        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        Matrix4by4 Scale = Matrix4by4.CreateScaleMatrix(PlanetScale);

        // Matrix4by4 T = myTransform;
        // Matrix4by4 R = yawMatrix * (axialTilt * rollMatrix);

        Matrix4by4 TransformedMatrix = Transform * (RotationMatrix * Scale);// * (axialTilt * Scale);

        for (int i = 0; i < TransformedVertices.Length; i++)
        {
            Vector4 TranslationModelSpaceV = new Vector4(ModelSpaceVertices[i].x, ModelSpaceVertices[i].y, ModelSpaceVertices[i].z, 1);
            TransformedVertices[i] = TransformedMatrix * TranslationModelSpaceV;
        }

        MeshFilter meshFilter = GetComponent<MeshFilter>();

        meshFilter.sharedMesh.vertices = TransformedVertices;

        meshFilter.sharedMesh.RecalculateNormals();
        meshFilter.sharedMesh.RecalculateBounds();
    }

}
