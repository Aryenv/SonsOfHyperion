using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : PersistentSingleton<SoundManager>
{
    public AudioSource Music;
    public AudioSource FX;

    public AudioClip selectCardClip;
    public AudioClip newDayClip;
    public AudioClip resourceObtainClip;
    public AudioClip turretTerrestreClip;
    public AudioClip playCardClip;
    public AudioClip enemyDie;
    public AudioClip enemyAttack;

    private void Start()
    {
        sonidosTorretas.activar_sonido += PlayTurretAttack;
    }
    public void PlayAnyClip(AudioClip clip)
    {
        FX.PlayOneShot(clip);
    }

    public void PlaySelectCard()
    {
        FX.PlayOneShot(selectCardClip);
    }

    public void PlayNewDay()
    {
        FX.PlayOneShot(newDayClip);
    }

    public void PlayResourceObtain()
    {
        FX.PlayOneShot(resourceObtainClip);
    }

    public void PlayPlayCard()
    {
        FX.PlayOneShot(playCardClip);
    }

    public void PlayTurretAttack(int torretaASonar)
    {
        FX.PlayOneShot(turretTerrestreClip);
    }

    public void PlayEnemyDie()
    {
        FX.PlayOneShot(enemyDie);
    }

    public void PlayEnemyAttack()
    {
        FX.PlayOneShot(enemyAttack);
    }
}
