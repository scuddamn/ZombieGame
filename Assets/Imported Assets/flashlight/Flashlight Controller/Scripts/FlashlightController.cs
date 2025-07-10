using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] AudioClip _onSFX;
    [SerializeField] AudioClip _offSFX;
    [SerializeField] KeyCode _toggleKey;
    [SerializeField] private float lightRange = 10f;

    private Camera _cameraObject;
    private GameObject _lightSource;
    private AudioSource _audioSource;

    public bool IsOn { get; private set; }

    public bool IsEnabled = true;


    private void Awake()
    {
        _cameraObject = FindFirstObjectByType<Camera>();
        _lightSource = transform.GetChild(0).gameObject;
        _audioSource = GetComponent<AudioSource>();
        
    }

    private void Start()
    {
        _lightSource.gameObject.SetActive(false);
       
    }

    private void Update()
    {

        if (!IsEnabled)
        {
            _lightSource.gameObject.SetActive(false);
            IsOn = false;
            return;
        }

        if (Input.GetKeyDown(_toggleKey))
        {
            _audioSource.PlayOneShot(_onSFX);
        }

        if (Input.GetKeyUp(_toggleKey))
        {
            _audioSource.PlayOneShot(_offSFX);

            if (IsOn == false)
            {
                _lightSource.gameObject.SetActive(true);
                IsOn = true;
            }
            else
            {
                _lightSource.gameObject.SetActive(false);
                IsOn = false;
            }
        }

        if (IsOn == true)
        {
            ProcessRaycast();
        }
        
        
    }

    public void PlayFlashlightOffSFX()
    {
        _audioSource.PlayOneShot(_onSFX);
        _audioSource.PlayDelayed(2f);
        _audioSource.PlayOneShot(_offSFX);
    }
    
    void ProcessRaycast()
    {
            RaycastHit hit;
            if (Physics.Raycast(_cameraObject.transform.position, _cameraObject.transform.forward, out hit, lightRange))
            {
                Debug.Log(hit.transform.name + "hit by flashlight beam");
                EnemyHealth target = hit.transform.GetComponent<EnemyHealth>();
                if (target == null) return;
                target.Flashed();
            }
            else
            {
                return;
            }
    }
}
