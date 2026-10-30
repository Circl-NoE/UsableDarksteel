using Il2CppAlta;
using Il2CppAlta.Blacksmithing;
using Il2CppAlta.Networking;
using System.Collections.Generic;
using MateriaLib;
using MelonLoader;
using System;
using System.Linq;
using UnityEngine;

[assembly: MelonInfo(typeof(UsableDarksteel.Main), "Usable Darksteel", "1.0.3-Q", "Circl")]

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
            Il2Cpp.PhysicalMaterial physmat = HashedGeneralValue<Il2Cpp.PhysicalMaterial>.Get(55232u);
            LibMaterial mat = new LibMaterial(physmat);
            mat.addToList = false;
            LibMaterial.NewMaterials.Add(mat);
            physmat.isShowingInList = true;

            Material[] materials = Resources.FindObjectsOfTypeAll<Material>();
            Material mainMat = Array.Find(materials, u => u.name == "Darksteel Alloy");
            Material nonForgeMat = Array.Find(materials, u => u.name == "Darksteel Alloy NonForging");

            Material[] matArray = new Material[] { mainMat, nonForgeMat };

            Il2Cpp.PhysicalMaterial.MaterialChannel channel = new Il2Cpp.PhysicalMaterial.MaterialChannel();
            Il2Cpp.PhysicalMaterial.MaterialChannel channel2 = new Il2Cpp.PhysicalMaterial.MaterialChannel();

            Il2Cpp.PhysicalMaterial.MaterialChannel[] newArr = new Il2Cpp.PhysicalMaterial.MaterialChannel[] { channel, channel2 };
            mat.physicalMaterial.androidMaterialChannels = newArr;
            mat.ReplaceAllMaterials(matArray, nonForgeMat);
        }
    }
}
