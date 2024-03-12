using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TestCircle : MonoBehaviour
{
    [SerializeField]
    private GameObject Circle;

    private float radius;

    [SerializeField]
    private GameObject Dot;

    [SerializeField]
    private GameObject DotPrefab;

    private GameObject[] dots;

    void CalculateTangentPoints(Transform tangentPoint, Transform circleCenter, float circleRadius)
    {
        // 1. Находим расстояние между центром окружности и точкой, из которой проводятся касательные
        float distance = Vector2.Distance(new Vector2(circleCenter.position.x, circleCenter.position.y), new Vector2(tangentPoint.position.x, tangentPoint.position.y));

        // 2. Проверяем, есть ли решение для касательных
        if (distance > circleRadius)
        {
            // 3. Находим угол alpha
            float alpha = Mathf.Atan2(tangentPoint.position.y - circleCenter.position.y, tangentPoint.position.x - circleCenter.position.x);

            // 4. Находим точки пересечения касательных
            float x1 = circleCenter.position.x + circleRadius * Mathf.Cos(alpha);
            float y1 = circleCenter.position.y + circleRadius * Mathf.Sin(alpha);

            float x2 = circleCenter.position.x + circleRadius * Mathf.Cos(alpha + Mathf.PI);
            float y2 = circleCenter.position.y + circleRadius * Mathf.Sin(alpha + Mathf.PI);

            // Выводим результат в консоль

            dots[0].transform.position = new Vector3(x1, y1, 0);
            dots[1].transform.position = new Vector3(x2, y2, 0);
        }
        else
        {
            Debug.Log("Решение не существует, так как расстояние больше радиуса окружности.");
        }
    }

    void MyCalc(Vector2 dotPos, Vector2 cPos, float rad)
    {
        float L = Vector2.Distance(cPos, dotPos);

        float cosA = rad / L;

        float sinA2 = (1 - cosA * cosA);
        float sinA = Mathf.Sqrt(sinA2);

        float xd = (cPos.x - dotPos.x) / L;
        float yd = (cPos.y - dotPos.y) / L;

        Vector2 t1n = new Vector2(cosA * xd - sinA * yd, cosA * yd + sinA * xd);
        Vector2 t2n = new Vector2(cosA * xd + sinA * yd, cosA * yd - sinA * xd);

        Vector2 t1 = cPos - t1n * rad;
        Vector2 t2 = cPos - t2n * rad;

        dots[0].transform.position = t1;
        dots[1].transform.position = t2;
    }

    bool CircleTangents_2(Vector2 center, float r, Vector2 p, ref Vector2 tanPosA, ref Vector2 tanPosB)
    {
        p -= center;

        float P = p.magnitude;

        // if p is inside the circle, there ain't no tangents.
        if (P <= r)
        {
            return false;
        }

        float a = r * r / P;
        float q = r * (float)System.Math.Sqrt((P * P) - (r * r)) / P;

        Vector2 pN = p / P;
        Vector2 pNP = new Vector2(-pN.y, pN.x);
        Vector2 va = pN * a;

        tanPosA = va + pNP * q;
        tanPosB = va - pNP * q;

        tanPosA += center;
        tanPosB += center;

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        dots = new GameObject[2];
        dots[0] = Instantiate(DotPrefab);
        dots[1] = Instantiate(DotPrefab);

        //radius = Circle.GetComponent<CircleCollider2D>().radius;
        radius = Circle.transform.localScale.x * 0.5f;
    }

    void Update()
    {

        //CalculateTangentPoints(Dot.transform, Circle.transform, radius);
        MyCalc(Dot.transform.position, Circle.transform.position, radius);
        /*Vector2 t1 = new Vector2(), t2 = new Vector2();
        CircleTangents_2(Circle.transform.position, radius, Dot.transform.position, ref t1, ref t2);
        dots[0].transform.position = t1;
        dots[1].transform.position = t2;*/

        Debug.DrawLine(Dot.transform.position, dots[0].transform.position, Color.red);
        Debug.DrawLine(Dot.transform.position, dots[1].transform.position, Color.red);
    }
}