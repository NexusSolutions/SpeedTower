using UnityEngine;
using System;
using System.Collections;


#if UNITY_IPHONE || UNITY_ANDROID

namespace Prime31
{
	public class GPGQuestMilestone
	{
		public string questMilestoneId;
		public string questId;
		public string eventId;
		public int state;
		public GPGQuestMilestoneState stateEnum
		{
			get { return (GPGQuestMilestoneState)state; }
		}
		public Int64 currentCount;
		public Int64 targetCount;
		public string rewardData;
	}

}
#endif
