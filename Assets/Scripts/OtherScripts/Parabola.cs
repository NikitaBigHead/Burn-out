using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public static class Parabola
{
    public static Vector3 FindParabola(Vector2 p1, Vector2 p2, Vector2 p3)
    {
        Matrix3x3 m = new Matrix3x3(p1.x * p1.x, p1.x, 1,
                                            p2.x * p2.x, p2.x, 1,
                                            p3.x * p3.x, p3.x, 1);
        m = m.Inverse();
        return new Vector3(
            m.data[0, 0] * p1.y + m.data[0, 1] * p2.y + m.data[0, 2] * p3.y,
            m.data[1, 0] * p1.y + m.data[1, 1] * p2.y + m.data[1, 2] * p3.y,
            m.data[2, 0] * p1.y + m.data[2, 1] * p2.y + m.data[2, 2] * p3.y);
    }
}

public class Matrix3x3
{
    public float[,] data;

    public Matrix3x3()
    {
        data = new float[3, 3];
    }

    public Matrix3x3(float[,] data)
    {
        this.data = new float[3, 3];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; i++)
            {
                this.data[i, j] = data[i, j];
            }
        }
    }

    public Matrix3x3(float a11, float a12, float a13, 
              float a21, float a22, float a23, 
              float a31, float a32, float a33)
    {
        data = new float[3, 3];
        data[0, 0] = a11;
        data[0, 1] = a12;
        data[0, 2] = a13;
        data[1, 0] = a21;
        data[1, 1] = a22;
        data[1, 2] = a23;
        data[2, 0] = a31;
        data[2, 1] = a32;
        data[2, 2] = a33;
    }

    public Matrix3x3 Inverse()
    {
        float det = Det();
        float m11 = data[1, 1] * data[2, 2] - data[1, 2] * data[2, 1];
        float m12 = data[0, 1] * data[2, 2] - data[0, 2] * data[2, 1];
        float m13 = data[0, 1] * data[1, 2] - data[0, 2] * data[1, 1];

        float m21 = data[1, 0] * data[2, 2] - data[1, 2] * data[2, 0];
        float m22 = data[0, 0] * data[2, 2] - data[0, 2] * data[2, 0];
        float m23 = data[0, 0] * data[1, 2] - data[0, 2] * data[1, 0];

        float m31 = data[1, 0] * data[2, 1] - data[1, 1] * data[2, 0];
        float m32 = data[0, 0] * data[2, 1] - data[0, 1] * data[2, 0];
        float m33 = data[0, 0] * data[1, 1] - data[0, 1] * data[1, 0];
        Matrix3x3 inversed = new Matrix3x3(m11 / det, -m12 / det, m13 / det, -m21 / det, m22 / det, -m23 / det, m31 / det, -m32 / det, m33 / det);
        return inversed;
    }

    public float Det()
    {
        return
            data[0, 0] * data[1, 1] * data[2, 2]
          + data[2, 0] * data[0, 1] * data[1, 2]
          + data[1, 0] * data[2, 1] * data[0, 2]
          - data[2, 0] * data[1, 1] * data[0, 2]
          - data[1, 0] * data[0, 1] * data[2, 2]
          - data[0, 0] * data[1, 2] * data[2, 1];
    }

    public string toString()
    {
        return $"{data[0, 0]} | {data[0, 1]} | {data[0, 2]}\n{data[1, 0]} | {data[1, 1]} | {data[1, 2]}\n{data[2, 0]} | {data[2, 1]} | {data[2, 2]}\n";
    }
}
