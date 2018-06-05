using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour 
{

	private Vector2 velocity;
	private Transform player;
    //private Animator anim;
	private float posX;
	private float posY;
    private string visaoCamera;
    private float balancarDistancia;
    private float balancarTime;
    private AudioSource[] audioCamera;
    private AudioSource terremotoSound;

	public float smoothTimeX;
	public float smoothTimeY;

	// Use this for initialization
    
	void Start () 
	{
        visaoCamera = "player";
        balancarTime = -1;
        balancarDistancia = 0;
        //anim = GetComponent<Animator>();
		player = GameObject.Find ("Player").GetComponent<Transform> ();
        audioCamera= GetComponents<AudioSource>(); ;
        terremotoSound = audioCamera[1];
    }
    // Update is called once per frame
    void FixedUpdate () 
	{
		if (player == true && visaoCamera == "player")
        {
			posX = Mathf.SmoothDamp (transform.position.x, player.position.x, ref velocity.x, smoothTimeX);
			posY = Mathf.SmoothDamp (transform.position.y, player.position.y, ref velocity.y, smoothTimeY);

			transform.position = new Vector3 (posX, posY + 0.8f, transform.position.z);
		}

        if (balancarTime >= 0f)
        {
            print("Shake");
            Vector2 shakepos = Random.insideUnitCircle * balancarDistancia;
            transform.position = new Vector3(transform.position.x + shakepos.x, transform.position.y + shakepos.y,
                transform.position.z);

            balancarTime -= Time.deltaTime;
        }
        else if (balancarTime < 0)
        {
            terremotoSound.Stop();
            visaoCamera = "player";
        }

    }

    public void ShakeCamera(float time, float disBalanca)
    {
        balancarTime = time;
        balancarDistancia = disBalanca;
        visaoCamera = "CameraShake";
        terremotoSound.time = 0.8f;
        terremotoSound.Play();
    }


}
