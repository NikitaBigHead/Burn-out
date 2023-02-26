using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    // ������ �����, ������� ����� ���������� � ��������
    private GameObject enemyPrefab;

    [SerializeField]
    // ������ ������ � ������� ���������� caption ��� ���������
    private GameObject tutorialUI;

    [SerializeField]
    // ����� ���� ����� ��������� ���������
    private TextMeshProUGUI caption;

    [SerializeField]
    // �����
    private Door door;

    private GameObject enemy;

    private GameObject player;
    private PlayerMovement playerMovement;

    private enum State
    {
        WaitForAttack,
        WaitForMove,
        WaitAfterMove,
        WaitForKill,
        End
    }

    private State state;

    private void Awake()
    {
        if (StateController.tutorialComplited) Destroy(this.gameObject);
    }

    void Start()
    {
        door.open = false;
        tutorialUI.SetActive(true);
        state = State.WaitForAttack;
        enemy = Instantiate(enemyPrefab, transform);
        enemy.transform.position = new Vector3(-4, 0, -1);
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(4, 0, -1);
        playerMovement = player.GetComponent<PlayerMovement>();

        // ����� �� ����� ��������� ���� �� ����� �� ������ ����
        playerMovement.speed = 0;
    }

    void Update()
    {
        switch (state)
        {
            // ��� ������� ����
            case State.WaitForAttack:
                // ����� ���� ������, �� ������ ����� ���������� � ������ ���������
                if (Input.GetMouseButtonDown(0))
                {
                    caption.text = "����������� ������� WASD ��� ������� ��� ������������";
                    playerMovement.speed = 0.1f;
                    state = State.WaitForMove;
                }
                break;

            // ��� ��������
            case State.WaitForMove:
                if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0)
                {
                    state = State.WaitAfterMove;
                    StartCoroutine(WaitAfterMove());
                }
                break;

            // ������ �� ������
            case State.WaitAfterMove:
                // Nothing
                break;

            // ��� ���� ������ �� ����
            case State.WaitForKill:
                if (enemy == null)
                {
                    caption.text = "��� �������������� �������� ����� ��������������� �������";
                    StartCoroutine(WaitAfterKill());
                }
                break;

            // ����� ���������
            case State.End:
                Destroy(this.gameObject);
                break;
        }
    }

    IEnumerator WaitAfterMove()
    {
        yield return new WaitForSeconds(2.3f);
        caption.text = "�������� ���� ������ � ������� ���� ����������";
        state = State.WaitForKill;
    }

    IEnumerator WaitAfterKill()
    {
        yield return new WaitForSeconds(3f);
        tutorialUI.SetActive(false);
        StateController.tutorialComplited = true;
        door.open = true;
        state = State.End;
    }
}
