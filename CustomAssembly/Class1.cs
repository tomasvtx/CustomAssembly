using System;
using System.IO;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace CustomAssembly
    {
    public class CustomAssembly
        {
            private readonly Assembly _assembly;
            public CustomAssembly(Assembly assembly)
            {
                _assembly = assembly;
            }
            public string GetTitle()
            {
                var version = _assembly.GetName().Version;
                var titleAttribute = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(_assembly, typeof(AssemblyTitleAttribute));

                return $"{titleAttribute?.Title} - {version}";
            }

            public CustomAssemblyOutput GetInfo( )
            {
            var customAssembly = new CustomAssemblyOutput();
                customAssembly.Version = _assembly.GetName().Version;
                customAssembly.TitleAttribute = (AssemblyTitleAttribute)Attribute.GetCustomAttribute(_assembly, typeof(AssemblyTitleAttribute));
                customAssembly.DescriptionAttribute = (AssemblyDescriptionAttribute)Attribute.GetCustomAttribute(_assembly, typeof(AssemblyDescriptionAttribute));
                customAssembly.CompanyAttribute = (AssemblyCompanyAttribute)Attribute.GetCustomAttribute(_assembly, typeof(AssemblyCompanyAttribute));
                customAssembly.ProductAttribute = (AssemblyProductAttribute)Attribute.GetCustomAttribute(_assembly, typeof(AssemblyProductAttribute));

                return customAssembly;
            }

            public CustomAssemblyStringOutput GetInfoString( )
            {
            var customAssembly = new CustomAssemblyStringOutput();
                customAssembly.Version = _assembly.GetName( ).Version?.ToString();
                customAssembly.TitleAttribute = ((AssemblyTitleAttribute)Attribute.GetCustomAttribute(_assembly , typeof(AssemblyTitleAttribute)))?.Title;
                customAssembly.DescriptionAttribute = Attribute.GetCustomAttribute(_assembly , typeof(AssemblyDescriptionAttribute))?.ToString();
                customAssembly.CompanyAttribute = ((AssemblyCompanyAttribute)Attribute.GetCustomAttribute(_assembly , typeof(AssemblyCompanyAttribute)))?.Company;
                customAssembly.ProductAttribute = ((AssemblyProductAttribute)Attribute.GetCustomAttribute(_assembly , typeof(AssemblyProductAttribute)))?.Product;

                return customAssembly;
            }

        public async Task<string> SaveToJsonFile(string filePath)
        {
            try
            {
                var json = JsonSerializer.Serialize(GetInfoString());
                await File.WriteAllTextAsync(filePath, json);
                return String.Empty;
            }
            catch(Exception ss)
            {
                return ss.Message;
            }
        }

        public CustomAssemblyStringOutput LoadFromJsonFile(string filePath, out string Err)
        {
            try
            {
                var json = File.ReadAllText(filePath);
                Err = string.Empty;
                return JsonSerializer.Deserialize<CustomAssemblyStringOutput>(json);
            }
            catch (Exception ss)
            {
                Err =ss.Message;
                return null;
            }
            }
        }

        public class CustomAssemblyOutput
        {
            public Version Version { get; set; }
            public AssemblyTitleAttribute TitleAttribute { get; set; }
            public AssemblyDescriptionAttribute DescriptionAttribute { get; set; }
            public AssemblyCompanyAttribute CompanyAttribute { get; set; }
            public AssemblyProductAttribute ProductAttribute { get; set; }
        }
        public class CustomAssemblyStringOutput
        {
            public string Version { get; set; }
            public string TitleAttribute { get; set; }
            public string DescriptionAttribute { get; set; }
            public string CompanyAttribute { get; set; }
            public string ProductAttribute { get; set; }
        }
    }
