using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    [SerializeField]
    // Префаб врага, который будет спавниться в обучении
    private GameObject enemyPrefab;

    [SerializeField]
    // Пустой объект в котором находиться caption для туториала
    private GameObject tutorialUI;

    [SerializeField]
    // Текст куда будет выводится подсказка
    private TextMeshProUGUI caption;

    [SerializeField]
    private TakeItemEdited pan;

    [SerializeField]
    // Дверь
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
        caption.text = "Используйте клаваши WASD или стрелки для передвижения.";
        pan.OnPickUp = (GameObject sender) => { panPicked = true; };
        // Игрок не может двигаться пока не нажмёт на кнопку мыши
        // playerMovement.speed = 0;
    }

    void Update()
    {
        switch (state)
        {
            // Ждём подбора сковороды
            case State.WaitForPickUpPan:
                if (panPicked)
                {
                    caption.text = "Нажмите левую кнопку мыши для атаки...";
                    state = State.WaitForAttack;
                }
                break;

            // Ждём нажатия мыши
            case State.WaitForAttack:
                // Когда мышь нажата, то меняем текст подсказики и меняем состояние
                if (Input.GetMouseButtonDown(0))
                {
                    caption.text = "Одолейте злое чучело с помощью атак сковородой.";
                    state = State.WaitForKill;
                }
                break;

            // Ждём движения
            case State.WaitForMove:
                if (Input.GetAxisRaw("Horizontal") > 0 || Input.GetAxisRaw("Vertical") > 0)
                {
                    state = State.WaitAfterMove;
                    StartCoroutine(WaitAfterMove());
                }
                break;

            // Ничего не делаем
            case State.WaitAfterMove:
                // Nothing
                break;

            // Ждём пока чучело не умрёт
            case State.WaitForKill:
                if (enemy == null)
                {
                    caption.text = "Для восстановления здоровья можно воспользоваться блинами.";
                    StartCoroutine(WaitAfterKill());
                }
                break;

            // Конец туториала
            case State.End:
                Destroy(this.gameObject);
                break;
        }
    }

    IEnumerator WaitAfterMove()
    {
        yield return new WaitForSeconds(2f);
        caption.text = "Хватай сковородку для самообороны!";
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
