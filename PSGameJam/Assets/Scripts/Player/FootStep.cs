using System;
using UnityEngine;

public class FootStep : MonoBehaviour
{
	[SerializeField]//N
	private GameObject footsies0;

	[SerializeField]//NE
	private GameObject footsies1;

	[SerializeField]//E
	private GameObject footsies2;

	[SerializeField]//SE
	private GameObject footsies3;

	[SerializeField]//S
	private GameObject footsies4;

	[SerializeField]//NW
	private GameObject footsies5;

	[SerializeField]//W
	private GameObject footsies6;

	[SerializeField]//SW
	private GameObject footsies7;
	
	[SerializeField]
	private bool Robot;
	
	private GameObject step;
	private float countdown = 10.0f;
	
	private SpriteRenderer sprite;
	private Color alphaChange;
	
	public bool left = true;
	
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Robot==false)
		{
			sprite = GetComponent<SpriteRenderer>();
			alphaChange=sprite.color;
		}
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Robot)
			return;
		else
		{
			countdown -= Time.deltaTime;
			alphaChange.a -= Time.deltaTime / 10.0f;
			sprite.color = alphaChange;
			if(countdown<0)
			{
				Destroy(gameObject);
			}
		}
    }
	
	public void makeStep(float angle)
	{
		Vector3 tempPos = new Vector3(0.0f,-0.6f,0.0f);
		switch(angle)
		{
			case -90.0f:
				step = Instantiate(footsies0);
				if(left)
					tempPos.x += 0.22f;
				else
					tempPos.x -= 0.22f;
				step.transform.position = transform.position + tempPos;
				break;
			
			case -135.0f:
				step = Instantiate(footsies1);
				if(left)
				{
					tempPos.x += 0.22f;
					tempPos.y -= 0.11f;
				}
				else
				{
					tempPos.x -= 0.22f;
					tempPos.y += 0.11f;
				}
				step.transform.position = transform.position + tempPos;
				break;
				
			case 180.0f:
				step = Instantiate(footsies2);
				if(left)
					tempPos.y -= 0.11f;
				else
					tempPos.y += 0.11f;
				step.transform.position = transform.position + tempPos;
				break;
				
			case 135.0f:
				step = Instantiate(footsies3);
				if(left)
				{
					tempPos.x -= 0.22f;
					tempPos.y -= 0.11f;
				}
				else
				{
					tempPos.x += 0.22f;
					tempPos.y += 0.11f;
				}
				step.transform.position = transform.position + tempPos;
				break;
			
			case 90.0f:
				step = Instantiate(footsies4);
				if(left)
					tempPos.x -= 0.22f;
				else
					tempPos.x += 0.22f;
				step.transform.position = transform.position + tempPos;
				break;
			
			case 45.0f:
				step = Instantiate(footsies5);
				if(left)
				{
					tempPos.x -= 0.22f;
					tempPos.y += 0.11f;
				}
				else
				{
					tempPos.x += 0.22f;
					tempPos.y -= 0.11f;
				}
				step.transform.position = transform.position + tempPos;
				break;
			
			case 0.0f:
				step = Instantiate(footsies6);
				if(left)
					tempPos.y += 0.11f;
				else
					tempPos.y -= 0.11f;
				step.transform.position = transform.position + tempPos;
				break;
			
			case -45.0f:
				step = Instantiate(footsies7);
				if(left)
				{
					tempPos.x += 0.22f;
					tempPos.y += 0.11f;
				}
				else
				{
					tempPos.x -= 0.22f;
					tempPos.y -= 0.11f;
				}
				step.transform.position = transform.position + tempPos;
				break;
			default:
				Debug.Log("WHAT THE FUCK");
				break;
			
				
		}
	}
}

