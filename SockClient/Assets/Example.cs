using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Example : MonoBehaviour {
	public Text ResultText;
	public Button SubmitBtn;
	public Text InputText;

	private void Start(){
		SubmitBtn.onClick.AddListener(Request);
	}
		
	private void OnRespone(){
		Debug.Log(NetworkManager.Instance.Message);
		ResultText.text = NetworkManager.Instance.Message;
	}

	private void Request(){
		NetworkManager.Instance.OnRequest(InputText.text, OnRespone);
	}
}
