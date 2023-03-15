using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckpointManager
{
    public enum Checkpoint
    {
        // Нет
        None,

        // В доме
        StartLocation,

        // Вход в дом
        VillageToStartLocation,
        
        // Вход в амбар
        VillageToBarn,
        
        // Вход к столбу
        VillageToPillar,
        
        // Вход в загон
        VillageToCoral,
        
        // Вход в замок
        VillageToCastle,

        // После победы над боссом в амбаре
        BarnEnd
    }

    public static float health;
    public static float maxHealth;
    public static List<Item> savedItems;

    public static void SaveCheckpoint(Checkpoint checkpoint)
    {
        if (checkpoint != Checkpoint.None)
        {
            health = PlayerData.playerCurrentHealth;
            maxHealth = PlayerData.playerMaxHealth;
            savedItems = new List<Item>();
            PlayerData.getListKey.ForEach((item) => { savedItems.Add((Item)item.Clone()); });
            PlayerData.checkpoint = checkpoint;
        }
    }

    public static void LoadCheckpoint()
    {
        PlayerData.playerCurrentHealth = health;
        PlayerData.playerMaxHealth = maxHealth;
        PlayerData.LoadSavedItems(savedItems);
        switch (PlayerData.checkpoint)
        {
            case Checkpoint.StartLocation:
                SceneLoader.LoadScene("StartLocation", SceneLoader.Position.VillageNearHouseEntrance);
                break;
            case Checkpoint.VillageToStartLocation:
                SceneLoader.LoadScene("Village", SceneLoader.Position.VillageNearHouseEntrance);
                break;
            case Checkpoint.VillageToBarn:
                SceneLoader.LoadScene("Village", SceneLoader.Position.VillageNearBarnEntrance);
                break;
            case Checkpoint.VillageToPillar:
                SceneLoader.LoadScene("Village", SceneLoader.Position.VillageNearPillarEntrance);
                break;
            case Checkpoint.VillageToCoral:
                SceneLoader.LoadScene("Village", SceneLoader.Position.VillageNearCoralEntrance);
                break;
            case Checkpoint.VillageToCastle:
                SceneLoader.LoadScene("Village", SceneLoader.Position.VillageNearCastleEntrance);
                break;
            case Checkpoint.BarnEnd:
                SceneLoader.LoadScene("Barnlocation", SceneLoader.Position.BarnAfterBosss);
                break;
        }
    }
}
