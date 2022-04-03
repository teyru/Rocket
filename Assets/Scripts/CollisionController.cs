using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionController : MonoBehaviour
{
    [SerializeField] private AudioClip _crashClip;
    [SerializeField] private AudioClip _winClip;

    [SerializeField] private ParticleSystem _defeatParticles;
    [SerializeField] private ParticleSystem _victoryParticles;


    private AudioSource _audioSource;

    private bool _is—ompleted;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (_is—ompleted) return;

            switch (collision.gameObject.tag)
            {
                case "Wall":
                    Crash();
                    break;

                case "Fuel":
                    //fuel adding
                    break;

                case "WinPlatform":
                    WinComplete();
                    break;
            }
    }

    private void WinComplete()
    {
        _victoryParticles.Play();
        GetComponent<Movement>().enabled = false;
        _audioSource.PlayOneShot(_winClip);
        _is—ompleted = true;
        Invoke("GoToNextLevel", 2f);
    }

    private void GoToNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentLevelIndex + 1;

        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex, LoadSceneMode.Single);
    }


    private void Crash()
    {
        _defeatParticles.Play();
        _is—ompleted = true;
        _audioSource.PlayOneShot(_crashClip);
        GetComponent<Movement>().enabled = false;
        Invoke("RestartLevel", 1f);
    }



    private void RestartLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex, LoadSceneMode.Single);
    }
}
