using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetOrbitor : MonoBehaviour
{
    private Vector3[] ModelSpaceVertices;

    public Mesh myMesh;

    [SerializeField]
    Vector3 Rotation = new Vector3();
    [SerializeField]
    Vector3 Scale = new Vector3();

    [SerializeField]
    float rotationSpeed = 1.0f;

    [SerializeField]
    float orbitalRadius = 1.0f;

    [SerializeField]
    float yawAngle = 1.0f;

    [SerializeField]
    float axialTiltAngle = 10.0f;
    // Start is called before the first frame update
    void Start()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.sharedMesh = Instantiate(myMesh);
        ModelSpaceVertices = meshFilter.sharedMesh.vertices;
    }

    // Update is called once per frame
    void Update()
    {
        Rotation.x += Time.deltaTime * rotationSpeed;

        Vector3[] TransformedVertices = new Vector3[ModelSpaceVertices.Length];

        Matrix4by4 scaleMatrix = Matrix4by4.CreateScaleMatrix(1);
        Matrix4by4 translationMatrix = Matrix4by4.CreateTranslationMatrix(new Vector3(orbitalRadius, 0, 0));
        Matrix4by4 axialTilt = Matrix4by4.CreatePitchMatrix(axialTiltAngle);
        Matrix4by4 orbitalRotation = Matrix4by4.CreateYawMatrix(Rotation.z);

        Matrix4by4 TransformedMatrix = scaleMatrix * axialTilt * translationMatrix;//* orbitalRotation;

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
