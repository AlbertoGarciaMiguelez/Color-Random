using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        Color[] colours = {Color.red, Color.blue, Color.green, Color.black, Color.green, Color.yellow, Color.grey, Color.cyan, Color.magenta, Color.gray};

        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        
        public NetworkVariable<Color> color = new NetworkVariable<Color>();

        public override void OnNetworkSpawn()
        {
             if (IsOwner)
            {
                Move();
            }
        }

        public void Move()
        {
            if (NetworkManager.Singleton.IsServer)
            {
                var randomPosition = GetRandomPositionOnPlane();
                transform.position = randomPosition;
                Position.Value = randomPosition;
            }
            else
            {
                SubmitPositionRequestServerRpc();
            }
        }

        public void Nuevo(){
            
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetRandomPositionOnPlane();
        }
        [ServerRpc]
        void SubmitColorRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            color.Value = GetRandomColor();
        }
    
        static Vector3 GetRandomPositionOnPlane()
        {
            return new Vector3(Random.Range(-3f, 3f), 1f, Random.Range(-3f, 3f));
        }


        void Update()
        {
            transform.position = Position.Value;
        }
    }
}