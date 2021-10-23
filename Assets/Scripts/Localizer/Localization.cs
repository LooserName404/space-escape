using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace SpaceEscape.Localizer
{
    public class Localization
    {
        private static Dictionary<string, Dictionary<string, string>> _dic;
        private static string _currentLanguage = "pt-BR";
        public static string CurrentLanguage {
            get => _currentLanguage;
            set {
                _currentLanguage = value;
                PlayerPrefs.SetString("language", value);
                OnLanguageChanged?.Invoke();
            }
        }

        private static bool _hasLoaded;

        public static Action OnLanguageChanged;

        private static void Load()
        {
            var current = PlayerPrefs.GetString("language");
            _currentLanguage = String.IsNullOrWhiteSpace(current) ? "pt-BR" : current;
            
            _dic = new Dictionary<string, Dictionary<string, string>>();

            TextAsset asset = Resources.Load<TextAsset>("Localization");

            string[] lines = asset.text.Split('\n');

            string[] languages = lines[0].Split(',');
            foreach (string lang in languages) {
                string trimmedLang = lang.Trim();
                if (trimmedLang == "key") {
                    continue;
                }
                _dic.Add(trimmedLang.Trim(), new Dictionary<string, string>());
            }

            Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
            for (int i = 1; i < lines.Length; i++) {
                string[] cells = CSVParser.Split(lines[i]);
                for (int j = 1; j < cells.Length; j++) {
                    string language = languages[j].Trim();
                    string key = cells[0].Trim();
                    string value = cells[j].Trim();

                    _dic[language].Add(key, value);
                }
            }
            _hasLoaded = true;
        }

        public static string Localize(string key) {
            if (!_hasLoaded) {
                Load();
            }
            if (!_dic[_currentLanguage].ContainsKey(key)) {
                Debug.LogWarning($"Localization.Localize(\"{key}\") not found.");
                return $"*_{key}_*";
            }
            return _dic[_currentLanguage][key];
        }
    }
}