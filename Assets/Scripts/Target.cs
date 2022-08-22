using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawners;

public sealed class Target : MonoBehaviour
{
    private static readonly HashSet<Target> allTarget = new HashSet<Target>();

    private Vector3 force;

    private Vector3 initialVelocity;

    private float speed = 1f;

    private bool isColliding;

    private Rigidbody rb;

    private TargetController controller;

    private float timeStamp = 0f;

    private Quaternion rotation;

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

        rotation = Random.rotation;
    }

    private void Update()
    {
        if (!(controller is { IsResourceLoaded: true })) return;

        Move();
        GetStuck();
        Disappear();
        SpeedUp();
        GameOver();
        Rotate();

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

            UIManager.Instance.IncreaseScore(controller.Score);

            int x = Random.Range(1, 4);
            SoundManager.Instance.PlaySFXSound("targetHit" + x, 0.2f);

            ParticleManager.Instance.PlayParticle("HitEffect", go.transform.position);

            // 앞에 있는 타겟은 사라짐
            var con = go.GetComponent<Target>().Controller;

            TargetSpawner.targetPool[con.Type].Enqueue(con);
            go.SetActive(false);
        }

        // 분열된 타겟과 고정된 타겟이 충돌할 때
        if (!controller.IsParticle && collision.gameObject.CompareTag("Target"))
        {
            UIManager.Instance.IncreaseScore(controller.Score);

            int x = Random.Range(1, 4);
            SoundManager.Instance.PlaySFXSound("targetHit" + x, 0.2f);

            ParticleManager.Instance.PlayParticle("HitEffect", transform.position);

            TargetSpawner.targetPool[controller.Type].Enqueue(controller);
            gameObject.SetActive(false);

            GetHit(collision.transform.position);

            var con = collision.gameObject.GetComponent<Target>().Controller;

            TargetSpawner.targetPool[con.Type].Enqueue(con);
            collision.gameObject.SetActive(false);

            UIManager.Instance.IncreaseScore(con.Score);
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

    private static void UnloadPool()
    {
        TargetSpawner.targetPool.Clear();
        ParticleSpawner.particlePool.Clear();
    }

    private void Move()
    {
        if (!controller.IsParticle) transform.Translate(speed * Time.deltaTime * Vector3.back);
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

        if (rb.velocity.z < 0 || pos.x <= -3f || pos.x >= 3f || pos.y <= 0.1f || pos.y >= 7f)
        {
            TargetSpawner.targetPool[controller.Type].Enqueue(controller);
            gameObject.SetActive(false);
        }
    }

    private void SpeedUp()
    {
        // GameManager.Instance.speed += Time.deltaTime / 100;

        var value = Mathf.Log10(Time.time);

        speed = Mathf.Clamp(value, 1.0f, value);
    }

    private void GameOver()
    {
        if (!GameManager.Instance.IsGameOver && transform.position.z <= 2f)
        {
            UnloadResources();
            UnloadPool();

            GameManager.Instance.GameOver();
        }
    }

    public void GetHit(Vector3 pos)
    {
        if (controller.IsParticle) return;
        
        controller.Fission(pos);
    }

    public IEnumerator GetForce(Vector3 direction)
    {
        force = direction * 15f;

        rb.AddForce(force, ForceMode.Impulse);

        yield return new WaitForFixedUpdate();

        initialVelocity = rb.velocity;
    }

    private void Rotate()
    {
        timeStamp += Time.deltaTime*0.5f;
        gameObject.transform.GetChild(0).rotation = Quaternion.SlerpUnclamped(Quaternion.identity, rotation, timeStamp);
    }
}
