using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MatrixMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 Position = new Vector3();

    private Vector3[] ModelSpaceVertices;

    public Mesh myMesh;

    //[SerializeField]
    //float yawAngle;
    //[SerializeField]
    //float rollAngle;
    //[SerializeField]
    //float pitchAngle;
    [SerializeField]
    Vector3 Rotation = new Vector3();
    //[SerializeField]
    //float scale = 2.0f;
    [SerializeField]
    Vector3 Scale = new Vector3(2.0f,2.0f,2.0f);
    [SerializeField]
    float rotationSpeed = 0.0f;

    [SerializeField]
    private Vector3 LinearTarget = new Vector3();
    [SerializeField]
    bool LinearInterpolate = false;
    [SerializeField]
    float LinearInterpolateSpeed = 1.0f;

    MyVector3 MyPosition;


    void Start()
    {
        MyPosition  = new MyVector3(Position.x, Position.y, Position.z);

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = Instantiate(myMesh);
        ModelSpaceVertices = meshFilter.sharedMesh.vertices;
    }

    void Update()
    {
        
            Rotation.x += Time.deltaTime * rotationSpeed;
           // Rotation.y += Time.deltaTime * rotationSpeed;
            //Rotation.z += Time.deltaTime * rotationSpeed;

            

            Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

            //ROTATION
            //ROLL
            Matrix4by4 rollMatrix = new Matrix4by4(
                new Vector3(Mathf.Cos(Rotation.z), Mathf.Sin(Rotation.z), 0),
                new Vector3(-Mathf.Sin(Rotation.z), Mathf.Cos(Rotation.z), 0),
                new Vector3(0, 0, 1),
                Vector3.zero
                );

            //PITCH
            Matrix4by4 pitchMatrix = new Matrix4by4(
                new Vector3(1, 0, 0),
                new Vector3(0, Mathf.Cos(Rotation.y), Mathf.Sin(Rotation.y)),
                new Vector3(0, -Mathf.Sin(Rotation.y), Mathf.Cos(Rotation.y)),
                Vector3.zero
                );

            //YAW
            Matrix4by4 yawMatrix = new Matrix4by4(
                new Vector3(Mathf.Cos(Rotation.x), 0, -Mathf.Sin(Rotation.x)),
                new Vector3(0, 1, 0),
                new Vector3(Mathf.Sin(Rotation.x), 0, Mathf.Cos(Rotation.x)),
                Vector3.zero
                );


            //SCALE
            Matrix4by4 scaleMatrix = new Matrix4by4(new Vector3(1, 0, 0) * Scale.x, new Vector3(0, 1, 0) * Scale.y, new Vector3(0, 0, 1) * Scale.z, Vector3.zero);

            //TRANSLATION
            Matrix4by4 transMatrix = new Matrix4by4(
                new Vector4(1, 0, 0, 0),
                new Vector4(0, 1, 0, 0),
                new Vector4(0, 0, 1, 0),
                new Vector4(Position.x, Position.y, Position.z, 1));

            
            Matrix4by4 T = transMatrix;
            Matrix4by4 R = yawMatrix * (pitchMatrix * rollMatrix);
            Matrix4by4 S = scaleMatrix;

             Matrix4by4 CombinedMatrix = T * (R * S);

            //if (LinearInterpolate)
            //{
            //    if (transform.position != LinearTarget)
            //    {
            //        transform.position = MathLib.LinInterpolate(transform.position, LinearTarget, Time.deltaTime * LinearInterpolateSpeed);
            //    }
            //    else
            //    {
            //        LinearTarget = newLinLocation();
            //    }
            //    MyPosition = new MyVector3(transform.position.x, transform.position.y, transform.position.z);
            //}
            //else
            //{
                for (int i = 0; i < TransformedVertices.Length; i++)
                {

                    //AABB TheBox = new AABB(new Vector3(0, 0, 0), new Vector3(3, 3, 3));

                    //Vector3 GlobalLineStart = new Vector3(-2, -2, -2);
                    //Vector3 GlobalLineEnd = new Vector3(3, 4, 5);

                    //Matrix4by4 InverseMatrix = S.ScaleInverse() * (R.RotationInverse() * T.TranslationInverse());

                    //Vector3 LocalStart = InverseMatrix * GlobalLineStart;
                    //Vector3 localEnd = InverseMatrix * GlobalLineEnd;

                    //Vector3 intersection;
                    //if (AABB.LineInetersection(TheBox, LocalStart, localEnd, out intersection))
                    //{
                    //    Debug.Log("Intersecting, Local Point: " + intersection);
                    //    Debug.Log("Globally at: " + (CombinedMatrix * intersection));
                    //}
                    //else
                    //{
                    //    Debug.Log("This did not intersect.");
                    //}
                    Vector4 TranslationModelSpaceV = new Vector4(ModelSpaceVertices[i].x, ModelSpaceVertices[i].y, ModelSpaceVertices[i].z, 1);
                    TransformedVertices[i] = CombinedMatrix * TranslationModelSpaceV;


                    //TransformedVertices[i] = transMatrix * TranslationModelSpaceV;



                    ////SRT
                    ////SCALE
                    //TransformedVertices[i] = scaleMatrix * ModelSpaceVertices[i];

                    ////ROTATE
                    ////TransformedVertices[i] = rotMatrix * TransformedVertices[i];
                    //Vector3 RolledV = rollMatrix * TransformedVertices[i];
                    //Vector3 PitchedV = pitchMatrix * RolledV;
                    //Vector3 YawedV = yawMatrix * PitchedV;
                    //TransformedVertices[i] = YawedV;

                    ////TRANSLATE

                }

                MeshFilter meshFilter = GetComponent<MeshFilter>();

                meshFilter.sharedMesh.vertices = TransformedVertices;

                meshFilter.sharedMesh.RecalculateNormals();
                meshFilter.sharedMesh.RecalculateBounds();
            //}
    }


    //Vector3 newLinLocation()
    //{
    //    Vector3 rv = new Vector3(Random.Range(-10,10), Random.Range(0, 10), Random.Range(-10, 10));

    //    return rv;
    //}

}
