using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx.Triggers;
using UniRx;

public class PlayJoyStick : MonoBehaviour,IBeginDragHandler,IDragHandler, IEndDragHandler        // Panel ������ ������ Ŭ�������� ������ ���� (�����)
{
    public static PlayerControllerEx playerControllerEx;

    public RectTransform joyStick;
    public Image leverImage;
    public Vector3 joyVec;          // ���̽�ƽ ������ ��ġ�� �� ���� ����ȭ �Ȱ� 
    private bool isMove;            // ��ġ���� üũ

    Vector2 stickFirstPosition;

    private Camera mainCamera;      // Main ī�޶�

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        playerControllerEx = GameManager.Player.playerController;
        // - UniRx ���̽�ƽ �̵����� ���� �� �÷��̾ ����
        this.UpdateAsObservable().Where(_ => isMove).Subscribe(_=> playerControllerEx.Movement(joyVec));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isMove = true;

        Vector2 mouseWorldPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        joyStick.transform.position = mouseWorldPos;
        stickFirstPosition = joyStick.transform.position;

        leverImage.enabled = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 DragPosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        joyVec = (DragPosition - stickFirstPosition).normalized; // lever �巡�� ����,��ġ��

        //Vector3.Distance(DragPosition, stickFirstPosition);  // �巡����ġ - ��ƽ�� ù��° ��ġ

        joyStick.transform.position = DragPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isMove = false;

        joyVec = Vector3.zero;
        joyStick.transform.position = stickFirstPosition;
    }
}
