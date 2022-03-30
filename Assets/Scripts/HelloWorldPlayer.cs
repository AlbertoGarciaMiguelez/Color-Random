using Unity.Netcode;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace HelloWorld
{
    public class HelloWorldPlayer : NetworkBehaviour
    {

        public NetworkVariable<Vector3> Position = new NetworkVariable<Vector3>();
        
        public NetworkVariable<Color> colorPlayer = new NetworkVariable<Color>();

        public static List<Color> coloresGuardados= new List<Color>();

        private Renderer enlace;
        


        private int x;

        public void Start(){
            
            Position.OnValueChanged += OnPositionChange;
            colorPlayer.OnValueChanged += OnColorChange;
            enlace= GetComponent<Renderer>();
            if(IsServer && IsOwner){
                coloresGuardados.Add(Color.cyan);
                coloresGuardados.Add(Color.clear);
                coloresGuardados.Add(Color.blue);
                coloresGuardados.Add(Color.gray);
                coloresGuardados.Add(Color.green); 
                coloresGuardados.Add(Color.magenta);
                coloresGuardados.Add(Color.red);
                coloresGuardados.Add(Color.yellow);
                
            }
            if(IsOwner){
                SubmitColorRequestServerRpc(true);
            }
            
        }
        public void OnPositionChange(Vector3 previousValue, Vector3 newValue){
            transform.position=Position.Value;
        }
        public void OnColorChange(Color previousValue, Color newValue){
            enlace.material.color = newValue;
        }

        public override void OnNetworkSpawn()
        {
            // IsOwner es el objeto(player) q controla la maquina
            if (IsOwner)
            {
                MovePlayer();
            }
        }
        public void MovePlayer()
        {
                SubmitPositionRequestServerRpc();
        }
        public void ChangeColor(){
                SubmitColorRequestServerRpc(false);
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

        [ServerRpc]
        void SubmitColorRequestServerRpc(bool primeravez = false, ServerRpcParams rpcParams = default)
        {
            /*
            Color oldColor= colorPlayer.Vlue
            Color newColor =availableColors[Random.Rqange(0,availableCOlors.Count)];
            avilableColors.R    emove(newColor);
            availableColors.Add(oldColor);
            colorPlayer.Value=newColor;
            
            */
            
            Color oldColor= colorPlayer.Value;
            Color newColor =coloresGuardados[Random.Range(0,coloresGuardados.Count)];
            
            coloresGuardados.Remove(newColor);
            
            if(!primeravez){
                coloresGuardados.Add(oldColor);
            }
            colorPlayer.Value=newColor;
        }

        // Position.OnvalueChanged += OnPosition Change
    
            // void OnPo Vector3 previousValue newValue { transform.position=Position.Value}
            void Update()
    {
            enlace.material.color = colorPlayer.Value;
    }
            
    }
    
}