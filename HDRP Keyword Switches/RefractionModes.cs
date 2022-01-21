using System.Reflection;
using UnityEngine;

namespace UnityEditor.ShaderGraph
{
    [Title("HDRP-Lit", "Keywords", "Refraction Modes")]
    class RefractionModes : CodeFunctionNode
    {
        public RefractionModes()
        {
            name = "Refraction Modes";
        }

        protected override MethodInfo GetFunctionToConvert()
        {
            return GetType().GetMethod("Keyword_Switch", BindingFlags.Static | BindingFlags.NonPublic);
        }

        static string Keyword_Switch(
            [Slot(0, Binding.None, 0, 0, 0, 0)] DynamicDimensionVector None,
            [Slot(1, Binding.None, 1, 1, 1, 1)] DynamicDimensionVector Plane,
            [Slot(2, Binding.None, 1, 1, 1, 1)] DynamicDimensionVector Sphere,
            [Slot(3, Binding.None, 1, 1, 1, 1)] DynamicDimensionVector Box,
            [Slot(4, Binding.None)] out DynamicDimensionVector Out)
        {
            return
@"
{
    #if defined (_REFRACTION_PLANE)
      Out = Plane;
    #elif defined(_REFRACTION_SPHERE)
      Out = Sphere;
    #elif defined(_REFRACTION_BOX)
      Out = Box;
    #else
      Out = None;
    #endif
}
";
        }
    }
}
