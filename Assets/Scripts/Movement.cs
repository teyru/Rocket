using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private float _thrustPower;
    [SerializeField] private float _thrustRotate;
    [SerializeField] private AudioClip _audioClip;

    [SerializeField] private ParticleSystem _thrustParticles;
    [SerializeField] private ParticleSystem _leftThrustParticles;
    [SerializeField] private ParticleSystem _rightThrustParticles;


    private Rigidbody _rigidbody;
    private AudioSource _thrustAudio;

    private bool _IsAlive;

    private void Awake()
    {
        if (_thrustPower < 700f)
        {
            _thrustPower = 700f;
        }
        if(_thrustRotate < 200f)
        {
            _thrustRotate = 200f;
        }
    } 

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _thrustAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        bool rotateToLeftDown = Input.GetKey(KeyCode.A);
        bool rotateToRightDown = Input.GetKey(KeyCode.D);
        bool move = Input.GetKey(KeyCode.Space);
        
        
        MoveController(move);
        Rotation(rotateToLeftDown, rotateToRightDown);
    }

    private void MoveController(bool move)
    {
        if (move)
        {
            SartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }

    private void StopThrusting()
    {
        _thrustAudio.Stop();
        _thrustParticles.Stop();
    }

    private void SartThrusting()
    {
        _rigidbody.AddRelativeForce(Vector3.up * _thrustPower * Time.deltaTime);
        if (!_thrustAudio.isPlaying) _thrustAudio.PlayOneShot(_audioClip);

        if (!_thrustParticles.isPlaying)
        {
            _thrustParticles.Play();
        }
    }

    private void Rotation(bool rotateToLeft, bool rotateToRight)
    {
        if (rotateToLeft)
        {
            RotatetoLeft();
        }

        if (rotateToRight)
        {
            RotateToRight();
        }

        if (!rotateToRight)_rightThrustParticles.Stop();

        if (!rotateToLeft)_leftThrustParticles.Stop();
    }

    private void RotateToRight()
    {
        if (!_rightThrustParticles.isPlaying)
        {
            _rightThrustParticles.Play();
        }
        ApplyRotate(-_thrustRotate);
    }

    private void RotatetoLeft()
    {
        if (!_leftThrustParticles.isPlaying)
        {
            _leftThrustParticles.Play();
        }
        ApplyRotate(_thrustRotate);
    }

    private void ApplyRotate(float rotationThisFrame)
    {
        _rigidbody.freezeRotation = true;
        transform.Rotate(Vector3.left * rotationThisFrame * Time.deltaTime);
        _rigidbody.freezeRotation = false;
    }
}
