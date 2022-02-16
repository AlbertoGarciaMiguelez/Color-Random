using Unity.Netcode;
using UnityEngine;

namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {
        Color[] colours = {Color.red, Color.blue, Color.green, Color.black, Color.green, Color.yellow, Color.grey, Color.cyan, Color.magenta, Color.gray};

        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        
        public NetworkVariable<Color> color = new NetworkVariable<Color>();

        private Renderer enlace;

        private int i;
        private int x;

        public override void OnNetworkSpawn()
        {
             if (IsOwner)
            {
                Change();
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

        public void Change(){

            if (NetworkManager.Singleton.IsServer)
            {
                NuevoColor();
            }
            else
            {
                SubmitColorRequestServerRpc();
            }

        }

        public void NuevoColor(){
            color.Value = GetComponent<Renderer>().material.color = GetRandomColor();
        }

        public Color GetRandomColor(){

            do{
                i = Random.Range(0, colours.Length);
            }while(i==x);

            x=i;

            return colours[i];
            
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
            enlace = GetComponent<Renderer>();
            enlace.material.color = color.Value;
        }
    }
}