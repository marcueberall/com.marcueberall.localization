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
	/// A specialized exception for localization purposes.
	/// </summary>
	public class LocalizationException : Exception {
		#region Constructors
		/// <summary>
		/// Standard constructor.
		/// </summary>
		public LocalizationException() : base() {
		}

		/// <summary>
		/// Standard constructor.
		/// </summary>
		/// <param name="message">The error message to be thrown.</param>
		/// <param name="inner">The inner exception if there was one.</param>
		public LocalizationException(string message, Exception inner = null) : base(message, inner) {
		}

		#endregion
	}
}