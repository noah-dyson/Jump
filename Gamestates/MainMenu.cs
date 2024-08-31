using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CS_Coursework;

public class MainMenu : Gamestate
{
    private ButtonBar _mainMenuBar;
    private Button _levelEditorButton;
    private Button _levelBrowserButton;
    private Button _quitButton;
    public override void LoadContent(ContentManager contentManager)
    {
        _mainMenuBar = new ButtonBar(new Vector2(50, 280), 540, 140);
        _levelEditorButton = new Button(160, 110, "Level Editor");
        _levelBrowserButton = new Button(160, 110, "Level Browser");
        _quitButton = new Button(160, 110, "Quit");

        _mainMenuBar.Buttons.Add(_levelEditorButton);
        _mainMenuBar.Buttons.Add(_levelBrowserButton);
        _mainMenuBar.Buttons.Add(_quitButton);
        _mainMenuBar.SetButtonPositions();

        AddObject(_mainMenuBar);
    }
}