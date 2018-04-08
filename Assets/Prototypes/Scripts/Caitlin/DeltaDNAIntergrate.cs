// SDK v4
using UnityEngine;
using DeltaDNA;

public class DeltaDNAIntergrate : MonoBehaviour {

	void Start()
	{
		// Enter additional configuration here
		DDNA.Instance.Settings.DebugMode = true;
		DDNA.Instance.ClientVersion = "1.0.0"; 

		// Launch the SDK
		DDNA.Instance.StartSDK(
			"11313339746854579424014158515211",
			"https://collect13215bldmy.deltadna.net/collect/api",
			"https://engage13215bldmy.deltadna.net"
		);


		// Record events and set params
		GameEvent eventParams = new GameEvent("options")
			.AddParam("option", "sword")
			.AddParam("action", "sell");       

		DDNA.Instance.RecordEvent(eventParams);
	}
}
