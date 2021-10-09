using Hellmade.Sound;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
public class Door : Triggerable
{
    public Sprite LockedSprite;
    public Sprite UnlockedSprite;

    public Color LockedColor = new Color(0, 0, 0, .5f);
    public Color UnlockedColor = new Color(1, 1, 1, .8f);

    private SpriteRenderer _renderer;

    public AudioClip OpenClip;
    public float Volume = 1;
    private int _audioId = -1;

    public float FadeOutTime = 1.5f;

    private bool _goingThroughDoor;

    [Header("Debug")]
    public bool ForceLoadNextLevel;

    // Start is called before the first frame update
    private void Start()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = LockedSprite;
    }

    private void Update()
    {
        if (ForceLoadNextLevel)
        {
            ForceLoadNextLevel = false;
            LoadNewLevel(FindObjectOfType<Player>());
        }
    }

    // Code that runs on the frame when all triggers turn on.
    public override void OnTriggerActivate() 
    {
        //_renderer.sprite = UnlockedSprite;
        _renderer.color = UnlockedColor;

        Audio audio = EazySoundManager.GetAudio(_audioId);
        if (audio == null)
        {
            _audioId = EazySoundManager.PlaySound(OpenClip, Volume, false, transform);
            audio = EazySoundManager.GetAudio(_audioId);
            audio.Min3DDistance = 10f;
            audio.Max3DDistance = 20f;
            audio.SpatialBlend = 0.5f;
            audio.SetVolume(Volume, 1, Volume);
            StopAllCoroutines();
            StartCoroutine(LowerVolumeAfterward());
        }
        else
        {
            audio.SetVolume(Volume);
            audio.Play();
            StopAllCoroutines();
            StartCoroutine(LowerVolumeAfterward());
        }
    }

    IEnumerator LowerVolumeAfterward()
    {
        yield return new WaitForSeconds(OpenClip.length - 0.05f);
        Audio audio = EazySoundManager.GetAudio(_audioId);
        if (audio != null)
        {
            audio.SetVolume(Volume / 2, 0, Volume / 2);
        } else
        {
            Debug.LogError("wtf");
        }
    }


    // Code that runs on teh frame when any trigger turns off.
    public override void OnTriggerDeactivate()
    {
        //_renderer.sprite = LockedSprite;
        _renderer.color = LockedColor;
        // TODO: Close the door, change sprite.

        Audio audio = EazySoundManager.GetAudio(_audioId);
        if (audio != null)
        {
            audio.Stop();
        }
    }

    // Code that runs for every frame that all triggers are on.
    public override void OnTriggerOn()
    {
        // TODO: Check for player and if player is on top, win level.
        var collider = Utils.CastForObjectOnTile(Utils.ToGridPosition(transform.position));
        if (collider && collider.CompareTag("Player") && !_goingThroughDoor)
        {
            _goingThroughDoor = true;
            LoadNewLevel(collider.GetComponent<Player>());
        }
    }

    public void LoadNewLevel(Player player)
    {
        player.CanMove = false;
        var uIManager = UIManager.Instance;
        uIManager.FadeManager.FadeOut(FadeOutTime, Color.white, () => {
            StartCoroutine(LoadNewLevel(uIManager));
        });
    }

    IEnumerator LoadNewLevel(UIManager uIManager)
    {
        yield return new WaitForSeconds(0.5f);

        // For if it goes over how many levels there are
        if (GameManager.CurrentLevel + 2 > GameManager.LevelScenes.Length)
        {
            Debug.Log("Moving to end scene...");

            MusicManager.Instance.StopMusic();
            yield return new WaitForSeconds(2);
            yield return SceneManager.LoadSceneAsync(GameManager.StoryEndScene);
        }
        else
        {
            Debug.Log("Moving to next level...");

            SceneManager.LoadScene(GameManager.LevelScenes[GameManager.CurrentLevel + 1]);
            FadeManager.instance.FadeIn(FadeOutTime, Color.white);
            UIManager.Instance.TutorialText.ShowText(true);
            UIManager.Instance.LevelNameText.ShowText();
        }
    }
}
