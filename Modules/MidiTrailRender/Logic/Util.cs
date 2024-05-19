using System;
using System.IO;
using System.Reflection;
using ZenithEngine.DXHelper.Presets;

namespace MIDITrailRender.Logic
{
    static class Util
    {
        static Assembly assembly = Assembly.GetExecutingAssembly();

        public static Stream OpenEmbedStream(string name)
        {
            return assembly.GetManifestResourceStream(name);
        }

        public static string ReadEmbed(string name)
        {
            using var stream = OpenEmbedStream(name);
            using var reader = new StreamReader(stream);
            if (reader.ReadToEnd() == null)
            {
                throw new Exception();
            }
            else
            {
                return reader.ReadToEnd();
            }
        }

        public static string ReadShader(string name)
        {
            var noise = Shaders.ReadShaderText("noise.fx");
            var shared = ReadEmbed("MidiTrailRender.Shaders.shared.fx");
            var shader = ReadEmbed("MidiTrailRender.Shaders." + name);
            return noise + "\n\n" + shared + "\n\n" + shader;
        }

        public static float Lerp(float s, float e, float f)
        {
            return s + (e - s) * f;
        }
    }
}
