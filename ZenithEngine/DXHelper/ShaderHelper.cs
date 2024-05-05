﻿using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ZenithEngine.DXHelper
{
    public static class ShaderHelper
    {
        public static string FormatToType(Format format)
        {
            switch (format)
            {
                case Format.R32G32B32A32_Float: return "float4";
                case Format.R32G32B32_Float: return "float3";
                case Format.R32G32_Float: return "float2";
                case Format.R32_Float: return "float";
                case Format.R16_Float: return "half";
                case Format.R32_UInt: return "int";
                default: throw new NotImplementedException($"Specified format not implemented yet: {format}");
            }
        }

        public static InputElement[] GetLayout(Type type, int slot = 0, int instancedStepRate = 0)
        {
            var parts = AssemblyElement.GetMembersOnType(type)
                .Select(AssemblyElement.GetAttributeFromMember)
                .Select(a => a.Layout)
                .ToArray();

            if (parts.Length > 0) parts[0].AlignedByteOffset = 0;
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].Slot = slot;
                parts[i].Classification = instancedStepRate == 0 ? InputClassification.PerVertexData : InputClassification.PerInstanceData;
                parts[i].InstanceDataStepRate = instancedStepRate;
            }
            return parts;
        }

        public static InputElement[] GetInstancedLayout(Type type)
        {
            var parts = AssemblyElement.GetMembersOnType(type)
                .Select(AssemblyElement.GetAttributeFromMember)
                .Select(a => a.Layout)
                .ToArray();
            return parts;
        }

        public static string BuildStructDefinition(Type type) =>
            BuildStructDefinition(type.Name, AssemblyElement.GetMembersOnType(type));
        public static string BuildStructDefinition(string name, Type type, Type instanceType) =>
            BuildStructDefinition(name, AssemblyElement.GetMembersOnType(type).Concat(AssemblyElement.GetMembersOnType(instanceType)));
        public static string BuildStructDefinition(Type type, Type instanceType) =>
            BuildStructDefinition(type.Name, type, instanceType);

        public static string BuildStructDefinition(string name, IEnumerable<MemberInfo> members)
        {
            List<string> lines = new List<string>();
            lines.Add($"struct {name}");
            lines.Add("{");
            foreach (var m in members)
            {
                var e = AssemblyElement.GetAttributeFromMember(m).Layout;
                lines.Add($"    {FormatToType(e.Format)} {m.Name.ToLower()} : {e.SemanticName};");
            }
            lines.Add("};");

            return string.Join("\n", lines);
        }
    }
}
