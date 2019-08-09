#region Directives

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;

using UnityEngine;
using UnityEngine.Events;

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
	/// The core localization manager.
	/// </summary>
	[DisallowMultipleComponent]
	[AddComponentMenu(@"com.marcueberall.localization/Localization Manager")]
	public class LocalizationManager : MonoBehaviour {
		/// <summary>
		/// A general localization event that gets raised when the current language has been changed.
		/// </summary>
		[Serializable]
		public class LocalizationEvent : UnityEvent {
		}

		#region Constants

		/// <summary>
		/// The PlayerPrefs key to store the current language.
		/// </summary>
		protected const string PLAYER_PREFS_LANGUAGE = @"Language";

		/// <summary>
		/// The path to the localization files.
		/// </summary>
		public const string LOCALIZATION_PATH = @"Localization";

		public const string XML_TAG_LANGUAGE = @"language";
		public const string XML_TAG_DATA = @"data";
		public const string XML_ATTRIBUTE_ID = @"id";

		#endregion

		#region Fields

		protected SortedDictionary<string, string> m_dictionary = new SortedDictionary<string, string>();

		private static LocalizationManager _instance = null;

		#endregion

		#region Indexers

		/// <summary>
		/// Returns a localization value for the specified key.
		/// </summary>
		/// <param name="key">The key to return the localization value for.</param>
		/// <returns>The localized value.</returns>
		public string this[string key] {
			get => this.Get(key);
		}

		#endregion

		#region Inspector

		[Header(@"General")]

		[SerializeField]
		protected string m_defaultLanguage = @"en";

		/// <summary>
		/// Gets or sets the default language.
		/// </summary>
		public string defaultLanguage {
			get => this.m_defaultLanguage;
			set => this.m_defaultLanguage = value;
		}

		[Header(@"Events")]

		[SerializeField]
		protected LocalizationEvent m_onChangeLanguage = new LocalizationEvent();

		/// <summary>
		/// Gets or sets the OnLanguageChanged events.
		/// </summary>
		public LocalizationEvent onLanguageChanged {
			get => this.m_onChangeLanguage;
			set => this.m_onChangeLanguage = value;
		}

		[SerializeField, HideInInspector]
		protected string[] m_supportedLanguages = default(string[]);

		/// <summary>
		/// Gets or sets the supported languages.
		/// </summary>
		public string[] supportedLanguages {
			get => this.m_supportedLanguages;
			set => this.m_supportedLanguages = value;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets the current language.
		/// </summary>
		public virtual string currentLanguage { get; protected set; }

		/// <summary>
		/// Gets a value whether the current instance of LocalizationManager has been destroyed.
		/// </summary>
		public static bool destroyed { get; private set; } = false;

		/// <summary>
		/// Gets the current instance of LocalizationManager or creates a new one.
		/// </summary>
		public static LocalizationManager instance {
			get {
				if (LocalizationManager.destroyed) {
					return null;
				}

				if (LocalizationManager._instance == null) {
					LocalizationManager._instance = GameObject.FindObjectOfType<LocalizationManager>();

					if (LocalizationManager._instance == null) {
						LocalizationManager._instance = new GameObject(string.Format(@"AUTOGEN_{0}", typeof(LocalizationManager).Name)).AddComponent<LocalizationManager>();
					}
				}

				return LocalizationManager._instance;
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Returns a localization value for the specified key.
		/// </summary>
		/// <param name="key">The key to return the localization value for.</param>
		/// <returns>The localized value.</returns>
		public virtual string Get(string key) {
			try {
				return this.m_dictionary[key] ?? $"[{key}]";
			}
			catch (Exception e) {
				throw new LocalizationException($"Failed to return requested localization item '{key}'.", e);
			}
		}

		/// <summary>
		/// Returns a localized audio clip for the specified key.
		/// </summary>
		/// <param name="key">The key to return the localized audio clip for.</param>
		/// <returns>The localized audio clip.</returns>
		public virtual AudioClip GetAudioClip(string key) {
			try {
				return Resources.Load<AudioClip>(this.m_dictionary[key]);
			}
			catch (Exception e) {
				throw new LocalizationException($"Failed to return requested audio clip '{key}'.", e);
			}
		}

		/// <summary>
		/// Returns a localized sprite for the specified key.
		/// </summary>
		/// <param name="key">The key to return the localized sprite for.</param>
		/// <returns>The localized sprite.</returns>
		public virtual Sprite GetSprite(string key) {
			try {
				return Resources.Load<Sprite>(this.m_dictionary[key]);
			}
			catch (Exception e) {
				throw new LocalizationException($"Failed to return requested sprite '{key}'.", e);
			}
		}

		/// <summary>
		/// Loads the data file for the specified language code.
		/// </summary>
		/// <param name="code">The language code to load.</param>
		/// <returns>True on success, otherwise false.</returns>
		protected virtual bool LoadLanguage(string code) {
			try {
				this.m_dictionary.Clear();

				TextAsset languageData = Resources.Load($"{LocalizationManager.LOCALIZATION_PATH}/{code}") as TextAsset;
				XDocument doc = XDocument.Parse(languageData.text);

				foreach (XElement element in doc.Descendants(LocalizationManager.XML_TAG_DATA)) {
					string key = element.Attribute(LocalizationManager.XML_ATTRIBUTE_ID)?.Value;

					if (!string.IsNullOrEmpty(element.Value)) {
						this.m_dictionary[key] = element.Value;
					}
					else {
						throw new ArgumentNullException(@"key");
					}
				}

				return true;
			}
			catch (Exception e) {
				throw new LocalizationException($"Failed to load language '{code}'.", e);
			}
		}

		/// <summary>
		/// Sets the current langauge.
		/// </summary>
		/// <param name="index">The index of the language within the supported languages list.</param>
		/// <param name="store">Indicates whether to write the new language to the PlayerPrefs.</param>
		public virtual void SetLanguage(int index, bool store = true) {
			try {
				this.SetLanguage(this.m_supportedLanguages[index], store);
			}
			catch (Exception e) {
				throw new LocalizationException($"Failed to set language to '{index}'.", e);
			}
		}

		/// <summary>
		/// Sets the current langauge.
		/// </summary>
		/// <param name="code">The language code to set.</param>
		/// <param name="store">Indicates whether to write the new language to the PlayerPrefs.</param>
		public virtual void SetLanguage(string code, bool store = true) {
			if (!string.IsNullOrEmpty(code) && this.currentLanguage != code) {
				if (!this.LoadLanguage(code)) {
					return;
				}

				this.currentLanguage = code;

				this.onLanguageChanged.Invoke();

				if (store) {
					PlayerPrefs.SetString(LocalizationManager.PLAYER_PREFS_LANGUAGE, code);
				}
			}
		}

		#endregion

		#region PersistentSingleton<LocalizationManager> Members

		/// <summary>
		/// Gets called when the script instance is being loaded.
		/// </summary>
		public virtual void Awake() {
			if (LocalizationManager._instance == null) {
				LocalizationManager._instance = this;

				GameObject.DontDestroyOnLoad(this.gameObject);
			}
			else {
				if (this != LocalizationManager._instance) {
					GameObject.Destroy(this.gameObject);
				}
			}

			if (this.m_supportedLanguages == null || this.m_supportedLanguages.Length <= 0) {
				throw new LocalizationException(@"Supported languages have not been cached. Did you update the localization data?");
			}

			string language = PlayerPrefs.GetString(LocalizationManager.PLAYER_PREFS_LANGUAGE);

			this.SetLanguage(!string.IsNullOrEmpty(language) ? language : this.m_defaultLanguage, false);
		}

		#endregion
	}
}