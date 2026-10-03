using Alta;
using Alta.Blacksmithing;
using Alta.Networking;
using CustomRecipesAPI;
using MateriaLib;
using MelonLoader;
using System;
using UnityEngine;

[assembly: MelonInfo(typeof(UsableDarksteel.Main), "Usable Darksteel", "1.0.0", "Circl")]

namespace UsableDarksteel
{
    public class Main : MelonMod
    {
        public override void OnInitializeMelon()
        {
            MateriaLib.Main.SetupMaterial += GetAndFix;
        }
        public static void GetAndFix()
        {
            PhysicalMaterial physmat = HashedGeneralValue<PhysicalMaterial>.Get(55232u);
            LibMaterial mat = new LibMaterial(physmat);
            mat.addToList = false;
            LibMaterial.NewMaterials.Add(mat);
            physmat.isShowingInList = true;

            Material[] materials = Resources.FindObjectsOfTypeAll<Material>();
            Material mainMat = Array.Find(materials, u => u.name == "Darksteel Alloy");
            Material nonForgeMat = Array.Find(materials, u => u.name == "Darksteel Alloy NonForging");

            Material[] matArray = new Material[] { mainMat, nonForgeMat };

            PhysicalMaterial.MaterialChannel channel = new PhysicalMaterial.MaterialChannel();
            PhysicalMaterial.MaterialChannel channel2 = new PhysicalMaterial.MaterialChannel();

            PhysicalMaterial.MaterialChannel[] newArr = new PhysicalMaterial.MaterialChannel[] { channel, channel2 };
            mat.physicalMaterial.materialChannels = newArr;
            mat.ReplaceAllMaterials(matArray, nonForgeMat);

            SmeltingRecipe recipe = HashedGeneralValue<SmeltingRecipe>.Get(60118u);
            Core.AddSmeltingRecipeToSmelterUpgrades(recipe, HashedGeneralValue<SmelterUpgrades>.Get(33428u), true);

            GameObject IngPrefab = Resources.Load<GameObject>("network prefabs/props/ingots/Darksteel Ingot");
            NetworkPrefab netPrefab = IngPrefab.GetComponent<NetworkPrefab>();
            netPrefab.canBeSpawnedThroughCommand = true;

            mat.ingot = IngPrefab;
            mat.unlockAt = 3;
        }
    }
}
