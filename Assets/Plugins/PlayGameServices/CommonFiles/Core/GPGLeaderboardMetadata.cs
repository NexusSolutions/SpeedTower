using UnityEngine;
using System.Collections;
using Prime31;


#if UNITY_IPHONE || UNITY_ANDROID

namespace Prime31
{
	public class GPGLeaderboardMetadata
	{
		public string title;
		public double order;
		public string leaderboardId;
		public string iconUrl;
	}

}
#endif
