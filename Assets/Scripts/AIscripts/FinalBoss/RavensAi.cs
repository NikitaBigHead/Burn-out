using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RavensAi : MonoBehaviour
{
    [SerializeField]
    private GameObject ravensPrefab;

    public float damage = 15;

    public float speed = 0.1f;

    public float maxX = 26;
    public float maxY = 12;

    private Vector2 distance;
    private Vector2 startPos;

    private Transform boss;
    private float offset;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Release()
    {
        StartCoroutine(FlyOffScreen());
    }

    public void Ensnare(Transform boss, Vector2 dot, float offset, float speed)
    {
        //animator.Play("OutAnimation");
        this.offset = Mathf.Abs(offset);
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

    public void DissolveOut(Transform p, Vector2 direction, Transform boss, float offset)
    {
        this.boss = boss;
        this.offset = offset;
        this.transform.position = p.position + new Vector3(direction.x, direction.y, 0) * offset * 2;
        this.transform.rotation = p.rotation * Quaternion.AngleAxis(180, Vector3.forward);
        transform.SetParent(boss);
        
        Invoke("WaitForDissolvingOut", 1f);
    }

    //void WaitForTest()
    //{
    //    animator.Play("DissolveOutAnimation");
    //    Invoke("WaitForDissolvingOut", 1f);
    //}

    void WaitForDissolvingOut()
    {
        animator.Play("InAnimation");
        Invoke("WaitForAnimation", 1f);
    }

    IEnumerator FlyOffScreen()
    {
        yield return new WaitForFixedUpdate();
    }

    IEnumerator FlyToBoss()
    {
        distance = new Vector2(boss.position.x - transform.position.x, boss.position.y - transform.position.y);
        while (distance.magnitude > offset * 3f)
        {
            distance = new Vector2(boss.position.x - transform.position.x, boss.position.y - transform.position.y);
            Vector2 nLocalDir = distance.normalized;
            float angle = Mathf.Atan2(nLocalDir.y, nLocalDir.x) * Mathf.Rad2Deg + 90f;
            this.transform.rotation = Quaternion.Euler(0, 0, angle);
            this.transform.position += new Vector3(nLocalDir.x, nLocalDir.y, 0) * speed;
            yield return new WaitForFixedUpdate();
        }
        this.transform.SetParent(boss);
        animator.Play("DissolveInAnimation");
        Instantiate(ravensPrefab).GetComponent<RavensAi>().DissolveOut(transform, distance.normalized, boss, offset);
        Invoke("WaitForAnimation", 1f);
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
