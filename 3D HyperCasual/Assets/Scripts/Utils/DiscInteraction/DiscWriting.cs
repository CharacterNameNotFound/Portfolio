using System;
using System.IO;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Utils.UtilityTypes.Result;

namespace Utils.DiscInteraction
{
    public static class DiscWriting
    {
        public static async UniTask<ProcedureResult> ConvertAndWriteJson(object value, string path, JsonSerializerSettings settings, CancellationToken cancellationToken)
        {
            string folder = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar));
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            
            try
            {
                var json = JsonConvert.SerializeObject(value, settings);
                
                await File.WriteAllTextAsync(path, json, Encoding.Unicode, cancellationToken).AsUniTask();

                return ProcedureResultBuilder.Success();
            }
            catch (Exception exception)
            {
                return ProcedureResultBuilder.Failure(exception);
            }
        }
        
    }
}