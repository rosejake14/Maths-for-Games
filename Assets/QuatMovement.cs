using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuatMovement : MonoBehaviour
{
    [SerializeField]
    public Vector3 UTransform = new Vector3();
    [SerializeField]
    public Vector3 URotation = new Vector3();
    [SerializeField]
    public Vector3 UScale = new Vector3();

    private MyVector3 MTransform;
    private MyVector3 MRotation;
    private MyVector3 MScale;

    private MyVector3 StartLocation = new MyVector3(0,0,0);

    [SerializeField]
    public float orbitSize = 1.0f;

    
    //public float axis = 1.0f;

    [SerializeField]
    float angle = 0f;

    [SerializeField]
    float RotationAngle = 0f;

    [SerializeField]
    public float RotationPeriod = 1.0f;
    public float RotationPeriodNoTimeWarp;
    [SerializeField]
    bool isMoon = false;

    [SerializeField]
    GameObject ParentPlanet = null;

    [SerializeField]
    float PlanetScale = 2.0f;

    private Vector3[] ModelSpaceVertices;

    public Mesh myMesh;

    [SerializeField]
    float axialTiltAngle = 10.0f;

    [SerializeField]
    public float Period = 1.0f;

    public float PeriodNoTimeWarp;

    private bool OrbitIncreasing = false;

    [SerializeField]
    private float HeightMagnitude = 0.5f;

    [SerializeField]
    private float Frequency = 0.5f;

    [SerializeField]
    private bool isMoveable = true;

    bool ParentAssigned = false;

    [SerializeField]
    public string PlanetDescription;

    void Start()
    {
        if (isMoveable)
        {
            MeshFilter meshFilter = GetComponent<MeshFilter>();
            meshFilter.sharedMesh = Instantiate(myMesh);
            ModelSpaceVertices = meshFilter.sharedMesh.vertices;
        }
        PeriodNoTimeWarp = Period;
        RotationPeriodNoTimeWarp = RotationPeriod;
        //Matrix4by4 TranslationMatrix = Matrix4by4.CreateTranslationMatrix(UTransform);
        // TranslateObject(TranslationMatrix);
    }

    void Update()
    {
        

        MTransform = new MyVector3(UTransform.x, UTransform.y, UTransform.z);
        MRotation = new MyVector3(URotation.x, URotation.y, URotation.z);
        MScale = new MyVector3(UScale.x, UScale.y, UScale.z);


        //if (isMoon && ParentPlanet)
        //{
        //    Vector3 OriginRotationPoint = ParentPlanet.transform.position;
        //}

        //Quat Movement Rotations
        //Local Rotation Period (Axial Tilt)
        RotationAngle += Time.deltaTime * RotationPeriod;

        Quat Orbit = new Quat(RotationAngle, new MyVector3(0, 1, 0));

        Quat AxialTilt = new Quat(axialTiltAngle, new MyVector3(1, 0, 0));
        Quat CombinedQuat = AxialTilt * Orbit;
        Matrix4by4 RotationMatrix = CombinedQuat.QuatToMatrix();


        //World Rotation Period (Orbit)

        if (OrbitIncreasing)
        {
            //orbitSize += .1f * Time.deltaTime;
        }

        angle += Time.deltaTime * Period;
        Quat q = new Quat(angle, new MyVector3(0, 1, 0.5f));
        
        MyVector3 p = new MyVector3(orbitSize, 0, 0);

        Quat K = new Quat(p);

        Quat newK = q * K * q.Inverse();
        
        MyVector3 newP = newK.GetAxis();

        Matrix4by4 Transform = Matrix4by4.Identity;

        Matrix4by4 TranslationMatrix = Matrix4by4.CreateTranslationMatrix(new Vector3(0,0,0));

        if (ParentAssigned)
        {
             //Transform = Matrix4by4.CreateTranslationMatrix(newP.ToUnityVector() + ParentPlanet.GetComponent<QuatMovement>().MTransform.ToUnityVector()); //Make all the transforms my own transforms instead of using unitys built in ones.
            UTransform = newP.ToUnityVector() + ParentPlanet.GetComponent<QuatMovement>().MTransform.ToUnityVector();
            UTransform = new Vector3(UTransform.x, HeightMagnitude * Mathf.Sin(Time.time * Frequency) - 1, UTransform.z);
        }
        else
        {
            if (ParentPlanet)
            {
                ParentAssigned = true;
            }
        }
        TranslationMatrix = Matrix4by4.CreateTranslationMatrix(UTransform);
        //if(ParentPlanet)
        //{ transform.position = newP.ToUnityVector() + ParentPlanet.transform.position; }
        //else
        //{ transform.position = newP.ToUnityVector(); }





        Matrix4by4 Scale = Matrix4by4.CreateScaleMatrix(PlanetScale);

        // Matrix4by4 T = myTransform;
        // Matrix4by4 R = yawMatrix * (axialTilt * rollMatrix);

        Matrix4by4 TransformedMatrix = TranslationMatrix * (RotationMatrix * Scale);// * (axialTilt * Scale);

        if (isMoveable)
        {
            TranslateObject(TransformedMatrix);
        }
        

        // UTransform = new Vector3(transform.position.x, transform.position.y, transform.position.z);


        
    }

    void TranslateObject(Matrix4by4 TransformedMatrix)
    {
        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

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
