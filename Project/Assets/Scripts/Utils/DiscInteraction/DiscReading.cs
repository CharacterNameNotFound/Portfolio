using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Newtonsoft.Json;
using Utils.UtilityTypes.Result;

namespace Utils.DiscInteraction
{
    public class DiscReading
    {
        /// <summary>
        /// Read all files in directory
        /// </summary>
        public static async UniTask<RequestResult<IEnumerable<T>>> ReadAndConvertAllJson<T>(string path, JsonSerializerSettings settings, CancellationToken cancellationToken)
        {
            try
            {
                if (!Directory.Exists(path))
                {
                    return RequestResultBuilder.Error<IEnumerable<T>>(new DirectoryNotFoundException());
                }

                string[] files = Directory.GetFiles(path);
                List<T> result = new List<T>(files.Length);
                
                foreach (string item in files)
                {
                    string json = await File.ReadAllTextAsync(item, Encoding.Unicode, cancellationToken).AsUniTask();
                    result.Add(JsonConvert.DeserializeObject<T>(json, settings));
                }
                
                return result.AsEnumerable().AsRequestResult();
            }
            catch (Exception exception)
            {
                return RequestResultBuilder.Error<IEnumerable<T>>(exception);
            }
        }
        
        public static async UniTask<RequestResult<T>> ReadAndConvertJson<T>(string path, JsonSerializerSettings settings, CancellationToken cancellationToken)
        {
            try
            {
                string json = await File.ReadAllTextAsync(path, Encoding.Unicode, cancellationToken).AsUniTask();
                return JsonConvert.DeserializeObject<T>(json, settings).AsRequestResult();
            }
            catch (Exception exception)
            {
                return RequestResultBuilder.Error<T>(exception);
            }
        }
    }
}