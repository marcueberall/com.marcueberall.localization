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
namespace com.marcueberall.localization {
	/// <summary>
	/// Controller for a localized Text.
	/// </summary>
	[RequireComponent(typeof(Text))]
	[AddComponentMenu(@"com.marcueberall.localization/Localized Text")]
	public class LocalizedText : LocalizedObject<Text> {
		#region LocalizedObject Members

		/// <summary>
		/// Gets called when the language has been changed.
		/// </summary>
		protected override void OnLanguageChanged() {
			this.m_instance.text = LocalizationManager.instance.Get(this.m_key);
		}

		#endregion
	}
}