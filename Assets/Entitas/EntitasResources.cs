using System.IO;

namespace Entitas {

    public static class EntitasResources {

        public static string GetVersion()
        {
            return "1.13.0";
            var assembly = typeof(Entity).Assembly;
            var stream = assembly.GetManifestResourceStream("version.txt");
            string version;
            using (var reader = new StreamReader(stream)) {
                version = reader.ReadToEnd();
            }

            return version;
        }
    }
}
