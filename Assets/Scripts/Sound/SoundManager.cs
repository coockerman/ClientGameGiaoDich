using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private void Awake()
    {
        instance = this;
    }

    public AudioClip sellSound,
        buySound,
        chatSound,
        chiMangSound,
        nhacNen1,
        nhacNen2,
        winSound,
        loseSound,
        daySound,
        buidingSound,
        finishBuildSound,
        pkSound;

    public AudioSource sourcePK,
        sourceShopee,
        sourceBuild,
        sourceChat,
        sourceMusic;

    public void PlayMusicLogin()
    {
        sourceMusic.clip = nhacNen1;
        sourceMusic.Play();
    }
    public void PlayMusicInGame()
    {
        sourceMusic.clip = nhacNen2;
        sourceMusic.Play();
    }
    public void PlayDaySound()
    {
        sourceChat.PlayOneShot(daySound);
    }

    public void PlayBuidingSound()
    {
        sourceBuild.clip = buidingSound;
        sourceBuild.loop = true;
        sourceBuild.Play();
    }

    public void PlayFinishBuildSound()
    {
        sourceBuild.clip = null;
        sourceBuild.loop = false;
        sourceBuild.PlayOneShot(finishBuildSound);
    }
    public void PlayWinSound()
    {
        sourcePK.PlayOneShot(winSound);
    }

    public void PlayLoseSound()
    {
        sourcePK.PlayOneShot(loseSound);
    }
    public void PlayChatSound()
    {
        sourceChat.PlayOneShot(chatSound);
    }
    public void PlayPKSound()
    {
        sourcePK.clip = pkSound;
        sourcePK.loop = true;
        sourcePK.Play();
        sourceMusic.mute = true;
    }

    public void PlayChiMangSound()
    {
        sourcePK.clip = null;
        sourcePK.loop = false;
        sourcePK.PlayOneShot(chiMangSound);
        sourceMusic.mute = false;
    }

    public void PlaySoundBuy()
    {
        sourceShopee.PlayOneShot(buySound);
    }
    public void PlaySoundSell()
    {
        sourceShopee.PlayOneShot(sellSound);
    }
}
