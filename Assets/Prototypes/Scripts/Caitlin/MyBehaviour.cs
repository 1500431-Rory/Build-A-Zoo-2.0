// SDK v4
using UnityEngine;
using DeltaDNA;

public class MyBehaviour : MonoBehaviour {

	void Start()
	{
		// Enter additional configuration here


		// Launch the SDK
		DDNA.Instance.StartSDK(
			"11313339746854579424014158515211",
			"https://collect13215bldmy.deltadna.net/collect/api",
			"https://engage13215bldmy.deltadna.net"
		);
	}
}
