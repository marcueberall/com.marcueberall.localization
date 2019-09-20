#region Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
using UnityEngine.UI;

#endregion

/// <copyright>
///
///
///    ███▄ ▄███▓ ▄▄▄       ██▀███   ▄████▄      █    ██ ▓█████  ▄▄▄▄   ▓█████  ██▀███   ▄▄▄       ██▓     ██▓    
///   ▓██▒▀█▀ ██▒▒████▄    ▓██ ▒ ██▒▒██▀ ▀█      ██  ▓██▒▓█   ▀ ▓█████▄ ▓█   ▀ ▓██ ▒ ██▒▒████▄    ▓██▒    ▓██▒    
///   ▓██    ▓██░▒██  ▀█▄  ▓██ ░▄█ ▒▒▓█    ▄    ▓██  ▒██░▒███   ▒██▒ ▄██▒███   ▓██ ░▄█ ▒▒██  ▀█▄  ▒██░    ▒██░    
///   ▒██    ▒██ ░██▄▄▄▄██ ▒██▀▀█▄  ▒▓▓▄ ▄██▒   ▓▓█  ░██░▒▓█  ▄ ▒██░█▀  ▒▓█  ▄ ▒██▀▀█▄  ░██▄▄▄▄██ ▒██░    ▒██░    
///   ▒██▒   ░██▒ ▓█   ▓██▒░██▓ ▒██▒▒ ▓███▀ ░   ▒▒█████▓ ░▒████▒░▓█  ▀█▓░▒████▒░██▓ ▒██▒ ▓█   ▓██▒░██████▒░██████▒
///   ░ ▒░   ░  ░ ▒▒   ▓▒█░░ ▒▓ ░▒▓░░ ░▒ ▒  ░   ░▒▓▒ ▒ ▒ ░░ ▒░ ░░▒▓███▀▒░░ ▒░ ░░ ▒▓ ░▒▓░ ▒▒   ▓▒█░░ ▒░▓  ░░ ▒░▓  ░
///   ░  ░      ░  ▒   ▒▒ ░  ░▒ ░ ▒░  ░  ▒      ░░▒░ ░ ░  ░ ░  ░▒░▒   ░  ░ ░  ░  ░▒ ░ ▒░  ▒   ▒▒ ░░ ░ ▒  ░░ ░ ▒  ░
///   ░      ░     ░   ▒     ░░   ░ ░            ░░░ ░ ░    ░    ░    ░    ░     ░░   ░   ░   ▒     ░ ░     ░ ░   
///          ░         ░  ░   ░     ░ ░            ░        ░  ░ ░         ░  ░   ░           ░  ░    ░  ░    ░  ░
///                                 ░                                 ░                                           
///	
/// </copyright>
namespace com.marcueberall.localization.Example {
	/// <summary>
	/// All the stuff that accesses the localization manager should be set to -999 in the script execution order to prevent.
	/// </summary>
	[RequireComponent(typeof(Dropdown))]
	[AddComponentMenu(@"")]
	public class LanguageSetter : MonoBehaviour {
		#region Methods

		public virtual void SetLanguage(int index) {
			// set the language by its index in the drop down list. we can do this because we generated the options by using the supported languages from the localization manager
			// and the index is the same.
			LocalizationManager.instance.SetLanguage(index);
		}

		public virtual void SetLanguage(string code) {
			// set the language by its country code.
			LocalizationManager.instance.SetLanguage(code);
		}

		public virtual void PingLanguage() {
			Debug.Log($"The current language has been set to '{LocalizationManager.instance.currentLanguage}'");
		}

		#endregion

		#region MonoBehaviour Members

		public virtual void Awake() {
			Dropdown dropdown = this.GetComponent<Dropdown>();

			// iterate the supported languages
			foreach (string language in LocalizationManager.instance.supportedLanguages) {
				// add the country codes to the drop down list
				dropdown.options.Add(new Dropdown.OptionData(language));

				// check for the current language and activate it's option in the drop down list
				if (LocalizationManager.instance.currentLanguage == language) {
					dropdown.value = dropdown.options.Count - 1;
				}
			}
		}

		#endregion
	}
}