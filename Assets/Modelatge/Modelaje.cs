using System.Collections.Generic;
using UnityEngine;

public class Modelaje : MonoBehaviour
{
    public Transform Joint0;
    public Transform Joint1;
    public Transform Joint2;
    public Transform Joint3;
    public Transform Joint4;
    public Transform EndEffector;

    public Transform Target;

    public float tolerance;
    public float maxIterationCount;
    private int iterationCount;

    private float rotation;
    private Vector3 axis;

    private int index;
    Vector3[] Joints = new Vector3[5];

    private Vector3[] Links;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tolerance = 1.0f;
        maxIterationCount = 1e5f;
        iterationCount = 0;
        index = 0;

        Joints[0] = Joint0.position;
        Joints[1] = Joint1.position;
        Joints[2] = Joint2.position;
        Joints[3] = Joint3.position;
        Joints[4] = Joint4.position;

        GetLinks();
    }

    // Update is called once per frame
    void Update()
    {
        if(iterationCount < maxIterationCount && Vector3.Distance(EndEffector.position, Target.position) > tolerance)
        {
            Vector3 Pd = Joints[index];
            Vector3[] referenceVectors = GetVectors(Pd);
            rotation = GetAngle(referenceVectors);
            axis = GetAxis(referenceVectors);

            UpdatePosition(index, rotation, axis);

            if (index == 4)
                index = 0;
            else
                index++;

            iterationCount++;
        }
    }

    void GetLinks()
    {
        Links = new Vector3[5];

        for (int i = 0; i < 4; ++i)
        {
            Links[i] = Joints[i + 1] - Joints[i];
        }

        Links[4] = EndEffector.position - Joints[4];
    }

    Vector3[] GetVectors(Vector3 Pd)
    {
        Vector3[] referenceVectors = new Vector3[2];

        referenceVectors[0] = Vector3.Normalize(EndEffector.position - Pd);

        referenceVectors[1] = Vector3.Normalize(Target.position - Pd);

        return referenceVectors;
    }

    float GetAngle(Vector3[] referenceVectors)
    {
        return Mathf.Acos(Mathf.Clamp(Vector3.Dot(referenceVectors[0], referenceVectors[1]), -1.0f, 1.0f));
    }

    Vector3 GetAxis(Vector3[] referenceVectors)
    {
        return Vector3.Cross(referenceVectors[0], referenceVectors[1]);
    }

    void UpdatePosition(int index, float rotation, Vector3 axis)
    {
        Quaternion quaternion = Quaternion.AngleAxis(Mathf.Rad2Deg * rotation, axis);

        if(index <= 3)
        {
            for (int i = index; i <= 3; ++i)
            {
                Joints[i + 1] = Joints[i] + quaternion * Links[i];
            }
        }

        EndEffector.position = Joints[4] + quaternion * Links[3]; 

        Joint1.position = Joints[1];
        Joint2.position = Joints[2];
        Joint3.position = Joints[3];
        Joint4.position = Joints[4];

        GetLinks();
    }
}
