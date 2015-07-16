using UnityEngine;
using System;
using System.Collections;


#if UNITY_IPHONE || UNITY_ANDROID

namespace Prime31
{
	public class GPGQuest
	{
		public string questId;
		public string name;
		public string questDescription;
		public string iconUrl;
		public string bannerUrl;
		public int state;
		public GPGQuestState stateEnum
		{
			get { return (GPGQuestState)state; }
		}
		public DateTime startTimestamp;
		public DateTime expirationTimestamp;
		public DateTime acceptedTimestamp;
		public GPGQuestMilestone currentMilestone;
	}

}
#endif
