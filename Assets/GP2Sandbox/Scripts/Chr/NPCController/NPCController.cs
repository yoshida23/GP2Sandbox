//#define DEBUG_LOG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AM1
{
    /// <summary>
    /// NPCを制御するスクリプト。
    /// 流れと各処理を結合するための下位クラスとして実装
    /// </summary>
    public class NPCController : MonoBehaviour
    {
        [Tooltip("攻撃距離"), SerializeField]
        float attackRange = 10f;
        [Tooltip("到着距離"), SerializeField]
        float reachDistance = 1f;
        [Tooltip("移動制限時間"), SerializeField]
        float moveStopSeconds = 5f;
        [Tooltip("角度誤差"), SerializeField]
        float degreeError = 5f;
        [Tooltip("ショットオブジェクト"), SerializeField]
        ShotAssetBase shotObject = default;

        /// <summary>
        /// 状態定義
        /// </summary>
        public enum State
        {
            Search,
            Move,
            Attack,
            Dead
        }

        /// <summary>
        /// 現在の状態
        /// </summary>
        State currentState = State.Search;

        /// <summary>
        /// 移動を終了する距離
        /// </summary>
        float targetReachDistance;

        ISearch search;
        IHeadToPoint headToPoint;
        IShooter shooter;
        ShotController shotController;

        int searchLayer;
        Transform targetTransform;
        Rigidbody rb;

        private void Awake()
        {
            search = GetComponent<ISearch>();
            headToPoint = GetComponent<IHeadToPoint>();
            searchLayer = LayerMask.GetMask("Player", "NPC");
            rb = GetComponent<Rigidbody>();
            shooter = shotObject.GetShooterInstance();
            shotController = GetComponent<ShotController>();
        }

        void Start()
        {
            StartCoroutine(UpdateCoroutine());
        }

        IEnumerator UpdateCoroutine()
        {
            while (currentState != State.Dead)
            {
                switch (currentState)
                {
                    case State.Search:
                        yield return SearchProc();
                        break;

                    case State.Move:
                        yield return MoveProc();
                        break;

                    case State.Attack:
                        yield return AttackProc();
                        break;

                    default:
                        yield return null;
                        break;
                }
            }
        }

        /// <summary>
        /// 探索処理
        /// </summary>
        /// <returns></returns>
        IEnumerator SearchProc()
        {
            yield return search.Search(searchLayer);
            targetTransform = search.GetFoundObjectTransform();
            if (targetTransform == null)
            {
                // ランダムで目的地を設定
                targetTransform = Spawner.GetRandomTransform();
                currentState = State.Move;
                targetReachDistance = reachDistance;
            }
            else
            {
                // 攻撃するか移動するか
                float distance = Vector3.Distance(targetTransform.position, transform.position);
                if (distance < attackRange)
                {
                    currentState = State.Attack;
                }
                else
                {
                    currentState = State.Move;
                }
                targetReachDistance = attackRange;
            }

            GameManager.DebugLog($"Select {currentState}");
        }

        /// <summary>
        /// targetTransformを目指して移動。
        /// 一定時間が経過するか、既定の距離まで近づくか、Deadになったら終了
        /// </summary>
        /// <returns></returns>
        IEnumerator MoveProc()
        {
            float endTime = Time.time + moveStopSeconds;

            while (currentState != State.Dead)
            {
                // 移動時間切れ
                if (Time.time >= endTime)
                {
                    currentState = State.Search;
                    break;
                }

                // 到着チェック
                var to = targetTransform.position - transform.position;
                if (to.magnitude < targetReachDistance)
                {
                    // 到着
                    rb.angularVelocity = Vector3.zero;
                    currentState = State.Search;
                    break;
                }

                // 方向調整
                headToPoint.TargetPoint(transform.position - to.normalized);

                // ショット
                float angle = Mathf.Abs(180f-Vector3.Angle(transform.forward, to.normalized));
                if (angle <= degreeError)
                {
                    shotController.Shot(shooter);
                }

                yield return null;
            }
        }

        IEnumerator AttackProc()
        {
            while (currentState != State.Dead)
            {
                // ターゲットから離れたら索敵へ
                if ((targetTransform == null)
                    || (Vector3.Distance(targetTransform.position, transform.position) > attackRange))
                {
                    currentState = State.Search;
                    break;
                }

                // 方向調整
                headToPoint.TargetPoint(targetTransform.position);

                // ショット
                var to = targetTransform.position - transform.position;
                if (to.magnitude > 0)
                {
                    float angle = Vector3.Angle(transform.forward, to.normalized);
                    if (angle <= degreeError)
                    {
                        shotController.Shot(shooter);
                    }
                }
                yield return null;
            }
        }
    }
}