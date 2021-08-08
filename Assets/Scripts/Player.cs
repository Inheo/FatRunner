using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer _skinnedMesh;
    [SerializeField] private Animator _animator;

    [SerializeField] private int _countFoodFormaxFatness = 20;

    [Header("Move Settings")]
    [SerializeField] private float _moveSpeed = 5;
    private bool canMove = false;

    private int _percentObesity;

    private Vector2 mousePosition;

    private int _xPos = 0;

    private void Start()
    {
        _xPos = 0;
    }

    private void Update()
    {
        if (!canMove) return;

        Rotate();
    }

    private void FixedUpdate()
    {
        if (!canMove) return;
        transform.Translate(transform.forward * _moveSpeed * Time.deltaTime);
    }

    private void Rotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            _xPos += (int)Mathf.Sign(Input.mousePosition.x - mousePosition.x);
            _xPos = Mathf.Clamp(_xPos, -1, 1);
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(_xPos, transform.position.y, transform.position.z), _moveSpeed * Time.deltaTime * 2);
    }

    public void StartRun()
    {
        _animator.SetBool("Run", true);
        canMove = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Food food)) 
        {
            if (food.Type == FoodType.ForObesity)
            {
                _percentObesity += 100 / _countFoodFormaxFatness;
            }
            else if (food.Type == FoodType.ForSlimming)
            {
                _percentObesity -= 100 / _countFoodFormaxFatness;
            }

            _percentObesity = Mathf.Clamp(_percentObesity, 0, 100);
            _animator.SetFloat("Fatness", _percentObesity / 100);
            _skinnedMesh.SetBlendShapeWeight(0, _percentObesity);

            other.gameObject.SetActive(false);
        }
        else if(other.gameObject.layer == 8) // if other is Finish
        {
            _animator.SetBool("Dance", true);
            _animator.SetBool("Run", false);
            canMove = false;
        }
    }
}
