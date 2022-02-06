using System;
using App.Fetures.Scripts;
using App.Fetures.Scripts.PipesMechanics;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace App.Features.Scripts
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private Bird _birdPrefab;
        [SerializeField] private Pipes _pipes;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private Button _button;

        [SerializeField] private Image _imageClick;
        [SerializeField] private Image _imageGameOver;

        private Bird _bird;
        private Score _score;

        private void Start()
        {
            _button.gameObject.SetActive(false);
            _imageClick.gameObject.SetActive(true);
            _imageGameOver.gameObject.SetActive(false);
            
            _bird = SpawnBird();
            _bird.PipePassed += OnPipePassed;
            
            _pipes.StartMove();

            _score = new Score();
            _scoreView.Initialize(_score);
        }

        private void OnDestroy()
        {
            _bird.PipePassed -= OnPipePassed;
        }

        private Bird SpawnBird()
        {
           var bird =  Instantiate(_birdPrefab, Vector2.zero, Quaternion.identity);
           bird.Died += OnBirdDied;
           
           return bird;
        }

        private void OnPipePassed()
        {
            _score.Value++;
            print(_score.Value);
        }
        
        private void OnBirdDied()
        {
            _pipes.StopMove();
            _bird.BlockJump();

            _button.gameObject.SetActive(true);
            _imageClick.gameObject.SetActive(false);
            _imageGameOver.gameObject.SetActive(true);
        }

        public void Restart()
        {
            Scene activeScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(activeScene.buildIndex);
        }
    }
}
