#region Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;

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
	/// Base controller for localized objects.
	/// </summary>
	/// <typeparam name="T">The object to be localized.</typeparam>
	[AddComponentMenu(@"")]
	public abstract class LocalizedObject<T> : MonoBehaviour {
		#region Fields

		protected T m_instance = default(T);

		#endregion

		#region Inspector

		[Header(@"General")]

		[SerializeField]
		protected string m_key = default(string);

		/// <summary>
		/// The database key of this localized object.
		/// </summary>
		public string key {
			get => this.m_key;
			set => this.m_key = value;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Gets called when the language has been changed.
		/// </summary>
		protected abstract void OnLanguageChanged();

		#endregion

		#region MonoBehaviour Members

		/// <summary>
		/// Gets called when the script instance is being loaded.
		/// </summary>
		public virtual void Awake() {
			LocalizationManager.instance.onLanguageChanged.AddListener(this.OnLanguageChanged);

			this.m_instance = this.GetComponent<T>();

			this.OnLanguageChanged();
		}

		#endregion
	}
}