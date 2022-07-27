using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawners;

public sealed class Target : MonoBehaviour
{
    private static readonly HashSet<Target> allTarget = new HashSet<Target>();

    /// <summary>
    /// 타겟이 버틸 수 있는 피격 수
    /// </summary>
    public int Life { get; set; }

    private Vector3 force;

    private Vector3 initialVelocity;

    private bool isColliding;

    private Rigidbody rb;

    private TargetController controller;

    /// <summary>
    /// 이거를 갈아치워주면 다른 타겟처럼 행동하기 시작함
    /// </summary>
    public TargetController Controller
    {
        get => controller;
        set
        {
            controller?.DetachThis();
            controller = value;
            controller?.AttachThis(this);
        }
    }

    private void Awake()
    {
        allTarget.Add(this);

        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!(controller is { IsResourceLoaded: true })) return;

        Move();
        GetStuck();
        Disappear();
        GameOver();

        controller.OnUpdate();
    }

    private void FixedUpdate()
    {
        Stop();
    }

    private void OnDestroy()
    {
        Controller = null;
    }

    private IEnumerator OnCollisionEnter(Collision collision)
    {
        // 분열된 타겟끼리 충돌할 때 (분열 안 함)
        if (controller.IsParticle && collision.gameObject.CompareTag("Target") && collision.transform.GetComponent<Target>().Controller.IsParticle)
        {
            GameObject go;

            isColliding = true;

            // 뒤에 있는 타겟은 튕겨남
            if (transform.position.z > collision.transform.position.z)
            {
                go = collision.gameObject;
                var back = gameObject;

                yield return StartCoroutine(back.GetComponent<Target>().GetForce((transform.position - collision.transform.position).normalized));
            }
            else
            {
                go = gameObject;
                var back = collision.gameObject;

                yield return StartCoroutine(back.GetComponent<Target>().GetForce((collision.transform.position - transform.position).normalized));
            }

            // 앞에 있는 타겟은 사라짐
            var con = go.GetComponent<Target>().Controller;

            TargetSpawner.targetPool[con.Name].Enqueue(con);
            go.SetActive(false);

            ScoreManager.Instance.IncreaseScore(controller.Score);

            int x = Random.Range(1, 4);
            SoundManager.Instance.PlaySFXSound("targetHit" + x, 0.2f);
        }

        // 분열된 타겟과 고정된 타겟이 충돌할 때
        if (!controller.IsParticle && collision.gameObject.CompareTag("Target"))
        {
            // 앞으로 튀어나가지 않게 분열
            if (controller.CheckParticleRoute((transform.position - collision.transform.position).normalized)) GetHit(collision.transform.position);
            else
            {
                TargetSpawner.targetPool[controller.Name].Enqueue(controller);
                gameObject.SetActive(false);

                ScoreManager.Instance.IncreaseScore(controller.Score);

                int x = Random.Range(1, 4);
                SoundManager.Instance.PlaySFXSound("targetHit" + x, 0.2f);
            }

            var con = collision.gameObject.GetComponent<Target>().Controller;

            TargetSpawner.targetPool[con.Name].Enqueue(con);
            collision.gameObject.SetActive(false);

            ScoreManager.Instance.IncreaseScore(con.Score);
        }

        // 위아래 벽에 충돌할 때
        if (collision.gameObject.CompareTag("VerticalCurtain"))
        {
            isColliding = true;

            force.y = -force.y;
            force *= 0.8f;

            rb.AddForce(force, ForceMode.Impulse);
        }

        // 좌우 벽에 충돌할 때
        if (collision.gameObject.CompareTag("HorizontalCurtain"))
        {
            isColliding = true;

            force.x = -force.x;
            force *= 0.8f;

            rb.AddForce(force, ForceMode.Impulse);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
    }

    private static void UnloadResources()
    {
        allTarget.Clear();
    }

    private void Move()
    {
        if (!controller.IsParticle) transform.Translate(1f * Time.deltaTime * Vector3.back);
    }

    private void GetStuck()
    {
        var pos = transform.position;

        if (pos.z > 20f)
        {
            rb.velocity = Vector3.zero;

            pos.z = 20f;
            transform.position = pos;

            controller.IsParticle = false;
        }
    }

    private void Stop()
    {
        if (controller.IsParticle && !isColliding && rb.velocity.magnitude < initialVelocity.magnitude * 0.5f)
        {
            rb.velocity = Vector3.zero;

            controller.IsParticle = false;
        }
    }

    private void Disappear()
    {
        var pos = transform.position;

        if (pos.x <= -3f || pos.x >= 3f || pos.y <= 0.1f || pos.y >= 7f)
        {
            TargetSpawner.targetPool[controller.Name].Enqueue(controller);
            gameObject.SetActive(false);
        }
    }

    private void GameOver()
    {
        if (!GameManager.Instance.IsGameOver && transform.position.z <= 2f)
        {
            UnloadResources();

            GameManager.Instance.GameOver();
        }
    }

    public void GetHit(Vector3 pos)
    {
        if (controller.IsParticle) return;

        Life--;

        if (Life == 0) controller.Fission(pos);
    }

    public IEnumerator GetForce(Vector3 direction)
    {
        force = direction * 15f;

        rb.AddForce(force, ForceMode.Impulse);

        yield return new WaitForFixedUpdate();

        initialVelocity = rb.velocity;
    }
}
