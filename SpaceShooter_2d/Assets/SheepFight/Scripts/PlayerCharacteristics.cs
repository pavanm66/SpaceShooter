using UnityEngine;

[CreateAssetMenu(fileName ="Player_Obj",menuName ="ScriptableObjs/Player")] 
public class PlayerCharacteristics : ScriptableObject
{
   
    public int weight;
    public int damage;
    public Sprite icon;


    public bool isPlayer;
    public int direction;
}

