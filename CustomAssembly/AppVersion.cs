using System;

namespace CustomAssembly
{
    public class AppVersion
    {
        public Version VersionParse(string version)
        {
            try
            {
                var parsedVersion = Version.Parse(version);
                return parsedVersion;
            }
            catch (FormatException)
            {
                // Při neplatném formátu vrátí null
                return null;
            }
        }

        public VersionStatus CompareVersions(string Local , string Server)
        {
            var parsedVersion1 = VersionParse(Local);
            var parsedVersion2 = VersionParse(Server);

            if (parsedVersion1 == null || parsedVersion2 == null)
            {
                // Pokud verze nelze parsovat, vrátí Status s hodnotou Invalid
                return VersionStatus.Invalid;
            }

            var comparisonResult = parsedVersion1.CompareTo(parsedVersion2);

            if (comparisonResult > 0)
            {
                return VersionStatus.New;
            }
            else if (comparisonResult < 0)
            {
                return VersionStatus.Old;
            }
            else
            {
                return VersionStatus.Same;
            }
        }

        public enum VersionStatus
        {
            New,
            Old,
            Same,
            Invalid // Přidána hodnota pro neplatné verze
        }
    }
}