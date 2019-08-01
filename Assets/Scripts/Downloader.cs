using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UniRx;
using UniRx.Async;
public class Downloader : MonoBehaviour
{
	[SerializeField]
	private Image image;
	
	[SerializeField]
	private Button button;

	private IObservable<byte[]> _wwwObs;
	private IDisposable _canceled;

	// Start is called before the first frame update
    void Start()
    {
	   var clickstream =  button.onClick.AsObservable();
	   clickstream.Buffer(clickstream.Throttle(TimeSpan.FromMilliseconds(250)))
		   .Subscribe(clickcount =>
	    {
		    if (clickcount.Count == 1)
		    {
			    var prog = new ScheduledNotifier<float>();
			    _wwwObs = ObservableWWW.GetAndGetBytes("https://speed.hetzner.de/100MB.bin", null, prog)
				    .DoOnTerminate(() => _wwwObs = null)
				    //.DoOnCompleted();
				    .DoOnCancel(() =>
				    {
					    Debug.Log("canceled");
					    image.fillAmount = 0;
				    });
			    prog.Subscribe(x => image.fillAmount = x);

			    _canceled = _wwwObs.Subscribe();
		    }
		    else
		    {
			    if (_canceled == null)
			    {
					return;
			    }
			    _canceled.Dispose();
			    _canceled = null;
		    }
		   
	    });
		



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
