using System;
using System.Threading.Tasks.Dataflow;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class Timer : GameObject {
    public static SpriteFont Font;
    private static Vector2 _position = new Vector2(32, Game1.SCREEN_HEIGHT - 102);
    private TimeSpan _time;
    public TimeSpan Time { get { return _time; } }
    private TimeSpan _startTime;
    private TimeSpan _pausedTime;
    private bool _isPaused = false;
    private bool _started = false;

    private void UpdateTimer(GameTime gameTime) {
        // if the timer hasn't started or just paused
        // set new start time
        if (!_started) {
            _startTime = gameTime.TotalGameTime;
            _started = true;
        }
        // if timer not paused then calculate new time
        if (_isPaused == false) {
            _time = gameTime.TotalGameTime - _startTime + _pausedTime;
        }
    }

    public void PauseTimer(PlayerCharacter playerCharacter) {
        // if the timer is already paused
        // start the timer and pause the player
        if (_isPaused) {
            StartTimer();
            playerCharacter.Paused = false;
        }
        // if the timer is not already paused
        // pause the timer and stop the player
        else {
            _isPaused = true;
            _pausedTime = _time;
            playerCharacter.Paused = true;
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