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
    float scale = 2.0f;
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();

        ModelSpaceVertices = meshFilter.mesh.vertices;
    }

    void Update()
    {
        yawAngle += Time.deltaTime * 1;
        //transform.position = MathLib.LinInterpolate(transform.position, TargetLocation, Time.deltaTime * 1);

        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        //ROTATION
        Matrix4by4 rotMatrix = new Matrix4by4(
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
            Vector4 TranslationModelSpaceV = new Vector4(ModelSpaceVertices[i].x, ModelSpaceVertices[i].y, ModelSpaceVertices[i].z, 1);
            TransformedVertices[i] = transMatrix * TranslationModelSpaceV;
            //TransformedVertices[i] = scaleMatrix * TransformedVertices[i];
            //TransformedVertices[i] = rotMatrix * TransformedVertices[i];
        }

        MeshFilter meshFilter = GetComponent<MeshFilter>();

        meshFilter.mesh.vertices = TransformedVertices;

        meshFilter.mesh.RecalculateNormals();
        meshFilter.mesh.RecalculateBounds();

    }
}
