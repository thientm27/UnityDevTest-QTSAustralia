using UnityEngine;
using UnityEngine.Events;

namespace Game.Enemy.Interface
{
    public interface IObstacle
    {
        void Initialize(Transform targetPosition, UnityAction<int> onHitPlayer); // init target position to chasing 
        void OnCollisionWithPlayer(GameObject player);
    }
}