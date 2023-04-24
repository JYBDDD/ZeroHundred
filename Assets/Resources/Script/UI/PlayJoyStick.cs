using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UniRx.Triggers;
using UniRx;

public class PlayJoyStick : MonoBehaviour,IBeginDragHandler,IDragHandler, IEndDragHandler        // Panel 삭제시 레버가 클릭지점을 따라가지 않음 (참고※)
{
    public static PlayerControllerEx playerControllerEx;

    public RectTransform joyStick;
    public Image leverImage;
    public Vector3 joyVec;          // 조이스틱 레버의 위치값 및 방향 정규화 된것 
    private bool isMove;            // 터치상태 체크

    Vector2 stickFirstPosition;

    private Camera mainCamera;      // Main 카메라

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        playerControllerEx = GameManager.Player.playerController;
        // - UniRx 조이스틱 이동값에 따른 값 플레이어에 전달
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
        joyVec = (DragPosition - stickFirstPosition).normalized; // lever 드래그 방향,위치값

        //Vector3.Distance(DragPosition, stickFirstPosition);  // 드래그위치 - 스틱의 첫번째 위치

        joyStick.transform.position = DragPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        isMove = false;

        joyVec = Vector3.zero;
        joyStick.transform.position = stickFirstPosition;
    }
}
