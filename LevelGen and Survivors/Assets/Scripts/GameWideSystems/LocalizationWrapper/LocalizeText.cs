using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Utils.UtilityTypes.Result;

namespace GameWideSystems.LocalizationWrapper
{
    public class LocalizeText : MonoBehaviour, ILocalizer
    {
        [SerializeField] private TranslationCategory _translationCategory;
        [SerializeField] private string _localizationReference;
        
        public UniTask<ProcedureResult> Localize(ILocalizationManager localizationManager, CancellationToken cancellationToken)
        {
            TMP_Text text = GetComponent<TMP_Text>();

            if (string.IsNullOrWhiteSpace(_localizationReference))
            {
                if (string.IsNullOrWhiteSpace(text.text))
                {
                    return ProcedureResultBuilder.Failure().AsUniTask();
                }

                _localizationReference = text.text;
            }

            if (localizationManager.TryGetLocalized(_localizationReference, _translationCategory, out string line))
            {
                text.text = line;
                return ProcedureResultBuilder.Success().AsUniTask();
            }
            
            return ProcedureResultBuilder.Failure().AsUniTask();
        }
        
        
    }
}