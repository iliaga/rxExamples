using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ObservableForm : MonoBehaviour, IForm
{
	public TMP_InputField firstNameInput, lastNameInput, cityInput, hobbiesInput;

	public Button confirmButton;

	public IObservable<Form> OForm { get; private set; }

	// Start is called before the first frame update
	private void Awake()
	{
		OForm = confirmButton.onClick.AsObservable().Select(_ => new Form
		{
			City = cityInput.text, FirstName = firstNameInput.text, LastName = lastNameInput.text, Hobbies = hobbiesInput.text.Split(',')
		});
	}
}