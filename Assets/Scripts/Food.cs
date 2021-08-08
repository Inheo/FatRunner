using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private FoodType _type;
    [SerializeField] private float _rotateSpeed = 100;

    public FoodType Type => _type;

    private void Update()
    {
        transform.Rotate(transform.up * _rotateSpeed * Time.deltaTime);
    }
}

public enum FoodType
{
    ForObesity,
    ForSlimming
}
