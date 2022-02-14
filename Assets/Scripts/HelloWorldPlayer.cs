using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        Color[] colours = {Color.red, Color.blue, Color.green, Color.black};

        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        
        public NetworkVariable<Color> color = new NetworkVariable<Color>();

        public override void OnNetworkSpawn()
        {
             if (IsOwner)
            {
                Move();
                int i = Random.Range(0, colours.Length);
                color.GetComponent<Renderer>().material.color = colours[i];
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
            int x = Random.Range(0, colours.Length);
        }

        [ServerRpc]
        void SubmitPositionRequestServerRpc(ServerRpcParams rpcParams = default)
        {
            Position.Value = GetRandomPositionOnPlane();
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