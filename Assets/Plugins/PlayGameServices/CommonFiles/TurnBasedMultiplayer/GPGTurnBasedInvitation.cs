using UnityEngine;
using System.Collections;


#if UNITY_IPHONE || UNITY_ANDROID

namespace Prime31
{
	public class GPGTurnBasedInvitation
	{
		public GPGTurnBasedParticipant invitingParticipant;
		public GPGTurnBasedMatch match;
	
	
		public override string ToString()
		{
			return Prime31.JsonFormatter.prettyPrint( Prime31.Json.encode( this ) );
		}
	}

}
#endif
