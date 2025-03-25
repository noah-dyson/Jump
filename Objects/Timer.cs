using System;
using System.Threading.Tasks.Dataflow;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class Timer : GameObject {
    public static SpriteFont Font;
    private static Vector2 _position = new Vector2(32, Game1.SCREEN_HEIGHT - 102);
    private TimeSpan _time;
    private TimeSpan _startTime;
    private TimeSpan _pausedTime;
    private bool _isPaused = false;
    private bool _started = false;

    private void UpdateTimer(GameTime gameTime) {
        if (!_started) {
            _startTime = gameTime.TotalGameTime;
            _started = true;
        }
        if (_isPaused == false) {
            _time = gameTime.TotalGameTime - _startTime + _pausedTime;
        }
    }

    public void PauseTimer() {
        if (_isPaused) {
            StartTimer();
        }
        else {
            _isPaused = true;
            _pausedTime = _time;
        }
    }

    public void StartTimer() {
        _started = false;
        _isPaused = false;
    }

    public override void Update(GameTime gameTime) {
        UpdateTimer(gameTime);
    }

    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.DrawString(Font, _time.ToString(), _position, Color.White);
    }
}