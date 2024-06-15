using UnityEngine;
namespace Game.Enemy.Interface
{
    public interface IObstacle
    {
        void Initialize(Vector3 targetPosition); // init target position to chasing 
        void OnCollisionWithPlayer(GameObject player);
    }
}