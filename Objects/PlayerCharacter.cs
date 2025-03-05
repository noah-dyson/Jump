using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Reflection.Metadata;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class PlayerCharacter : GameObject {
    public static Texture2D Texture;
    private const int SCALE = 2;
    public List<BoundingBox> Colliders = new List<BoundingBox>();
    private BoundingBox _boundingBox;
    public BoundingBox BoundingBox { get { return _boundingBox; } }
    private static float _horizontalAccel = 1;
    private static float _verticalAccel = 7f;
    private static float _frictionalForce = 3f;
    private static float _stoppingGravity = 2f;
    private static float _normalGravity = 0.3f;
    private static float _maxHorizontalSpeed = 10;
    private static float _maxVerticalSpeed = 30;
    private Vector2 _currentPosition;
    private Vector2 _nextPosition;
    private Vector2 _velocity = Vector2.Zero;
    private bool _onGround = false;

    public PlayerCharacter(Vector2 position) {
        _boundingBox = new BoundingBox(position, new Vector2(Texture.Width * SCALE, Texture.Height * SCALE));
        _currentPosition = _boundingBox.Position;
        _nextPosition = _boundingBox.Position;
    }

    private void CollisionHandling(List<BoundingBox> colliders) {
        // sets temporary variables
        _currentPosition = _boundingBox.Position;
        _boundingBox.Position.X = _nextPosition.X;

        // loops over colliders checking for collision
        foreach (BoundingBox targetBox in colliders) {
            if (_boundingBox.Intersects(targetBox)) {
                // perform actions depending on side of collision
                ResolveHorizontal(targetBox);
            }
        }
        _boundingBox.Position.X = _nextPosition.X;

        _boundingBox.Position.Y = _nextPosition.Y;
        foreach (BoundingBox targetBox in colliders) {
            if (_boundingBox.Intersects(targetBox)) {
                ResolveVertical(targetBox);
            }
        }
        _boundingBox.Position = _nextPosition;
    }

    private void ResolveVertical(BoundingBox targetBox) {
        // checks if player is falling onto block, onto ground
        if (_currentPosition.Y + _boundingBox.Size.Y <= targetBox.Position.Y) {
            _nextPosition.Y = targetBox.Position.Y - _boundingBox.Size.Y;
            _velocity.Y = 0;
            _onGround = true;
        }
        // checks if player hits ceiling
        else if (_currentPosition.Y >= targetBox.Size.Y + targetBox.Position.Y) {
            _nextPosition.Y = targetBox.Size.Y + targetBox.Position.Y;
            _velocity.Y = 0;
        }
    }

    private void ResolveHorizontal(BoundingBox targetBox) {
        // checks if player collides with block to the right
        if (_currentPosition.X + _boundingBox.Size.X <= targetBox.Position.X) {
            _nextPosition.X = targetBox.Position.X - _boundingBox.Size.X;
            _velocity.X = 0;
        }
        // checks if player collides with block to the left
        else if (_currentPosition.X >= targetBox.Size.X + targetBox.Position.X) {
            _nextPosition.X = targetBox.Size.X + targetBox.Position.X;
            _velocity.X = 0;
        }
    }

    private void Movement() {
        KeyboardState keyboard = Keyboard.GetState();

        // moves player left and right
        if (keyboard.IsKeyDown(Keys.A)) {
            _velocity.X -= _horizontalAccel;
        }
        if (keyboard.IsKeyDown(Keys.D)) {
            _velocity.X += _horizontalAccel;
        }

        // produces frictional force on player
        if (keyboard.IsKeyUp(Keys.A) && keyboard.IsKeyUp(Keys.D) && Math.Abs(_velocity.X) > 0) {
            // ensures that frictional force does not cause player to moving in opposite direction
            if (_frictionalForce > Math.Abs(_velocity.X)) {
                _velocity.X = 0;
            }
            else {
                _velocity.X -= Math.Sign(_velocity.X) * _frictionalForce;
            }
        }

        // ensures player does not move too fast
        _velocity.X = Math.Clamp(_velocity.X, -_maxHorizontalSpeed, _maxHorizontalSpeed);

        // jumps player if they are on ground and pressing space
        if (keyboard.IsKeyDown(Keys.Space) && _onGround) {
            _velocity.Y = _verticalAccel;
        }

        // slows the player down more before they reach the peak of their jump
        if (keyboard.IsKeyUp(Keys.Space) && _velocity.Y > 0) {
            _velocity.Y -= _stoppingGravity;
        }
        else {
            _velocity.Y -= _normalGravity;
        }

        // ensures there is a terminal velocity
        _velocity.Y = Math.Clamp(_velocity.Y, -_maxVerticalSpeed, _maxVerticalSpeed);
        _nextPosition.X += _velocity.X;
        _nextPosition.Y -= _velocity.Y;

        _onGround = false;
    }
    public override void Update(GameTime gameTime) {
        Movement();
        CollisionHandling(Colliders);
    }
    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture, _boundingBox.Position, null, Color.White, 0, Vector2.Zero, SCALE, SpriteEffects.None, 0);
    }
}