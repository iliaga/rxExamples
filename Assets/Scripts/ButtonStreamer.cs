using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System.Linq;
using DG.Tweening;

public class ButtonStreamer : MonoBehaviour
{
	[SerializeField] private InputField thingy;
    // Start is called before the first frame update
    void Start()
    {
	
		/// main obs
		var thingyEdit = thingy.onEndEdit.AsObservable();
		var rectObservables = GetComponentsInChildren<Button>()
			.Select(button => button.onClick.AsObservable().Select(x => new {button , rect = button.transform.GetComponent<RectTransform>() }));
		var mergedObervable = Observable.Merge(rectObservables);
		var buttonBufferObservable = mergedObervable.Buffer(3);
		var lastButtonText = buttonBufferObservable.Zip(thingyEdit, (bb, te) => new {bb,te }).Select(x => $"[{x.bb.Last().button.name}]+{x.te}");

		//debug sub
		mergedObervable.Subscribe(x=>Debug.Log(x.button));

		//animation sub
		buttonBufferObservable.Subscribe(presses=>
		{
			var thingyRect = thingy.GetComponent<RectTransform>();
			var sequence = DOTween.Sequence();
			for (int i = 0; i < presses.Count; i++)
			{
				var button = presses[i];
				var thingyReqPos = presses[i].rect.anchoredPosition3D;
				thingyReqPos.y += thingyRect.rect.height +10;
				var tween = thingyRect.DOAnchorPos3D(thingyReqPos, 0.4f);
				sequence.Append(tween);
			}
			
		});

		lastButtonText.Subscribe(x=>Debug.Log(x));


	}

   
}
