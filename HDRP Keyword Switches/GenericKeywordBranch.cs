using System.Reflection;
using UnityEngine;
using UnityEditor.Graphing;
using UnityEditor.ShaderGraph.Drawing.Controls;
using UnityEditor.ShaderGraph.Internal;

namespace UnityEditor.ShaderGraph
{
    [Title("HDRP-Lit", "Keywords", "Generic")]
    class GenericKeywordBranch : AbstractMaterialNode, IGeneratesBodyCode
    {
        public GenericKeywordBranch()
        {
            UpdateNodeName();
            UpdateNodeAfterDeserialization();
        }

        public override bool hasPreview
        {
            get { return false; }
        }


        const int InputEnabledSlotId = 0;
        const int InputDisabledSlotId = 1;
        const int OutputSlotId = 2;
        const int OutputBoolSlotId = 3;
        const string kInputEnabledSlotName = "Enabled";
        const string kInputDisabledSlotName = "Disabled";
        const string kOutputSlotName = "Out";
        const string kOutputBoolSlotName = "State Bool";

        [SerializeField]
        private string m_keyword = "_KEYWORD";

        [TextControl]
        public string keyword
        {
            get
            {
                return m_keyword;
            }
            set
            {
                m_keyword = value;
            }
        }

        void UpdateNodeName()
        {
            name = m_keyword;
        }

        public sealed override void UpdateNodeAfterDeserialization()
        {
            AddSlot(new DynamicVectorMaterialSlot(InputEnabledSlotId, kInputEnabledSlotName, kInputEnabledSlotName, SlotType.Input, Vector4.zero));
            AddSlot(new DynamicVectorMaterialSlot(InputDisabledSlotId, kInputDisabledSlotName, kInputDisabledSlotName, SlotType.Input, Vector4.zero));
            AddSlot(new DynamicVectorMaterialSlot(OutputSlotId, kOutputSlotName, kOutputSlotName, SlotType.Output, Vector4.zero));
            AddSlot(new BooleanMaterialSlot(OutputBoolSlotId, kOutputBoolSlotName, kOutputBoolSlotName, SlotType.Output, false));
            RemoveSlotsNameNotMatching(new[] { InputDisabledSlotId, InputEnabledSlotId, OutputSlotId, OutputBoolSlotId });
        }


        public void GenerateNodeCode(ShaderStringBuilder sb, GenerationMode generationMode)
        {
            var inputEnabledValue = GetSlotValue(InputEnabledSlotId, generationMode);
            var inputDisabledValue = GetSlotValue(InputDisabledSlotId, generationMode);
            var outputName = GetVariableNameForSlot(OutputSlotId);
            var outputBoolName = GetVariableNameForSlot(OutputBoolSlotId);

            sb.AppendLine("{0} {1};", FindOutputSlot<MaterialSlot>(OutputSlotId).concreteValueType.ToShaderString(), GetVariableNameForSlot(OutputSlotId));
            sb.AppendLine("{0} {1};", FindOutputSlot<MaterialSlot>(OutputBoolSlotId).concreteValueType.ToShaderString(), GetVariableNameForSlot(OutputBoolSlotId));
            sb.AppendLine("#ifdef {0}", m_keyword );
            sb.AppendLine("{0} = {1};", outputName, inputEnabledValue);
            sb.AppendLine("{0} = true;", outputBoolName);
            sb.AppendLine("#else");
            sb.AppendLine("{0} = {1};", outputName, inputDisabledValue);
            sb.AppendLine("{0} = false;", outputBoolName);
            sb.AppendLine("#endif");
        }
    }
}
