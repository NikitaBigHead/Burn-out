using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavensAi : MonoBehaviour
{
    public float damage = 15;

    public float speed = 0.1f;

    public float maxX = 26;
    public float maxY = 12;

    private Vector2 direction;
    private Vector2 startPos;

    private Transform boss;
    private float offset;

    public void Release()
    {
        StartCoroutine(FlyOffScreen());
    }

    public void Ensnare(Transform boss, Vector2 dot, float offset, float speed)
    {
        this.offset = offset;
        this.speed = speed;
        this.boss = boss;
        Vector2 localDir = new Vector2(dot.x - boss.position.x, dot.y - boss.position.y).normalized;
        Vector2 rLocalDir = Quaternion.AngleAxis(45f, Vector3.forward) * localDir;
        if (rLocalDir.y > 0)
            if (rLocalDir.x > 0) startPos = new Vector2(maxX, maxY * 1.43f * localDir.y); // RIGHT
            else startPos = new Vector2(maxX * 1.43f * localDir.x, maxY); // UP
        else
            if (rLocalDir.x > 0) startPos = new Vector2(maxX * 1.43f * localDir.x, -maxY); // DOWN
        else startPos = new Vector2(-maxX, maxY * 1.43f * localDir.y); // LEFT

        float angle = Mathf.Atan2(-localDir.y, -localDir.x) * Mathf.Rad2Deg + 90f;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
        this.transform.position = startPos;
        StartCoroutine(FlyToBoss());
    }

    IEnumerator FlyOffScreen()
    {
        yield return new WaitForFixedUpdate();
    }

    IEnumerator FlyToBoss()
    {
        Vector2 localDir = new Vector2(boss.position.x - transform.position.x, boss.position.y - transform.position.y);
        while (localDir.magnitude > 1f)
        {
            Vector2 nLocalDir = localDir.normalized;
            float angle = Mathf.Atan2(nLocalDir.y, nLocalDir.x) * Mathf.Rad2Deg + 90f;
            this.transform.rotation = Quaternion.Euler(0, 0, angle);
            this.transform.position += new Vector3(nLocalDir.x, nLocalDir.y, 0) * speed;
            yield return new WaitForFixedUpdate();
        }
    }

    void WaitForAnimation()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<AttackableEntity>().RecieveDamage(damage);
        }
    }
}
