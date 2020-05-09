using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

//Monster 상속
public class TurtleShell : Monster
{
    public override string Name { get => "거북이"; }
    public float moveSpeed;
    //자동순찰 범위 변수
    public float moveRange;
    //플레이어 인식거리
    public float reactDist;
    private Transform _transform;
    private GameObject player;
    private Vector3 playerPos;
    //몬스터와 플레이어와의 거리
    private float playerDist;
    //자동순찰 간격시간 랜덤 변수
    private float moveRanTime;
    //자동순찰 랜덤 행동 변수 0:휴식, 1~2:순찰 (순찰 횟수의 확률을 높이고자 선택지를 2개로 주었음)
    private int moveType = 0;
    private Vector3 startPos;
    private Vector3 targetPos;
    private Vector3 targetLook;
    private NavMeshAgent nav;


    protected override void Start()
    {
        base.Start();
        _transform = GetComponent<Transform>();
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player");        
        startPos = _transform.position;
        StartCoroutine(MoveCtrl());
    }

    void FixedUpdate()
    {
        Move();
    }

    void Update()
    {
        DistChk();
        playerPos = player.transform.position;
    }

    IEnumerator MoveCtrl()
    {
        //몬스터의 순찰 목표 좌표를 랜덤을 설정
        float posY = _transform.position.y;
        float ranX = Random.Range(-1f, 1f);
        float ranZ = Random.Range(-1f, 1f);
        targetPos = new Vector3(_transform.position.x + ranX, posY, _transform.position.z + ranZ);

        //몬스터 생성 위치를 기준으로 지정한 범위내에서만 순찰
        if (Vector3.Distance(startPos, targetPos) <= moveRange)
        {
            targetLook = targetPos - _transform.position;
            moveType = Random.Range(0, 3);
            moveRanTime = Random.Range(3, 7);
            Debug.Log("몬스터 행동타입은" + moveType + "입니다. 0:휴식 1~2:이동");
            Debug.Log("몬스터가" + moveRanTime + "초 후 다음 행동을 진행합니다.");
            Debug.Log("몬스터와 플레이어의 거리는"+playerDist+"입니다");
            yield return new WaitForSeconds(moveRanTime);
            StartCoroutine(MoveCtrl());
        }
        //지정한 범위를 벗어날시 목표 좌표 재설정
        else
            StartCoroutine(MoveCtrl());
        
    }


    void Move()
    {
        //무브 타입이 1~2일시에 목표 좌표로 순찰이동
        if (moveType == 1 || moveType == 2)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, targetPos, moveSpeed * Time.deltaTime);
            _transform.rotation = Quaternion.LookRotation(targetLook);
        }
        
        else
            _transform.position += Vector3.zero;
    }

    //플레이어와의 거리체크와 반응
    void DistChk()
    {
        
        playerDist = Vector3.Distance(_transform.position, playerPos);


    }







}
