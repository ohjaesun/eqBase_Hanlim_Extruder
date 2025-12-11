using EQ.Common.Logs;
using EQ.Core.Act;
using EQ.Domain.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EQ.Core.Act
{
    public class ActLanguage : ActComponent
    {
        private Dictionary<string, string> _currentDict = new Dictionary<string, string>();
        private string _currentFilePath;
        private readonly object _fileLock = new object();

        public event Action OnLanguageChanged;

        public ActLanguage(ACT act) : base(act) { }

        // 언어 변경 요청
        public void ChangeLanguage()
        {
            var langType = _act.Option.Option1.Language;

            LoadLanguage(langType);
//            _act.Option.Option1.Language = langType; // 옵션 값 변경
            OnLanguageChanged?.Invoke(); // UI 갱신 알림
        }

        // 언어 파일 로드 (없으면 빈 파일 생성 준비)
        private void LoadLanguage(UserOption1.LanguageType langType)
        {
            // 영어(기본)일 경우 딕셔너리 비움 -> 원본 텍스트 그대로 출력
            if (langType == UserOption1.LanguageType.English)
            {
                _currentDict.Clear();
                _currentFilePath = null; // 저장 안 함
                return;
            }

            string folderPath = Path.Combine(Environment.CurrentDirectory, "CommonData", "Language");
            string fileName = $"Lang_{langType}.json";
            _currentFilePath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            if (File.Exists(_currentFilePath))
            {
                try
                {
                    string json = File.ReadAllText(_currentFilePath);
                    _currentDict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
                    Log.Instance.Info($"[ActLanguage] Loaded: {langType}");
                }
                catch
                {
                    _currentDict = new Dictionary<string, string>();
                }
            }
            else
            {
                _currentDict = new Dictionary<string, string>();
                SaveDictionaryToFile(); // 빈 파일 생성
            }
        }

        // 번역 함수 (핵심: 키가 없으면 자동 추가)
        public string GetText(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return "";

            // 1. 딕셔너리에 있으면 반환
            if (_currentDict.TryGetValue(key, out string val))
            {
                return string.IsNullOrEmpty(val) ? key : val; // 값이 비어있으면 원본 반환
            }

            // 2. 영어 모드이거나 파일 경로가 없으면 그냥 원본 반환
            if (string.IsNullOrEmpty(_currentFilePath)) return key;

            // 3. 키가 없으면 -> 딕셔너리에 추가하고 파일 저장
            lock (_fileLock)
            {
                if (!_currentDict.ContainsKey(key))
                {
                    _currentDict[key] = ""; // 번역 필요함을 알리기 위해 빈 값 저장
                    SaveDictionaryToFile();
                    Log.Instance.Info($"[ActLanguage] New Key Added: '{key}'");
                }
            }
            return key;
        }

        private void SaveDictionaryToFile()
        {
            if (string.IsNullOrEmpty(_currentFilePath)) return;
            try
            {
                // 정렬해서 저장 (가독성)
                var sortedDict = new SortedDictionary<string, string>(_currentDict);
                string json = JsonConvert.SerializeObject(sortedDict, Formatting.Indented);
                lock (_fileLock) { File.WriteAllText(_currentFilePath, json); }
            }
            catch { }
        }
    }
}