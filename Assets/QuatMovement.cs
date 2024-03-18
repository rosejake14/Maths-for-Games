using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuatMovement : MonoBehaviour
{

    [SerializeField]
    Vector3 OriginRotationPoint = new Vector3();

    [SerializeField]
    public float MovementMultiplier = 1.0f;

    [SerializeField]
    public float orbitSize = 1.0f;

    [SerializeField]
    public float axis = 1.0f;

    float angle = 0;

    [SerializeField]
    bool isMoon = false;

    [SerializeField]
    GameObject ParentPlanet;

    [SerializeField]
    float PlanetScale = 2.0f;

    [SerializeField]
    private Vector3 Position = new Vector3();

    private Vector3[] ModelSpaceVertices;

    public Mesh myMesh;

    [SerializeField]
    float axialTiltAngle = 10.0f;

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
        angle += Time.deltaTime * MovementMultiplier;

        Quat q = new Quat(angle, new MyVector3(0, 1, 0));

        MyVector3 p = new MyVector3(orbitSize, 0, 0);

        Quat K = new Quat(p);

        Quat newK = q * K * q.Inverse();
        
        MyVector3 newP = newK.GetAxis();

        Matrix4by4 myTransform = newK.QuatToMatrix();
        //Matrix4by4 myTransform = Matrix4by4.CreateTranslationMatrix(newP.ToUnityVector() + ParentPlanet.transform.position);

        //if(ParentPlanet)
        //{ transform.position = newP.ToUnityVector() + ParentPlanet.transform.position; }
        //else
        //{ transform.position = newP.ToUnityVector(); }

        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        Matrix4by4 axialTilt = Matrix4by4.CreatePitchMatrix(axialTiltAngle);

        Matrix4by4 Scale = Matrix4by4.CreateScaleMatrix(PlanetScale);

        // Matrix4by4 T = myTransform;
        // Matrix4by4 R = yawMatrix * (axialTilt * rollMatrix);

        Matrix4by4 TransformedMatrix = Scale * axialTilt * myTransform;// * (axialTilt * Scale);

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
