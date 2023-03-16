using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CheckpointManager
{
    public enum Checkpoint
    {
        // ���
        None,

        // � ����
        StartLocation,
        
        // � ���� � ������
        StartLocationToVillage,

        // ���� � ���
        VillageToStartLocation,
        
        // ���� � �����
        VillageToBarn,
        
        // ���� � ������
        VillageToPillar,
        
        // ���� � �����
        VillageToCorral,
        
        // ���� � �����
        VillageToCastle,

        // �� ������ � �������
        PillarToVillage,

        // �� ������ � �������
        CorralToVillage,

        // �� ����� � �������
        CastleToVillage,

        // ����� ������ ��� ������ � ������
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
                SceneLoader.LoadScene("StartLocation", SceneLoader.Position.StartLocationHouseRightSide);
                break;
            case Checkpoint.StartLocationToVillage:
                SceneLoader.LoadScene("StartLocation", SceneLoader.Position.StartLocationNearExit);
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
            case Checkpoint.VillageToCorral:
                SceneLoader.LoadScene("Village", SceneLoader.Position.VillageNearCoralEntrance);
                break;
            case Checkpoint.VillageToCastle:
                SceneLoader.LoadScene("Village", SceneLoader.Position.VillageNearCastleEntrance);
                break;
            case Checkpoint.PillarToVillage:
                SceneLoader.LoadScene("PostLocation", SceneLoader.Position.PillarExit);
                break;
            case Checkpoint.CorralToVillage:
                SceneLoader.LoadScene("�orralLocation", SceneLoader.Position.CorralExit);
                break;
            case Checkpoint.CastleToVillage:
                SceneLoader.LoadScene("CastleLocation", SceneLoader.Position.CastleExit);
                break;
            case Checkpoint.BarnEnd:
                SceneLoader.LoadScene("Barnlocation", SceneLoader.Position.BarnAfterBosss);
                break;
        }
    }

    public static void LoadCheckpoint(GameObject sender)
    {
        PlayerData.playerCurrentHealth = health;
        PlayerData.playerMaxHealth = maxHealth;
        PlayerData.LoadSavedItems(savedItems);
        switch (PlayerData.checkpoint)
        {
            case Checkpoint.StartLocation:
                SceneLoader.LoadScene("StartLocation", SceneLoader.Position.StartLocationHouseRightSide);
                break;
            case Checkpoint.StartLocationToVillage:
                SceneLoader.LoadScene("StartLocation", SceneLoader.Position.StartLocationNearExit);
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
            case Checkpoint.VillageToCorral:
                SceneLoader.LoadScene("Village", SceneLoader.Position.VillageNearCoralEntrance);
                break;
            case Checkpoint.VillageToCastle:
                SceneLoader.LoadScene("Village", SceneLoader.Position.VillageNearCastleEntrance);
                break;
            case Checkpoint.PillarToVillage:
                SceneLoader.LoadScene("PostLocation", SceneLoader.Position.PillarExit);
                break;
            case Checkpoint.CorralToVillage:
                SceneLoader.LoadScene("�orralLocation", SceneLoader.Position.CorralExit);
                break;
            case Checkpoint.CastleToVillage:
                SceneLoader.LoadScene("CastleLocation", SceneLoader.Position.CastleExit);
                break;
            case Checkpoint.BarnEnd:
                SceneLoader.LoadScene("Barnlocation", SceneLoader.Position.BarnAfterBosss);
                break;
        }
    }
}
