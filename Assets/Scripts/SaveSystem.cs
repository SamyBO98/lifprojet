using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.UI;

// Script permettant la création d'un dossier de sauvegarde dans le PC pour y accéder même si le jeu est fermée.
// Le dossier de sauvegarde est sécurisée puisqu'il est au format binaire donc personne ne peut rentrer pour s'augmenter des statistiques 
// ( ce qui n'est pas le cas si nous avions fait un .json).

public static class SaveSystem
{
   
    public static void SavePlayer(Player player, GameMaster gm, AttackTrigger at)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player,gm,at);


        formatter.Serialize(stream, data);
        stream.Close();
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();

            return data;
        }
        else
        {
            return null;
        }
    }

    public static void Delete()
    {
        string path = Application.persistentDataPath + "/player.save";
        File.Delete(path);
    }
}
