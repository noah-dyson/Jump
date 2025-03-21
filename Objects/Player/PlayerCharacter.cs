using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class PlayerCharacter : GameObject {
    public static Texture2D Texture;
    private const int SCALE = 3;
    public List<LevelObject> Colliders = new List<LevelObject>();
    private BoundingBox _boundingBox;
    public BoundingBox BoundingBox { get { return _boundingBox; } }
    private static float _horizontalAccel = 1;
    public float VerticalAccel = 7f;
    public float FrictionalForce = 3f;
    private static float _stoppingGravity = 2f;
    private static float _normalGravity = 0.3f;
    public float MaxHorizontalSpeed = 10;
    private static float _maxVerticalSpeed = 30;
    private Vector2 _currentPosition;
    private Vector2 _nextPosition;
    public Vector2 Velocity = Vector2.Zero;
    public Vector2 NextVelocity = Vector2.Zero;
    public Vector2 InitialVelocity = Vector2.Zero;

    public bool OnGround = false;

    public bool HasKey = false;

    public void SetSpawn(Vector2 spawnPoint) {
        _boundingBox = new BoundingBox(spawnPoint, new Vector2(Texture.Width * SCALE, Texture.Height * SCALE));
        _currentPosition = _boundingBox.Position;
        _nextPosition = _boundingBox.Position;
    }

    private void CollisionHandling(List<LevelObject> colliders) {
        _currentPosition = _boundingBox.Position;
        _boundingBox.Position = _nextPosition;
        InitialVelocity = Velocity;

        // performs general check on collision if there is no specific implemention
        // for horizontal and vertical collisions
        foreach (LevelObject collider in colliders) {
            if (_boundingBox.Intersects(collider.BoundingBox)) {
                collider.OnCollision(this);
            }
        }

        _boundingBox.Position = _currentPosition;
        _boundingBox.Position.X = _nextPosition.X;

        // performs checks on horizontal collisions
        // loops over colliders checking for collision
        foreach (LevelObject collider in colliders) {
            if (_boundingBox.Intersects(collider.BoundingBox)) {
                collider.OnCollisionHorizontal(this);
                // perform actions depending on side of collision
                if (collider.Impactable) {
                    ResolveHorizontal(collider.BoundingBox);
                }
            }
        }
        // next position may have been changed in the resolve collision function
        // so it must be reapplied to the bounding box
        _boundingBox.Position.X = _nextPosition.X;

        // same code as above but for vertical direction
        _boundingBox.Position.Y = _nextPosition.Y;
        foreach (LevelObject collider in colliders) {
            if (_boundingBox.Intersects(collider.BoundingBox)) {
                collider.OnCollisionVertical(this);
                if (collider.Impactable) {
                    ResolveVertical(collider.BoundingBox);
                }
            }
        }
        _boundingBox.Position = _nextPosition;

        Velocity += NextVelocity;
    }

    private void ResolveVertical(BoundingBox targetBox) {
        // checks if player is falling onto block, onto ground
        if (_currentPosition.Y + _boundingBox.Size.Y <= targetBox.Position.Y) {
            _nextPosition.Y = targetBox.Position.Y - _boundingBox.Size.Y;
            Velocity.Y = 0;
            OnGround = true;
        }
        // checks if player hits ceiling
        else if (_currentPosition.Y >= targetBox.Size.Y + targetBox.Position.Y) {
            _nextPosition.Y = targetBox.Size.Y + targetBox.Position.Y;
            Velocity.Y = 0;
        }
    }

    private void ResolveHorizontal(BoundingBox targetBox) {
        // checks if player collides with block to the right
        if (_currentPosition.X + _boundingBox.Size.X <= targetBox.Position.X) {
            _nextPosition.X = targetBox.Position.X - _boundingBox.Size.X;
            Velocity.X = 0;
        }
        // checks if player collides with block to the left
        else if (_currentPosition.X >= targetBox.Size.X + targetBox.Position.X) {
            _nextPosition.X = targetBox.Size.X + targetBox.Position.X;
            Velocity.X = 0;
        }
    }

    private void Movement() {
        KeyboardState keyboard = Keyboard.GetState();

        // moves player left and right
        if (keyboard.IsKeyDown(Keys.A)) {
            Velocity.X -= _horizontalAccel;
        }
        if (keyboard.IsKeyDown(Keys.D)) {
            Velocity.X += _horizontalAccel;
        }

        // produces frictional force on player
        if (keyboard.IsKeyUp(Keys.A) && keyboard.IsKeyUp(Keys.D) && Math.Abs(Velocity.X) > 0 && OnGround) {
            // ensures that frictional force does not cause player to moving in opposite direction
            if (FrictionalForce > Math.Abs(Velocity.X)) {
                Velocity.X = 0;
            }
            else {
                Velocity.X -= Math.Sign(Velocity.X) * FrictionalForce;
            }
        }

        // ensures player does not move too fast
        Velocity.X = Math.Clamp(Velocity.X, -MaxHorizontalSpeed, MaxHorizontalSpeed);

        // jumps player if they are on ground and pressing space
        if (keyboard.IsKeyDown(Keys.Space) && OnGround) {
            Velocity.Y = VerticalAccel;
        }

        // slows the player down more before they reach the peak of their jump
        if (keyboard.IsKeyUp(Keys.Space) && Velocity.Y > 0) {
            Velocity.Y -= _stoppingGravity;
        }
        else {
            Velocity.Y -= _normalGravity;
        }

        // ensures there is a terminal velocity
        Velocity.Y = Math.Clamp(Velocity.Y, -_maxVerticalSpeed, _maxVerticalSpeed);
        _nextPosition.X += Velocity.X;
        _nextPosition.Y -= Velocity.Y;

        OnGround = false;
    }
    public override void Update(GameTime gameTime) {
        Movement();
        FrictionalForce = 3f;
        NextVelocity = Vector2.Zero;
        MaxHorizontalSpeed = 7;
        VerticalAccel = 7;
        CollisionHandling(Colliders);
    }
    public override void Draw(SpriteBatch spriteBatch) {
        spriteBatch.Draw(Texture, _boundingBox.Position, null, Color.White, 0, Vector2.Zero, SCALE, SpriteEffects.None, 0);
    }
}