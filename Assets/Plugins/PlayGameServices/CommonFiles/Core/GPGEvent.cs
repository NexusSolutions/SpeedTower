using UnityEngine;
using System;
using System.Collections;


#if UNITY_IPHONE || UNITY_ANDROID

namespace Prime31
{
	public class GPGEvent
	{
		public Int64 count;
		public string eventDescription;
		public string eventId;
		public string imageUrl;
		public string name;
		public bool visible;
	}

}
#endif
