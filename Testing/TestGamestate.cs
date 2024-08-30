using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CS_Coursework;

public class TestGamestate : Gamestate
{
    private string _name;
    private TestGameObject testGameObject;
    public TestGamestate(string name)
    {
        _name = name;
    }
    public override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Back))
            RemoveObject(testGameObject);
        base.Update(gameTime);
    }
    public override void LoadContent(ContentManager contentManager)
    {
        Texture2D placeholder = contentManager.Load<Texture2D>("Placeholder");
        testGameObject = new TestGameObject(new Vector2(0,0), 4, placeholder);
        AddObject(testGameObject);
    }
}