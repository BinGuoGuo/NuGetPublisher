using System;

namespace NuGetPusher
{
    class Program
    {
        static void Main(string[] args)
        {
            // arg[0] $(ProjectDir)/bin/Debug 发布文件所在目录
            // arg[1] $(TargetPath)           项目程序集路径
            // arg[2] ApiKey                  仓库密钥
            // arg[3] Url                     仓库地址
            try
            {
                
                if (args.Length != 4)
                    throw new Exception($"invalid parameters: {System.Text.Json.JsonSerializer.Serialize(args)}");
                string publishDir = args[0];
                string assemblyFile = args[1];
                string apiKey = args[2];
                string url = args[3];

                var assemblyName = System.Reflection.AssemblyName.GetAssemblyName(assemblyFile);
                var nupkgName = $"{assemblyName.Name}.{assemblyName.Version.ToString(3)}.nupkg";
                var fullName = System.IO.Path.Combine(publishDir, nupkgName);
                Console.WriteLine($"nuget push {fullName} {apiKey} -Source {url}");
                ProcessHelper.Run("nuget.exe", $"push {fullName} {apiKey} -Source {url}", waitExit: true);
                Console.WriteLine("publish succeed!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
