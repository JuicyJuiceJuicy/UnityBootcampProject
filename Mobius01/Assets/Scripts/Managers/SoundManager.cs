using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource fireSource;
    public AudioClip fireSound;

    private AudioSource overheatSource;
    public AudioClip overheatSound;

    private AudioSource enemyFireSource;
    public AudioClip[] enemyFireSound;

    private AudioSource startSource;
    private AudioClip[] startSound = new AudioClip[3];

    private AudioSource lockedOnSource;
    private AudioClip lockedOnSound;
    public bool lockedOn;
    public bool lockedOnSoundEnd = true;

    private AudioSource warningSource;
    private AudioClip warningSound;
    public bool warning;
    public bool warningSoundEnd = true;
    private AudioClip signalLostSound;

    public AudioClip[] exposionSound;
    private AudioSource exposionSource;
    private AudioSource exposionMiniSource;

    private AudioSource incommingSource;
    public AudioClip incommingSound;

    private AudioSource exposionBigSource;

    private AudioSource laserSource;
    public AudioClip laserSound;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
        startSource = gameObject.AddComponent<AudioSource>();
        lockedOnSource = gameObject.AddComponent<AudioSource>();
        warningSource = gameObject.AddComponent<AudioSource>();
        fireSource = gameObject.AddComponent<AudioSource>();
        overheatSource = gameObject.AddComponent<AudioSource>();
        enemyFireSource = gameObject.AddComponent<AudioSource>();
        exposionSource = gameObject.AddComponent<AudioSource>();
        exposionMiniSource = gameObject.AddComponent<AudioSource>();
        exposionBigSource = gameObject.AddComponent<AudioSource>();
        laserSource = gameObject.AddComponent<AudioSource>();
        incommingSource = gameObject.AddComponent<AudioSource>();

        for (int i = 0; i < 3; i++)
        {
            startSound[i] = CreateStartSound(i);
        }
    }

    void Start()
    {
        lockedOnSound = CreateLockedOnSound();
        warningSound = CreateWarningSound();
        signalLostSound = CreateSignalLostSound();
    }

    public void FireSound(float heat)
    {
        fireSource.volume = 0.1f;
        fireSource.pitch = 1 + (heat / 100);
        fireSource.PlayOneShot(fireSound);
    }

    public void OverheatSound()
    {
        overheatSource.volume = 0.3f;
        overheatSource.PlayOneShot(overheatSound);
    }

    public void EnemyFireSound()
    {
        enemyFireSource.volume = 0.2f;

        int soundNum = Random.Range(0, enemyFireSound.Length);
        enemyFireSource.PlayOneShot(enemyFireSound[soundNum]);
    }

    AudioClip CreateStartSound(int j)
    {
        int sampleRate = 44100;
        int length = (int)(0.5f * sampleRate);

        AudioClip startClip = AudioClip.Create("Beep", length, 1, sampleRate, false); ;

        float[] data = new float[length];
        float[] frequency = new float[] { 261 * 0.7f, 329 * 0.7f, 392 * 0.7f };
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Mathf.Sin(2 * Mathf.PI * frequency[j] * i / 44100);
        }

        startClip.SetData(data, 0);
        return startClip;
    }

    public void StartSound(int i)
    {
        startSource.clip = startSound[i];
        startSource.Play();
    }

    AudioClip CreateLockedOnSound()
    {
        int sampleRate = 44100;
        int length = (int)(0.05f * sampleRate);

        AudioClip lockedOnClip = AudioClip.Create("Beep", length, 1, sampleRate, false);

        float[] data = new float[length];
        float frequency = 300f;
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / 44100);
        }

        lockedOnClip.SetData(data, 0);
        return lockedOnClip;
    }

    public IEnumerator LockedOnSound()
    {
        while (lockedOn)
        {
            lockedOnSoundEnd = false;
            lockedOnSource.clip = lockedOnSound;
            lockedOnSource.Play();
            yield return new WaitForSeconds(0.2f);
            lockedOnSoundEnd = true;
        }
    }

    AudioClip CreateWarningSound()
    {
        int sampleRate = 44100;
        int length = (int)(0.05f * sampleRate);

        AudioClip warningClip = AudioClip.Create("Beep", length, 1, sampleRate, false); ;
        
        float[] data = new float[length];
        float frequency = 700f;
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / 44100);
        }

        warningClip.SetData(data, 0);
        return warningClip;
    }

    public IEnumerator WarningSound()
    {
        while (warning)
        {
            warningSoundEnd = false;
            warningSource.clip = warningSound;
            warningSource.Play();
            yield return new WaitForSeconds(0.1f);
            warningSoundEnd = true;
        }
    }

    AudioClip CreateSignalLostSound()
    {
        int sampleRate = 44100;
        int length = (int)(1f * sampleRate);

        AudioClip warningClip = AudioClip.Create("Beep", length, 1, sampleRate, false); ;

        float[] data = new float[length];
        float frequency = 700f;
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Mathf.Sin(2 * Mathf.PI * frequency * i / 44100) * 0.7f;
        }

        warningClip.SetData(data, 0);
        return warningClip;
    }

    public void SignalLostSoundStart()
    {
        InvokeRepeating("SignalLostSound", 0, 1.5f);
        Invoke("SignalLostSound", 0);
    }

    public void SignalLostSound()
    {
        warningSource.clip = signalLostSound;
        warningSource.Play();
    }

    public void ExposionSound(float volume, float pitch)
    {
        int soundNum = Random.Range(0, exposionSound.Length);
        exposionSource.clip = exposionSound[soundNum];
        exposionSource.volume = volume;
        exposionSource.pitch = pitch;
        exposionSource.Play();
    }

    public void ExposionMiniSound(float volume, float pitch)
    {
        int soundNum = Random.Range(0, exposionSound.Length);
        exposionMiniSource.clip = exposionSound[soundNum];
        exposionMiniSource.volume = volume;
        exposionMiniSource.pitch = pitch;
        exposionMiniSource.Play();
    }

    public void IncommingSound()
    {
        /*
        incommingSource.volume =0.7f;
        incommingSource.Play();
        incommingSource.PlayOneShot(incommingSound);
        */
    }

    public void ExposionBigSound(float volume, float pitch)
    {
        int soundNum = Random.Range(0, exposionSound.Length);
        exposionBigSource.clip = exposionSound[soundNum];
        exposionBigSource.volume = volume;
        exposionBigSource.pitch = pitch;
        exposionBigSource.Play();
    }

    public void LaserSound(float volume, float pitch)
    {
        laserSource.volume = volume;
        laserSource.pitch = pitch;
        laserSource.Play();
        laserSource.PlayOneShot(laserSound);
    }
}
