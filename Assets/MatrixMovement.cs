using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MatrixMovement : MonoBehaviour
{
    [SerializeField]
    private Vector3 TargetLocation = new Vector3();

    [SerializeField]
    private Vector3 MatrixTargetLocation = new Vector3();

    private Vector3[] ModelSpaceVertices;

    [SerializeField]
    float yawAngle;
    [SerializeField]
    float rollAngle;
    [SerializeField]
    float pitchAngle;
    [SerializeField]
    float scale = 2.0f;
    [SerializeField]
    float rotationSpeed = 1.0f;

    [SerializeField]
    bool LinearInterpolate = false;
    [SerializeField]
    float LinearInterpolateSpeed = 1.0f;

    
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        ModelSpaceVertices = meshFilter.mesh.vertices;
    }

    void Update()
    {
        if (!LinearInterpolate)
        {
            yawAngle += Time.deltaTime * rotationSpeed;
            pitchAngle += Time.deltaTime * rotationSpeed;
            rollAngle += Time.deltaTime * rotationSpeed;

            Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

            //ROTATION
            //ROLL
            Matrix4by4 rollMatrix = new Matrix4by4(
                new Vector3(Mathf.Cos(rollAngle), Mathf.Sin(rollAngle), 0),
                new Vector3(-Mathf.Sin(rollAngle), Mathf.Cos(rollAngle), 0),
                new Vector3(0, 0, 1),
                Vector3.zero
                );

            //PITCH
            Matrix4by4 pitchMatrix = new Matrix4by4(
                new Vector3(1, 0, 0),
                new Vector3(0, Mathf.Cos(pitchAngle), Mathf.Sin(pitchAngle)),
                new Vector3(0, -Mathf.Sin(pitchAngle), Mathf.Cos(pitchAngle)),
                Vector3.zero
                );

            //YAW
            Matrix4by4 yawMatrix = new Matrix4by4(
                new Vector3(Mathf.Cos(yawAngle), 0, -Mathf.Sin(yawAngle)),
                new Vector3(0, 1, 0),
                new Vector3(Mathf.Sin(yawAngle), 0, Mathf.Cos(yawAngle)),
                Vector3.zero
                );


            //SCALE
            Matrix4by4 scaleMatrix = new Matrix4by4(new Vector3(1, 0, 0) * scale, new Vector3(0, 1, 0) * scale, new Vector3(0, 0, 1) * scale, Vector3.zero);

            //TRANSLATION
            Matrix4by4 transMatrix = new Matrix4by4(
                new Vector3(1, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(0, 0, 1),
                new Vector3(MatrixTargetLocation.x, MatrixTargetLocation.y, MatrixTargetLocation.z));


            for (int i = 0; i < TransformedVertices.Length; i++)
            {
                //TRANSLATE ROTATE SCALE
                //TRANSLATE
                Vector4 TranslationModelSpaceV = new Vector4(ModelSpaceVertices[i].x, ModelSpaceVertices[i].y, ModelSpaceVertices[i].z, 1);
                TransformedVertices[i] = transMatrix * TranslationModelSpaceV;

                //ROTATE
                //TransformedVertices[i] = rotMatrix * TransformedVertices[i];
                Vector3 RolledV = rollMatrix * TransformedVertices[i];
                Vector3 PitchedV = pitchMatrix * RolledV;
                Vector3 YawedV = yawMatrix * PitchedV;
                TransformedVertices[i] = YawedV;

                //SCALE
                TransformedVertices[i] = scaleMatrix * TransformedVertices[i];
            }

            MeshFilter meshFilter = GetComponent<MeshFilter>();

            meshFilter.mesh.vertices = TransformedVertices;

            meshFilter.mesh.RecalculateNormals();
            meshFilter.mesh.RecalculateBounds();
        }
        else
        {
            if(transform.position != TargetLocation)
            {
                transform.position = MathLib.LinInterpolate(transform.position, TargetLocation, Time.deltaTime * LinearInterpolateSpeed);
            }
            else
            {
                TargetLocation = newLinLocation();
            }
        }
    }


    Vector3 newLinLocation()
    {
        Vector3 rv = new Vector3(Random.Range(-10,10), Random.Range(0, 10), Random.Range(-10, 10));

        return rv;
    }
}
