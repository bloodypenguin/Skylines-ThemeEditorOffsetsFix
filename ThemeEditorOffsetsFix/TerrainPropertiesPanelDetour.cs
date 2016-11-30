using System;
using System.Collections.Generic;
using System.Reflection;
using ColossalFramework;
using ColossalFramework.UI;
using ThemeEditorOffsetsFix.Redirection;
using UnityEngine;

namespace ThemeEditorOffsetsFix
{
    [TargetType(typeof(TerrainPropertiesPanel))]
    public class TerrainPropertiesPanelDetour : TerrainPropertiesPanel
    {
        private static readonly string m_Offsetformat = "0.000";

        private static Dictionary<MethodInfo, RedirectCallsState> _redirects;

        public static void Deploy()
        {
            if (_redirects != null)
            {
                return;
            }
            _redirects = RedirectionUtil.RedirectType(typeof(TerrainPropertiesPanelDetour));
        }
        public static void Revert()
        {
            if (_redirects == null)
            {
                return;
            }
            foreach (var redirect in _redirects)
            {
                RedirectionHelper.RevertRedirect(redirect.Key, redirect.Value);
            }
            _redirects = null;
        }


        private static UITextField m_GrassOffsetPollutionRed;
        private static UITextField m_GrassOffsetPollutionGreen;
        private static UITextField m_GrassOffsetPollutionBlue;
        private static UITextField m_GrassOffsetFieldRed;
        private static UITextField m_GrassOffsetFieldGreen;
        private static UITextField m_GrassOffsetFieldBlue;
        private static UITextField m_GrassOffsetFertilityRed;
        private static UITextField m_GrassOffsetFertilityGreen;
        private static UITextField m_GrassOffsetFertilityBlue;
        private static UITextField m_GrassOffsetForestRed;
        private static UITextField m_GrassOffsetForestGreen;
        private static UITextField m_GrassOffsetForestBlue;

        private static int step = 0;

        public static void Initialize()
        {
            var find = GameObject.Find("Offsets");
            if (find == null)
            {
                return;
            }
            UIComponent uiComponent5 = find.GetComponent<UIPanel>();
            m_GrassOffsetPollutionRed = uiComponent5.Find<UITextField>("GrassPollutionOffsetValueRed");
            m_GrassOffsetPollutionRed.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetPollutionRed.eventMouseDown += eventMouseDown;
            m_GrassOffsetPollutionGreen = uiComponent5.Find<UITextField>("GrassPollutionOffsetValueGreen");
            m_GrassOffsetPollutionGreen.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetPollutionGreen.eventMouseDown += eventMouseDown;
            m_GrassOffsetPollutionBlue = uiComponent5.Find<UITextField>("GrassPollutionOffsetValueBlue");
            m_GrassOffsetPollutionBlue.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetPollutionBlue.eventMouseDown += eventMouseDown;
            m_GrassOffsetFieldRed = uiComponent5.Find<UITextField>("GrassFieldOffsetValueRed");
            m_GrassOffsetFieldRed.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetFieldRed.eventMouseDown += eventMouseDown;
            m_GrassOffsetFieldGreen = uiComponent5.Find<UITextField>("GrassFieldOffsetValueGreen");
            m_GrassOffsetFieldGreen.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetFieldGreen.eventMouseDown += eventMouseDown;
            m_GrassOffsetFieldBlue = uiComponent5.Find<UITextField>("GrassFieldOffsetValueBlue");
            m_GrassOffsetFieldBlue.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetFieldBlue.eventMouseDown += eventMouseDown;
            m_GrassOffsetFertilityRed = uiComponent5.Find<UITextField>("GrassFertilityOffsetValueRed");
            m_GrassOffsetFertilityRed.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetFertilityRed.eventMouseDown += eventMouseDown;
            m_GrassOffsetFertilityGreen = uiComponent5.Find<UITextField>("GrassFertilityOffsetValueGreen");
            m_GrassOffsetFertilityGreen.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetFertilityGreen.eventMouseDown += eventMouseDown;
            m_GrassOffsetFertilityBlue = uiComponent5.Find<UITextField>("GrassFertilityOffsetValueBlue");
            m_GrassOffsetFertilityBlue.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetFertilityBlue.eventMouseDown += eventMouseDown;
            m_GrassOffsetForestRed = uiComponent5.Find<UITextField>("GrassForestOffsetValueRed");
            m_GrassOffsetForestRed.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetForestRed.eventMouseDown += eventMouseDown;
            m_GrassOffsetForestGreen = uiComponent5.Find<UITextField>("GrassForestOffsetValueGreen");
            m_GrassOffsetForestGreen.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetForestGreen.eventMouseDown += eventMouseDown;
            m_GrassOffsetForestBlue = uiComponent5.Find<UITextField>("GrassForestOffsetValueBlue");
            m_GrassOffsetForestBlue.eventMouseWheel += MGrassOffsetPollutionRedOnEventMouseWheel;
            m_GrassOffsetForestBlue.eventMouseDown += eventMouseDown;

//            WaterSimulation simulation = (WaterSimulation)typeof (TerrainManager).GetField("m_waterSimulation",
//                BindingFlags.NonPublic | BindingFlags.Instance).GetValue(TerrainManager.instance);
//            var buffers = (WaterSimulation.Cell[][])typeof(WaterSimulation).GetField("m_waterBuffers",
//                BindingFlags.NonPublic | BindingFlags.Instance).GetValue(simulation);
            for (int i = 0; i < 200; i++)
            {
                for (int j = 0; j < 200; j++)
                {
                    NaturalResourceManager.instance.m_naturalResources[
                        i*NaturalResourceManager.RESOURCEGRID_RESOLUTION + j].m_pollution = 255;
                    NaturalResourceManager.instance.m_naturalResources[
                        i*NaturalResourceManager.RESOURCEGRID_RESOLUTION + j].m_modified = true;
//                    if (i < 2)
//                    {
//                        buffers[i][j].m_pollution = buffers[i][j].m_height;
//                    }
                }


            }
            NaturalResourceManager.instance.AreaModified(-10000, -10000, 10000, 10000);
        }

        private static void eventMouseDown(UIComponent component, UIMouseEventParameter eventParam)
        {
            step = (step + 1) % 3;
        }

        private static void MGrassOffsetPollutionRedOnEventMouseWheel(UIComponent component, UIMouseEventParameter eventParam)
        {
            var tf = (UITextField)component;
            var val = Double.Parse(tf.text);

            var stepValue = 0f;
            switch (step)
            {
                case 0:
                    stepValue = 0.1f;
                    break;
                case 1:
                    stepValue = 0.01f;
                    break;
                case 2:
                    stepValue = 0.001f;
                    break;
            }
            if (eventParam.wheelDelta > 0)
            {
                val += stepValue;
            }
            else
            {
                val -= stepValue;
            }
            tf.text = val.ToString(m_Offsetformat);
            typeof(UITextField).GetMethod("OnSubmit", BindingFlags.Instance | BindingFlags.NonPublic).Invoke(tf, new object[] { });
        }

        [RedirectMethod]
        private void OnGrassOffsetPollutionRedChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset = new Vector3(Mathf.Clamp(result, -1f, 1f), Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset.y, Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset.z);
            m_GrassOffsetPollutionRed.text = Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset.x.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }

        [RedirectMethod]
        private void OnGrassOffsetPollutionGreenChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset = new Vector3(Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset.x, Mathf.Clamp(result, -1f, 1f), Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset.z);
            m_GrassOffsetPollutionGreen.text = Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset.y.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }

        [RedirectMethod]
        private void OnGrassOffsetPollutionBlueChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset = new Vector3(Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset.x, Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset.y, Mathf.Clamp(result, -1f, 1f));
            m_GrassOffsetPollutionBlue.text = Singleton<TerrainManager>.instance.m_properties.m_grassPollutionColorOffset.z.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }

        [RedirectMethod]
        private void OnGrassOffsetFieldRedChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset = new Vector3(Mathf.Clamp(result, -1f, 1f), Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset.y, Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset.z);
            m_GrassOffsetFieldRed.text = Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset.x.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }

        [RedirectMethod]
        private void OnGrassOffsetFieldGreenChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset = new Vector3(Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset.x, Mathf.Clamp(result, -1f, 1f), Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset.z);
            m_GrassOffsetFieldGreen.text = Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset.y.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }

        [RedirectMethod]
        private void OnGrassOffsetFieldBlueChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset = new Vector3(Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset.x, Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset.y, Mathf.Clamp(result, -1f, 1f));
            m_GrassOffsetFieldBlue.text = Singleton<TerrainManager>.instance.m_properties.m_grassFieldColorOffset.z.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }

        [RedirectMethod]
        private void OnGrassOffsetFertilityRedChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset = new Vector3(Mathf.Clamp(result, -1f, 1f), Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset.y, Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset.z);
            m_GrassOffsetFertilityRed.text = Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset.x.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }

        [RedirectMethod]
        private void OnGrassOffsetFertilityGreenChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset = new Vector3(Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset.x, Mathf.Clamp(result, -1f, 1f), Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset.z);
            m_GrassOffsetFertilityGreen.text = Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset.y.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }

        [RedirectMethod]
        private void OnGrassOffsetFertilityBlueChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset = new Vector3(Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset.x, Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset.y, Mathf.Clamp(result, -1f, 1f));
            m_GrassOffsetFertilityBlue.text = Singleton<TerrainManager>.instance.m_properties.m_grassFertilityColorOffset.z.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }

        [RedirectMethod]
        private void OnGrassOffsetForestRedChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset = new Vector3(Mathf.Clamp(result, -1f, 1f), Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset.y, Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset.z);
            m_GrassOffsetForestRed.text = Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset.x.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }

        [RedirectMethod]
        private void OnGrassOffsetForestGreenChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset = new Vector3(Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset.x, Mathf.Clamp(result, -1f, 1f), Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset.z);
            m_GrassOffsetForestGreen.text = Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset.y.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }

        [RedirectMethod]
        private void OnGrassOffsetForestBlueChanged(UIComponent c, string value)
        {
            float result;
            if (float.TryParse(value, out result))
                Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset = new Vector3(Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset.x, Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset.y, Mathf.Clamp(result, -1f, 1f));
            m_GrassOffsetForestBlue.text = Singleton<TerrainManager>.instance.m_properties.m_grassForestColorOffset.z.ToString(m_Offsetformat);
            Singleton<TerrainManager>.instance.m_properties.InitializeShaderProperties();
        }
    }
}