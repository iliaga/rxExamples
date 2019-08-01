using UniRx;
using UnityEngine;
using Zenject;

public class FormController : IInitializable
{
	private IForm formUi;

	public FormController(IForm formUi)
	{
		this.formUi = formUi;
	}

	public void PrintForm(Form form)
	{
		Debug.Log(form.ToString());
	}

	public void Initialize()
	{
		formUi.OForm.Subscribe(PrintForm);
	}
}