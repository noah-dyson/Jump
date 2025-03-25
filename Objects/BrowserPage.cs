using System.Collections.Generic;
using System.ComponentModel;
using System.Security.Cryptography;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class BrowserPage : GameObject {
    private static int _pad = 20;
    private Vector2 _position;
    private int _width;
    private int _height;
    private List<BrowserItem> _browserItems;
    public List<BrowserItem> BrowserItems {
        get { return _browserItems; }
        set {
            _browserItems = value;
            SetItemPositions();
        }
    }

    public BrowserPage(Vector2 position, int width, int height) {
        _position = position;
        _width = width;
        _height = height;
    }

    private void SetItemPositions() {
        int counter = 0;
        int height = 75;
        foreach (BrowserItem item in _browserItems) {
            item.InitialiseItem(new Vector2(_position.X + _pad, _position.Y + _pad * (counter + 1) + height * counter), _width - _pad * 2, height);
            counter++;
        }
    }

    public override void Update(GameTime gameTime) {
        foreach (BrowserItem item in _browserItems) {
            item.Update(gameTime);
        }
    }
    public override void Draw(SpriteBatch spriteBatch) {
        foreach (BrowserItem item in _browserItems) {
            item.Draw(spriteBatch);
        }
    }
}