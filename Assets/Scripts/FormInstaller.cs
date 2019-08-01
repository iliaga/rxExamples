using System;
using System.Collections;
using System.Collections.Generic;
using Zenject;

public class FormInstaller : MonoInstaller
{
	public override void InstallBindings()
	{
		Container.Bind<IForm>().FromComponentInHierarchy().AsSingle();
		Container.BindInterfacesAndSelfTo<FormController>().AsSingle();
	}
}