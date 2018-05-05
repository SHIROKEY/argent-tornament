using Assets.Scripts.Logic;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Management
{
    class RecordManager: MonoBehaviour
    {
        public GameObject RecordPrefab;

        private GameObject _recordImage;
        private RectTransform _recordBoard;

        private List<GameRecord> _records;
        private GameObject[] _objects;

        private int _recordNum;

        public void ShowRecords()
        {
            _recordImage = transform.Find("RecordLayer").gameObject;
            _recordImage.SetActive(true);
            _recordBoard = GetComponentInChildren<RecordsContainer>().GetComponent<RectTransform>();
            GetRecords();
            FillWithRecords();
        }

        public void CloseRecords()
        {
            foreach (var item in _objects)
            {
                Destroy(item);
            }
            _recordNum = 0;
            _recordImage.SetActive(false);
        }

        private void GetRecords()
        {
            _records = new List<GameRecord>();
            var tmp = 0;
            while (PlayerPrefs.HasKey(tmp.ToString()))
            {
                _records.Add(SerializeManager.Deserialize<GameRecord>(PlayerPrefs.GetString(tmp.ToString())));
                tmp++;
            }
            _recordBoard.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 120 * _records.Count + 300);
        }

        private void FillWithRecords()
        {
            _objects = new GameObject[_records.Count];
            foreach (var record in _records)
            {
                var rec = Instantiate(RecordPrefab, _recordBoard.transform);
                rec.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -120 * _recordNum);
                rec.transform.Find("Number").GetComponent<Text>().text = (_recordNum + 1).ToString();
                rec.transform.Find("Score").GetComponent<Text>().text = record.Score.ToString();
                _objects[_recordNum] = rec;
                _recordNum++;
            }
        }

        public void ClearRecords()
        {
            PlayerPrefs.DeleteAll();
            CloseRecords();
        }
    }
}
