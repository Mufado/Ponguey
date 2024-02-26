using System.Collections;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rb;

    [SerializeField] private float _shootForce = 250f;
    [SerializeField] private float _shootDelay = 2f;
    [SerializeField] private GameObject _indicator;

    private Vector3 _initialPos;
    private Quaternion _initialRotation;

    private void Awake()
    {
        _initialPos = transform.position;
        _initialRotation = transform.rotation;
        _rb = GetComponent<Rigidbody2D>();

        StartCoroutine(HandleShoot());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        UpdateScore(ref collision);
        ResetPosition();
        StartCoroutine(HandleShoot());
    }

    private void UpdateScore(ref Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Left Court"))
        {
            GameManager.Instance.UpdateScore(Enums.Player.Two);
        }
        else if (collision.gameObject.CompareTag("Right Court"))
        {
            GameManager.Instance.UpdateScore(Enums.Player.One);
        }
    }

    private void ResetPosition()
    {
        transform.position = _initialPos;
        transform.rotation = _initialRotation;
        _rb.velocity = Vector2.zero;
    }

    private IEnumerator HandleShoot()
    {
        transform.rotation *= Quaternion.Euler(0f, 0f, Random.Range(-90f, 90f));

        float gravityScale = _rb.gravityScale;
        _rb.gravityScale = 0;

        yield return IndicateShootDirection(_shootDelay);

        _rb.AddForce(transform.right * _shootForce);
        _rb.gravityScale = gravityScale;
    }

    private IEnumerator IndicateShootDirection(float delay)
    {
        _indicator.SetActive(true);

        yield return new WaitForSecondsRealtime(delay);

        _indicator.SetActive(false);
    }
}
