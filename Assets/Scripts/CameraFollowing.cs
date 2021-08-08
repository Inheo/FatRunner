using UnityEngine;

public class CameraFollowing : MonoBehaviour
{
    [SerializeField] private Transform _player;

    private Vector3 _offset;


    void Start()
    {
        _offset = _player.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z - _offset.z);
    }
}
