using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageManager : MonoBehaviour {
	public static bool inMiniGame = false;

	public GameObject minigame;
	public Image box;
	public Image box1;
	public Image box2;
	public Image box3;
	public Image box4;


	private Sprite img;

	private Feed feed; 
	public Speech speech;

	public void Start(){
		feed = GameObject.Find ("AffectionManager").GetComponent<Feed> ();
	}	

	public void Update(){
		if (inMiniGame) 
			if (Mathf.Round(box1.transform.rotation.z) == 0 &&
				Mathf.Round(box2.transform.rotation.z) == 0 &&
				Mathf.Round(box3.transform.rotation.z) == 0 &&
				Mathf.Round(box4.transform.rotation.z) == 0) {
				inMiniGame = false;
				box.gameObject.SetActive (true);
				box1.gameObject.SetActive (false);
				box2.gameObject.SetActive (false);
				box3.gameObject.SetActive (false);
				box4.gameObject.SetActive (false);
				StopCoroutine ("OffMinigame");
				StartCoroutine ("OffMinigame");
			}
	}

	public void LoadImage(){
		img = Resources.Load ("food" + Random.Range (1, 3).ToString (), typeof(Sprite)) as Sprite;
		box.sprite = img;

		box.gameObject.SetActive (false);
		box1.gameObject.SetActive (true);
		box2.gameObject.SetActive (true);
		box3.gameObject.SetActive (true);
		box4.gameObject.SetActive (true);


		SplitImage (img);
		SpinImage ();

	}
	public void SpinImage(){
		float rot = -90f;
		box1.transform.Rotate (0, 0, rot*Random.Range (1, 4));
		box2.transform.Rotate (0, 0, rot*Random.Range (1, 4));
		box3.transform.Rotate (0, 0, rot*Random.Range (0, 4));
		box4.transform.Rotate (0, 0, rot*Random.Range (0, 4));

		inMiniGame = true;
	}

	public void SplitImage(Sprite img){
		int x = 0;
		int y = 0;
		int size = 256;

		Color[] pix1 = img.texture.GetPixels (x, y, size, size);
		Color[] pix2 = img.texture.GetPixels (x+size, y, size, size);
		Color[] pix3 = img.texture.GetPixels (x, y+size, size, size);
		Color[] pix4 = img.texture.GetPixels (x+size, y+size, size, size);

		Texture2D pic1 = new Texture2D (size, size);
		Texture2D pic2 = new Texture2D (size, size);
		Texture2D pic3 = new Texture2D (size, size);
		Texture2D pic4 = new Texture2D (size, size);

		pic1.SetPixels (pix1);
		pic1.Apply ();
		pic2.SetPixels (pix2);
		pic2.Apply ();
		pic3.SetPixels (pix3);
		pic3.Apply ();
		pic4.SetPixels (pix4);
		pic4.Apply ();

		Rect rec = new Rect (0, 0, size, size);

		box1.GetComponent<Image> ().sprite = Sprite.Create(pic1,rec, new Vector2 (0.5f,0.5f),100);
		box2.GetComponent<Image> ().sprite = Sprite.Create(pic2,rec, new Vector2 (0.5f,0.5f),100);
		box3.GetComponent<Image> ().sprite = Sprite.Create(pic3,rec, new Vector2 (0.5f,0.5f),100);
		box4.GetComponent<Image> ().sprite = Sprite.Create(pic4,rec, new Vector2 (0.5f,0.5f),100);
	}

	IEnumerator OffMinigame(){
		feed.DoFeed ();
		yield return new WaitForSeconds (1);
		minigame.SetActive (false);
		speech.SpeechTrigger ("feed");
	}


}
