using System;   
using System.Text;  
using System.Net;  
using System.Net.Sockets;  
using System.Threading;  
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NetworkManager {
	private Socket _socket;
	public string Message;

	private static NetworkManager _instance ;
	public static NetworkManager Instance{
		get{
			if(_instance == null){
				_instance = new NetworkManager();
			}
			return _instance;
		}
	}

	private NetworkManager(){
		ConnectServer("127.0.0.1", 21567);
	}

	private void ConnectServer(string ipAddress, int port){
		//服务器IP地址  
		IPAddress address = IPAddress.Parse(ipAddress);
		//服务器端口  
		IPEndPoint endpoint = new IPEndPoint(address, port);
		_socket = new Socket( AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp );
		//异步连接,连接成功调用connectCallback方法  
		IAsyncResult result = _socket.BeginConnect( endpoint, new AsyncCallback(ConnectCallback), _socket );
		//这里做一个超时的监测，当连接超过5秒还没成功表示超时  
		bool success = result.AsyncWaitHandle.WaitOne( 5000, true );
		if( !success ) {
			//超时  
			Closed();
			Debug.Log( "connect Time Out" );
		}
	}

			
	public void OnRequest(string requestText, System.Action callback){
		byte[] data = System.Text.Encoding.Default.GetBytes(requestText);
		_socket.Send(data);

		byte[] respone = new byte[1024];
		int bytesRead = _socket.Receive(respone);
		string responeText = System.Text.Encoding.UTF8.GetString(respone, 0, bytesRead);
		Debug.Log("respone: " + responeText);
		Message = responeText;
		callback();
	}
		
	private void ConnectCallback(IAsyncResult asyncConnect){  
		Debug.Log("Connect Server Success! ");  
	}  

	//关闭Socket  
	public void Closed(){  
		if (_socket != null && _socket.Connected){  
			_socket.Shutdown(SocketShutdown.Both);  
			_socket.Close();  
		}  
		_socket = null;  
	}

}
