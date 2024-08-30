using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public abstract class Gamestate
{
    private List<GameObject> _gameObjects = new List<GameObject>();

    public abstract void LoadContent(ContentManager contentManager);
    public virtual void Update(GameTime gameTime)
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.Update(gameTime);
        }
    }
    public virtual void Draw(SpriteBatch spriteBatch)
    {
        foreach (GameObject gameObject in _gameObjects)
        {
            gameObject.Draw(spriteBatch);
        }
    }
    public void AddObject(GameObject gameObject)
    {
        _gameObjects.Add(gameObject);
    }
    public void RemoveObject(GameObject gameObject)
    {
        if (!_gameObjects.Contains(gameObject))
        {
            throw new Exception("Object does not exist in gamestate.");
        }
        _gameObjects.Remove(gameObject);
    }
}