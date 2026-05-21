using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using Game.UITools.GenericViewRoutines;
using GameWideSystems.TooltipsManagement;
using TMPro;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

namespace Game.UI.Tooltips
{
    public class TextTooltip : TooltipBase
    {
        [SerializeField] private TMP_Text _header;
        [SerializeField] private TMP_Text _body;

        [SerializeField] private LayoutElement _layoutElement;
        
        public override TooltipType TooltipType => TooltipType.GenericText;
        

        public override async UniTask Show(TooltipParams tooltipParams, CancellationToken cancellationToken)
        {
            await base.Show(tooltipParams, cancellationToken);

            TextTooltipParams textTooltipParams = (TextTooltipParams) tooltipParams;

            _header.text = textTooltipParams.Header;
            _body.text = textTooltipParams.Body;
            
            
            await GenericUIEntranceRoutines.ShowInstantly(gameObject, cancellationToken);
            await UniTask.WaitForEndOfFrame(cancellationToken);

            float width = Mathf.Max(
                _header.textInfo.lineInfo.Max(x => x.width),
                _body.textInfo.lineInfo.Max(x => x.width));

            width += _header.margin.x + _header.margin.z;

            _layoutElement.enabled = width > _layoutElement.preferredWidth;
        }

        public override async UniTask Hide(CancellationToken cancellationToken)
        {
            await GenericUIExitRoutines.HideInstantly(gameObject, cancellationToken);
        }
        
        public override void Dispose()
        {
            Addressables.ReleaseInstance(gameObject);
        }
    }
}