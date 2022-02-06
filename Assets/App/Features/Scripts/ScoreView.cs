using TMPro;
using UnityEngine;

namespace App.Features.Scripts
{
   public class ScoreView : MonoBehaviour
   {
      private TextMeshProUGUI _textField;
      private Score _score;

      private void Awake()
      {
         _textField = GetComponent<TextMeshProUGUI>();
      }
      
      private void OnDestroy()
      {
         _score.Changed -= OnScoreChanged;
      }
   
      public void Initialize(Score score)
      {
         _score = score;
         _score.Changed += OnScoreChanged;
      }

      private void OnScoreChanged(int score)
      {
         _textField.text = score.ToString();
      }
   }
}
