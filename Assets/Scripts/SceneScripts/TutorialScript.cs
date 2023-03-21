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
    private TakeItemEdited pan;

    [SerializeField]
    // �����
    private Door door;

    private GameObject enemy;

    private GameObject player;
    private PlayerMovement playerMovement;

    private enum State
    {
        WaitForPickUpPan,
        WaitForMove,
        WaitAfterMove,
        WaitForAttack,
        WaitForKill,
        End
    }

    private State state;

    private bool panPicked = false;

    private void Awake()
    {
        if (PlayerData.tutorialComplited) {
            pan.gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
    }

    void Start()
    {
        door.open = false;
        tutorialUI.SetActive(true);
        state = State.WaitForMove;
        enemy = Instantiate(enemyPrefab, transform);
        enemy.transform.position = new Vector3(-4, 0, -1);
        player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = new Vector3(4, 0, -1);
        playerMovement = player.GetComponent<PlayerMovement>();
        caption.text = "����������� ������� WASD ��� ������� ��� ������������.";
        pan.OnPickUp = (GameObject sender) => { panPicked = true; };
        // ����� �� ����� ��������� ���� �� ����� �� ������ ����
        // playerMovement.speed = 0;
    }

    void Update()
    {
        switch (state)
        {
            // ��� ������� ���������
            case State.WaitForPickUpPan:
                if (panPicked)
                {
                    caption.text = "������� ����� ������ ���� ��� �����...";
                    state = State.WaitForAttack;
                }
                break;

            // ��� ������� ����
            case State.WaitForAttack:
                // ����� ���� ������, �� ������ ����� ���������� � ������ ���������
                if (Input.GetMouseButtonDown(0))
                {
                    caption.text = "�������� ���� ������ � ������� ���� ����������.";
                    state = State.WaitForKill;
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
                    caption.text = "��� �������������� �������� ����� ��������������� �������.";
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
        yield return new WaitForSeconds(2f);
        caption.text = "������ ���������� ��� �����������!";
        state = State.WaitForPickUpPan;
    }

    IEnumerator WaitAfterKill()
    {
        yield return new WaitForSeconds(2f);
        tutorialUI.SetActive(false);
        PlayerData.tutorialComplited = true;
        door.open = true;
        state = State.End;
    }
}
