using R3;
using R3.Triggers;
using UnityEngine;

namespace Gameplay
{
	public sealed class GoalObservable
	{
		public readonly Observable<Player> OnHitGoal;

		public GoalObservable(Collider playerOneGoalZone, Collider playerTwoGoalZone)
		{
			Debug.Log("Goalobservable");
			
			OnHitGoal = Observable.Merge<Player>(
				playerOneGoalZone.OnTriggerEnterAsObservable()
				                 .Select(_ => Player.Two),
				
				playerTwoGoalZone.OnTriggerEnterAsObservable()
				                 .Select(_ => Player.One));
		}
	}
}