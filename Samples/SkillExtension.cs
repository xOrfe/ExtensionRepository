using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Xo.LiquidFramework.Samples
{

	[Description("SkillExtension")]
	public struct SkillExtension : IExtension
	{
		private ExtensionRepository _extensionRepository;
		[SerializeField] private int x;
		public void Init(ExtensionRepository extensionRepository)
		{
			_extensionRepository = extensionRepository;
			_extensionRepository.AssignExtensionToEvent("OnBulletHıt",Execute);
		}

		//Execute.
		public IEArgsOutput Execute(IEArgsInput ieArgsInput)
		{
			Debug.Log("Executed");
			return null;
		}
	}
}