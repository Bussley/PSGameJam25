using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEditor.Animations;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

//^ I BORROWED ALL THE STUFF FROM PLAYERCONTROLLER I DO NOT KNOW WHAT I NEED :^)

/* SOUND EFFECTS CONTROLLER BY RCCOLA96 (GAME DESIGNER)
*  THE PURPOSE OF THIS FILE IS TO CONTAIN PUBLIC FUNCTIONS AND HOLD ONTO SOUND EFFECTS FOR THE PLAYER.
*  THIS IS IN A SEPARATE FILE FOR BETTER ORGANIZATION, AND TO AVOID CLUTTERING THE PLAYER CONTROLLER.
*  THE PLAYER CONTROLLER SCRIPT WILL CALL EACH OF THESE PUBLIC FUNCTIONS, WHICH ARE SPECIFIC IN CASE
*  AND NOT GENERALIST FOR OTHER FACETS OF THE GAME. (THOUGH IT COULD BE ADAPTED.)
*/

public class SFXController : MonoBehaviour
{
	
	[SerializeField]
	private AudioClip flameStart;
	
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float flameStartVolume;
	
	[SerializeField]
	private AudioClip flameOngoing;
	
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float flameOngoingVolume;
	
	[SerializeField]
	private AudioClip bladeSwing;
		
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float bladeVolume;
	
	[SerializeField]
	private AudioClip hydroCannon;
			
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float hydroVolume;
	
	[SerializeField]
	private AudioClip jetPack;
			
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float jetVolume;
	
	[SerializeField]
	private AudioClip laserSound;
			
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float laserVolume;
	
	[SerializeField]
	private AudioClip shotgunBurst;
			
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float shotgunVolume;
	
	[SerializeField]
	private AudioClip shotgunReload;
	
	[SerializeField]
	private AudioClip footStep;
			
	[Range(0.0f, 1.0f)]
	[SerializeField]
	private float stepVolume;
	
	AudioSource myaudio;
	float countdown = 0;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myaudio=GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
	
	private void FixedUpdate() 
	{
		if (countdown > 0)
		{
			myaudio.volume = myaudio.volume - 0.05f;
			countdown = countdown - Time.deltaTime;
			if (myaudio.volume == 0.0f)
			{
				myaudio.Stop();
				countdown = 0.0f;
			}
		}
	}
	
	public void playSound(int index)
	{
		//0=FlameOpener
		//1=flameOngoing
		//2=bladeSwing
		//3=hydroCannon
		//4=jetPack
		//5=laserSound
		//6=shotgunBurst
		//7=shotgunReload
		//8=footStep

		//volume scaling is janky so lets keep reseting it until we get smarter about it
		myaudio.volume = 1.0f;
		float variance = UnityEngine.Random.Range(-0.1f, 0.1f);
		myaudio.pitch += variance;
		if(myaudio.pitch > 1.25f)
		{	
		   myaudio.pitch = 1.25f;
		   myaudio.pitch -= variance;
		}
		else if(myaudio.pitch < 0.75f)
		{
			myaudio.pitch = 0.75f;
			myaudio.pitch -= variance;
		}
			
		
		if(index==0)
		{
			myaudio.PlayOneShot(flameStart, flameStartVolume);
		}
		else if(index==1)
		{
			myaudio.clip = flameOngoing;
			myaudio.Play();
		}
		else if(index==2)
		{
			myaudio.PlayOneShot(bladeSwing, bladeVolume);
		}
		else if(index==3)
		{
			myaudio.PlayOneShot(hydroCannon, hydroVolume);
		}
		else if(index==4)
		{
			myaudio.clip = jetPack;
			myaudio.Play();
		}
		else if(index==5)
		{
			myaudio.PlayOneShot(laserSound, laserVolume);
		}
		else if(index==6)
		{
			myaudio.PlayOneShot(shotgunBurst, shotgunVolume);
		}
		else if(index==7)
		{
			myaudio.PlayOneShot(shotgunReload, shotgunVolume);
		}
		else if(index==8)
		{
			myaudio.PlayOneShot(footStep, 0.2f);
		}
		else
		{
			
		}
	}
	
	//QUIETS THE SOUND LINEARLY. MAYBE ADD SMARTER LERP LATER.
	public void stopSound(float newCountdown)
	{
		//myaudio.Stop();
		countdown = newCountdown;
	}
		
}
